using UnityEngine;
using UnityEngine.UIElements;

namespace Helper
{
    public class SetRotationToZero : MonoBehaviour
    {
        private Transform parentTransform; 
        private Vector3 offset;

        private void Start()
        {
            parentTransform = transform.parent;
            // Calculate the world-space offset
            offset = transform.position - parentTransform.position;

            // Unparent to avoid inheriting rotation
            transform.SetParent(null);
        }

        private void Update()
        {
            // Follow the parent’s position with original offset
            transform.position = parentTransform.position + offset;
        }
    }
}

