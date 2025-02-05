﻿using Caliburn.Micro;
using Microsoft.Win32;
using OngekiFumenEditor.Kernel.Audio;
using OngekiFumenEditor.Modules.FumenVisualEditor.Models;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels.Dialogs
{
    public class EditorProjectSetupDialogViewModel : Screen
    {
        private EditorProjectDataModel editorProjectData = new();
        public EditorProjectDataModel EditorProjectData
        {
            get => editorProjectData;
            set => Set(ref editorProjectData, value);
        }

        public async void OnSelectAudioFilePathButtonClicked()
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = FileDialogFilterHelper.GetSupportAudioFileExtensionFilter();
            if (dialog.ShowDialog() == true)
            {
                EditorProjectData.AudioFilePath = dialog.FileName;
                using var audio = await IoC.Get<IAudioManager>().LoadAudioAsync(EditorProjectData.AudioFilePath);
                var durationMs = audio.Duration;
                EditorProjectData.AudioDuration = durationMs;
            }
        }

        public void OnSelectFumenFilePathButtonClicked()
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = FileDialogFilterHelper.GetSupportFumenFileExtensionFilter();
            if (dialog.ShowDialog() == true)
                EditorProjectData.FumenFilePath = dialog.FileName;
        }

        public async void OnCreateButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(EditorProjectData.AudioFilePath) || !File.Exists(EditorProjectData.AudioFilePath))
            {
                MessageBox.Show("音频文件未填写或者不存在");
                return;
            }

            await TryCloseAsync(true);
        }
    }
}
