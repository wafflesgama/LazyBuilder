using System.Collections.Generic;
using Sceelix.Extensions;
using Sceelix.Mathematics.Data;
using UnityEngine;

namespace Sceelix.Actors.Data
{
    internal class GenericMaterial
    {
        /*private String _name;
        private String _shader;*/

        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();



        public string DiffuseTexture
        {
            get { return GetTexture("DiffuseTexture"); }
            set { SetTexture("DiffuseTexture", value); }
        }



        public string Path
        {
            get { return GetTexture("Path"); }
            set { SetTexture("Path", value); }
        }



        public string Shader
        {
            get { return GetTexture("Shader"); }
            set { SetTexture("Shader", value); }
        }



        public UnityEngine.Color GetColor(string name)
        {
            return (UnityEngine.Color) _properties.GetOrDefault(name, UnityEngine.Color.white);
        }



        public string GetTexture(string name)
        {
            return (string) _properties.GetOrDefault(name);
        }



        public void SetColor(string name, UnityEngine.Color value)
        {
            _properties[name] = value;
        }



        public void SetTexture(string name, string value)
        {
            _properties[name] = value;
        }
    }
}