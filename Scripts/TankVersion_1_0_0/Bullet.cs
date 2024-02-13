using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankVersion_1_0_0
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float amountDamage = 5f;
        [SerializeField] private float lifeTime = 2f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }


        void Update()
        {
            Move();
            CollisionCheck();
        }

        private void Move()
        {
            transform.Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        }

        private void CollisionCheck()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                Tank tank = hit.transform.root.GetComponent<Tank>();
                if (tank != null)
                {
                    tank.Damage(amountDamage);
                }
                Destroy(gameObject);
            }
        }
    }

}
