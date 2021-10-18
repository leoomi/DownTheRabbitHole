using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Serializables
    [SerializeField]
    private float moveForce = 5f;
    [Range(0.01f, 1f)]
    private float slowSpeedPercent = 0.5f;
    // rigid-body stuff
    [SerializeField]
    private float[] linearDrag = new float[3] { 16f, 4f, 0.05f };
    [SerializeField]
    private float[] angularDrag = new float[3] { 0.05f, 0.15f, 0.01f };
    #endregion

    #region Publics
    // disallows user-input
    public bool impedeMovement = false;
    [SerializeField]
    public bool slowMovement = false;
    public PlayerState state = PlayerState.Normal;
    #endregion

    #region Protected
    protected float[] direction = new float[2] { 0, 0 }; // either -1, 1 or 0
    #endregion

    #region Components
    private Rigidbody2D myRigidbody;
    private Animator animator;
    #endregion Components

    #region privates properties
    private Vector2 movementInput;
    private Vector2 moveVelocity;
    private Vector2 lastInput;
    private Vector2 lastForce;
    private Vector2 storedVelocityForTransition;
    private Vector3 storedPositionForTransition;
    #endregion

    public static PlayerController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
       switch (state)
       {
            case PlayerState.Normal:
                NormalFixedUpdate();
                break;
       }
    }

    private void NormalFixedUpdate()
    {
        // movement
        var forceToBeAdded = new Vector2();
        if (!impedeMovement) { lastInput = movementInput; }
        if (movementInput.x != 0)
        {
            // checks if user input is allowed
            forceToBeAdded.x = (impedeMovement is false ? movementInput.x : 1) * moveForce * (impedeMovement ? lastInput.x : 1) / (slowMovement ? slowSpeedPercent : 1);
        }

        if (movementInput.y != 0)
        {
            // checks if user input is allowed
            forceToBeAdded.y = (impedeMovement is false ? movementInput.y : 1) * moveForce * (impedeMovement ? lastInput.y : 1) / (slowMovement ? slowSpeedPercent : 1);
        }

        direction[0] = (movementInput.x > 0 ? 1 : movementInput.x < 0 ? -1 : 0);
        direction[1] = (movementInput.y > 0 ? 1 : movementInput.y < 0 ? -1 : 0);
        lastForce = (impedeMovement is false ? forceToBeAdded : lastForce);

        //myRigidbody.AddForce((impedeMovement is false ? forceToBeAdded : Vector2.zero));
        //myRigidbody.mass = (impedeMovement is false ? 1 : 0);
        //myRigidbody.AddForce(lastForce + new Vector2(1f * direction[0], 1f * direction[1]));
        myRigidbody.AddForce(lastForce);
        // print(forceToBeAdded + " force");
        // print(lastForce + " last force");
        // print(myRigidbody.velocity + "velocity");
        myRigidbody.drag = (impedeMovement is false ? linearDrag[1] : slowMovement is false ? linearDrag[2] : linearDrag[0]); // swap
        myRigidbody.angularDrag = (impedeMovement is false ? angularDrag[1] : slowMovement is false ? angularDrag[2] : angularDrag[0]);
    }

    private void OnSceneLoaded(Scene _, LoadSceneMode __)
    {
        state = PlayerState.Normal;
        animator.SetTrigger("Walk");
        RestoreVelocityPosition();
    }

    public void GoUp(Action loadSceneAction)
    {

    }

    public void GoDown(Action loadSceneAction, GameObject stairsGameObject)
    {
        state = PlayerState.Falling;
        animator.SetTrigger("Fall");
        StoreVelocityPosition();

        Vector3 velocityDirection = storedVelocityForTransition.normalized;
        var direction = (stairsGameObject.transform.position - transform.position).normalized;
        StartCoroutine(MoveCoroutine(transform.position, transform.position + direction, 1f, loadSceneAction));
    }

    IEnumerator MoveCoroutine(Vector3 beginPos, Vector3 endPos, float time, Action callback){
        for(float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(beginPos, endPos, t);
            yield return null;
        }
        callback();
    }

    private void StoreVelocityPosition()
    {
        storedVelocityForTransition = myRigidbody.velocity;
        storedPositionForTransition = transform.position;
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.isKinematic = true;
    }

    private void RestoreVelocityPosition()
    {
        myRigidbody.velocity = storedVelocityForTransition;
        transform.position = storedPositionForTransition;
        storedVelocityForTransition = Vector2.zero;
        myRigidbody.isKinematic = false;
    }
}

public enum PlayerState
{
    Normal,
    Falling
}