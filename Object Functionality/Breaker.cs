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

    // Start is called before the first frame update
    void Start()
    {
        breakerFlipping = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLive != lastState)
            toggle();
    }

    void setbreakerOff()
    {
        this.breakerSwitch.transform.eulerAngles = new Vector3(0, 0, -45);
    }

    void setbreakerOn()
    {
        this.breakerSwitch.transform.eulerAngles = new Vector3(0, 0, 45);
    }

    void toggle()
    {
        if(isLive)
        {
            Potential p = this.gameObject.GetComponent<Potential>();
            foreach(GameObject obj in children)
            {
                Potential childP = obj.GetComponent<Potential>();
                if( childP == null )
                {
                    Potential newP = obj.AddComponent<Potential>();
                    newP.setParams(false, p.getPhase(), p.getPotential());
                }
            }
            setbreakerOn();
            breakerFlipping.Play(0);
        }else
        {
            foreach(GameObject obj in children)
            {
                Potential childP = obj.GetComponent<Potential>();
                if( childP != null )
                {
			        obj.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    Destroy(childP);
                }
            }
            setbreakerOff();
            breakerFlipping.Play(0);
        }
        lastState = !lastState;
    }
}
