using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( (collision.gameObject.tag != "Player") && (collision.gameObject.tag != "weapon") )
        {
            Destroy(gameObject);
        }
    }
}
