using UnityEngine;

namespace AssemblyGraph
{
    [DisallowMultipleComponent]
    public class AssemblyComponent : MonoBehaviour
    {
        public AssemblyConnector[] GetConnectors()
        {
            return gameObject.GetComponentsInChildren<AssemblyConnector>();
        }
    }
}
