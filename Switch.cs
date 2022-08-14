using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject lineSide;
    public GameObject loadSide;

    public bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Potential p = lineSide.GetComponent<Potential>();
        Neutral n = lineSide.GetComponent<Neutral>();

        if( isOn )
        {
            if( p != null )
            {
                if( loadSide.GetComponent<Potential>() == null)
                {
                    Potential p2 = loadSide.AddComponent<Potential>();
                    p2.setParams(false, p.getPhase(), p.getPotential());
                    p2.addChild(lineSide);
                }
            }/*else{

                Potential p2 = loadSide.GetComponent<Potential>();
                p2.removeChild(lineSide);
                Destroy( p2);
            }*/
        }
        else
        {
            Potential p2 = loadSide.GetComponent<Potential>();
            if(p2 != null)
                Destroy(p2);
            // TODO: Add a way for items to check back for source
            //loadSide.AddComponent<Neutral>();
        }
    }
}
