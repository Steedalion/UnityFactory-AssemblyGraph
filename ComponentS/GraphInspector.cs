using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AssemblyGraph
{
    [Serializable]
    public class LogMessageEvent : UnityEvent<String>
    {
    }

    public class GraphInspector : MonoBehaviour
    {
        [SerializeField] public LogMessageEvent graphFound,graphError;

        [SerializeField] private float radius = 0.3f;
        public GraphBuilder builder;


        public void BuildAndDestroy()
        {
            try
            {
                var cols = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0);
                var root = cols
                    .Select(hit => hit.collider.GetComponent<RootComponent>())
                    .Union(cols.Select(hit => hit.collider.GetComponentInParent<RootComponent>()))
                    .Single(component => component != null);

                if (root == null)
                {
                    Debug.Log("No root component found");
                    return;
                }

                builder = new GraphBuilder(root);
                graphFound?.Invoke(builder.Graph.ToCsvRow());

                print(builder.Graph);
                foreach (Dictionary<int, GraphEdge>.ValueCollection valueCollection in builder.Graph.Edges.ToList()
                    .Select(pair => pair.Value).Select(edges => edges.Values))
                {
                    foreach (GraphEdge graphEdge in valueCollection)
                    {
                        print(graphEdge.Distance());
                    }
                }
            }
            catch (Exception e)
            {
                graphError?.Invoke(e.StackTrace);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}