using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
*
*	This class adds the functionality of the meter tip 
*	Comments update: 18 Aug 22
*
*/


public class MeterLead : MonoBehaviour
{
    public Transform meterTip;
    public reading potentialReading;

    public bool debug;

    [SerializeField]
    private int slectedFunction;
    [SerializeField]
    private string myName;

    public List<GameObject> objectsTouching;


    // Start is called before the first frame update
    void Start()
    {
        potentialReading = new reading(' ', 0, 0);
        this.myName = this.gameObject.name;

        if( this.objectsTouching == null )
            objectsTouching = new List<GameObject>();
    }
    
    private void FixedUpdate()
    {
        switch( slectedFunction )
        {
            case 0:
                voltMeter();
                break;
            case 1:
                //ohmMeter();
                break;
            case 2:
                getResistanceReading();
                break;
        }
    }

    public void setFunction(int newFunction)
    {
        this.slectedFunction = newFunction;
    }

    void voltMeter()
    {
            // Collision detection
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.1f);

        if( hitColliders.Length < 1 )
        {
            this.potentialReading = new reading(' ',0,0);
            return;
        }

            // Itterating through imediate area
        foreach(Collider c in hitColliders)
                // Verify not checking self
            if( !c.gameObject.Equals(this.gameObject) )
            {
                if(debug)
                    Debug.Log("Meter is reading something");

                if( c.gameObject.layer != 7 )
                    continue;
                
                SubPotential p = c.gameObject.GetComponent<SubPotential>();

                if(p == null)
                {
                    this.potentialReading = new reading(' ', 0, 0);
                    continue;
                }

                // Debug.Log("Got new reading!");
                this.potentialReading = new reading(p.getPhase(), p.getMyPotential(), p.getAmperage());

                // if(debug)
                // {
                //     Debug.Log("Meter reading: " + this.potentialReading.toString());
                //     Debug.Log("Meter reading2: " + p.getPotential().ToString());
                // }
            
                // else
                //     this.potentialReading = new reading(' ',0,0);
            }else{
                this.potentialReading = new reading(' ',0,0);
            }
    }

    public bool ohmMeter()
    {
        bool found;
        Debug.Log("Using ohm meter to check " + objectsTouching.Count + " nodes");
        foreach(GameObject obj in objectsTouching)
        {
            Debug.Log("Checking for pos on " + obj.name);
            // if( obj.name == "Meter Lead Pos" )
            //     return true;

            WireTip wt = obj.GetComponent<WireTip>();
            if( wt == null )
                continue;

            Conductor con = wt.getParentConductor();
            if( con == null )
                continue;

            found = con.findPosLead(this.gameObject );
            if( found )
                return true;
        }
        return false;

            // Collision detection
		// Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.5f);

        //     // Itterating through imediate area
        // foreach(Collider col in hitColliders)
        // {
        //     if( col.gameObject.layer != 7 && col.gameObject.layer != 6 )
        //         continue;
                
        //     if( col.gameObject.GetComponent<MeterLead>() != null )
        //         return true;

        //     WireTip wt = col.GetComponent<WireTip>();
        //     found = wt.findPosLead();
        //     if( found )
        //     {
        //         // if(debug)
        //             Debug.Log("HAS CONTINUITY!!!");
        //         return true;
        //     }
        // }
        return false;
    }

    public float getResistanceReading()
    {
            // The posative lead is what is being searched for, so this would always return true if not removed
        if( this.gameObject.name == "PosLead")
            return -1.0f;;

        bool found = false;
        Queue<GameObject> path = new Queue<GameObject>();
        float totalResistance = 0.0f;

        foreach(GameObject obj in objectsTouching)
        {
            if( obj.Equals(this.gameObject) )
                continue;

            WireTip wt = obj.GetComponent<WireTip>();
            if( wt == null )
                continue;

            found = wt.getParentConductor().getReisitance(ref path, this.gameObject);
            if( found )
            {
                // Do que calculations
                Debug.Log("FOUND RESISTANCE");
                foreach(GameObject o in path)
                {
                    if(debug)
                        Debug.Log(o.name);
                    totalResistance += o.GetComponent<Conductor>().getWireResistance();
                }
                return totalResistance;
            }
            if(found)
                break;
        }
        return -1.0f;
        // Que Shit
        // if(found)
        // {
        //     if(debug)
        //         Debug.Log("GOT A FULL PATH!");
        //     float totalResistance = 0.0f;
        //     foreach(GameObject o in path)
        //     {
        //         if(debug)
        //             Debug.Log(o.name);
        //         totalResistance += o.GetComponent<Resistance>().getReisitance();
        //     }
        //     if(debug)
        //         Debug.Log("END OF PATH\tRES: " + totalResistance);
        // }
        // else
        //     if(debug)
        //         Debug.Log("No path");
    }

	void OnTriggerEnter(Collider c)
	{		
		if(c.gameObject.layer < 6 || c.gameObject.layer > 7 )
			return;

        if( !objectsTouching.Contains(c.gameObject) )
		    objectsTouching.Add(c.gameObject);
	}

	void OnTriggerStay(Collider c)
	{
		if(c.gameObject.layer < 6 || c.gameObject.layer > 7 )
			return;

        if(!objectsTouching.Contains(c.gameObject))
            objectsTouching.Add(c.gameObject);
	}

	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.layer < 6 || c.gameObject.layer > 7 )
			return;
            
		objectsTouching.Remove(c.gameObject);
	}

    public reading getReading()
    {
        return this.potentialReading;
    }

	// override object.Equals
	public bool Equals(GameObject obj)
	{
        if (obj == null || GetType() != obj.GetType())
			return false;

		if( this.gameObject.name != obj.name )
			return false;

		if( this.gameObject.transform != obj.transform )
			return false;

		//throw new System.NotImplementedException();
		return true;
	}
}