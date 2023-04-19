using System;
using System.Collections.Generic;
using Sceelix.Core.Data;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Parameters
{
    /// <summary>
    /// Generates random 2D vectors within a specified range.
    /// </summary>
    public class Vector2RandomParameters : RandomProcedure.RandomParameter
    {
        /// <summary>
        /// Inclusive lower bound of the random vector returned.
        /// </summary>
        private readonly Vector2Parameter _parameterMin = new Vector2Parameter("Minimum", UnityEngine.Vector2.zero);

        /// <summary>
        /// Exclusive upper bound of the random vector returned.
        /// </summary>
        private readonly Vector2Parameter _parameterMax = new Vector2Parameter("Maximum", UnityEngine.Vector2.One * 10);

        /// <summary>
        /// Attribute where to store the random value.
        /// </summary>
        private readonly AttributeParameter<UnityEngine.Vector2> _attributeValue = new AttributeParameter<UnityEngine.Vector2>("Value", AttributeAccess.Write);



        public Vector2RandomParameters()
            : base("UnityEngine.Vector2")
        {
        }



        public override void Execute(Random random, List<IEntity> entities)
        {
            var difference = _parameterMax.Value - _parameterMin.Value;

            foreach (var entity in entities)
                _attributeValue[entity] = new UnityEngine.Vector2(Convert.ToSingle(_parameterMin.Value.x + random.NextDouble() * difference.x),
                    Convert.ToSingle(_parameterMin.Value.y + random.NextDouble() * difference.y));
        }
    }
}