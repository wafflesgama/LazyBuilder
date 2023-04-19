using Sceelix.Actors.Data;
using Sceelix.Core.Annotations;
using Sceelix.Core.Data;
using Sceelix.Mathematics.Data;

namespace Sceelix.Meshes.Data
{
    [Entity("Billboard")]
    public class BillboardEntity : Entity, IActor
    {
        public BillboardEntity(UnityEngine.Vector2 value, UnityEngine.Color color)
        {
            BoxScope = new BoxScope(sizes: new UnityEngine.Vector3(value.x, 0, value.y));
            Color = color;
        }



        public BoxScope BoxScope
        {
            get;
            set;
        }


        public UnityEngine.Color Color
        {
            get;
            set;
        }


        public string Image
        {
            get;
            set;
        }



        public override IEntity DeepClone()
        {
            var clone = (BillboardEntity) base.DeepClone();
            clone.BoxScope = BoxScope;

            return clone;
        }



        /*public void Translate(UnityEngine.Vector3 direction, bool scopeRelative)
        {
            _boxScope.Translation += direction;
        }

        public void Scale(UnityEngine.Vector3 scaling, UnityEngine.Vector3 pivot, bool scopeRelative)
        {
            _boxScope.Scale(scaling,pivot,scopeRelative);
        }*/



        public void InsertInto(BoxScope target)
        {
            BoxScope = target;
        }
    }
}