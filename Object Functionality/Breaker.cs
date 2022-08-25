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
            if( o.GetComponent<Potential>() == null )
            {
                Potential p = o.AddComponent<Potential>();
                p.setParams(false, mypotential.getPhase(), mypotential.getPotential());
                p.setAsRemoteConnection();
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

    void toggle()
    {
        if(isLive)
        {
            Potential p = this.gameObject.GetComponent<Potential>();
            if(p != null)
                foreach(GameObject obj in children)
                {
                    Potential childP = obj.GetComponent<Potential>();
                    if( childP == null )
                    {
                        Potential newP = obj.AddComponent<Potential>();
                        newP.setParams(true, p.getPhase(), p.getPotential());
                        newP.setAsRemoteConnection();
                    }
                }
            else
                Debug.Log("POTENTIAL IS NULL");
            setbreakerOn();
            //breakerFlipping.Play(0);
        }else
        {
            foreach(GameObject obj in children)
            {
                if(obj == this.gameObject)
                    continue;
                    
                Potential childP = obj.GetComponent<Potential>();
                if( childP != null )
                {
                    Renderer r = obj.GetComponent<Renderer>();
                    if( r!= null )
                        r.material.SetColor("_Color", Color.white);
                    Destroy(childP);
                }
            }
            setbreakerOff();
            //breakerFlipping.Play(0);
        }
        lastState = !lastState;
    }
}
