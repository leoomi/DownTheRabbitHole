using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // TODO this should be set by the shooter
    [SerializeField]
    private float timeToLive = 3f;
    private Rigidbody2D myRigibody;

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
}
