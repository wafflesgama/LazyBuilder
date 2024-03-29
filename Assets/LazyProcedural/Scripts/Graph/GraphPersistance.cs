using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Sceelix.Mathematics.Data;
using Sceelix.Extensions;

namespace LazyProcedural
{
    public class GraphPersistance
    {

        public static void SaveDefaultGraph(string filePath)
        {
            GraphPersistanceData graphData = new GraphPersistanceData();
            WriteGraph(filePath, graphData);
        }

        public string filePath;

        public (IEnumerable<Node>, IEnumerable<Edge>, IEnumerable<(string, object)>) LoadGraph(Graph graph)
        {
            GraphPersistanceData graphData = null;
            List<Node> nodes = new List<Node>();
            List<Edge> edges = new List<Edge>();
            List<(string, object)> globalParams = new List<(string, object)>();

            string basePath = Path.GetDirectoryName(Application.dataPath);
            string fullFilePath = $"{basePath}/{filePath}".AbsoluteFormat();

            if (!File.Exists(fullFilePath))
            {
                Debug.LogError("Incorrect file path");
                return (nodes, edges, globalParams);
            }

            string rawText = null;
            try
            {
                rawText = File.ReadAllText(fullFilePath);
                graphData = JsonUtility.FromJson<GraphPersistanceData>(rawText);
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                //If the file is empty or partially complete
                if (UnityEditor.EditorUtility.DisplayDialog(
                    "Invalid Graph Data",
                    "The selected graph contains invalid/unreadable data. Do you wish to overwrite it with a valid empty graph?",
                    "Overwrite",
                    "Cancel"))
                {
                    graphData = new GraphPersistanceData();
                    WriteGraph(filePath, graphData);
                }
                else
                    Debug.LogException(ex);
#else
                Debug.LogException(ex);
#endif
            }

            //First pass to create nodes
            foreach (NodePersistanceData nodeData in graphData.Nodes)
            {
                Node node = new Node(
                    nodeData.Id,
                    nodeData.Name,
                    Type.GetType(nodeData.Type),
                    nodeData.Position,
                    nodeData.CreatedParams.Select(x => new CreatedParameterInfo { accessIndex = x.AccessIndex, parameterName = x.Name }).ToArray(),
                    nodeData.ChangedParams.Select(x => new ChangedParameterInfo { accessIndex = x.AccessIndex, isExpression = x.IsExpression, value = ConvertValue(x.ValueType, x.Value) }).ToArray(),
                    graph
                    );
                nodes.Add(node);
            }

            //Second pass to create edges
            foreach (NodePersistanceData nodeData in graphData.Nodes)
            {
                Node currentNode = nodes.FirstOrDefault(x => x.id == nodeData.Id);

                int currentPortIndex = 0;
                foreach (var outPortData in nodeData.OutPorts)
                {
                    Port currentPort = currentNode.outPorts[currentPortIndex];

                    foreach (var connection in outPortData.Connections)
                    {
                        Node inNode = nodes.FirstOrDefault(x => x.id == connection.DestinationNodeId);
                        Port inPort = inNode.inPorts[connection.DestinationPortIndex];
                        Edge edge = new Edge(currentPort, inPort);
                        edges.Add(edge);
                    }
                    currentPortIndex++;
                }
            }

            //Then Load Global Parameters
            foreach (var globalParam in graphData.GlobalParameters)
            {
                globalParams.Add((globalParam.Name, ConvertValue(globalParam.ValueType, globalParam.Value)));
            }


            return (nodes, edges, globalParams);
        }

