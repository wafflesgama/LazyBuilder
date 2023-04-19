using Sceelix.Core.Annotations;
using Sceelix.Mathematics.Data;

namespace Sceelix.Surfaces.Data
{
    [Entity("Normal Layer", TypeBrowsable = false)]
    public class NormalLayer : GenericSurfaceLayer<UnityEngine.Vector3>
    {
        //private HeightLayer _heightLayer;
        //private Func<int, int, UnityEngine.Vector3> _getDelegate;



        public NormalLayer(UnityEngine.Vector3[,] normals)
            : base(normals)
        {
            //_getDelegate = base.GetValue;
        }



        public NormalLayer(UnityEngine.Vector3[,] normals, UnityEngine.Vector3 defaultValue)
            : base(normals)
        {
            this.Fill(defaultValue);
        }



        /*private UnityEngine.Vector3 CalculateGeometryNormal(int layerColumn, int layerRow)
        {
            UnityEngine.Vector3?[] directions = new UnityEngine.Vector3?[4];
            UnityEngine.Vector3 centralPosition = _heightLayer.GetLayerPosition(layerColumn, layerRow);

            if (layerRow > 0)
                directions[0] = _heightLayer.GetPosition(layerColumn, layerRow - 1) - centralPosition;

            if (layerColumn > 0)
                directions[1] = _heightLayer.GetPosition(layerColumn - 1, layerRow) - centralPosition;

            if (layerRow < _surface.NumRows - 1)
                directions[2] = _heightLayer.GetPosition(layerColumn, layerRow + 1) - centralPosition;

            if (layerColumn < _surface.NumColumns - 1)
                directions[3] = _heightLayer.GetPosition(layerColumn + 1, layerRow) - centralPosition;


            UnityEngine.Vector3 normal = UnityEngine.Vector3.zero;
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Vector3? direction1 = directions[i];
                UnityEngine.Vector3? direction2 = i + 1 > 3 ? directions[0] : directions[i + 1];

                if (direction1.HasValue && direction2.HasValue)
                    normal += UnityEngine.Vector3.Cross(direction1.Value, direction2.Value);
            }

            return normal.normalized;
        }*/


        /*public void RecalculateNormals()
        {
            if (_values != null)
            {
                Parallel.For(0, NumColumns, (x) =>
                {
                    for (int y = 0; y < NumRows; y++)
                    {
                        _values[x, y] = CalculateGeometryNormal(x, y);
                    }
                });
            }
        }*/



        protected override UnityEngine.Vector3 Add(UnityEngine.Vector3 valueA, UnityEngine.Vector3 valueB)
        {
            return valueA + valueB;
        }



        /*public override UnityEngine.Vector3 GetLayerValue(int layerColumn, int layerRow)
        {
            if (_values == null)
                return CalculateGeometryNormal(layerColumn, layerRow);

            return base.GetLayerValue(layerColumn, layerRow);
        }
        */


        /*protected internal override void Initialize(SurfaceEntity surface)
        {
            base.Initialize(surface);

            _heightLayer = surface.GetLayer<HeightLayer>();
        }*/


        /*public UnityEngine.Vector3 GetNormal(int surfaceColumn, int surfaceRow)
        {
            return CalculateGeometryNormal(surfaceColumn, surfaceRow);
        }*/



        public override SurfaceLayer CreateEmpty(int numColumns, int numRows)
        {
            return new NormalLayer(new UnityEngine.Vector3[numColumns, numRows]);
        }



        protected override UnityEngine.Vector3 InvertValue(UnityEngine.Vector3 value)
        {
            return -value;
        }



        protected override UnityEngine.Vector3 Minus(UnityEngine.Vector3 valueA, UnityEngine.Vector3 valueB)
        {
            return valueA - valueB;
        }



        protected override UnityEngine.Vector3 Multiply(UnityEngine.Vector3 value1, UnityEngine.Vector3 value2)
        {
            //TODO: review
            return value1;
        }



        protected override UnityEngine.Vector3 MultiplyScalar(UnityEngine.Vector3 value, float scalar)
        {
            return value * scalar;
        }


        public override void Update()
        {
            //do nothing
        }
    }
}