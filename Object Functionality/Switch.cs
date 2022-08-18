using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject lineSide;
    public GameObject loadSide;


    public GameObject switchLever;
    public AudioSource switchFlipping;

    public bool isOn;
    bool lastState;
    
    // Start is called before the first frame update
    void Start()
    {
        switchFlipping = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn != lastState)
            switchFunction();
    }

    void switchFunction()
    {

        if(lineSide == null || loadSide == null)
        {
            if(isOn)
            {
                this.switchLever.transform.eulerAngles = new Vector3(45, 0, 0);
            }
            else
            {
                this.switchLever.transform.eulerAngles = new Vector3(-45, 0, 0);
            }

            switchFlipping.Play(0);
            lastState = !lastState;
            return;
        }
        Potential p = lineSide.GetComponent<Potential>();
        Neutral n = lineSide.GetComponent<Neutral>();

        if( isOn )
        {
            if( p != null )
            {
                if( loadSide.GetComponent<Potential>() == null)
                {
                    Potential p2 = loadSide.AddComponent<Potential>();
                    p2.setParams(false, p.getPhase(), p.getPotential());
                    p2.addChild(lineSide);
                    p2.setAsRemoteConnection();
                }
            }/*else{

                Potential p2 = loadSide.GetComponent<Potential>();
                p2.removeChild(lineSide);
                Destroy( p2);
            }*/

            this.switchLever.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            this.switchLever.transform.eulerAngles = new Vector3(0, 0, -45);
            Potential p2 = loadSide.GetComponent<Potential>();
            if(p2 != null)
            {
                Debug.Log("IM SPAZZING!");
                Destroy(p2);
            }
            // TODO: Add a way for items to check back for source
            //loadSide.AddComponent<Neutral>();
            
        }
        lastState = !lastState;
    }

}
