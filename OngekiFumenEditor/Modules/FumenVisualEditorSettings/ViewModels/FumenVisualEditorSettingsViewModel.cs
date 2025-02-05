﻿using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using OngekiFumenEditor.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor.Kernel;
using OngekiFumenEditor.Modules.FumenVisualEditor.Models;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenVisualEditorSettings.ViewModels
{
    [Export(typeof(IFumenVisualEditorSettings))]
    public class FumenVisualEditorSettingsViewModel : Tool, IFumenVisualEditorSettings
    {
        public double[] UnitCloseSizeValues { get; } = new[]
        {
            1,
            1.5,
            2,
            3,
            4,
            4.5,
            6,
            8,
            9,
            12,
        };

        public override PaneLocation PreferredLocation => PaneLocation.Right;

        private EditorSetting setting = default;
        public EditorSetting Setting
        {
            get
            {
                return setting;
            }
            set
            {
                var prev = setting;
                setting = value;
                NotifyOfPropertyChange(() => Setting);

                if (value is null)
                    DisplayName = "编辑器设置";
                else
                    DisplayName = "编辑器设置 - " + value.EditorDisplayName;
            }
        }

        public FumenVisualEditorSettingsViewModel()
        {
            DisplayName = "编辑器设置";
            IoC.Get<IEditorDocumentManager>().OnActivateEditorChanged += OnActivateEditorChanged;
            Setting = IoC.Get<IEditorDocumentManager>().CurrentActivatedEditor?.Setting;
        }

        private void OnActivateEditorChanged(FumenVisualEditorViewModel @new, FumenVisualEditorViewModel old)
        {
            Setting = @new?.Setting;
            this.RegisterOrUnregisterPropertyChangeEvent(old, @new, OnEditorPropertyChanged);
        }

        private void OnEditorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FumenVisualEditorViewModel.Setting))
                Setting = (sender as FumenVisualEditorViewModel).Setting;
        }
    }
}
