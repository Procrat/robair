using UnityEngine;
using System.Collections;

public class ShootingEnemy : MonoBehaviour {

    private Rigidbody2D body;
    public Rigidbody2D projectile;

    public Transform Launcher;

    private Vector2[] actions;

    public float projectileInterval;
    private float projectileTime;
    public Vector2 projectileSpeed;

    public float jumpSpeed;
    public float jumpProb;

    // ground checking - add publics via drag and drop
    public LayerMask whatIsGround;
    public GameObject feet;
    public float feetWidth;


    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D> ();
    }

    private bool isGrounded()
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

    private void Shoot()
    {
        Rigidbody2D projectileInstance;
        projectileInstance = Instantiate(projectile, Launcher.position, Launcher.rotation) as Rigidbody2D;
        projectileInstance.velocity = Vector2.left * projectileSpeed;

        AudioManager.Instance.PlayLaserSound();
    }

    // Update is called once per frame
    void FixedUpdate () {
        float randJump = Random.Range(0.0f, 1.0f);
        if(randJump < jumpProb)
        {
            Jump();
        }
        projectileTime += Time.fixedDeltaTime;
        if(projectileTime > projectileInterval)
        {
            projectileTime = 0;
            Shoot();
        }
    }


}
