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
    [SerializeField]
    [Range(-30, 30)]
    private float[] spray = new float[2] { 0, 0 }; // no spray by default
    [SerializeField]
    [Range(2, 0)]
    private float rotationMultiplier = 1f;
    [SerializeField]
    private Vector2 maximumRotation = new Vector2(10, 10);
    [SerializeField]
    private bool backAndForthSpray = true;

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

            print(transform.up);
            // kind of has a mind of its own but will stay mostly within range (will deviate mostly if the multiplier is touched
            (float, float) sprayRange = (spray[0] > spray[1] ? (spray[1], spray[0]) : (spray[0], spray[1])); // random spray
            float toadd = Random.Range(sprayRange.Item1, sprayRange.Item2);
            float toadd2 = ((toadd + transform.rotation.z) < transform.up.z - sprayRange.Item1 ? -toadd : (transform.transform.rotation.z - toadd) > transform.transform.rotation.z - sprayRange.Item2 ? toadd : -toadd);
            transform.Rotate(0, 0, (maximumRotation.y < (toadd2 + transform.rotation.z) ? toadd : maximumRotation.x > (toadd2 + transform.transform.rotation.z) ? -toadd : toadd2));
            projectile.SetVelocity(-transform.up * shotVelocity);
        }
    }
}
