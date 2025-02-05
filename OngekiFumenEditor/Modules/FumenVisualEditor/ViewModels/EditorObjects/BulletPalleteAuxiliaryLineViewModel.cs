﻿using OngekiFumenEditor.Base.EditorObjects;
using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Base.OngekiObjects.Beam;
using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Base.OngekiObjects.Wall;
using OngekiFumenEditor.Modules.FumenVisualEditor.Views.EditorObjects;
using OngekiFumenEditor.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels.EditorObjects
{
    [MapToView(ViewType = typeof(BulletPalleteAuxiliaryLineView))]
    public class BulletPalleteAuxiliaryLineViewModel : ConnectorViewModel<IBulletPalleteReferencable>
    {
        public override Brush LineBrush => throw new NotImplementedException();

        public override void OnObjectCreated(object createFrom, FumenVisualEditorViewModel editorViewModel)
        {
            base.OnObjectCreated(createFrom, editorViewModel);
        }
    }
}
