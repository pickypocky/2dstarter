using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraScript : MonoBehaviour
{


    public Transform target;
    public Vector3 target_Offset;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + target_Offset, 0.05f);
        }

    }
}
