using UnityEngine;
using System.Collections;

public class ShootingEnemy : EnemyBase {

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
        animator = gameObject.GetComponent<Animator>();
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
        var spawnPoint = transform.Find("laserSpawnPoint");
        Rigidbody2D projectileInstance = Instantiate(projectile, spawnPoint) as Rigidbody2D;
        projectileInstance.velocity = Vector2.left * projectileSpeed;

        AudioManager.Instance.PlayLaserSound();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isRepaired)
        {
            //Debug.Log("ShootingEnemy dead");
            return;
        }

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
