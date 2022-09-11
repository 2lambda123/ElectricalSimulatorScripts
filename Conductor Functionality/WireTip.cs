using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTip : MonoBehaviour
{

	public List<GameObject> objectsTouching;
    public GameObject parent;


    // Start is called before the first frame update
    void Start()
    {
        if( this.objectsTouching == null )
            objectsTouching = new List<GameObject>();
    }

    public void setParent(GameObject obj)
    {
        this.parent = obj;
    }

    public Conductor getParentConductor()
    {
        Conductor c = parent.GetComponent<Conductor>();

        if( c == null )
        {
            Debug.Log("Parent Conductor has no Conductor!");
            parent.AddComponent<Conductor>();
        }

        return c;
    }

	public List<GameObject> getConnections()
	{
		return objectsTouching;
	}

	void OnTriggerEnter(Collider c)
	{
		
		if(c.gameObject.layer < 7 || c.gameObject.layer > 8 )
			return;

		if(c.gameObject.GetComponent<SubPotential>() != null)
			return;

		objectsTouching.Add(c.gameObject);
	}

	void OnTriggerStay(Collider c)
	{
		if(c.gameObject == null )
			return;

		if(c.gameObject.layer < 7 || c.gameObject.layer > 8)
			return;

		if(c.gameObject.GetComponent<SubPotential>() == null)
			return;

		if(objectsTouching != null)
			if(!objectsTouching.Contains(c.gameObject))
				objectsTouching.Add(c.gameObject);
	}

	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.layer < 7 || c.gameObject.layer > 8)
			return;

		// if(c.gameObject.GetComponent<SubPotential>() == null)
		// 	return;

		// cow.getParentConductor().turnOff();
		objectsTouching.Remove(c.gameObject);
	}
}
