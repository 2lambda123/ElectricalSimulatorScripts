using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public List<GameObject> children;
    public bool isLive;
    private bool lastState;

    public GameObject breakerSwitch;
    public AudioSource breakerFlipping;

    public Transform onTransform;
    public Transform offTransform;

    public Potential mypotential;

    // Start is called before the first frame update
    void Start()
    {
        //breakerFlipping = gameObject.GetComponent<AudioSource>();

        if(this.children == null)
        {
            this.children = new List<GameObject>();
            Debug.Log("Breaker children not initialized!");
        }

        foreach(GameObject o in children)
        {
            if( o.GetComponent<SubPotential>() == null )
            {
                o.AddComponent<SubPotential>().setPotential(this.mypotential, this.gameObject);
                o.GetComponent<SubPotential>().setAsSource();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLive != lastState)
            toggle();
    }

    void setbreakerOff()
    {
        this.breakerSwitch.transform.position = offTransform.position; // .eulerAngles = new Vector3( 90, -20, 0 ); //this.breakerSwitch.transform.rotation.x, this.breakerSwitch.transform.rotation.y , this.breakerSwitch.transform.rotation.z );
        this.breakerSwitch.transform.rotation = offTransform.rotation; // .eulerAngles = new Vector3( 90, -20, 0 ); //this.breakerSwitch.transform.rotation.x, this.breakerSwitch.transform.rotation.y , this.breakerSwitch.transform.rotation.z );
    }

    void setbreakerOn()
    {
        this.breakerSwitch.transform.position = onTransform.position; //eulerAngles = new Vector3( 90, 20, 0 ); //this.breakerSwitch.transform.rotation.x, this.breakerSwitch.transform.rotation.y, this.breakerSwitch.transform.rotation.z );
        this.breakerSwitch.transform.rotation = onTransform.rotation;
    }

    void OnMouseDown()
    {
        isLive = !isLive;
    }

    public Potential getPotential()
    {
        return this.mypotential;
    }

    void toggle()
    {
        if(isLive)
        {
            foreach(GameObject obj in children)
            {
                if( obj.GetComponent<SubPotential>() == null)
                {
                    SubPotential sp = obj.AddComponent<SubPotential>();
                    sp.setPotential(this.getPotential(), this.gameObject);
                    sp.setAsSource();
                }
                // Conductor c = obj.GetComponent<Conductor>();
                // if( c != null )
                // {
                //     c.setPotential(this.mypotential);
                // }
            }
            
            setbreakerOn();
            //breakerFlipping.Play(0);
        }else
        {
            foreach(GameObject obj in children)
            {
                if( obj.GetComponent<SubPotential>() != null)
                {
                    obj.GetComponent<SubPotential>().removeSelf();
                }
                // Conductor c = obj.GetComponent<Conductor>();
                // if( c != null )
                // {
                //     c.turnOff();
                // }
            }
            setbreakerOff();
            //breakerFlipping.Play(0);
        }
        lastState = !lastState;
    }
}
