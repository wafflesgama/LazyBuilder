using Sceelix.Core.Annotations;
using Sceelix.Loading;
using Sceelix.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sceelix.Core.Procedures
{
    public class ProcedureInfoManager
    {

        public static List<ProcedureInfo> ProceduresInfo
        {
            get;
            private set;
        }



        public static void Init()
        {
            ProceduresInfo = new List<ProcedureInfo>();
            var allProcedures = SceelixDomain.Types.Where(type => typeof(Procedure).IsAssignableFrom(type) && type.GetCustomAttributes(typeof(ProcedureAttribute), true).Count() > 0);
            ProceduresInfo.AddRange(allProcedures.Select(x => new ProcedureInfo(x)));
        }

        public static ProcedureInfo GetProcedure(Type procedureType) => ProceduresInfo.FirstOrDefault(x => x.Type == procedureType);
    }

    public class ProcedureInfo
    {
        public Type Type { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string[] Tags { get; set; }

        public ProcedureInfo(Type procedureType)
        {
            var attributes = procedureType.GetCustomAttributes(typeof(ProcedureAttribute), true);

            //if (attributes == null || attributes.Length == 0)
            //{
            //    //SceelixDomain.Logger.Log($"{procedureType.Name} requires a ProcedureAttribute to identify it");
            //    return;
            //}

            var attribute = (ProcedureAttribute)attributes[0];

            Label = attribute.Label;
            Category = attribute.Category;
            Tags = attribute.Tags.Replace(" ", "").Split(',');
            Type = procedureType;
            //var commentData = CommentLoader.GetComment(procedureType);
            //Description = commentData?.Summary ?? "";

        }
    }
}
