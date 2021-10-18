using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    [SerializeField]
    private bool goesUp = true;
    [SerializeField]
    private float inStayDelta = 1f;

    private bool isActive = false;

    private float timeInStay = 0f;

    void Start()
    {
        StartCoroutine(ActivationCoroutine());

    }

    private IEnumerator ActivationCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        isActive = true;
    }

    // might want to check for projectile/box collision triggering this (could also make for some fun levels if left in?) - Red
    // when falling from a level-up onto the hole/stairs, the behaviour is NOT triggered. This should be fixed.. eventually.. - Red
    // Bug where you can sometimes spawn on this instead of the x, y, z when switching levels by going DOWN a level. - Red
    // This behaves fine when going up.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // workaround for ice/(hole/stairs) down behaviour - Red
        // ensure the gameobject is in fact the player
        var gameObj = other.gameObject;
        var component = gameObj.GetComponent<PlayerController>();

        if (!component)
        {
            return;
        }

        if (component.state != PlayerState.Normal)
        {
            return;
        }

        // not player
        if (component) component.impedeMovement = false;

        // Workaround to avoid player to change the scene when falling/going up to a stairs object
        if (!isActive)
        {
            return;
        }

        SetupPlayerToLoadScene();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // workaround for ice/(hole/stairs) down behaviour - Red
        // ensure the gameobject is in fact the player
        var gameObj = other.gameObject;
        var component = gameObj.GetComponent<PlayerController>();

        if (!component)
        {
            return;
        }

        // Workaround to avoid player to change the scene when falling/going up to a stairs object
        if (!isActive || component.state != PlayerState.Normal)
        {
            return;
        }

        // Gives player some time understand what's going if we don't have animations
        timeInStay += Time.deltaTime;
        if (timeInStay > inStayDelta)
        {
            SetupPlayerToLoadScene();
        }
    }

    private void SetupPlayerToLoadScene()
    {
        var player = PlayerController.instance;

        player.GoDown(LoadScene, this.gameObject);
    }

    private void LoadScene()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        var destinationIndex = currentIndex + (goesUp ? 1 : - 1);
        SceneManager.LoadScene(destinationIndex);
    }
}
