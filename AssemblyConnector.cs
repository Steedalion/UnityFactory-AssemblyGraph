using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AssemblyGraph
{
    [RequireComponent(typeof(BoxCollider))]
    public class AssemblyConnector : MonoBehaviour
    {
        public float radius = 0.05f;
        public AssemblyComponent Component { get; private set; }
        public int Index { get; private set; }

        private void Start()
        {
            Index = ConnectorIDs.Instance.GetNextAvailable();
            Component = GetComponentInParent<AssemblyComponent>();
            if (Component == null)
            {
                Debug.LogError("Connector requires component as parent: "+name);
                // Debug.LogException(new ConnectorWithoutComponentException());
            }
        }


        public AssemblyConnector[] GetNeighbors()
        {
            var hit = Physics.SphereCastAll(transform.position, radius, Vector3.forward, 0);

            var cons =
                from colliders in hit.Select(raycastHit => raycastHit.collider)
                    .Where(collider1 => collider1.transform.parent != gameObject.transform.parent)
                let connectors = colliders.GetComponent<AssemblyConnector>()
                where connectors != null
                where connectors != this
                select connectors;

            return cons.ToArray();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}