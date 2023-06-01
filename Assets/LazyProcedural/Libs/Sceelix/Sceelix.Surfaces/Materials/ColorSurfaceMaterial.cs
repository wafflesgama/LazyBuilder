using Sceelix.Mathematics.Data;
using UnityEngine;

namespace Sceelix.Surfaces.Materials
{
    public class ColorSurfaceMaterial : SurfaceMaterial
    {
        public ColorSurfaceMaterial()
        {
            DefaultColor = Color.white;
        }



        public Color DefaultColor
        {
            get;
            set;
        }
    }
}