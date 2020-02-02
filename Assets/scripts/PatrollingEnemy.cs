using UnityEngine;
using System.Collections;

public class PatrollingEnemy : EnemyBase {

    private Rigidbody2D body;

    private Vector2[] actions;
    private int action = -1;
    private int[] durations;
    private int duration = 0;
    private int count = 0;

    private float speed = 3.0f;
    public float jumpSpeed;
    public float jumpProb = 0.5f;
    private Vector2 leftSpeed;
    private Vector2 rightSpeed;

    // ground checking - add publics via drag and drop
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public GameObject feet;
    public float feetWidth;


    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D> ();

        leftSpeed = new Vector2(-speed, 0.0f);
        rightSpeed = new Vector2(speed, 0.0f);

        createMovementPattern();
    }

    private bool isGrounded ()
    {
        var feetRect = new Rect (feet.transform.position.x, feet.transform.position.y, feetWidth, 0.1f);
        return Physics2D.OverlapArea (feetRect.min, feetRect.max, whatIsGround);
    }

    private void Jump ()
    {
        if (isGrounded()) {
            body.velocity = jumpSpeed * Vector2.up;
        }
    }

    void createMovementPattern() {

        actions = new Vector2[8];
        durations = new int[8];
        actions[0] = rightSpeed;
        durations[0] = 20;
        actions[1] = Vector2.zero;
        durations[1] = 20;
        actions[2] = rightSpeed;
        durations[2] = 20;
        actions[3] = Vector2.zero;
        durations[3] = 40;

        actions[4] = leftSpeed;
        durations[4] = 20;
        actions[5] = Vector2.zero;
        durations[5] = 20;
        actions[6] = leftSpeed;
        durations[6] = 20;
        actions[7] = Vector2.zero;
        durations[7] = 40;

    }

    // Update is called once per frame
    void Update () {
        if (isRepaired)
        {
            //Debug.Log("PatrollingEnemy dead");
            return;
        }

        
        if (!isGrounded()) {
            count = 0;
            duration = 0;
            Vector2 current = body.velocity;
            body.velocity = new Vector2(0.0f, current.y);
        } else if (count == duration) {
            pickAction();
        } else {
            count++;
        }
    }

    void pickAction() {
        action++;
        if (action == actions.Length) {
            action = 0;
        }
        count = 0;

        // Random jump
        float randJump = Random.Range(0.0f, 1.0f);
        if(randJump < jumpProb)
        {
            Jump();
        }
        else
        {
            body.velocity = actions[action];
            duration = durations[action];
        }
    }
}
