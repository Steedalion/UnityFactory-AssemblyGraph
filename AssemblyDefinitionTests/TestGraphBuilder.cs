using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class TestGraphBuilder
    {

        
        [UnitySetUp]
        public IEnumerator DestroyAll()
        {
            yield return A.DestroyAllExistingGameObjects();
        }
        [UnityTearDown]
        public IEnumerator TeadrDonw()
        {
            yield return A.DestroyAllExistingGameObjects();
        }

        [UnityTest]
        public IEnumerator GraphBuilderEmptyRoot()
        {
            GameObject root = A.root.Build();
            Expect(root, 1, 0, 0);
            yield return null;
        }

        private static void Expect(GameObject root, int componenets, int connectors, int edges)
        {
            var graph = new GraphBuilder(root.GetComponent<RootComponent>()).Graph;
            Assert.AreEqual(componenets, graph.Components.Count, "Components =" + graph.Components.Count);
            Assert.AreEqual(connectors, graph.Connectors.Count, "Connectors = " + graph.Connectors);
            Assert.AreEqual(edges, graph.Edges.Count, "Edges = " + graph.Edges);
        }


        [UnityTest]
        public IEnumerator
            BuildAGraphWithConnectors()
        {
            GameObject r = A.root.ConnectorAt(Vector3.one).ConnectorAt(-1 * Vector3.one);
            yield return null;
            Expect(r, 1, 2, 0);
        }


        [UnityTest]
        public IEnumerator TwoComponents()
        {
            GameObject r = A.root.ConnectorAt(Vector3.zero);
            var second = A.Component.ConnectorAt(Vector3.zero).Build();
            yield return null;
            Expect(r, 2, 2, 1);
        }

        [UnityTest]
        public IEnumerator TwoComponentsWithUnusedConnectors()
        {
            GameObject root = A.root.ConnectorAt(Vector3.zero).ConnectorAt(Vector3.one);
            var second = A.Component.ConnectorAt(Vector3.zero).ConnectorAt(Vector3.one * -1).Build();
            yield return null;
            Expect(root, 2, 4, 1);
        }


        [UnityTest]
        public IEnumerator BuildStar()
        {
            GameObject root = A.root.ConnectorAt(Vector3.zero);
            GameObject second = A.Component.ConnectorAt(Vector3.zero);
            GameObject Third = A.Component.ConnectorAt(Vector3.zero);
            yield return null;
            Expect(root, 3, 3, 3);
        }

        [UnityTest]
        public IEnumerator BuildTriangle()
        {
            var r = A.root.ConnectorAt(1, 0, 0).ConnectorAt(-1, 0, 0).Build();
            var left = A.Component.ConnectorAt(-1, 0, 0.005f).ConnectorAt(0, 0, -1).Build();
            var right = A.Component.ConnectorAt(1, 0, -0.005f).ConnectorAt(0, 0, -1).Build();
            yield return null;
            yield return null;
            yield return null;
            Expect(r, 3, 6, 3);
        }

        [UnityTest] 
        public IEnumerator ComponentShouldNotConnectToSelf()
        {
            GameObject root = A.root.ConnectorAt(Vector3.zero).ConnectorAt(Vector3.zero);
            yield return null;
            Expect(root, 1, 2, 0);
        }

        [UnityTest]
        public IEnumerator MuscialTriangle()
        {
            GameObject r = A.root.ConnectorAt(1, 0, 0).ConnectorAt(-1, 0, 0);
            GameObject left = A.Component.ConnectorAt(-1, 0, 0.05f).ConnectorAt(0, 0, -1);
            GameObject right = A.Component.ConnectorAt(1, 0, -0.05f).ConnectorAt(0, 0, -1);
            GameObject bottom = A.Component.ConnectorAt(0, 0, -1.05f).ConnectorAt(0, 0, -2);
            yield return null;
            yield return null;
            Expect(r, 4, 8, 5);
        }


        [UnityTest]
        public IEnumerator TableTop2D()
        {
            GameObject root = A.root.ConnectorAt(-1, 0, 0).ConnectorAt(1, 0, 0);
            var leftLeg = A.Component.ConnectorAt(-1.05f, 0f, 0f);
            var rightLeg = A.Component.ConnectorAt(1.05f, 0, 0);

            yield return null;
            Expect(root, 3, 4, 2);
        }

        [UnityTest]
        public IEnumerator Table3D()
        {
            GameObject root = A.root.ConnectorAt(1, 0, -1).ConnectorAt(1, 0, 1).ConnectorAt(-1, 0, 1)
                .ConnectorAt(-1, 0, -1);
            var leftFront = A.Component.ConnectorAt(-1, -0.05f, -1);
            var leftBack = A.Component.ConnectorAt(-1, -0.05f, 1);
            var rightFront = A.Component.ConnectorAt(1, -0.05f, -1);
            var rightBack = A.Component.ConnectorAt(1, -0.5f, 1);

            yield return null;
            Expect(root, 5, 8, 4);
        }
    }
}