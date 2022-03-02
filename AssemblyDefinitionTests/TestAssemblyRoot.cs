using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class TestAssemblyRoot
    {
        GameObject gameObject;


        [UnitySetUp]
        public IEnumerator CreateAssemblyRootComponent()
        {
            gameObject = A.go;
            yield return null;
        }

        [UnityTest]
        public IEnumerator CreateRoot()
        {
            RootComponent root = gameObject.AddComponent<RootComponent>();
            Assert.IsNotNull(root);
            yield return null;
        }
    }
}