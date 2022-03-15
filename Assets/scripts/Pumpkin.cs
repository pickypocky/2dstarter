using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    Rigidbody2D rb;
    float xDirection = -1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.velocity = new Vector2(1,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = new Vector2(xDirection, rb.velocity.y);

    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "wall")
        {
            
            xDirection = -xDirection;
            print("hit wall");
            
        }
    }
}
