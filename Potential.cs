using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

public class Potential : MonoBehaviour
{
	Material mat;
	GameObject obj;

	public bool isSource;
	private bool isTouchingSource;
	
	public char phase;
	public int myPotential;

	float hitDistance = 0.55f;

	bool debug;

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


	public void setParams( bool isSource, char phase, int myPotential)
	{
		this.isSource = isSource;
		this.phase = phase;
		this.myPotential = myPotential;
		
		switch (phase)
		{
			case 'a':
				this.gameObject.AddComponent<setColorBlack>();
				break;
			case 'b':
				this.gameObject.AddComponent<setColorRed>();
				break;
			case 'c':
				this.gameObject.AddComponent<setColorBlue>();
				break;
		}

	}
	
	private void FixedUpdate()
	{
		this.isTouchingSource = false;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position,hitDistance);
		if( hitColliders.Length>1)
		{
			if(debug)
				Debug.Log("Found: " + hitColliders.Length);
			for( int i = 0; i < hitColliders.Length; i++)
			{
				if( debug)
					Debug.Log("Checking: " + hitColliders[i].gameObject);
				if(hitColliders[i].gameObject.GetComponent<Potential>() == null)
				{
					if(hitColliders[i].gameObject.layer != 6)
					{
						Potential p = hitColliders[i].gameObject.AddComponent<Potential>();
						p.setParams(false, this.phase, this.myPotential);
						if( debug )
							Debug.Log(hitColliders[i].gameObject + " does not have Potential!");
					}
				}

				if(hitColliders[i].gameObject.layer != 6)
					if(hitColliders[i].gameObject.GetComponent<Potential>().findSource(this.gameObject))
						this.isTouchingSource = true;

			}
		}else{
			this.isTouchingSource = false;
		}

		if(!isTouchingSource)
		{
			if(isSource)
			return;
			Destroy(this.gameObject.GetComponent<Potential>());
			this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
		}
	}

	public bool findSource(GameObject initiated)
	{

		if( this.isSource )
			return true;

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

		for(int i = 0; i < hitColliders.Length; i++)
		{
			if( hitColliders[i].gameObject.transform != initiated.transform && hitColliders[i].gameObject.transform != this.gameObject.transform)
			if( hitColliders[i].gameObject.GetComponent<Potential>().findSource(this.gameObject))
			{
				return true;
			}
		}

		return false;

	}

	void Start()
	{

		switch (phase)
		{
			case 'a':
				this.gameObject.AddComponent<setColorBlack>();
				break;
			case 'b':
				this.gameObject.AddComponent<setColorRed>();
				break;
			case 'c':
				this.gameObject.AddComponent<setColorBlue>();
				break;
		}
	}

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
}
