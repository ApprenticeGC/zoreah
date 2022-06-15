namespace Sample06152022.Game
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Parallax : MonoBehaviour
    {
        public GameObject cameraGO;
        
        private float _length;
        private float _startPos;

        public float parallaxEffect;

        private void Start()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            var temp = (cameraGO.transform.position.x * (1 - parallaxEffect));
            var dist = (cameraGO.transform.position.x * parallaxEffect);
            // Debug.Log($"dist: {dist}");
            transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
            // Debug.Log($"transform.position: {transform.position}");

            if (temp > _startPos + _length)
            {
                _startPos += _length;
            }
            else if (temp < _startPos - _length)
            {
                _startPos -= _length;
            }
        }
    }
}
