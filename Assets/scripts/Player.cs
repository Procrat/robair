using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float feetWidth;

    public GameObject feet;
    public HealthBar healthBar;
    public LayerMask whatIsGround;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private Rigidbody2D body;
    private float laserDamage = 0.15f;
    private float enemyDamage = 0.25f;

    private Vector2 movementInput;


    public void takeDamage(float damage)
    {
        healthBar.subtractHealth(damage);
    }

    private void OnTriggerEnter2D (Collider2D other) {
     // Using the tag method.
     if (other.tag == "Projectile") {
         takeDamage(laserDamage);
     }
     else if(other.tag == "Enemy")
     {
         takeDamage(enemyDamage);
     }
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate ()
    {
        HandleInput ();
    }

    private void HandleInput ()
    {
        Move(new Vector2(movementInput.x, 0));

        //Move(PlayerInput.actions["Move"].ReadValue<Vector2>());

        //if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Q)) {
        //    Debug.Log ("Quitting.");
        //    Application.Quit ();
        //}


        //if (Input.GetKey (right)) {
        //    MoveRight ();
        //} else if (Input.GetKey (left)) {
        //    MoveLeft ();
        //}

        //if (Input.GetKey (jump)) {
        //    Jump ();
        //}
    }

    //private void MoveLeft ()
    //{
    //    Move (Vector2.left);
    //}

    //private void MoveRight ()
    //{
    //    Move (Vector2.right);
    //}

    private void Move (Vector2 translationVector)
    {
        transform.Translate (moveSpeed * translationVector);
    }

    private void Jump ()
    {
        if (isGrounded ()) {
            body.velocity = jumpSpeed * Vector2.up;
        }
    }

    private bool isGrounded ()
    {
        var feetRect = new Rect (feet.transform.position.x, feet.transform.position.y, feetWidth, 0.1f);
        return Physics2D.OverlapArea (feetRect.min, feetRect.max, whatIsGround);
    }

    // New Input System: OnMove message
    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    // New Input System: OnJump message
    private void OnJump(InputValue value)
    {
        Jump();
    }
}
