using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float feetWidth;

    public GameObject feet;
    public HealthBar healthBar;
    public LayerMask whatIsGround;
    public Player otherPlayer;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private Rigidbody2D body;
    private Animator animator;
    private float enemyDamage = 0.25f;
    private float waterDamage = 0.35f;

    private Vector2 movementInput;
    private bool lockMovement;
    private bool canRepair;
    private bool isRepairingFriend;
    private GameObject collidedObject;
    private bool lookingRight;

    private SpriteRenderer spriteRenderer;


    public void takeDamage(float damage)
    {
        AudioManager.Instance.PlayStabSoundHeavy();
        if (healthBar.GetHealth() <= 0) {
            return;
        }
        healthBar.subtractHealth(damage);
        otherPlayer.healthBar.subtractHealth(damage);
        if (healthBar.GetHealth() <= 0) {
            Die ();
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {

        // Using the tag method.
        if (other.tag == "Enemy")
        {
            collidedObject = other.gameObject;
            canRepair = true;

        }
        else if (other.tag == "Spike")
        {
            takeDamage(enemyDamage);
        }
        else if (other.tag == "PlayerRepairZone")
        {
            canRepair = true;
            isRepairingFriend = true;
        }
        else if (other.tag == "Water") {
            takeDamage(waterDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Using the tag method.
        if (other.tag == "Enemy")
        {
            canRepair = false;
            collidedObject = null;
        }
        else if (other.tag == "PlayerRepairZone")
        {
            canRepair = false;
            isRepairingFriend = false;
        }
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lookingRight = true;
    }

    void FixedUpdate ()
    {
        HandleInput ();
    }

    private void HandleInput ()
    {
        if (!lockMovement)
        {
            Move(new Vector2(movementInput.x, 0));

            if (movementInput.x != 0) {
                lookingRight = movementInput.x > 0;
            }
            spriteRenderer.flipX = !lookingRight;
        }

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
            animator.SetTrigger("JumpNow");


            //animator.SetTrigger("JumpEnd");


        }
    }

    private void Die ()
    {
        AudioManager.Instance.PlayHealthDownSound();
        animator.Play("death-start");
        AudioManager.Instance.PlayHealthDownSound();
        // Load start screen after delay
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay ()
    {
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene ("StartScene");
    }

    private bool isGrounded ()
    {
        var feetRect = new Rect (feet.transform.position.x, feet.transform.position.y, feetWidth, 0.1f);
        var feetCollisions = Physics2D.OverlapAreaAll (feetRect.min, feetRect.max, whatIsGround);
        return feetCollisions.Length > 1;  // The player itself is always colliding
    }

    // New Input System: OnMove message
    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        animator.SetBool("IsRunning", movementInput.magnitude != 0);
    }

    // New Input System: OnJump message
    private void OnJump(InputValue value)
    {
        Jump();
    }

    public void RepairHealth()
    {
        healthBar.repairHealth();
        otherPlayer.healthBar.repairHealth();
    }

    private void OnRepair(InputValue value)
    {
        if (canRepair) StartCoroutine(HandleRepair());
    }

    IEnumerator HandleRepair()
    {
        if (!isGrounded()) yield break;

        if (isRepairingFriend)
        {
            RepairHealth();
        } else if (collidedObject.transform.position.x < transform.position.x)
        {
            collidedObject.GetComponentInParent<EnemyBase>().RepairMe();
        } else {
            yield break;
        }

        lockMovement = true;
        animator.Play("repair-start");
        AudioManager.Instance.PlayHealthUpSound1();
        AudioManager.Instance.PlayDrillSoundMedium();
        yield return new WaitForSeconds(1);

        animator.Play("repair-end");
        lockMovement = false;
    }
}
