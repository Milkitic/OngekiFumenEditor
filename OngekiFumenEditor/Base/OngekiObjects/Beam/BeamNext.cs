﻿using Caliburn.Micro;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels.OngekiObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OngekiFumenEditor.Base.OngekiObjects.Beam
{
    public class BeamNext : BeamChildBase
    {
        public override Type ModelViewType => typeof(BeamNextViewModel);

        public override string IDShortName => "BMN";
    }
}
