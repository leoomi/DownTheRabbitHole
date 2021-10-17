using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 8f;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        var isPlayer = other.gameObject.TryGetComponent<PlayerController>(out var _);
        if (isPlayer)
        {
            Destroy(gameObject);
        }
    }
}
