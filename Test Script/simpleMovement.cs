using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    public GameObject [] children;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < children.Length; i++)
        {
            Rigidbody rb = children[i].GetComponents<Rigidbody>()[0];

            rb.velocity = new Vector3(0,1,0);
        }
    }
}
