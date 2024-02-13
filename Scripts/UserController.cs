using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankVersion_1_0_0;

[RequireComponent(typeof(Tank))]
public class UserController : MonoBehaviour
{
    private Tank tank;
    
    void Start()
    {
        tank = GetComponent<Tank>();
    }

   
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(x, 0, z);

        if(inputDir.magnitude > 1)
        {
            inputDir.Normalize();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            tank.Shoot();
        }

        tank.Move(inputDir);
    }
}
