using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class TestBuildAndDestroyOfComponentsInAScene
    {
        GraphInspector ins;

        [UnitySetUp]
        public IEnumerator SetYp()
        {
            ins = A.go.WithA<GraphInspector>();
            yield return null;
        }

        [UnityTest]
        public IEnumerator RootWithConnector()
        {
            GameObject root = A.root.ConnectorAt(Vector3.zero).Build();
            yield return null;
            ExpectFromBUild(1, 1, 0);
        }

        [UnityTest]
        public IEnumerator RootWithoutConnectorIsUndetectable()
        {
            GameObject root = A.root.Build();
            yield return null;
            ins.BuildAndDestroy();
          
                // ExpectFromBUild(0, 0, 0);
          
        }

        [UnityTest]
        public IEnumerator RootWithMultipleConnectors()
        {
            GameObject root = A.root.ConnectorAt(Vector3.zero).ConnectorAt(Vector3.one * 4);
            yield return null;
            ExpectFromBUild(1, 2, 0);
        }

        [UnityTest]
        public IEnumerator TwoComponentsInRange()
        {
            A.root.ConnectorAt(Vector3.zero).Build();
            A.Component.ConnectorAt(Vector3.zero).Build();
            yield return null;
            ExpectFromBUild(2, 2, 1);
        }

        [UnityTest]
        public IEnumerator TwoComponentsOutRange()
        {
            A.root.ConnectorAt(Vector3.zero).Build();
            A.Component.ConnectorAt(Vector3.one * 2).Build();
            yield return null;
            ExpectFromBUild(1, 1, 0);
        }

        [UnityTest]
        public IEnumerator DestroysComponents()
        {
            GameObject root = A.root.ConnectorAt(Vector3.zero).Build();
            GameObject other = A.Component.ConnectorAt(Vector3.zero).Build();
            yield return null;
            ExpectFromBUild(2, 2, 1);
            yield return null;
            Assert.IsTrue(root == null);
            Assert.IsTrue(other == null);
        }

        [UnityTest]
        public IEnumerator NestedComponent()
        {
            GameObject parent = A.go.With<Rigidbody>().Build();
            GameObject root = A.root.Parent(parent).ConnectorAt(Vector3.zero);
            GameObject other = A.Component.Parent(root).ConnectorAt(Vector3.zero);
            yield return null;
            ExpectFromBUild(2,2,1);
        }
        private void ExpectFromBUild(float componenets, float connectors, float edges)
        {
            ins.BuildAndDestroy();
            ins.builder.Graph.Expect(componenets, connectors, edges);
        }
    }
}