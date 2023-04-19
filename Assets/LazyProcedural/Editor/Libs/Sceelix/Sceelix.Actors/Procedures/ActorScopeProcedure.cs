using System.Collections.Generic;
using System.Linq;
using Sceelix.Actors.Data;
using Sceelix.Core.Annotations;
using Sceelix.Core.Parameters;
using Sceelix.Core.Procedures;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Helpers;
using Sceelix.Mathematics.Parameters;

namespace Sceelix.Actors.Procedures
{
    /// <summary>
    /// Applies scope transformations to Actors.
    /// </summary>
    [Procedure("a7a6544e-abff-44fe-9156-975fe0608718", Label = "Actor Scope", Category = "Actor")]
    public class ActorScopeProcedure : TransferProcedure<IActor>
    {
        /// <summary>
        /// Scope operation to be applied.
        /// </summary>
        private readonly SelectListParameter<ActorScopeParameter> _parameterOperation = new SelectListParameter<ActorScopeParameter>("Operation", "Reset Scope");


        public override IEnumerable<string> Tags => base.Tags.Union(_parameterOperation.SubParameterLabels);



        protected override IActor Process(IActor actor)
        {
            //apply all the transforms sequentially
            foreach (var parameterTransform in _parameterOperation.Items)
                actor = parameterTransform.Transform(actor);

            return actor;
        }



        #region Transformation Pivot

        public class TransformationPivot : CompoundParameter
        {
            private readonly ChoiceParameter _parameterRelativeTo = new ChoiceParameter("Relative To", "Scope", "Scope", "World");

            /// <summary>
            /// Position of the Pivot.
            /// </summary>
            private readonly Vector3Parameter _parameterPosition = new Vector3Parameter("Position", new UnityEngine.Vector3(0.5f, 0.5f, 0.5f));

            /// <summary>
            /// Indicates if the position is measured as absolute units or scope-size relative value (between 0 and 1).
            /// </summary>
            private readonly ChoiceParameter _parameterReferential = new ChoiceParameter("Referential", "Relative", "Absolute", "Relative");



            public TransformationPivot(string label)
                : base(label)
            {
            }
        }

        #endregion

        #region Abstract Parameter

        public abstract class ActorScopeParameter : CompoundParameter
        {
            protected ActorScopeParameter(string label)
                : base(label)
            {
            }



            protected internal abstract IActor Transform(IActor actor);
        }

        #endregion

        #region Reset Scope

        /// <summary>
        /// Resets the scope orientation so that it becomes aligned with the world.
        /// </summary>
        /// <seealso cref="Sceelix.Actors.Procedures.ActorScopeProcedure.ActorScopeParameter" />
        public class ResetScopeParameter : ActorScopeParameter
        {
            public ResetScopeParameter()
                : base("Reset Scope")
            {
            }



            /// <summary>
            /// Transforms the specified actor.
            /// </summary>
            /// <param name="actor">The actor.</param>
            /// <returns></returns>
            protected internal override IActor Transform(IActor actor)
            {
                actor.BoxScope = new BoxScope(UnityEngine.Vector3.right, UnityEngine.Vector3.up, UnityEngine.Vector3.forward, actor.BoxScope.Translation, actor.BoxScope.Sizes);

                return actor;
            }
        }

        #endregion

        #region Rotate Scope

        /// <summary>
        /// Rotates the scope around an axis.
        /// </summary>
        /// <seealso cref="Sceelix.Actors.Procedures.ActorScopeProcedure.ActorScopeParameter" />
        public class RotateScopeParameter : ActorScopeParameter
        {
            /// <summary>
            /// The angle to rotate.
            /// </summary>
            private readonly FloatParameter _parameterAngle = new FloatParameter("Angle", 45);

            /// <summary>
            /// Axis around which the rotation should be performed.
            /// </summary>
            private readonly Vector3Parameter _parameterAxis = new Vector3Parameter("Axis", UnityEngine.Vector3.forward);

            /// <summary>
            /// Controls if the indicated axis is relative to the current scope or to the world.
            /// </summary>
            private readonly ChoiceParameter _parameterRelativeTo = new ChoiceParameter("Relative To", "Scope", "Scope", "World");



            public RotateScopeParameter()
                : base("Rotate Scope")
            {
            }



            protected internal override IActor Transform(IActor actor)
            {
                var direction = _parameterRelativeTo.Value == "Scope" ? actor.BoxScope.ToWorldDirection(_parameterAxis.Value) : _parameterAxis.Value;

                Matrix matrix = Matrix.CreateTranslation(actor.BoxScope.Translation) * Matrix.CreateAxisAngle(direction, MathHelper.ToRadians(_parameterAngle.Value)) * Matrix.CreateTranslation(-actor.BoxScope.Translation);

                var newScope = actor.BoxScope.Transform(matrix);

                actor.BoxScope = newScope;
                return actor;
            }
        }

        #endregion

        #region Orient Scope

        /// <summary>
        /// Rotates the scope so that a given scope axis direction faces a certain direction.
        /// For instance, setting the axis direction "X" to face (0,0,1) will rotate the actor so that its X-axis arrow faces the Z-Up direction.
        /// </summary>
        /// <seealso cref="Sceelix.Actors.Procedures.ActorScopeProcedure.ActorScopeParameter" />
        public class OrientScopeParameter : ActorScopeParameter
        {
            /// <summary>
            /// The scope axis that is meant to be oriented.
            /// </summary>
            private readonly ChoiceParameter _parameterAxisParameter = new ChoiceParameter("Axis", "X", "X", "Y", "Z");

            /// <summary>
            /// The direction to which the mentioned axis should be facing.
            /// </summary>
            private readonly Vector3Parameter _parameterDirectionParameter = new Vector3Parameter("Direction", UnityEngine.Vector3.one);



            public OrientScopeParameter()
                : base("Orient Scope")
            {
            }



            protected internal override IActor Transform(IActor actor)
            {
                UnityEngine.Vector3 axis = actor.BoxScope.XAxis;

                if (_parameterAxisParameter.Value == "Y")
                    axis = actor.BoxScope.YAxis;
                else if (_parameterAxisParameter.Value == "Z")
                    axis = actor.BoxScope.ZAxis;

                float angleTo = axis.AngleTo(_parameterDirectionParameter.Value);

                UnityEngine.Vector3 rotationAxis = axis.Cross(_parameterDirectionParameter.Value).normalized;
                if (!rotationAxis.Equals(UnityEngine.Vector3.zero))
                {
                    var rotation = Matrix.CreateTranslation(actor.BoxScope.Translation) * Matrix.CreateAxisAngle(rotationAxis, angleTo) * Matrix.CreateTranslation(-actor.BoxScope.Translation);
                    var newScope = actor.BoxScope.Transform(rotation);
                    actor.BoxScope = newScope;
                }
                //RotateScopeProcedure.PerformRotateScope(meshEntity, rotationAxis, angleTo, false);

                return actor;
            }
        }

        #endregion
    }
}