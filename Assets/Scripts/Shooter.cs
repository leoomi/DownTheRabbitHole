using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform[] shotOrigin;
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
    private bool heatseeking = false;
    [SerializeField]
    private bool targetPlayer = false; // unused
    [SerializeField]
    private float projectileForgetTime = 0.1f;
    #endregion

    #region Privates
    private GameObject target = null;
    #endregion

    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        if (heatseeking is true && target == null) target = GameObject.FindGameObjectWithTag("Player");

        while (true)
        {
            yield return new WaitForSeconds(delayBetweenShots);
            for (int i = 0; i < shotOrigin.Length; i++) { 
                GameObject projectileInstance = Projectile.CreateProjectile(projectilePrefab, -shotOrigin[i].transform.up, shotOrigin[i], Quaternion.identity, shotVelocity, projectileForgetTime);
                var projectile = projectileInstance.GetComponent<Projectile>();
                //projectileInstance.AddComponent(projectileScript);

                projectile.timeToLive = projectileTimeToLive;
                projectile.maxBounces = projectileMaxBounces;

                //Debug.Log(transform.up);

                StartCoroutine(projectile.waitToSetTarget(heatseeking is true ? target != null ? target.transform : null : null));
                //projectile.SetTarget(heatseeking is true ? target != null ? target.transform : null : null);
                //projectile.forgetTarget(2f);
                //projectile.SetInitialVelocity(-shotOrigin[i].transform.up * shotVelocity);
            }
        }
    }

}