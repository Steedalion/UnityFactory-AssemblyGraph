using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class TestAssemblyConnectors
    {
        GameObject gameObject;
        GameObject child;
        private AssemblyConnector connector;

        [UnitySetUp]
        public IEnumerator CreateGo()
        {
            gameObject = A.Component;
            child = A.go.Parent(gameObject);
            connector = child.AddComponent<AssemblyConnector>();
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator ClearGOs()
        {
            yield return A.DestroyAllExistingGameObjects();
        }

        [UnityTest]
        public IEnumerator ConnectorRequiresAssemblyComponentParent()
        {
            GameObject go = A.go.With<AssemblyConnector>().Build();
            LogAssert.Expect(LogType.Error,new Regex("."));
            yield return null;
        }

        [UnityTest]
        public IEnumerator AddConnectorToAssemblyComponent()
        {
            yield return null;
        }

        [UnityTest]
        public IEnumerator CannotGetConnectorOutOfRange()
        {
            var component = A.Component.ConnectorAt(Vector3.zero).Build();
            var connector = A.go.At(Vector3.one * 2).Parent(component).WithA<AssemblyConnector>();
            var outOfRange = A.Component.ConnectorAt(Vector3.zero).Build();
            yield return null;
            Assert.AreEqual(0, connector.GetNeighbors().Length);
        }

        [UnityTest]
        public IEnumerator GetSingleNeighbour()
        {
            var secondComponenet = A.Component.Build();
            var scon = A.go.Parent(secondComponenet).WithA<AssemblyConnector>();
            yield return null;
            Assert.AreEqual(1, connector.GetNeighbors().Length);
            Assert.AreSame(scon, connector.GetNeighbors()[0]);
        }

        [UnityTest]
        public IEnumerator GetMultiNeighbors()
        {
            var secondComponent = A.Component.ConnectorAt(0, 0, 0.1f).ConnectorAt(0, 0, -0.1f).Build();
            Assert.AreEqual(2, connector.GetNeighbors().Length);
            yield return null;
        }

        [UnityTest]
        public IEnumerator NeighborOutOfRange()
        {
            Assert.AreEqual(0, connector.GetNeighbors().Length);
            var secondComponent = A.Component.At(Vector3.one * 4).ConnectorAt(Vector3.one * 4);
            // A.CreateConnector(secondComponent, Vector3.one);
            connector.radius = 0.1f;
            yield return null;
            yield return null;
            Assert.AreEqual(0, connector.GetNeighbors().Length);
        }

        [UnityTest]
        public IEnumerator ConnectorsHaveUniqueIndexs()
        {
            yield return null;
            var c1 = A.go.Parent(gameObject).WithA<AssemblyConnector>();
            var c2 = A.go.Parent(gameObject).WithA<AssemblyConnector>();
            var c3 = A.go.Parent(gameObject).WithA<AssemblyConnector>();
            yield return null;
            Assert.AreNotEqual(c1.Index, c2.Index);
            Assert.AreNotEqual(c1.Index, c3.Index);
            Assert.AreNotEqual(c2.Index, c3.Index);
            Assert.AreNotEqual(connector.Index, c3.Index);
            yield return null;
        }
    }
}