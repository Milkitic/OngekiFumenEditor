﻿using Caliburn.Micro;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Base.EditorObjects
{
    public abstract class ConnectorLineBase<T> : PropertyChangedBase, IDisplayableObject where T : IDisplayableObject
    {
        public abstract Type ModelViewType { get; }

        private T from;
        public T From
        {
            get => from;
            set => Set(ref from, value);
        }

        private T to;
        public T To
        {
            get => to;
            set => Set(ref to, value);
        }

        public virtual bool CheckVisiable(TGrid minVisibleTGrid, TGrid maxVisibleTGrid)
        {
            return (From?.CheckVisiable(minVisibleTGrid, maxVisibleTGrid) ?? false) || (To?.CheckVisiable(minVisibleTGrid, maxVisibleTGrid) ?? false);
        }

        public IEnumerable<IDisplayableObject> GetDisplayableObjects()
        {
            yield return this;
        }

        public override string ToString() => $"[T:{typeof(T).GetTypeName()}] {From} -> {To}";
    }
}
