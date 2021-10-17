using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serializables
    [SerializeField]
    private float moveForce = 5f;
    // rigid-body stuff
    [SerializeField]
    private float[] linearDrag = new float[2] { 4f, 0 };
    [SerializeField]
    private float[] angularDrag = new float[2] { 0.05f, 0 };
    #endregion

    #region Publics
    // disallows user-input
    public bool impedeMovement = false;
    #endregion

    #region Protected
    protected float[] direction = new float[2] { 0, 0 }; // either -1, 1 or 0
    #endregion

    #region Components
    private Rigidbody2D myRigidbody;
    #endregion Components

    #region privates properties
    private Vector2 movementInput;
    private Vector2 moveVelocity;
    private Vector2 lastInput;
    private Vector2 lastForce;
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

    // check over this to make sure what I did makes sense - Red
    void FixedUpdate()
    {
        var forceToBeAdded = new Vector2();
        if (!impedeMovement) { lastInput = movementInput; }
        if (movementInput.x != 0)
        {
            // checks if user input is allowed
            forceToBeAdded.x = (impedeMovement is false ? movementInput.x : 1) * moveForce * (impedeMovement ? lastInput.x : 1);
        }

        if (movementInput.y != 0)
        {
            // checks if user input is allowed
            forceToBeAdded.y = (impedeMovement is false ? movementInput.y : 1) * moveForce * (impedeMovement ? lastInput.y : 1);
        }

        direction[0] = (movementInput.x > 0 ? 1 : movementInput.x < 0 ? -1 : 0);
        direction[1] = (movementInput.y > 0 ? 1 : movementInput.y < 0 ? -1 : 0);
        lastForce = (impedeMovement is false ? forceToBeAdded : lastForce);

        //myRigidbody.AddForce((impedeMovement is false ? forceToBeAdded : Vector2.zero));
        //myRigidbody.mass = (impedeMovement is false ? 1 : 0);
        //myRigidbody.AddForce(lastForce + new Vector2(1f * direction[0], 1f * direction[1]));
        myRigidbody.AddForce(lastForce);
        print(forceToBeAdded + " force");
        print(lastForce + " last force");
        print(myRigidbody.velocity + "velocity");
        myRigidbody.drag = (impedeMovement is false ? linearDrag[0] : linearDrag[1]);
        myRigidbody.angularDrag = (impedeMovement is false ? angularDrag[0] : angularDrag[1]);
    }
}
