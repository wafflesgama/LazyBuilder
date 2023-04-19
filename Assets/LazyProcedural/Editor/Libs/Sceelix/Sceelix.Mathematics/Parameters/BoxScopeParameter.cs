using Sceelix.Core.Parameters;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Parameters
{
    public class BoxScopeParameter : CompoundParameter
    {
        private readonly Vector3Parameter _sizeParameter = new Vector3Parameter("Size", UnityEngine.Vector3.one);
        private readonly Vector3Parameter _translationParameter = new Vector3Parameter("Translation", UnityEngine.Vector3.zero);
        private readonly Vector3Parameter _xAxisParameter = new Vector3Parameter("XAxis", UnityEngine.Vector3.right);
        private readonly Vector3Parameter _yAxisParameter = new Vector3Parameter("YAxis", UnityEngine.Vector3.up);
        private readonly Vector3Parameter _zAxisParameter = new Vector3Parameter("ZAxis", UnityEngine.Vector3.forward);



        public BoxScopeParameter(string label)
            : base(label)
        {
        }



        public BoxScopeParameter(string label, BoxScope boxScope)
            : base(label)
        {
            Value = boxScope;
        }



        public BoxScope Value
        {
            get { return new BoxScope(_xAxisParameter.Value, _yAxisParameter.Value, _zAxisParameter.Value, _translationParameter.Value, _sizeParameter.Value); }
            set
            {
                _xAxisParameter.Value = value.XAxis;
                _yAxisParameter.Value = value.YAxis;
                _zAxisParameter.Value = value.ZAxis;
                _sizeParameter.Value = value.Sizes;
                _translationParameter.Value = value.Translation;
            }
        }
    }
}