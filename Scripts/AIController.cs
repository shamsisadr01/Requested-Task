using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankVersion_1_0_0;

[RequireComponent(typeof(Tank))]
public class AIController : MonoBehaviour
{

    private Tank tank;
    private float moveTime;
    private float moveTimer;
    private Vector3 moveDir;

    private bool isFire;

    void Start()
    {
        tank = GetComponent<Tank>();
        InputController();
    }

    private void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer > moveTime)
        {
            InputController();
        }
        tank.Move(moveDir);
    }

    private void InputController()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        Vector3 newDir = new Vector3(x, 0, z);
        newDir = transform.InverseTransformDirection(newDir);

        moveDir = newDir;

        Vector3 targetDir;
        if (tank.CalculateTargetDir(out targetDir))
        {
            moveDir = Vector3.Lerp(moveDir, targetDir, 0.5f);
            if (!isFire)
            {
                Invoke("Fire", Random.Range(0.5f, 1.5f));
            }
        }
        else
        {
            isFire = false;
            CancelInvoke("Fire");
        }

        if (moveDir.magnitude > 1)
        {
            moveDir.Normalize();
        }
        moveTime = Random.Range(1f, 2f);
        moveTimer = 0;
    }

    private void Fire()
    {
        isFire = true;
        tank.Shoot();
        Invoke("Fire", Random.Range(0.5f, 1.5f));
    }

}
