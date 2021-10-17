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
    private float shotVelocity = 10f;
    [SerializeField]
    private float delayBetweenShots = 1f;
    [SerializeField]
    private float initialDelay = 0f;
    [SerializeField]
    private float projectileTimeToLive = 3f;
    [SerializeField]
    private int projectileMaxBounces = 0;

    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            yield return new WaitForSeconds(delayBetweenShots);
            var projectileInstance = Instantiate(projectilePrefab.gameObject, shotOrigin.position, Quaternion.identity);
            var projectile = projectileInstance.GetComponent<Projectile>();

            projectile.timeToLive = projectileTimeToLive;
            projectile.maxBounces = projectileMaxBounces;

            Debug.Log(transform.up);

            projectile.SetVelocity(-transform.up * shotVelocity);
        }
    }
}
