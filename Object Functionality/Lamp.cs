using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject lineSide;
    public GameObject loadSide;

    bool isOn;

    public float amperage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Potential p = lineSide.GetComponent<Potential>();
        Neutral n = loadSide.GetComponent<Neutral>();

        if( p != null && n != null)
        {
            if( !this.isOn )
            {
                    // Yellow
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));      
                this.isOn = true;
                p.addAmperage(this.gameObject);
            }
        }else{
            if( this.isOn )
            {
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.white);      
                this.isOn = false;
            }
        }
    }

    public float getAmperage()
    {
        return this.amperage;
    }

}
