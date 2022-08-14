using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public List<GameObject> children;
    public bool isLive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        toggle();
    }

    void toggle()
    {
        //this.isLive = !isLive;

        if(isLive)
        {
            Potential p = this.gameObject.GetComponent<Potential>();
            foreach(GameObject obj in children)
            {
                Potential childP = obj.GetComponent<Potential>();
                if( childP == null )
                {
                    Potential newP = obj.AddComponent<Potential>();
                    newP.setParams(false, p.getPhase(), p.getPotential());
                }
            }
        }else
        {
            foreach(GameObject obj in children)
            {
                Potential childP = obj.GetComponent<Potential>();
                if( childP != null )
                {
			        obj.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    Destroy(childP);
                }
            }
        }
    }
}
