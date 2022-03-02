using NUnit.Framework;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public static class TestExtensionMethods
    {
        public static void Expect(this AssemblyComponentGraph graph, float componenets, float connectors, float edges)
        {
            Assert.AreEqual(componenets, graph.Components.Count, "Components =" + graph.Components.Count);
            Assert.AreEqual(connectors, graph.Connectors.Count, "Connectors = " + graph.Connectors);
            Assert.AreEqual(edges, graph.Edges.Count, "Edges = " + graph.Edges);
        }
    }
}