using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int maxBounces { get; set; } = 0;
    public float timeToLive { get; set; } = 10f;
    public float angleChangingSpeed { get; set; } = 400;
    private Rigidbody2D myRigibody;
    private int bounces = 0;
    public Transform target = null;
    [SerializeField]
    private float waitBeforeLockon = 1.5f;
    private float shotVelocity = 1f;
    private bool bounce = false;
    private float waittime;

    void Awake()
    {
        myRigibody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    // factory design method babyyy
    public static GameObject CreateProjectile(GameObject projectilePrefab, Vector2 velocity, Transform shotOrigin, Quaternion q, float shotVelocity = 1f, float waittime = 0.1f)
    {
        var projectileInstance = Instantiate(projectilePrefab.gameObject, shotOrigin.position, Quaternion.identity);

        Projectile Instance = projectileInstance.AddComponent<Projectile>();
        Instance.myRigibody.velocity = velocity * shotVelocity;
        Instance.shotVelocity = shotVelocity;
        Instance.waittime = waittime;
        Instance.gameObject.transform.SetPositionAndRotation(Instance.gameObject.transform.position, shotOrigin.rotation);
        //Instance.myRigibody

        return projectileInstance;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public IEnumerator DestroyCoroutine()
    {
        // maybe add animation and sfx - Red
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //print(target);
        if (target)
        {
            Vector2 direction = (Vector2)target.position - myRigibody.position;
            direction.Normalize();
            float rot = Vector3.Cross(direction, transform.up).z;
            //print(rot);
            // wait before changing the velocity
            //new WaitForSeconds(waitBeforeLockon);
            // StartCoroutine(wait());
            if (bounce is false || (target != null && bounce is true)) myRigibody.angularVelocity = transform.rotation.z + -angleChangingSpeed * rot;
            // print(-angleChangingSpeed * rot);
            if (bounce is false || (target != null && bounce is true)) myRigibody.velocity = transform.up * shotVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Zero is infinite
        if (maxBounces == 0) return;

        var hasProjectileDestroyer = other.gameObject.TryGetComponent<ProjectileDestroyer>(out var _);
        if (hasProjectileDestroyer) return;

        bounces += 1;
        if (bounces >= maxBounces)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator waitToSetTarget(Transform t, float seconds = 0.5f)
    {
        yield return new WaitForSeconds(seconds);
        target = t;
    }

    public IEnumerator forgetTarget(float seconds = 0.1f)
    {
        Transform actualTarget = null;
        bounce = true;
        if (target)
        {
            actualTarget = target;
            target = null;
        }

        yield return new WaitForSeconds(seconds);

        if (actualTarget) { target = actualTarget; }
        bounce = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ice" || other.gameObject.tag == "Mud") return;
        StartCoroutine(forgetTarget(waittime));
    }

}