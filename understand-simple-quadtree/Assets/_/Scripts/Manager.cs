namespace TestUse
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Manager : MonoBehaviour
    {
        public List<Collider> colliders;

        public Collider toCheckCollider;
        
        private Quadtree _quadtree;

        private List<Collider> _returnedObjects = new List<Collider>();
        
        private void Start()
        {
            _quadtree = new Quadtree(0, new Bounds(Vector3.zero, new Vector3(200.0f, 1.0f, 200.0f)));
        }

        private void Update()
        {
            _quadtree.Clear();
            for (var i = 0; i < colliders.Count; ++i)
            {
                var collider = colliders[i];
                _quadtree.Insert(collider);
            }
            
            // for (var i = 0; i < colliders.Count; ++i)
            // {
                _returnedObjects.Clear();
                _returnedObjects = _quadtree.Retrieve(_returnedObjects, toCheckCollider);
                // _returnedObjects = _quadtree.Retrieve(_returnedObjects, colliders[i]);
            
                // for (var j = 0; j < _returnedObjects.Count; ++j)
                // {
                //     var possibleCollider = _returnedObjects[j];
                //     var distance = Vector3.Distance(possibleCollider.transform.position, toCheckCollider.transform.position);
                //     Debug.Log($"{possibleCollider} distance: {distance}");
                // }
            // }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            for (var j = 0; j < _returnedObjects.Count; ++j)
            {
                var c = _returnedObjects[j];
                var adjustedSize = c.bounds.size * 1.2f;
                Gizmos.DrawCube(c.bounds.center, adjustedSize);
            }

        }
    }
}