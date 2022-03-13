using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float playerWalkSpeed = 1;
    public float playerJumpVelocity = 3;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("run", true);

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

    }

    private void Move()
    {
        Vector3 vel = rb.velocity;

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

    void Animate()
    {
        print(rb.velocity.x);
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

    void Jump()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpVelocity );
        }
    }

}
