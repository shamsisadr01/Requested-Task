using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankVersion_1_0_0
{
    public class Billboard : MonoBehaviour
    {
        private Transform cam;

        private void Start ()
        {
            cam = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(cam.position);
        }
    }

}
