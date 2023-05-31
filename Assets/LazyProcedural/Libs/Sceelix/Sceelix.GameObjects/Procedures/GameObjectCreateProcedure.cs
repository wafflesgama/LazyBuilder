using Sceelix.Core.Procedures;
using Sceelix.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sceelix.Core.IO;
using Sceelix.Core.Parameters;
using UnityEngine;

namespace Sceelix.GameObjects.Procedures
{
    /// <summary>
    /// Creates a Game Object Entity
    /// </summary>
    [Procedure("74716153-9685-4a42-8409-a9c79de51234", Label = "GameObject Create", Category = "GameObject")]
    public class GameObjectCreateProcedure : SystemProcedure
    {
        /// <summary>
        /// G Obj Entity created according to the defined parameters and/or inputs.
        /// </summary>
        private readonly Output<GameObjectEntity> _output = new Output<GameObjectEntity>("Output");

        /// <summary>
        ///The gameobject to be instantiated from
        /// </summary>
        private readonly ObjectParameter<GameObject> _parameterGameObject = new ObjectParameter<GameObject>("GameObject");

        protected override void Run()
        {
            if (_parameterGameObject.Value == null) return;

            var entity = new GameObjectEntity(_parameterGameObject.Value);
            _output.Write(entity);
        }
    }
}
