using Sceelix.Core.Annotations;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Meshes.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Sceelix.Meshes.Procedures
{

    /// <summary>
    /// Applies rendering materials to meshes.
    /// </summary>
    [Procedure("ab33c263-352e-4917-a7ec-0686aebc079c", Label = "Mesh Native Material", Category = "Mesh")]
    public class MeshNativeMaterial : TransferProcedure<MeshEntity>
    {


        /// <summary>
        /// List of random values to create. There are many types of random values that can be created, all of which are stored to entity attributes so as to be used in later nodes.
        /// </summary>
        private readonly ListParameter<ObjectParameter<Material>> _parameterMaterials = new ListParameter<ObjectParameter<Material>>("Materials");

        /// <summary>
        /// The Material component to be applied 
        /// </summary>
        private readonly ObjectParameter<Material> _parameterMaterial = new ObjectParameter<Material>("Material");

        protected override MeshEntity Process(MeshEntity entity)
        {

            entity.Materials = _parameterMaterials.Items.Select(objParam => objParam.Value).ToArray();
            //entity.Material = _parameterMaterial.Value;

            return entity;
        }


    }
}
