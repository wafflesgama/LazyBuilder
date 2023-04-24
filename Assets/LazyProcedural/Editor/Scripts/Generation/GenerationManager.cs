using Sceelix.Core;
using Sceelix.Core.Data;
using Sceelix.Core.Environments;
using Sceelix.Core.Parameters;
using Sceelix.Loading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using UnityEngine;

namespace LazyProcedural
{
    public class GenerationManager
    {
        private static ProcedureEnvironment environment;

        public static void Init()
        {
            SceelixDomain.LoadAssembliesFrom($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}");
            EngineManager.Initialize();
            ParameterManager.Initialize();

            environment = new ProcedureEnvironment(new ResourceManager($"{PathFactory.absoluteToolPath}\\{PathFactory.SCEELIX_PATH}", Assembly.GetExecutingAssembly()));
        }

        public List<Entity> ExecuteGraph(List<Node> nodes)
        {
            List<Entity> output= new List<Entity>();



            return output;
        }

        
        private List<Node> GetRootNodes(List<Node> nodes)
        {
            return nodes.Where(x=> x.GetTotalConnectedPorts(true) == 0).ToList();
        }



    }
}
