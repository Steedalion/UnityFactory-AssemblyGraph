using Setup;
using UnityEngine;

namespace AssemblyGraph
{
    public class RemoveAllAssemblyStuffAtStart : MonoBehaviour
    {
        private void Awake()
        {
            DestoryAllInChildren<AssemblyConnector>();
            DestoryAllInChildren<AssemblyComponent>();
            DestoryAllInChildren<BoxCollider>();
            DestoryAllInChildren<PrepareForJoining>();

        }

        private void DestoryAllInChildren<T>() where  T: Component
        {
            foreach (T componentsInChild in GetComponentsInChildren<T>())
            {
                Destroy(componentsInChild);
            }
        }
    }
}