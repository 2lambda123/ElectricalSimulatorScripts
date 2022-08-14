using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

public class Potential : MonoBehaviour
{
		// If this is a source of electrical power
	public bool isSource;
	public bool isNeutral;

	public char phase;
	public int myPotential;

		// True if object has a path to an electrical source
	private bool isTouchingSource;

		// For sphere detection radious
	float hitDistance = 0.55f;

		// For debug output
	bool debug;

	bool isRemoteConnection;
	public List<GameObject> remoteChildren;


	void Start()
	{

		switch (phase)
		{
			case 'a':
				//this.gameObject.AddComponent<setColorBlack>();
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.black); 
				break;
			case 'b':
				//this.gameObject.AddComponent<setColorRed>();
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.red); 
				break;
			case 'c':
				//this.gameObject.AddComponent<setColorBlue>();
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.blue); 
				break;
			case 'd':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(165/255.0f,42/255.0f,42/255.0f,1));     
				break;
			case 'e':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 165/255.0f, 0));     
				break;
			case 'f':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));     
				break;
		}

		// TEST CODE
			// Checks if there is a wire link on this object
		WireLink wl = this.gameObject.GetComponent<WireLink>();
		if( wl != null )
			wl.energizeLink(this.gameObject, this.phase, this.myPotential);
	}


		// Set initial settings (Stand in for constructor)
	public void setParams( bool isSource, char phase, int myPotential)
	{
		this.isSource = isSource;
		this.phase = phase;
		this.myPotential = myPotential;
		
		switch (phase)
		{
			case 'a':
				//this.gameObject.AddComponent<setColorBlack>();
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.black);     
				break;
			case 'b':
				//this.gameObject.AddComponent<setColorRed>();
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.red);     
				break;
			case 'c':
				//this.gameObject.AddComponent<setColorBlue>();
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);     
				break;
			case 'd':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(165/255.0f,42/255.0f,42/255.0f,1));     
				break;
			case 'e':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 165/255.0f, 0));     
				break;
			case 'f':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));      
				break;
		}

	}

		// Getters 
	public char getPhase()
	{
		return this.phase;
	}


	public int getPotential()
	{
		return this.myPotential;
	}
	

	public float getAmperage()
	{
		return 0.0f;
	}

	
	public void setAsRemoteConnection()
	{
		this.isRemoteConnection = true;
	}


	public void setAsInactive()
	{
		this.isRemoteConnection = false;
	}
	
	public void addChild(GameObject obj)
	{
		if(remoteChildren == null)
		{
			remoteChildren = new List<GameObject>();
		}
		if(!remoteChildren.Contains(obj))
		{
			remoteChildren.Add(obj);
		}
	}

	public void removeChild(GameObject obj)
	{
		if( remoteChildren != null )
		remoteChildren.Remove(obj);
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
				if(hitColliders[i].gameObject.GetComponent<Potential>() == null)
				{
						// Equipment is on layer 6. This is to prevent items from trying to energize equipment
					if(hitColliders[i].gameObject.layer != 6)
					{
						Potential p = hitColliders[i].gameObject.AddComponent<Potential>();
						p.setParams(false, this.phase, this.myPotential);
							// Debug output
						if( debug )
							Debug.Log(hitColliders[i].gameObject + " does not have Potential!");
					}
				}

				if(hitColliders[i].gameObject.layer != 6)
					if(hitColliders[i].gameObject.GetComponent<Potential>().findSource(this.gameObject))
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
			this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
			Destroy(this.gameObject.GetComponent<Potential>());
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
				Potential p = hitColliders[i].gameObject.GetComponent<Potential>();
				if(p != null)
					if( p.isSource )
						return true;
			}
		}

			// Recursively check for the source
		for(int i = 0; i < hitColliders.Length; i++)
		{
			if( hitColliders[i].gameObject.transform != initiated.transform && hitColliders[i].gameObject.transform != this.gameObject.transform)
			{
				Potential op = hitColliders[i].gameObject.GetComponent<Potential>();
				if( op != null )
					if( op.findSource(this.gameObject))
					{
						return true;
					}
			}
		}

		return false;

	}


	public string toString()
	{
		var str = new StringBuilder();
		str.Append("Is source: [");
		str.Append(isSource);
		str.Append("]\tPhase: [");
		str.Append(phase.ToString());
		str.Append("]\tmyPotential: [");
		str.Append(myPotential.ToString());
		str.Append("]");
		return str.ToString();
	}
}
