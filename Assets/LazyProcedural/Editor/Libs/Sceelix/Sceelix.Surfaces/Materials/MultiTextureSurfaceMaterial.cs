using Sceelix.Mathematics.Data;

namespace Sceelix.Surfaces.Materials
{
    public class TextureSetup
    {
        public string DiffuseMapPath
        {
            get;
            set;
        }


        public string NormalMapPath
        {
            get;
            set;
        }


        public float NormalScale
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

    public class MultiTextureSurfaceMaterial : SurfaceMaterial
    {
        public TextureSetup[] TextureSetups
        {
            get;
            set;
        }
    }
}