﻿using Gemini.Framework;
using OngekiFumenEditor.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenObjectPropertyBrowser
{
    public interface IFumenObjectPropertyBrowser : ITool
    {
        public OngekiObjectBase OngekiObject { get; }
        public void SetCurrentOngekiObject(OngekiObjectBase ongekiObject, FumenVisualEditorViewModel referenceEditor);
    }
}
