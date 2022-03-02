using System;
using System.Text;
using GraphDomain;

namespace AssemblyGraph
{
    public class AssemblyComponentGraph
    {
        private const string Separator = ",";
        private int id = 0;
        public ComponentStore Components { get; set; }
        public NodeStore<GraphConnector> Connectors { get; set; }
        public BigraphStore<GraphEdge> Edges { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Graph");
            builder.Append("-Components:" + Components.Count + Environment.NewLine);
            builder.Append("-Connectors:" + Connectors.Count + Environment.NewLine);
            builder.Append("-Edges:" + Edges.Count + Environment.NewLine);
            return builder.ToString();
        }


        public string ToCsvHeader()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("id" + Separator);
            builder.Append("Components" + Separator);
            builder.Append("Connectors" + Separator);
            builder.Append("Edges" + Separator);
            foreach (GraphEdge edge in Edges)
            {
                builder.Append(edge.Distance() + Separator);
            }

            return builder.ToString();
        }
        public string ToCsvRow()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(id + Separator);
            builder.Append(Components.Count + Separator);
            builder.Append(Connectors.Count + Separator);
            builder.Append(Edges.Count + Separator);
            foreach (GraphEdge edge in Edges)
            {
                builder.Append(edge.Distance() + Separator);
                builder.Append(edge.Angle() + Separator);
            }

            return builder.ToString();
        }
    }
}