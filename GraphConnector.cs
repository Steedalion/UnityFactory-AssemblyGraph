using GraphDomain;
using UnityEngine;

namespace AssemblyGraph
{
    public class GraphConnector : GNode
    {
        public readonly Transform transform;

        public GraphConnector(AssemblyConnector connector)
        {
            Index = connector.Index;
            this.transform = connector.transform;
        }
    }
}