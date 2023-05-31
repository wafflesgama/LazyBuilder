using Sceelix.Core.Annotations;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Surfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.Surfaces.Procedures
{
    /// <summary>
    /// Assign a Material to a Surface
    /// </summary>
    [Procedure("e7a1fccf-9331-4d76-8911-ce398b26299a", Label = "Surface Material", Category = "Surface")]
    public class SurfaceMaterialProcedure : TransferProcedure<SurfaceEntity>
    {

        /// <summary>
        ///The material to be applied to the surface
        /// </summary>
        private readonly ObjectParameter<Material> _parameterMaterial = new ObjectParameter<Material>("Material");

        protected override SurfaceEntity Process(SurfaceEntity entity)
        {
            entity.Material = _parameterMaterial.Value;

            return entity;
        }

    }
}
