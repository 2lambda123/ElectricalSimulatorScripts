using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
*
*	This class adds the functionality a wire link, or remote connection.
*	Comments update: 18 Aug 22
*
*/


public class WireLink : MonoBehaviour
{
    public List<GameObject> linkedObjects=new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if( linkedObjects == null )
            linkedObjects = new List<GameObject>();

        foreach(GameObject obj in linkedObjects)
        {
            WireLink wl = obj.GetComponent<WireLink>();
            if(wl == null)
            {
                obj.AddComponent<WireLink>();
                obj.GetComponent<WireLink>().addObjectToList(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Potential p = this.gameObject.GetComponent<Potential>();
        if( p == null )
        {
            foreach(GameObject obj in linkedObjects)
            {
                Potential subP = obj.GetComponent<Potential>();
                if( subP != null)
                {
                    Debug.Log("Inactive");
                    subP.setAsInactive();
                }
            }
        }else{
            foreach(GameObject obj in linkedObjects)
            {
                Potential subP = obj.GetComponent<Potential>();
                if( subP != null)
                {
                    subP.setAsRemoteConnection();
                }
            }
        }
    }

    public bool energizeLink(GameObject linkSource, char phase, int potential)
    {
        foreach(GameObject obj in linkedObjects)
        {
            Potential p = obj.GetComponent<Potential>();
            if( p == null )
            {
                Potential np = obj.AddComponent<Potential>();
                np.setParams(false, phase, potential);
                np.setAsRemoteConnection();
                return true;
            }
            else
            {
                if(p.getPhase() != phase)
                {
                    Debug.Log("TWO DIFFERENT PHASES TOUCHED. BIG BOOM!");
                    return false;
                }
            }
        }
        return false;
    }

    public void addObjectToList(GameObject obj)
    {
        linkedObjects.Add(obj);
    }
}
