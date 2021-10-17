using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serializables
    [SerializeField]
    private float moveForce = 5f;
    #endregion

    #region Components
    private Rigidbody2D myRigidbody;
    #endregion Components

    #region privates properties
    private Vector2 movementInput;
    private Vector2 moveVelocity;
    #endregion

    public static bool isSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        isSpawned = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
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
            forceToBeAdded.x = movementInput.x * moveForce;
        }

        if (movementInput.y != 0)
        {
            forceToBeAdded.y = movementInput.y * moveForce;
        }

        myRigidbody.AddForce(forceToBeAdded);
    }
}
