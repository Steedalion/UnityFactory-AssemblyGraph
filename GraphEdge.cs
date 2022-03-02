using System.Collections.Generic;
using GraphDomain;
using UnityEngine;

namespace AssemblyGraph
{
    public class GraphEdge : GEdge
    {
        private readonly GraphConnector start;
        private readonly GraphConnector end;


        public GraphEdge(GraphConnector start, GraphConnector end) : base(start.Index, end.Index)
        {
            this.start = start;
            this.end = end;
        }

        public float Distance()
        {
            return Vector3.Distance(start.transform.position, end.transform.position);
        }

        public float Angle()
        {
            return Vector3.Angle(start.transform.up, end.transform.up);
        }
    }
}