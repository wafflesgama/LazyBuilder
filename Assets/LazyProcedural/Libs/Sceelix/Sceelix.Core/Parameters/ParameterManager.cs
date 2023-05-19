using System;
using System.Collections.Generic;
using System.Linq;
using Sceelix.Loading;

namespace Sceelix.Core.Parameters
{
    public class ParameterManager
    {
        /// <summary>
        /// List of all types of derived classes from this Parameter class.
        /// </summary>
        public static List<Type> ParameterTypes
        {
            get;
            private set;
        } = new List<Type>();



        public static void Init()
        {
            ParameterTypes = SceelixDomain.Types.Where(type => typeof(Parameter).IsAssignableFrom(type)).ToList();
        }
    }
}