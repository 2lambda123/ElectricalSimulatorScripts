using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject lineSideNode;
    public GameObject loadSideNode;

    public List<GameObject> lineSideNodeList;
    public List<GameObject> loadSideNodeList;


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

        // if( Input.GetMouseButtonUp(0))
        // {
        //     this.isOn = !this.isOn;
        // }
        lineSideNodeList = lineSideNode.GetComponent<InteractionNode>().getTouching();
        loadSideNodeList = loadSideNode.GetComponent<InteractionNode>().getTouching();


        if( loadSideNode.GetComponent<SubPotential>() ==  null)
            this.lineSideHot = false;
        else
            this.lineSideHot = true;

        if(isOn)
        {
            energizeLoad();
            // makeConnections();
        }else{
            deenergizeLoad();
        }

        // if( isOn != lastState )
        // {
        //     if(isOn)
        //     {
        //         this.switchLever.transform.position = onTransform.position;
        //         this.switchLever.transform.rotation = onTransform.rotation; // eulerAngles = new Vector3(this.switchLever.transform.rotation.x-45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
        //     }
        //     else
        //     {
        //         this.switchLever.transform.position = offTransform.position;
        //         this.switchLever.transform.rotation = offTransform.rotation; //eulerAngles = new Vector3(this.switchLever.transform.rotation.x+45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
        //     }

        //     lastState = isOn;
        //     //switchFlipping.Play(0);
        // }
    }

    void energizeLoad()
    {
        SubPotential sp = loadSideNode.GetComponent<SubPotential>();
        if( sp == null )
        {
            sp = loadSideNode.AddComponent<SubPotential>();

            sp.setPotential(lineSideNode.GetComponent<InteractionNode>().getPotential(), this.gameObject);
            sp.setAsSource();
        }
    }

    public void toggle()
    {
        this.isOn = !isOn;
    }

    void deenergizeLoad()
    {
        SubPotential sp = loadSideNode.GetComponent<SubPotential>();
        if( sp != null )
            sp.removeSelf();
    }

    public Potential getLinePotential()
    {
        return this.lineSideNode.GetComponent<InteractionNode>().getPotential();
    }

    void switchFunction()
    {


        //     // Do nothing if there are no line and load side objects
        // if(lineSide == null || loadSide == null)
        // {
        //     if(isOn)
        //     {
        //         this.switchLever.transform.eulerAngles = new Vector3(this.switchLever.transform.rotation.x+45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
        //     }
        //     else
        //     {
        //         this.switchLever.transform.eulerAngles = new Vector3(this.switchLever.transform.rotation.x-45, this.switchLever.transform.rotation.y, this.switchLever.transform.rotation.z);
        //     }

        //     //switchFlipping.Play(0);
        //     lastState = !lastState;
        //     return;
        // }

        // Potential p = lineSide.GetComponent<Potential>();
        // Neutral n = lineSide.GetComponent<Neutral>();


    //     if( isOn )
    //     {
    //         if( p != null )
    //         {
    //             if( loadSide.GetComponent<Potential>() == null)
    //             {
    //                 Potential p2 = loadSide.AddComponent<Potential>();
    //                 p2.setParams(false, p.getPhase(), p.getPotential());
    //                 p2.addChild(lineSide);
    //                 p2.setAsRemoteConnection();
    //             }else
    //             {
    //                 Debug.Log("Load side is already hot!");
    //             }
    //         }/*else{

    //             Potential p2 = loadSide.GetComponent<Potential>();
    //             p2.removeChild(lineSide);
    //             Destroy( p2);
    //         }*/

    //         //this.switchLever.transform.eulerAngles = new Vector3(0, 0, 45);
    //     }
    //     else
    //     {
    //         //this.switchLever.transform.eulerAngles = new Vector3(0, 0, -45);
    //         Potential p2 = loadSide.GetComponent<Potential>();
    //         if(p2 != null)
    //         {
    //             Debug.Log("IM SPAZZING!");
    //             p2.setAsInactive();
    //             //Destroy(p2);
    //         }
    //         // TODO: Add a way for items to check back for source
    //         //loadSide.AddComponent<Neutral>();
            
    //     }
    //     lastState = isOn;
    //         switchFlipping.Play(0);
    }

    // private void makeConnections()
    // {
    //     Amperage aIn = lineSide.GetComponent<Amperage>();
    //     Amperage aOut = lineSide.GetComponent<Amperage>();

    //     if( aIn != null )
    //         if( aOut != null )
    //         {
    //             aIn.addRemoteConnection(loadSide);
    //             aOut.addRemoteConnection(lineSide);
    //         }

    // }

}
