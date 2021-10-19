using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int maxBounces { get; set; }
    public float timeToLive { get; set; }
    private Rigidbody2D myRigibody;
    private int bounces = 0;

    void Awake()
    {
        myRigibody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public void SetVelocity(Vector2 velocity)
    {
        myRigibody.velocity = velocity;
    }

    public IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Zero is infinite
        if (maxBounces == 0)
        {
            return;
        }

        var hasProjectileDestroyer = other.gameObject.TryGetComponent<ProjectileDestroyer>(out var _);
        if (hasProjectileDestroyer)
        {
            return;
        }

        bounces += 1;
        if (bounces >= maxBounces)
        {
            Destroy(gameObject);
        }
    }
}
