using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
*
*	This class adds the functionality of continuity between conductors
*	Comments update: 18 Aug 22
*
*/


public class Continuity : MonoBehaviour
{
    public List<GameObject> remoteConnections;
    bool debug;
    float hitDistance = 0.6f;

		// Depricated??
    bool isTouchingPosLead;



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
				if( c.gameObject.Equals(this.gameObject ))
					continue;

					// Debug output
				if( debug)
					Debug.Log("Checking: " + c.gameObject);


				if(c.gameObject.layer == 6)
					if(c.gameObject.GetComponent<MeterLead>() != null)
					{
                        if( c.gameObject.name == "PosLead" )
                        {
							// Depricated??
                            this.isTouchingPosLead = true;
						    break;
                        }
					}

			}
		}else{
				// This is to make sure something doesn't stay energized when it doesn't need to be
				// Depricated??
			this.isTouchingPosLead = false;
		}
    }

    public bool findPosLead(GameObject initiated)
	{
			// Gets everythiing touching THIS item (immediate vacinity) for source
		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);
		foreach(Collider c in hitColliders)
		{
			if( this.gameObject.Equals(c.gameObject) || initiated.Equals(c.gameObject) )
                continue;

            if( c.gameObject.layer == 6 )
                if( c.gameObject.name == "PosLead")
                    return true;
		}

			// Recursively check for the source
		foreach(Collider c in hitColliders)
		{
			if( this.gameObject.Equals(c.gameObject) || initiated.Equals(c.gameObject) )
                continue;
			
            Continuity con = c.gameObject.GetComponent<Continuity>();
            if( con != null )
                if( con.findPosLead(this.gameObject))
                    return true;
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
