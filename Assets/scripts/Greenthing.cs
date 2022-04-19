using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greenthing : MonoBehaviour
{
    Rigidbody2D rb;
    float xDirection = -1;
    public int layerMask;
    SpriteRenderer sr;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        rb.velocity = new Vector2(1,0);

        layerMask = 1 << LayerMask.NameToLayer("wall");
        layerMask += 1 << LayerMask.NameToLayer("enemy");

        speed = 1.3f;
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
        rb.velocity = new Vector2(xDirection, rb.velocity.y);

        int side = CheckCollision( transform.position.x, transform.position.y, 0.25f, 0.3f );

        //print("side=" + side);

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
        // returns -1 (hit on left side)
        // or 1 (hit on right side)
        // or 0 (no collision)
        
        Vector2 lineStart, lineEnd;
        RaycastHit2D hit;
        
        
        lineStart.x = x-width;
        lineStart.y = y+height;
        lineEnd.x = x-width;
        lineEnd.y = y;
        
        // check for left side
        hit = Physics2D.Linecast( lineStart,lineEnd, layerMask);
        Debug.DrawLine( lineStart,lineEnd, hit?Color.red:Color.white );
        if( hit )
        {
            return -1;
        }

        // check right side
        lineStart.x = x + width;
        lineEnd.x = x + width;

        hit = Physics2D.Linecast( lineStart,lineEnd, layerMask);
        Debug.DrawLine( lineStart,lineEnd, hit?Color.red:Color.white );

        if( hit == true )
        {
            return 1;
        }
        return 0;
    
    }    
}