        public void SaveGraph(IEnumerable<Node> nodes, IEnumerable<(string, object)> globalParameters)
        {
            GraphPersistanceData graphData = new GraphPersistanceData();
            List<NodePersistanceData> nodesData = new List<NodePersistanceData>();

            foreach (Node node in nodes)
            {
                NodePersistanceData nodeData = new NodePersistanceData();
                nodeData.Id = node.id;
                nodeData.Position = node.GetPosition().position;
                nodeData.Type = node.nodeData.GetType().FullName;
                nodeData.CreatedParams = node.createdDataParams.Select(x =>
                 {
                     return new CreatedParameterData { AccessIndex = x.accessIndex, Name = x.parameterName };
                 }).ToArray();
                nodeData.ChangedParams = node.changedDataParams.Select(x =>
                {
                    UnityEngine.Object castedObj = null;
                    bool isUnityObj = x.value != null && typeof(UnityEngine.Object).IsAssignableFrom(x.value.GetType());
                    if (isUnityObj) castedObj = ((UnityEngine.Object)x.value);
                    return new ChangedParameterData { AccessIndex = x.accessIndex, IsExpression = x.isExpression, Value = isUnityObj ? castedObj.GetInstanceID().ToString() : x.value.ToString(), ValueType = x.value.GetType().FullName };
                }).ToArray();
                nodeData.Name = node.title;

                List<PortPesistanceData> inPortsData = new List<PortPesistanceData>();
                foreach (var port in node.inPorts)
                {
                    PortPesistanceData portData = new PortPesistanceData();
                    portData.Id = port.id;
                    inPortsData.Add(portData);
                }
                nodeData.InPorts = inPortsData.ToArray();

                List<PortPesistanceData> outPortsData = new List<PortPesistanceData>();
                foreach (var port in node.outPorts)
                {
                    PortPesistanceData portData = new PortPesistanceData();
                    portData.Id = port.id;

                    //Add only in outports the connections
                    portData.Connections = port.connections.Select(x => (new EdgePersistanceData { DestinationNodeId = ((Node)((Port)x.input).node).id, DestinationPortIndex = ((Node)((Port)x.input).node).GetPortIndex((Port)x.input) })).ToArray();
                    outPortsData.Add(portData);
                }
                nodeData.OutPorts = outPortsData.ToArray();


                nodesData.Add(nodeData);
            }
            graphData.Nodes = nodesData.ToArray();

            List<GlobalParameterPersistanceData> globalParametersData = new List<GlobalParameterPersistanceData>();
            foreach (var globalParameter in globalParameters)
            {
                GlobalParameterPersistanceData paramData = new GlobalParameterPersistanceData();
                paramData.Name = globalParameter.Item1;
                paramData.Value = globalParameter.Item2.ToString();
                paramData.ValueType = globalParameter.Item2.GetType().FullName;
                globalParametersData.Add(paramData);
            }
            graphData.GlobalParameters = globalParametersData.ToArray();

            WriteGraph(filePath, graphData);
        }

        private static void WriteGraph(string filePath, GraphPersistanceData graphData)
        {
            string json = JsonUtility.ToJson(graphData, true);
            File.WriteAllText(filePath, json);
        }


        public static object ConvertValue(string valueTypeString, string valueString)
        {
            object value = null;

            Type valueType = Type.GetType(valueTypeString);

            if (valueType == typeof(System.Single) || valueType == typeof(float))
            {
                value = float.Parse(valueString);
            }
            else if (valueType == typeof(int))
            {
                value = int.Parse(valueString);
            }
            else if (valueType == typeof(string))
            {
                value = valueString;
            }
            else if (valueType == typeof(bool))
            {
                value = bool.Parse(valueString);
            }
            else if (valueTypeString == "UnityEngine.Color")
            {
                value = ColorExtension.Parse(valueString);
            }
            else if (valueType == typeof(Vector2D))
            {
                value = Vector2D.Parse(valueString);
            }
            else if (valueType == typeof(Vector3D))
            {
                value = Vector3D.Parse(valueString);
            }
            else if (valueType == typeof(Vector4D))
            {
                value = Vector4D.Parse(valueString);
            }
            //As the current domain cannot get Unity Types this first string validation happens (assuming all unity engine data below is an object)
            else if (valueTypeString.StartsWith("UnityEngine") || typeof(UnityEngine.Object).IsAssignableFrom(valueType))
            {
                value = FindObjectFromInstanceID(int.Parse(valueString));
            }

            return value;
        }


        public static UnityEngine.Object FindObjectFromInstanceID(int iid)
        {
            return (UnityEngine.Object)typeof(UnityEngine.Object)
                    .GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                    .Invoke(null, new object[] { iid });

        }
    }



    [Serializable]
    public class GraphPersistanceData
    {
        public string Description = "";

        public NodePersistanceData[] Nodes = new NodePersistanceData[0];

        public GlobalParameterPersistanceData[] GlobalParameters = new GlobalParameterPersistanceData[0];
    }

    [Serializable]
    public class GlobalParameterPersistanceData
    {
        public string Name;
        public string Value;
        public string ValueType;
    }

    [Serializable]
    public class NodePersistanceData
    {
        public string Id;
        public string Name;
        public Vector2 Position;
        public string Type;

        public ChangedParameterData[] ChangedParams = new ChangedParameterData[0];
        public CreatedParameterData[] CreatedParams = new CreatedParameterData[0];
        public PortPesistanceData[] InPorts = new PortPesistanceData[0];
        public PortPesistanceData[] OutPorts = new PortPesistanceData[0];
    }

    [Serializable]
    public class PortPesistanceData
    {
        public string Id;

        //Ids of ports connected to self
        public EdgePersistanceData[] Connections = new EdgePersistanceData[0];
    }

    [Serializable]
    public class EdgePersistanceData
    {
        public string DestinationNodeId;
        public int DestinationPortIndex;
    }

    [Serializable]
    public class ChangedParameterData
    {
        public int[] AccessIndex = new int[0];
        public bool IsExpression;
        public string Value;
        public string ValueType;
    }

    [Serializable]
    public class CreatedParameterData
    {
        public int[] AccessIndex = new int[0];
        public string Name;
    }


}

