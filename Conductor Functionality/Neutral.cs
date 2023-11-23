using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
*
*	This class adds the functionality of a neutral.
*	Comments update: 18 Aug 22
*
*/


public class Neutral : MonoBehaviour
{
		// A source cannot be turned off.
    public bool isSource;

		// Radious of the Sphere collider detection
    int hitDistance;


    bool isTouchingSource;
    bool isRemoteConnection;

    public List<GameObject> children = new List<GameObject>();

		// Debug output
    bool debug;

	public Potential potential;

	void Start()
	{
		if( this.potential == null )
			this.potential = this.gameObject.GetComponent<Potential>();
		
		// if( this.potential == null )
		// 	Debug.Log("Looks like " + gameObject.name + " needs potential!");
	}



    void Update()
    {
            // Set children as neutrals
        foreach(GameObject obj in children)
        {
			InteractionNode irnode = obj.GetComponent<InteractionNode>();
			if( irnode == null )
				continue;


            SubPotential sp = obj.AddComponent<SubPotential>();
			sp.setPotential(this.potential, this.gameObject);
			sp.setAsSource();
        }
    }

	

	// 	// Where the update happends to check the sphere overlapping
	// private void FixedUpdate()
	// {
	// 		// Set to false initially. Otherwise the objects may not detect that it lost connection
	// 	this.isTouchingSource = false;

	// 	Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);

	// 		// Make sure it's not performing opperations on this object
	// 	if( hitColliders.Length>1)
	// 	{
	// 			// Debug output
	// 		if(debug)
	// 			Debug.Log("Found: " + hitColliders.Length);

	// 			// Itterate through everything touching this object to see if anything has potential
	// 		foreach(Collider c in hitColliders)
	// 		{
	// 				// Olverlap sphere will detect self.
	// 			if( c.gameObject.Equals(this.gameObject))
	// 				continue;

	// 				// Debug output
	// 			if( debug)
	// 				Debug.Log("Checking: " + c.gameObject);

	// 				// Make sure not wasting opperations
	// 			if(c.gameObject.GetComponent<Neutral>() == null)
	// 			{
	// 					// Equipment is on layer 6. This is to prevent items from trying to energize equipment
	// 				if(c.gameObject.layer != 6)
	// 				{
	// 					Neutral p = c.gameObject.AddComponent<Neutral>();
	// 						// Debug output
	// 					if( debug )
	// 						Debug.Log(c.gameObject + " does not have Potential!");
	// 				}
	// 			}

	// 				// Layer 6 is meter equipment
	// 			if(c.gameObject.layer != 6)
	// 				if(c.gameObject.GetComponent<Neutral>().findSource(this.gameObject))
	// 				{
	// 					this.isTouchingSource = true;
	// 					break;
	// 				}

	// 		}
	// 	}else{
	// 			// This is to make sure something doesn't stay energized when it doesn't need to be
	// 		this.isTouchingSource = false;
	// 	}

	// 		// Last call in function. Destroyes potential if not there
	// 	if(!isTouchingSource && !isRemoteConnection)
	// 	{
	// 		if(isSource)
	// 			return;
	// 		Destroy(this.gameObject.GetComponent<Neutral>());
	// 		this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
	// 	}
	// }



	// 	// Recursive function to check if this object has a connection to an electrical source
	// public bool findSource(GameObject initiated)
	// {
	// 		// If this IS a source then it obviously can end here
	// 	if( this.isSource )
	// 		return true;

	// 		// Gets everythiing touching THIS item (immediate vacinity) for source
	// 	Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);
	// 	foreach(Collider c in hitColliders)
	// 	{
	// 			// Olverlap sphere will detect self.
	// 		if( c.gameObject.Equals(this.gameObject) || c.gameObject.Equals(initiated))
	// 			continue;
	
	// 		Neutral n = c.gameObject.GetComponent<Neutral>();
	// 		if(n != null)
	// 			if( n.isSource )
	// 				return true;
	// 	}

	// 		// Recursively check for the source
	// 	foreach(Collider c in hitColliders)
	// 	{
	// 			// Olverlap sphere will detect self.
	// 		if( c.gameObject.Equals(this.gameObject) || c.gameObject.Equals(initiated))
	// 			continue;

	// 		Neutral on = c.gameObject.GetComponent<Neutral>();
	// 		if( on != null )
	// 			if( on.findSource(this.gameObject))
	// 				return true;
	// 	}

	// 	return false;

	// }



	// override object.Equals
	public bool Equals(GameObject obj)
	{
		//
		// See the full list of guidelines at
		//   http://go.microsoft.com/fwlink/?LinkID=85237
		// and also the guidance for operator== at
		//   http://go.microsoft.com/fwlink/?LinkId=85238
		//
		
		if (obj == null || GetType() != obj.GetType())
			return false;

		if( this.gameObject.name != obj.name )
			return false;

		if( this.gameObject.transform != obj.transform )
			return false;

		
		// TODO: write your implementation of Equals() here
		//throw new System.NotImplementedException();
		return true;
	}
}
