namespace Sample06152022.Game
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BounceMove : MonoBehaviour
    {
        public float speed;
        public float height;
        private float _accValue;
        
        private void Update()
        {
            _accValue += (Time.deltaTime * speed);
            var yValue = Mathf.Sin(Mathf.Deg2Rad * _accValue) * height;
            transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
        }
    }
}