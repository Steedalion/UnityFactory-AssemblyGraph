using System.Collections.Generic;
using System.Linq;
using GraphDomain;
using Object = UnityEngine.Object;

namespace AssemblyGraph
{
    public class GraphBuilder
    {
        private AssemblyComponentGraph graph;
        private readonly RootComponent root;

        public AssemblyComponentGraph Graph
        {
            get
            {
                if (graph == null)
                {
                    Build();
                }

                return graph;
            }
        }

        public GraphBuilder(RootComponent root, bool runImmediately = true)
        {
            this.root = root;
            if (runImmediately)
            {
                Build();
            }
        }


        private void Build()
        {
            Queue<AssemblyComponent> queue = new Queue<AssemblyComponent>();
            ComponentStore components = new ComponentStore();
            NodeStore<GraphConnector> connectors = new NodeStore<GraphConnector>();
            BigraphStore<GraphEdge> edges = new BigraphStore<GraphEdge>();
            HashSet<AssemblyComponent> visitedComponents = new HashSet<AssemblyComponent>();
            HashSet<AssemblyConnector> visitedConnectors = new HashSet<AssemblyConnector>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                AssemblyComponent current = queue.Dequeue();
                if (visitedComponents.Contains(current))
                {
                    continue;
                }
                visitedComponents.Add(current);
                components.Add(new AComponent(current, components.GetNextAvailableIndex()));
                var unvisitedConnectors =
                    current.GetConnectors().Where(c => !connectors.Contains(c.Index));

                foreach (AssemblyConnector connector in unvisitedConnectors)
                {
                    visitedConnectors.Add(connector);
                    var graphConnector = GetOrCreate(connector);
                    connectors.Add(graphConnector);
                    var neighbors = connector.GetNeighbors();
                    foreach (var neighbor in neighbors.Where(n => !visitedConnectors.Contains(n)) )
                    {
                        var asGraphConnector = GetOrCreate(neighbor);
                        edges.Add(new GraphEdge(graphConnector, asGraphConnector));
                    }

                    var unvisitedComponentNeighbors = neighbors
                        .Select(c => c.Component)
                        .Where(component => !visitedComponents.Contains(component));
                    foreach (AssemblyComponent neighborComponent in unvisitedComponentNeighbors)
                    {
                        queue.Enqueue(neighborComponent);
                    }
                }
            }

            graph = new AssemblyComponentGraph
            {
                Components = components,
                Connectors = connectors,
                Edges = edges
            };

            foreach (AssemblyComponent component in visitedComponents)
            {
                Object.Destroy(component.transform.root.gameObject);
            }
            GraphConnector GetOrCreate(AssemblyConnector neighbor)
            {
                if (connectors.Contains(neighbor.Index))
                {
                    return connectors.GetNode(neighbor.Index);
                }

                return new GraphConnector(neighbor);
            }
        }
    }
}