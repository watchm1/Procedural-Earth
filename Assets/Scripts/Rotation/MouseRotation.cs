using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Mechanics {
    public class MouseRotation : MonoBehaviour
    {   
        void Start()
        {
            
        }
        [SerializeField] private float rotationSpeed = 2f;

        private void Update()
        {
            float xAxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

            Debug.Log(xAxisRotation);
            Debug.Log(yAxisRotation);
            transform.Rotate(Vector3.up, xAxisRotation);
            transform.Rotate(Vector3.right, yAxisRotation);
        }
    }

}