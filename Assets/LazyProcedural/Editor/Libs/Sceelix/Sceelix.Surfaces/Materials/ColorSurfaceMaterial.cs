using Sceelix.Mathematics.Data;

namespace Sceelix.Surfaces.Materials
{
    public class ColorSurfaceMaterial : SurfaceMaterial
    {
        public ColorSurfaceMaterial()
        {
            DefaultColor = UnityEngine.Color.white;
        }



        public UnityEngine.Color DefaultColor
        {
            get;
            set;
        }
    }
}