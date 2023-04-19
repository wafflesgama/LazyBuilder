using System;
using System.Collections.Generic;
using Sceelix.Core.Data;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Parameters
{
    /// <summary>
    /// Generates random 3D vectors within a specified range.
    /// </summary>
    public class Vector3RandomParameters : RandomProcedure.RandomParameter
    {
        /// <summary>
        /// Inclusive lower bound of the random vector returned.
        /// </summary>
        private readonly Vector3Parameter _parameterMin = new Vector3Parameter("Minimum", UnityEngine.Vector3.zero);

        /// <summary>
        /// Exclusive upper bound of the random vector returned.
        /// </summary>
        private readonly Vector3Parameter _parameterMax = new Vector3Parameter("Maximum", UnityEngine.Vector3.one * 10);

        /// <summary>
        /// Attribute where to store the random value.
        /// </summary>
        private readonly AttributeParameter<UnityEngine.Vector3> _attributeValue = new AttributeParameter<UnityEngine.Vector3>("Value", AttributeAccess.Write);



        public Vector3RandomParameters()
            : base("UnityEngine.Vector3")
        {
        }



        public override void Execute(Random random, List<IEntity> entities)
        {
            var difference = _parameterMax.Value - _parameterMin.Value;

            foreach (var entity in entities)
                _attributeValue[entity] = new UnityEngine.Vector3(Convert.ToSingle(_parameterMin.Value.x + random.NextDouble() * difference.x),
                    Convert.ToSingle(_parameterMin.Value.y + random.NextDouble() * difference.y),
                    Convert.ToSingle(_parameterMin.Value.z + random.NextDouble() * difference.z));
        }
    }
}