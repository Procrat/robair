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
    private float laserDamage = 0.15f;
    private float enemyDamage = 0.25f;
    private float waterDamage = 0.35f;

    private Vector2 movementInput;
    private bool lockMovement;
    private bool canRepair;
    private GameObject enemy;

    private SpriteRenderer spriteRenderer;


    public void takeDamage(float damage)
    {
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
        if (other.tag == "Projectile") {
            //Debug.Log("Hit by laser)");
            takeDamage(laserDamage);
        }
        else if (other.tag == "Enemy")
        {
            enemy = other.gameObject;

            if (other.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                Debug.Log("Enemy caused damage");
                takeDamage(enemyDamage);
                canRepair = false;
            }
            else
            {
                Debug.Log("Can repair now");
                canRepair = isGrounded() ? true : false;
            }
        }
        else if (other.tag == "Water") {
            takeDamage(waterDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemy = null;
        canRepair = false;
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

            spriteRenderer.flipX = movementInput.x < 0 ? true : false;
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
        }
    }

    private void Die ()
    {
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

    private void OnRepair(InputValue value)
    {
        if (canRepair && enemy != null) StartCoroutine(HandleRepair());
        //animator.Play("repair-start");
        // TODO: actually do something
        // TODO: Don't forget to play "repair-end" animation afterwards
    }

    IEnumerator HandleRepair()
    {
        Debug.Log("HandleRepair");

        lockMovement = true;
        enemy.GetComponentInParent<EnemyBase>().RepairMe();

        animator.Play("repair-start");
        AudioManager.Instance.PlayHealthUpSound1();
        AudioManager.Instance.PlayDrillSoundMedium();
        yield return new WaitForSeconds(1);

        animator.Play("repair-end");
        lockMovement = false;
    }
}
