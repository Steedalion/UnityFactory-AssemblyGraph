using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class TestAssemblyComponent
    {
        private GameObject gameObject;

        [UnityTest]
        public IEnumerator CreateAComponent()
        {
            gameObject = A.Component.Build();
            yield return null;
        }

        [UnityTest]
        public IEnumerator CreateAComponentWithConnector()
        {
            gameObject = A.go;
            var comp = gameObject.AddComponent<AssemblyComponent>();
            yield return null;
            var child = A.go.Parent(gameObject).Build();
            yield return null;
            child.AddComponent<AssemblyConnector>();
            yield return null;
            Assert.AreEqual(1,gameObject.transform.childCount);
            Assert.AreEqual(1, comp.GetConnectors().Length);
        }

        [UnityTest]
        public IEnumerator CreateAComponentWithTwoConnectors()
        {
            gameObject = A.Component.ConnectorAt(Vector3.zero).ConnectorAt(Vector3.one);
            yield return null;
            Assert.AreEqual(2, gameObject.GetComponent<AssemblyComponent>().GetConnectors().Length);
        }

        [UnityTest]
        public IEnumerator GetZeroConnectors()
        {
            var comp = A.go.WithA<AssemblyComponent>();
            yield return null;
            Assert.AreEqual(0, comp.GetConnectors().Length);
        }
    }
}