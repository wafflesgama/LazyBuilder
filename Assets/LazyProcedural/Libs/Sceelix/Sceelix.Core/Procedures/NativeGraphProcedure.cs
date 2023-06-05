using Sceelix.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sceelix.Core.Procedures.AttributeProcedure;

namespace Sceelix.Core.Procedures
{
    public class NativeGraphProcedure : Procedure
    {
        private ListParameter<AttributeParameter> globalParameters = new ListParameter<AttributeParameter>("Global Parameters");

        public void AddParameter(string parameterName, object value)
        {
            var atr = new AttributeParameter(parameterName);
            atr.Set(value);
            atr.EntityEvaluation = false;

            globalParameters.Items.Add(atr);
        }
        protected override void Run()
        {
            //throw new NotImplementedException();
        }
    }
}
