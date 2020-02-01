using UnityEngine;
using System.Collections;

public class ShootingEnemy : MonoBehaviour {

    private Rigidbody2D body;

    private Vector2[] actions;

    public float jumpSpeed;
    public float jumpProb;

    // ground checking - add publics via drag and drop
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public GameObject feet;
    public float feetWidth;


    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D> ();
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

    // Update is called once per frame
    void Update () {
        Debug.Log(isGrounded());
        float randJump = Random.Range(0.0f, 1.0f);
        if(randJump < jumpProb)
        {
            Jump();
        }
    }
}
