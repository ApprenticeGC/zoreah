namespace Sample06152022.Game
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Move : MonoBehaviour
    {
        public Vector3 direction;
        public float speed;

        private void Update()
        {
            var adjustedValue = direction * Time.deltaTime * speed;
            
            transform.Translate(adjustedValue);
        }
    }
}