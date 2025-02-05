﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Base.OngekiObjects
{
    public interface IBulletPalleteReferencable : IDisplayableObject, IHorizonPositionObject, ITimelineObject
    {
        BulletPallete ReferenceBulletPallete { get; set; }
    }
}
