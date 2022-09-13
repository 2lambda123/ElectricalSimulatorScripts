using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPotential : MonoBehaviour
{
    public GameObject parent;
	public char phase;
	public int myPotential;
    public bool isSource;
    
    private bool isRemoteConnection;
    private bool isTouchingSource;
    private float hitDistance = 0.5f;

	public Potential potential;
    
    public void setPotential(Potential p, GameObject parent)
    {
		if( p == null )
		{
			removeSelf();
			return;
		}
		this.potential = p;
        this.phase = p.getPhase();
        this.myPotential = p.getPotential();
        this.parent = parent;
    }

    public Potential getParentPotential()
    {
		Breaker bkr = this.parent.GetComponent<Breaker>();
		if( bkr != null )
			return bkr.getPotential();

		Switch sw = this.parent.GetComponent<Switch>();
		if( sw != null )
		{
			Potential potentiallyTheFixQuestionMark = sw.getLinePotential();
			if( potentiallyTheFixQuestionMark != null )
				return potentiallyTheFixQuestionMark;
		}

		InteractionNode inter = this.parent.GetComponent<InteractionNode>();
		if( inter != null )
		{
			return inter.getPotential();
		}

        Conductor pc = this.parent.GetComponent<Conductor>();
        if( pc != null )
        {
        	return pc.getPotential();
        }


		Debug.Log(this.gameObject + " has nothing to look for potential");
		return null;
    }

	public Potential GetPotential()
	{
		return this.potential;
	}

	public void setAsSource()
	{
		this.isSource = true;
	}

	public bool checkIfSource()
	{
		return this.isSource;
	}

	public void removeSelf()
	{
		Destroy( this.gameObject.GetComponent<SubPotential>() );
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
	