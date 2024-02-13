using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankVersion_1_0_0
{
    public class Tank : MonoBehaviour, IDamageable
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private Transform health;
        [SerializeField] private Transform target;
        [SerializeField] private Transform tower;
        [SerializeField] private Bullet bulletPrefabs;
        [SerializeField] private Transform spawnPoint;

        public bool CalculateTargetDir(out Vector3 targetDir)
        {
            targetDir = transform.position;

            if(target != null)
            {
                targetDir = target.position - targetDir;
                targetDir.y = 0;
                targetDir.Normalize();
                return true;
            }

            return false;
        }

        public void Damage(float damage)
        {
            maxHealth -= damage;
            health.localScale = new Vector3 (maxHealth / 100f, 1, 1);

            if(maxHealth < 0)
            {
                Destroy(gameObject);
            }
        }

        public void Move(Vector3 direction)
        {
            transform.Translate(direction * moveSpeed * Time.smoothDeltaTime, Space.World);

            Rotate(direction);
            TowerRotate();
        }

        private void Rotate(Vector3 direction)
        {
            if(direction == Vector3.zero)
            {
                return;
            }

            Quaternion rotation = new Quaternion();
            rotation.SetLookRotation(direction);

            transform.rotation = Quaternion.Slerp( transform.rotation, rotation, Time.smoothDeltaTime * rotateSpeed);
        }

        public void Shoot()
        {
            Instantiate(bulletPrefabs, spawnPoint.position, spawnPoint.rotation);
        }

        private void TowerRotate()
        {
            Vector3 targetDir;
            if (CalculateTargetDir(out targetDir))
            {
                tower.LookAt(tower.position + targetDir);
            }
        }
    }
}

