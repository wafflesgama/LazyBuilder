using Sceelix.Core.Attributes;
using Sceelix.Core.Extensions;
using Sceelix.Mathematics.Data;

namespace Sceelix.Meshes.Materials
{
    public class ColorMaterial : MeshMaterial
    {
        private static readonly AttributeKey ColorKey = new GlobalAttributeKey("DefaultColor");



        public ColorMaterial(UnityEngine.Color defaultColor)
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