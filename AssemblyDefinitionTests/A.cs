using System.Collections;
using UnityEngine;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public static class A
    {
        public static ComponentBuilder go => new ComponentBuilder();
        public static ComponentBuilder root => new ComponentBuilder()
            .With<BoxCollider>()
            .With<RootComponent>()
            .Name("Root");

        public static ComponentBuilder Component =>
            new ComponentBuilder()
                .With<BoxCollider>()
                .With<AssemblyComponent>()
                .Name("Component");

        public static IEnumerator DestroyAllExistingGameObjects()
        {
            foreach (GameObject gameObject in Object.FindObjectsOfType<GameObject>())
            {
                Object.Destroy(gameObject);
                yield return null;
            }

        }
    }
}