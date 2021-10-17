using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform shotOrigin;
    [SerializeField]
    private Vector2 shotVelocity;
    [SerializeField]
    private float shotDelay;
    [SerializeField]
    private float projectileTimeToLive = 3f;

    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotDelay);
            var projectileInstance = Instantiate(projectilePrefab.gameObject, shotOrigin.position, Quaternion.identity);
            var projectile = projectileInstance.GetComponent<Projectile>();

            projectile.timeToLive = projectileTimeToLive;
            projectile.SetVelocity(shotVelocity);
        }
    }
}
