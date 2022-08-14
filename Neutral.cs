using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral : MonoBehaviour
{
    public bool isSource;

    int hitDistance;
    bool isTouchingSource;
    bool isRemoteConnection;

    public List<GameObject> children = new List<GameObject>();

    bool debug;

    // Start is called before the first frame update
    void Start()
    {
            // Set children as neutrals
        foreach(GameObject obj in children)
        {
            Potential p = obj.GetComponent<Potential>();
            Neutral n = obj.GetComponent<Neutral>();

                // Make sure part is not energized
            if( p == null && n == null )
                obj.AddComponent<Neutral>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	

		// Where the update happends to check the sphere overlapping
	private void FixedUpdate()
	{
			// Set to false initially. Otherwise the objects may not detect that it lost connection
		this.isTouchingSource = false;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);

			// Make sure it's not performing opperations on this object
		if( hitColliders.Length>1)
		{
				// Debug output
			if(debug)
				Debug.Log("Found: " + hitColliders.Length);

				// Itterate through everything touching this object to see if anything has potential
			for( int i = 0; i < hitColliders.Length; i++)
			{
					// Debug output
				if( debug)
					Debug.Log("Checking: " + hitColliders[i].gameObject);

					// Make sure not wasting opperations
				if(hitColliders[i].gameObject.GetComponent<Neutral>() == null)
				{
						// Equipment is on layer 6. This is to prevent items from trying to energize equipment
					if(hitColliders[i].gameObject.layer != 6)
					{
						Neutral p = hitColliders[i].gameObject.AddComponent<Neutral>();
							// Debug output
						if( debug )
							Debug.Log(hitColliders[i].gameObject + " does not have Potential!");
					}
				}

				if(hitColliders[i].gameObject.layer != 6)
					if(hitColliders[i].gameObject.GetComponent<Neutral>().findSource(this.gameObject))
						this.isTouchingSource = true;

			}
		}else{
				// This is to make sure something doesn't stay energized when it doesn't need to be
			this.isTouchingSource = false;
		}

			// Last call in function. Destroyes potential if not there
		if(!isTouchingSource && !isRemoteConnection)
		{
			if(isSource)
				return;
			Destroy(this.gameObject.GetComponent<Neutral>());
			this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
		}
	}



		// Recursive function to check if this object has a connection to an electrical source
	public bool findSource(GameObject initiated)
	{
			// If this IS a source then it obviously can end here
		if( this.isSource )
			return true;

			// Gets everythiing touching THIS item (immediate vacinity) for source
		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);
		for(int i = 0; i < hitColliders.Length; i++)
		{
			if( hitColliders[i].gameObject.transform != this.gameObject.transform && hitColliders[i].gameObject.transform != initiated.transform)
			{
				Neutral n = hitColliders[i].gameObject.GetComponent<Neutral>();
				if(n != null)
					if( n.isSource )
						return true;
			}
		}

			// Recursively check for the source
		for(int i = 0; i < hitColliders.Length; i++)
		{
			if( hitColliders[i].gameObject.transform != initiated.transform && hitColliders[i].gameObject.transform != this.gameObject.transform)
			{
				Neutral on = hitColliders[i].gameObject.GetComponent<Neutral>();
				if( on != null )
					if( on.findSource(this.gameObject))
					{
						return true;
					}
			}
		}

		return false;

	}
}