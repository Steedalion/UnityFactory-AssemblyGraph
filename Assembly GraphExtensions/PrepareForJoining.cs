using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Setup
{
    public class PrepareForJoining : MonoBehaviour
    {
        private Rigidbody rb;

        void Start()
        {
            rb = AddOrCreate<Rigidbody>();
            AddOrCreate<Throwable>();
            rb.drag = 25;
            rb.angularDrag = 25;
        }

        private T AddOrCreate<T>() where T : Component
        {
            T item = GetComponent<T>();
            if (item == null)
            {
                item = gameObject.AddComponent<T>();
            }

            return item;
        }
    }
}