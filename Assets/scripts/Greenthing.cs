using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greenthing : MonoBehaviour
{
    Rigidbody2D rb;
    float xDirection;
    int layerMask;
    SpriteRenderer sr;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        

        // we only want to check collision with the platform layer
        layerMask = 1 << LayerMask.NameToLayer("platform");
        

        // set initial speed
        speed = 1.0f;
        xDirection = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
    }

    void Update()
    {
        // Make enemy face the direction he is moving
        FaceDirection();
    }


    void FaceDirection()
    {
        // set enemy to face the direction of movement
        if (rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    

    void MoveEnemy()
    {
        // change enemy direction if not touching a platform
        rb.velocity = new Vector2(xDirection, rb.velocity.y);

        int side = CheckCollision( transform.position.x, transform.position.y, 0.25f, 0.3f );

        

        if( side == -1 )
        {
            xDirection = speed;
        }

        if( side == 1 )
        {
            xDirection = -speed;
        }

    }


    int CheckCollision( float x, float y, float width, float height )
    {
        // returns -1 (not hitting left side)
        // or 1 (not hitting on right side)
        // or 0 (collision on both sides)
        
        Vector2 lineStart, lineEnd;
        RaycastHit2D hit;


        // check for no collision on left side
        lineStart.x = x-width;
        lineStart.y = y - height;

        lineEnd.x = x - width;
        lineEnd.y = y;
        
        hit = Physics2D.Linecast( lineStart,lineEnd, layerMask);
        Debug.DrawLine( lineStart,lineEnd, hit?Color.red:Color.green ); // draw the coloured debug line
        if( hit == false )
        {
            // not touching platform on left side of sprite
            return -1;
        }




        // check for no collision on right side
        lineStart.x = x + width;
        lineEnd.x = x + width;

        hit = Physics2D.Linecast( lineStart,lineEnd, layerMask);
        Debug.DrawLine( lineStart,lineEnd, hit?Color.red:Color.green );

        if( hit == false )
        {
            // not touching platform on right side of sprite
            return 1;
        }

        // Sprite is touching platform
        return 0;
    
    }    
}
