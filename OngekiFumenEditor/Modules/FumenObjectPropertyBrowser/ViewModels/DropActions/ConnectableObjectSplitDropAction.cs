﻿using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Modules.FumenVisualEditor;
using OngekiFumenEditor.Modules.FumenVisualEditor.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor.Base.DropActions;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OngekiFumenEditor.Modules.FumenObjectPropertyBrowser.ViewModels.DropActions
{
    public class ConnectableObjectSplitDropAction : IEditorDropHandler
    {
        private readonly ConnectableStartObject startObject;
        private readonly ConnectableStartObject nextStartObject;
        private readonly DisplayObjectViewModelBase prevEndViewModel;
        private readonly DisplayObjectViewModelBase nextStartViewModel;
        private readonly Action callback;

        public ConnectableObjectSplitDropAction(ConnectableStartObject startObject, ConnectableChildObjectBase childObject, Action callback = default)
        {
            this.startObject = startObject;
            prevEndViewModel = CacheLambdaActivator.CreateInstance(childObject.ModelViewType) as DisplayObjectViewModelBase;
            prevEndViewModel.ReferenceOngekiObject = childObject;
            nextStartViewModel = CacheLambdaActivator.CreateInstance(startObject.ModelViewType) as DisplayObjectViewModelBase;
            nextStartObject = nextStartViewModel.ReferenceOngekiObject as ConnectableStartObject;
            this.callback = callback;
        }

        public void Drop(FumenVisualEditorViewModel editor, Point dragEndPoint)
        {
            var dragTGrid = TGridCalculator.ConvertYToTGrid(dragEndPoint.Y, editor);
            var backupStores = new HashSet<ConnectableChildObjectBase>();

            editor.UndoRedoManager.ExecuteAction(LambdaUndoAction.Create("添加物件", () =>
            {
                //初始化
                prevEndViewModel.OnObjectCreated(prevEndViewModel.ReferenceOngekiObject, editor);
                nextStartViewModel.OnObjectCreated(nextStartViewModel.ReferenceOngekiObject, editor);

                //计算出需要划分出来的后边子物件
                backupStores.AddRange(startObject.Children.Where(x => x.TGrid > dragTGrid));
                //前面删除，后面添加
                foreach (var item in backupStores)
                {
                    startObject.RemoveChildObject(item);
                    nextStartObject.InsertChildObject(item.TGrid, item);
                }

                startObject.AddChildObject(prevEndViewModel.ReferenceOngekiObject as ConnectableChildObjectBase);
                editor.AddObject(nextStartViewModel);

                prevEndViewModel.MoveCanvas(dragEndPoint);
                nextStartViewModel.MoveCanvas(dragEndPoint);

                editor.Redraw(RedrawTarget.OngekiObjects);
                callback?.Invoke();
            }, () =>
            {
                editor.RemoveObject(nextStartViewModel);
                startObject.RemoveChildObject(prevEndViewModel.ReferenceOngekiObject as ConnectableChildObjectBase);

                foreach (var item in backupStores)
                {
                    nextStartObject.RemoveChildObject(item);
                    startObject.InsertChildObject(item.TGrid, item);
                }

                backupStores.Clear();
                editor.Redraw(RedrawTarget.OngekiObjects);
                callback?.Invoke();
            }));
        }
    }
}
