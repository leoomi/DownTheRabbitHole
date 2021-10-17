using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour 
{

    #region Ice Behaviour
    [SerializeField]
    private float maintainedVelocityPercent = 0.95f; // unused (for now)
    [SerializeField]
    private bool rotationAllowed = false; // unused (for now)
    [SerializeField]
    private bool movementAllowed = false; // unused (for now)
    [SerializeField]
    private bool slowMovement = false;
    #endregion

    #region internal
    // IM EMPTY!
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ensure the gameobject is in fact the player
        GameObject gameObj = collision.gameObject;
        var component = gameObj.GetComponent<PlayerController>();
        // not player
        if (!component) return;

        // makes readability better
        PlayerController controller = component;
        controller.impedeMovement = true;
        controller.slowMovement = slowMovement;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ensure the gameobject is in fact the player
        GameObject gameObj = collision.gameObject;
        var component = gameObj.GetComponent<PlayerController>();
        // not player
        if (!component) return;

        // makes readability better
        PlayerController controller = component;
        controller.impedeMovement = false;
        controller.slowMovement = false;

    }

}
