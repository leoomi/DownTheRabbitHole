using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serializables
    [SerializeField]
    private float moveSpeed = 5f;
    #endregion

    #region Components
    private Rigidbody2D myRigidbody;
    #endregion Components

    #region privates properties
    private Vector2 movementInput;
    private Vector2 moveVelocity;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(horizontalMovement, verticalMovement);
    }

    void FixedUpdate()
    {
        var forceToBeAdded = new Vector2();
        if (movementInput.x != 0)
        {
            forceToBeAdded.x = movementInput.x * moveSpeed;
        }

        if (movementInput.y != 0)
        {
            forceToBeAdded.y = movementInput.y * moveSpeed;
        }

        myRigidbody.AddForce(forceToBeAdded);
    }
}
