using Sceelix.Actors.Data;
using Sceelix.Core.Annotations;
using Sceelix.Core.Data;
using Sceelix.Mathematics.Data;

namespace Sceelix.Meshes.Data
{
    [Entity("Mesh Instance")]
    public class MeshInstanceEntity : Entity, IActor
    {
        private BoxScope _boxScope;



        public MeshInstanceEntity(MeshEntity meshEntity)
        {
            _boxScope = meshEntity.BoxScope;

            MeshEntity = meshEntity;
            RelativeScale = new UnityEngine.Vector3(1 / _boxScope.Sizes.x, 1 / _boxScope.Sizes.y, 1 / _boxScope.Sizes.z).MakeValid(1);

            //reset the orientation of the mesh
            meshEntity.InsertInto(new BoxScope(sizes: _boxScope.Sizes));

            //_meshEntity.InsertInto(BoxScope.Identity);
        }



        public BoxScope BoxScope
        {
            get { return _boxScope; }
            set { _boxScope = value; }
        }



        public MeshEntity MeshEntity
        {
            get;
        }


        public UnityEngine.Vector3 RelativeScale
        {
            get;
            set;
        }


        public UnityEngine.Vector3 Scale => RelativeScale * _boxScope.Sizes.ReplaceValue(0, 1);



        public override IEntity DeepClone()
        {
            var clone = (MeshInstanceEntity) base.DeepClone();
            clone._boxScope = _boxScope;

            return clone;
        }



        /*public void Translate(UnityEngine.Vector3 direction, bool scopeRelative)
        {
            _boxScope.Translation += direction;
        }

        public void Scale(UnityEngine.Vector3 scaling, UnityEngine.Vector3 pivot, bool scopeRelative)
        {
            if (scopeRelative)
                pivot = BoxScope.ToWorldPosition(pivot);

            Matrix transformation = scopeRelative ?
                Matrix.CreateTranslation(pivot) * BoxScope.ToWorldDirectionMatrix() * Matrix.CreateScale(scaling) * BoxScope.ToScopeDirectionMatrix() * Matrix.CreateTranslation(-pivot)
                :
                Matrix.CreateTranslation(pivot) * Matrix.CreateScale(scaling) * Matrix.CreateTranslation(-pivot);

            _boxScope.Transform(transformation);
        }*/



        public void InsertInto(BoxScope target)
        {
            _boxScope = target;
        }
    }
}