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
    
    public bool lineSideHot;
    public bool loadSideHot;


    public Transform onTransform;
    public Transform offTransform;

    // Start is called before the first frame update
    void Start()
    {
        //switchFlipping = gameObject.GetComponent<AudioSource>();
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        Potential pIn = lineSide.GetComponent<Potential>();
        Potential pOut = loadSide.GetComponent<Potential>();

            // Line side checks
        if( pIn == null )
            lineSideHot = false;
        else
            lineSideHot = true;

            // Load side checks
        if( pOut == null )
            loadSideHot = false;
        else
            loadSideHot = true;

        if(isOn)
        {
            if(lineSideHot && !loadSideHot)
                energizeLoad(pIn);
            if(loadSideHot && !lineSideHot)
                deenergizeLoad();
            
            makeConnections();
        }else{
            if(pOut)
                pOut.setAsInactive();
        }

        if( isOn != lastState )
        {
            if(isOn)
            {
                this.switchLever.transform.position = onTransform.position;
                this.switchLever.transform.rotation = onTransform.rotation; // eulerAngles = new Vector3(this.switchLever.transform.rotation.x-45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
            }
            else
            {
                this.switchLever.transform.position = offTransform.position;
                this.switchLever.transform.rotation = offTransform.rotation; //eulerAngles = new Vector3(this.switchLever.transform.rotation.x+45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
            }

            lastState = isOn;
            //switchFlipping.Play(0);
        }
    }


    void OnMouseDown()
    {
        isOn = !isOn;
    }

    void energizeLoad(Potential pIn)
    {
        Potential p = loadSide.AddComponent<Potential>();
        p.setParams(false, pIn.getPhase(), pIn.getPotential());
        p.addChild(lineSide);
        p.setAsRemoteConnection();
    }

    void deenergizeLoad()
    {
        loadSide.GetComponent<Potential>().setAsInactive();
    }

    void switchFunction()
    {


            // Do nothing if there are no line and load side objects
        if(lineSide == null || loadSide == null)
        {
            if(isOn)
            {
                this.switchLever.transform.eulerAngles = new Vector3(this.switchLever.transform.rotation.x+45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
            }
            else
            {
                this.switchLever.transform.eulerAngles = new Vector3(this.switchLever.transform.rotation.x-45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
            }

            //switchFlipping.Play(0);
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
                }else
                {
                    Debug.Log("Load side is already hot!");
                }
            }/*else{

                Potential p2 = loadSide.GetComponent<Potential>();
                p2.removeChild(lineSide);
                Destroy( p2);
            }*/

            //this.switchLever.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            //this.switchLever.transform.eulerAngles = new Vector3(0, 0, -45);
            Potential p2 = loadSide.GetComponent<Potential>();
            if(p2 != null)
            {
                Debug.Log("IM SPAZZING!");
                p2.setAsInactive();
                //Destroy(p2);
            }
            // TODO: Add a way for items to check back for source
            //loadSide.AddComponent<Neutral>();
            
        }
        lastState = isOn;
            switchFlipping.Play(0);
    }

    private void makeConnections()
    {
        Amperage aIn = lineSide.GetComponent<Amperage>();
        Amperage aOut = lineSide.GetComponent<Amperage>();

        if( aIn != null )
            if( aOut != null )
            {
                aIn.addRemoteConnection(loadSide);
                aOut.addRemoteConnection(lineSide);
            }

    }

}
