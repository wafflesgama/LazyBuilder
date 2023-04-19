﻿using Sceelix.Core.Attributes;
using Sceelix.Core.Extensions;
using Sceelix.Mathematics.Data;

namespace Sceelix.Meshes.Materials
{
    /// <summary>
    /// Why material is generic:
    /// - Easier to create derivatives, because
    ///    - No need to implement equality comparer
    ///    - No need to implement serializer
    /// </summary>
    public class EmissiveMaterial : MeshMaterial
    {
        private static readonly AttributeKey ColorKey = new GlobalAttributeKey("DefaultColor");



        public EmissiveMaterial(UnityEngine.Color defaultColor)
        {
            DefaultColor = defaultColor;
        }



        public UnityEngine.Color DefaultColor
        {
            get { return this.GetAttribute<UnityEngine.Color>(ColorKey); }
            set { this.SetAttribute(ColorKey, value); }
        }
    }
}