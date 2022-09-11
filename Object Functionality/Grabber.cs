using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject grabbed;
    public bool holding;
    public float distance_to_screen;
    public float dist;
    public List<GameObject> otherTouch;
    public float multiplier;
    public Vector3 otherV;
    public Vector3 posInWorld;
    public Vector3 output;
    private bool snap;
    // Start is called before the first frame update
    void Start()
    {
        if( multiplier <= 0)
            multiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
            if( this.grabbed != null )
            {
                this.holding = false;
            }else{
                RaycastHit hit; 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                if ( Physics.Raycast (ray,out hit,100.0f)) {
                    if( hit.collider.gameObject.layer == 6)
                    {
                        this.grabbed = hit.collider.gameObject;
                        // Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                        this.holding = true;
                    }else if(hit.collider.gameObject.layer == 7)
                    {
                        this.grabbed = hit.collider.gameObject;
                        // Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                        this.holding = true;

                        otherTouch = hit.collider.gameObject.GetComponent<WireTip>().getConnections();
                    }
                }
            }
        }
        if( this.holding )
        {
            // this.grabbed.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
            float distance = 0.0f;
            // Debug.Log("Touch Count: " + otherTouch.Count);
            posInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
            if( otherTouch.Count > 0 )
            {
                otherV = otherTouch.ToArray()[0].transform.position;
                if( Vector3.Distance(posInWorld, otherV) < 0.75f )
                {
                    distance = Vector3.Distance (grabbed.transform.position, otherV);
                    Vector3 df = this.grabbed.transform.position - posInWorld;
                    df.z = posInWorld.z;
                    //this.grabbed.transform.position = new Vector3((((posInWorld.x+otherV.x)/2)*(1-distance)),(((posInWorld.y+otherV.y)/2)*(1-distance)),posInWorld.z);
                    this.grabbed.transform.position = otherV - df*0.4f; //new Vector3(output.x, output.y, posInWorld.z);
                    // TODO: otherTouch.ToArray()[0].GetComponent<InteractionNode>().scale(distance);
                }else
                    this.grabbed.transform.position = posInWorld;
            }else
            {
                this.grabbed.transform.position = posInWorld;
                // TODO:  otherTouch.ToArray()[0].GetComponent<InteractionNode>().resetScale();
            }
           // posInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
        }else{
            if(  this.grabbed != null)
            {
                Vector3 x = this.grabbed.transform.position;
                Debug.Log("Distance!: " + Vector3.Distance(x, otherV));
                if( Vector3.Distance(x, otherV) <= 1 )
                    this.grabbed.transform.position = otherV;
                this.grabbed = null;
            }
            // this.grabbed=null;
            // this.otherV = new Vector3(0,0,0);
            // this.otherTouch = null;
        }

    }
}