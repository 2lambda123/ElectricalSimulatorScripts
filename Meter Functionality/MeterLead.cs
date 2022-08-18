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

    private bool debug;

    private char slectedFunction;


    // Start is called before the first frame update
    void Start()
    {
        potentialReading = new reading(' ', 0, 0);
    }
    
    void FixedUpdate()
    {
        switch(slectedFunction)
        {
            case 'v':
                voltMeter();
                break;
            case 'o':
                //ohmMeter();
                break;
            case 'r':
                getResistanceReading();
                break;
        }
    }

    public void setFunction(char newFunction)
    {
        this.slectedFunction = newFunction;
    }

    void voltMeter()
    {
            // Collision detection
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);

            // Itterating through imediate area
        foreach(Collider c in hitColliders)
                // Verify not checking self
            if( !c.gameObject.Equals(this.gameObject) )
            {
                if(debug)
                    Debug.Log("Meter is reading something");
                
                Potential p = c.gameObject.GetComponent<Potential>();
                Neutral n = c.gameObject.GetComponent<Neutral>();

                if(p!= null)
                {
                    this.potentialReading = new reading(p.getPhase(), p.getPotential(), p.getAmperage());

                    if(debug)
                    {
                        Debug.Log("Meter reading: " + this.potentialReading.toString());
                        Debug.Log("Meter reading2: " + p.getPotential().ToString());
                    }
                }else if(n!=null)
                {
                    this.potentialReading = new reading('n', 0, 0);
                }
                else
                    this.potentialReading = new reading(' ',0,0);
            }else{
                    this.potentialReading = new reading(' ',0,0);
            }
    }

    public bool ohmMeter()
    {
        bool found;

            // The posative lead is what is being searched for, so this would always return true if not removed
        if( this.gameObject.name == "PosLead")
            return false;

            // Collision detection
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);

            // Itterating through imediate area
        foreach(Collider col in hitColliders)
        {
                // Verify not checking self
            if( this.gameObject.Equals(col.gameObject))
                continue;
                
            Continuity con = col.gameObject.GetComponent<Continuity>();

            if(con != null)
            {
                found = con.findPosLead(this.gameObject);
                if( found )
                {
                    if(debug)
                        Debug.Log("HAS CONTINUITY!!!");
                    return true;
                }
            }
        }
        return false;
    }

    void getResistanceReading()
    {
            // The posative lead is what is being searched for, so this would always return true if not removed
        if( this.gameObject.name == "PosLead")
            return;

        bool found = false;
        Queue<GameObject> path = new Queue<GameObject>();

		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);
        
        foreach(Collider c in hitColliders)
        {
            if( c.Equals(this.gameObject) )
                continue;

            Resistance r = c.gameObject.GetComponent<Resistance>();
            if( r!= null)
                found = r.getResistanceReading(ref path, this.gameObject);
            if(found)
                break;
        }

        if(found)
        {
            if(debug)
                Debug.Log("GOT A FULL PATH!");
            float totalResistance = 0.0f;
            foreach(GameObject o in path)
            {
                if(debug)
                    Debug.Log(o.name);
                totalResistance += o.GetComponent<Resistance>().getReisitance();
            }
            if(debug)
                Debug.Log("END OF PATH\tRES: " + totalResistance);
        }
        else
            if(debug)
                Debug.Log("No path");
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