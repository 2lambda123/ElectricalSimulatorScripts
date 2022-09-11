using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;


/*
*
*	This class adds the functionality of potential on a conductor.
*	Comments update: 18 Aug 22
*
*/


public class Potential : MonoBehaviour 
{
		// If this is a source of electrical power
	public bool isSource;

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

	public List<GameObject> amperageSources;


	void Start()
	{
		if(amperageSources == null)
			amperageSources = new List<GameObject>();

		setColor(this.phase);

			// Checks if there is a wire link(remote connection) on this object
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
		setColor(phase);
	}


	private void setColor(char phase)
	{
		Renderer r = this.gameObject.GetComponent<Renderer>();
		if( r == null )
			return;


		if( this.gameObject.name == "Breaker")
			return;

		switch (phase)
		{
			case 'a':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.black);     
				break;
			case 'b':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.red);     
				break;
			case 'c':
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);     
				break;
			case 'd':
					// Brown?
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(165/255.0f,42/255.0f,42/255.0f,1));     
				break;
			case 'e':
					// Yellow?
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 165/255.0f, 0));     
				break;
			case 'f':
					// Orange?
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));      
				break;
		}
	}

	public Color getColor()
	{

		switch (phase)
		{
			case 'a':
				return Color.black;
			case 'b':
				return Color.red;
			case 'c':
				return Color.blue;
			case 'd':
					// Brown?
				return  new Color(165/255.0f,42/255.0f,42/255.0f,1);
			case 'e':
					// Yellow?
				return new Color(255/255.0f, 165/255.0f, 0);
			case 'f':
					// Orange?
				return new Color(255/255.0f, 255/255.0f, 0);
		}

		return Color.white;
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
	

	public bool addAmperage(GameObject ampObj)
	{
		if( !amperageSources.Contains(ampObj) )
		{
			amperageSources.Add(ampObj);
			return true;
		}
		return false;
	}

	public bool removeAmperage(GameObject ampObj)
	{
		if( amperageSources.Contains(ampObj) )
		{
			amperageSources.Remove(ampObj);
			return true;
		}

		// TODO: see if remove returns a bool that i can just return
		return false;
	}

		// TODO: Develop
	public float getAmperage()
	{
		float amp = 0;
		Debug.Log("AMPERAGE NOT IMPLEMENTED!");
		foreach(GameObject o in amperageSources)
		{
			// TODO: make this better
			Lamp l = o.GetComponent<Lamp>();
			if( l == null )
				continue;
			else
			{
				amp += l.getAmperage();
			}
		}
		return 0.0f;
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

	public bool checkIfIsSource()
	{
		return this.isSource;
	}

	public void getAmperagePath()
	{
		
	}

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