using UnityEngine;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class ComponentBuilder
    {
        private GameObject go = Object.Instantiate(new GameObject());

        public GameObject Build()
        {
            return go;
        }

        public static implicit operator GameObject(ComponentBuilder builder)
        {
            return builder.Build();
        }

        public T WithA<T>() where T : Component
        {
            GameObject gameObject = Build();
            return gameObject.AddComponent<T>();
        }

        public ComponentBuilder With<T>() where T : Component
        {
            go.AddComponent<T>();
            return this;
        }

        public ComponentBuilder ConnectorAt(float x, float y, float z)
        {
            var pos = new Vector3(x, y, z);
            return ConnectorAt(pos);
        }

        public ComponentBuilder ConnectorAt(Vector3 pos)
        {
            GameObject connector = new ComponentBuilder()
                .At(pos)
                .With<AssemblyConnector>()
                .Parent(go)
                .Name("Connector of " + go.name)
                .Build();
            return this;
        }

        public ComponentBuilder At(float x, float y, float z)
        {
            var pos = new Vector3(x, y, z);
            return At(pos);
        }

        public ComponentBuilder At(Vector3 position)
        {
            go.transform.position = position;
            return this;
        }

        public ComponentBuilder Parent(GameObject parent)
        {
            go.transform.parent = parent.transform;
            return this;
        }

        public ComponentBuilder Name(string name)
        {
            go.name = name;
            return this;
        }
    }
}