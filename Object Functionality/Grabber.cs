using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject grabbed;
    public bool holding;
    public float distance_to_screen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
            if( this.grabbed != null )
            {
                this.grabbed = null;
                this.holding = false;
            }else{
                RaycastHit hit; 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                if ( Physics.Raycast (ray,out hit,100.0f)) {
                    if( hit.collider.gameObject.layer == 6)
                    {
                        this.grabbed = hit.collider.gameObject;
                        Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                        this.holding = true;
                    }
                }
            }
        }
        if( this.holding )
            this.grabbed.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));

    }
}