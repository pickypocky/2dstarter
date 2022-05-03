using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keys=left,right,space

public class PlayerScript : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float playerWalkSpeed = 1;
    public float playerJumpVelocity = 3;

    public Transform restartPoint;

    public GameObject weaponPrefab;

    int floorLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("run", true);

        // player treats these layers as platforms
        floorLayerMask = 1 << LayerMask.NameToLayer("platform");
        floorLayerMask += 1 << LayerMask.NameToLayer("wall");

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("run", false);
        anim.SetBool("idle", false);
        anim.SetBool("jump", false);

        Move();
        Animate();
        Jump();
        ShootWeapon();

    }

    private void Move()
    {
        Vector3 vel = rb.velocity;

        vel.x = vel.x * 0.9f; // slow player down when not pressing left or right


        if (Input.GetKey("left"))
        {
            vel.x = -playerWalkSpeed;
        }

        if (Input.GetKey("right"))
        {
            vel.x = playerWalkSpeed;
        }

        rb.velocity = vel;
    }

    void Jump()
    {
        bool touchingFloor = CheckFloorCollision(transform.position.x, transform.position.y, 0.2f, 0.05f);

        if (Input.GetKeyDown("left alt") && (touchingFloor==true) )
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpVelocity );
        }
    }    

    void Animate()
    {
        if ( (rb.velocity.x > 0.1f) || (rb.velocity.x < -0.1f) )
        {
            anim.SetBool("run", true);

            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
        else
        {
            anim.SetBool("idle", true);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            print("player dead");
            transform.position = restartPoint.position;
        }
    }



    bool CheckFloorCollision(float x, float y, float width, float height)
    {
        // returns -1 (not hitting left side)
        // or 1 (not hitting on right side)
        // or 0 (collision on both sides)

        Vector2 lineStart, lineEnd;
        RaycastHit2D hit;


        // check for collision with floor

        lineStart.y = y;
        lineEnd.y = y-height;

        lineStart.x = x;
        lineEnd.x = x;
        hit = Physics2D.Linecast(lineStart, lineEnd, floorLayerMask);
        Debug.DrawLine(lineStart, lineEnd, hit ? Color.red : Color.green); // draw the coloured debug line

        if (hit == false)
        {
            lineStart.x = x - width;
            lineEnd.x = x - width;
            hit = Physics2D.Linecast(lineStart, lineEnd, floorLayerMask);
            Debug.DrawLine(lineStart, lineEnd, hit ? Color.red : Color.blue); // draw the coloured debug line

        }

        if (hit == false)
        {
            lineStart.x = x + width;
            lineEnd.x = x + width;
            hit = Physics2D.Linecast(lineStart, lineEnd, floorLayerMask);
            Debug.DrawLine(lineStart, lineEnd, hit ? Color.red : Color.yellow); // draw the coloured debug line

        }


        return hit;


    }

    void ShootWeapon()
    {
        Vector2 position;
        Rigidbody2D rb;

        position.x = transform.position.x;
        position.y = transform.position.y + 0.2f;

        if( Input.GetKeyDown("f") == true )
        {
            GameObject weapon = Instantiate(weaponPrefab, position, Quaternion.identity );
            rb = weapon.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2( -2,0 );

            if( sr.flipX == false )
            {
                rb.velocity = new Vector2( 2,0 );
            }
        }
    }




}
