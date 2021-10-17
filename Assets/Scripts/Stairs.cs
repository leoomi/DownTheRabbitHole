using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    [SerializeField]
    private bool goesUp = true;

    private bool isActive = false;

    void Start()
    {
        StartCoroutine(ActivationCoroutine());
    }

    private IEnumerator ActivationCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        isActive = true;
    }

    // might want to check for projectile/box collision triggering this (could also make for some fun levels if left in?) - Red
    // when falling from a level-up onto the hole/stairs, the behaviour is NOT triggered. This should be fixed.. eventually.. - Red
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // workaround for ice/(hole/stairs) down behaviour - Red
        // ensure the gameobject is in fact the player
        GameObject gameObj = other.gameObject;
        var component = gameObj.GetComponent<PlayerController>();
        // not player
        if (component) component.impedeMovement = false;

        // Workaround to avoid player to change the scene when falling/going up to a stairs object
        if (!isActive)
        {
            return;
        }

        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        var destinationIndex = currentIndex + (goesUp ? 1 : - 1);
        Debug.Log(currentIndex);

        


        SceneManager.LoadScene(destinationIndex);
    }
}
