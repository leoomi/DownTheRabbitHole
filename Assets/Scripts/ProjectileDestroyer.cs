using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckAndDestroyOtherObject(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckAndDestroyOtherObject(other.gameObject);
    }

    private void CheckAndDestroyOtherObject(GameObject otherGameObject)
    {
        var isOtherProjectile = otherGameObject.TryGetComponent<Projectile>(out var _);

        if (isOtherProjectile)
        {
            Destroy(otherGameObject);
        }
    }
}
