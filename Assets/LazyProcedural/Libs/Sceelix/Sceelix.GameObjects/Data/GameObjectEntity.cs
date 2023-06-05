using Sceelix.Actors.Data;
using Sceelix.Core.Annotations;
using Sceelix.Core.Attributes;
using Sceelix.Core.Data;
using Sceelix.Mathematics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.GameObjects
{
    [Entity("GameObject")]
    public class GameObjectEntity : Entity, IActor
    {

        //scope that encapsulates all 
        private BoxScope _boxScope;

        /// <summary>
        /// The current boxscope that encloses the GameObject
        /// </summary>
        public BoxScope BoxScope
        {
            get { return _boxScope; }
            set { _boxScope = value; }
        }

        /// <summary>
        /// The reference GameObject to instantiate from
        /// </summary>
        public GameObject gameObject { get; private set; }


        public GameObjectEntity(GameObject gameObject) : base()
        {
            this.gameObject = gameObject;
            _boxScope = new BoxScope(translation: new Vector3D(0, 0, 0), sizes: new Vector3D(0, 0, 0));
        }

        public void InsertInto(BoxScope target)
        {
            _boxScope = target;
        }
    }
}
