using Sceelix.Mathematics.Data;

namespace Sceelix.Surfaces.Materials
{
    public class SingleTextureSurfaceMaterial : SurfaceMaterial
    {
        public SingleTextureSurfaceMaterial(string texture)
        {
            Texture = texture;
        }



        public string Texture
        {
            get;
            set;
        }


        public UnityEngine.Vector2 UVTiling
        {
            get;
            set;
        }
    }
}