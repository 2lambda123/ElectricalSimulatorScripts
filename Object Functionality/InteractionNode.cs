using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNode : MonoBehaviour
{
    public float scaleModifier;
    public List<GameObject> touching;

    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        if( this.touching == null )
            this.touching = new List<GameObject>();
    }
    // public void grow()
    // {
    //     this.gameObject.transform.LocalScale += new Vector3(0.75f,0.75f,0.75f);
    // }

    // public void shrink()
    // {
    //     this.gameObject.transform.LocalScale += new Vector3(0.75f,0.75f,0.75f);
    // }

    public Potential getPotential()
    {
        if(this.touching.Count == 0 )
            return null;
            
        SubPotential sp = this.touching[0].GetComponent<SubPotential>();
        if( sp == null )
            return null;
            
        Potential p = sp.GetPotential();
        if( p == null )
            return null;
            
        return p;
    }

    public void resetScale()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);   
    }

    void Update()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);   
    }

    public void scale( float factor)
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f) + new Vector3(factor*scaleModifier,factor*scaleModifier,factor*scaleModifier);
    }

    public List<GameObject> getTouching()
    {
        return this.touching;
    }

	void OnTriggerEnter(Collider c)
	{
		
		if( c.gameObject.layer != 7 )
			return;

		touching.Add(c.gameObject);
	}

	void OnTriggerExit(Collider c)
	{
        if( touching.Contains(c.gameObject) )
            touching.Remove(c.gameObject);
	}
}
