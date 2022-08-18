using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
*
*	This class adds the functionality of resistance in a conductor.
*	Comments update: 18 Aug 22
*
*/


public class Resistance : MonoBehaviour
{
    public List<GameObject> remoteConnections;
    bool debug;
    float hitDistance = 0.6f;
    bool isTouchingPosLead;

	public int feet;
	const float RESISTANCE_PER_THOUSAND_FEET = 1.588f;
	public float totalResistance;

/* I don't thin i need this here
    void FixedUpdate()
	{

		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);

			// Make sure it's not performing opperations on this object
		if( hitColliders.Length>1)
		{
				// Debug output
			if(debug)
				Debug.Log("Found: " + hitColliders.Length);

				// Itterate through everything touching this object to see if anything has potential
			foreach(Collider c in hitColliders)
			{
					// Debug output
				if( debug)
					Debug.Log("Checking: " + c.gameObject);


				if(c.gameObject.layer == 6)
					if(c.gameObject.GetComponent<MeterLead>() != null)
					{
                        if( c.gameObject.name == "PosLead" )
                        {
                            this.isTouchingPosLead = true;
						    break;
                        }
					}

			}
		}else{
				// This is to make sure something doesn't stay energized when it doesn't need to be
			this.isTouchingP	osLead = false;
		}
    } */


		// Depricated??
    public float findReistance(float res)
	{
			// Gets everythiing touching THIS item (immediate vacinity) for source
		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);
		foreach(Collider c in hitColliders)
		{
			if( this.gameObject.Equals(c.gameObject) )
            	continue;

				//if()
			//res += c.gameObject.GetComponent<Restiance>().findReistance();
		}

		res += (feet /1000.0f) * RESISTANCE_PER_THOUSAND_FEET;
		return res;
	}

	public float getReisitance()
	{
		return this.totalResistance;
	}


		// Recursively loop through all children touching the neg lead until the pos lead is found.
		// Uses a type of breadth first serch to get a que of all the objects that make up the 
		// shortest path so that can be itterated through and get the cumalitive resistance.
	public bool getResistanceReading(ref Queue<GameObject> path, GameObject previous)
	{

		bool found = false;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);
        foreach(Collider c in hitColliders)
        {
				// Layer 6 is for test equipment, which means the meter lead can be here
			if( c.gameObject.layer == 6 )
					// TODO: Change Cylinder(3) to something fucking usefull!
				if( c.gameObject.name == "Cylinder (3)")
				{
					if(debug)
						Debug.Log("I TOUCHED THE BUTT!!!\t\t" + c.gameObject.name);
					path.Enqueue(this.gameObject);
					return true;
				}
        }

        foreach(Collider c in hitColliders)
        {
			if(c.gameObject.Equals(this.gameObject) || c.gameObject.Equals(previous))
				continue;

			Resistance r = c.gameObject.GetComponent<Resistance>();
			if( r!= null)
            	found = r.getResistanceReading(ref path, this.gameObject);
			if(found)
			{
				path.Enqueue(this.gameObject);
				return true;
			}

        }

		return false;
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
