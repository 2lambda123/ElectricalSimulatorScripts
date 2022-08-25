using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject lineSide;
    public GameObject loadSide;
    public GameObject lampShader;

    public bool isOn;

    public float amperage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( lineSide == null || loadSide == null)
            return;
            
        Potential p = lineSide.GetComponent<Potential>();
        Neutral n = loadSide.GetComponent<Neutral>();

        if( p == null || n == null)
        {
            isOn = false;
			lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);      
            return;
        }else 
        {
                // Yellow\
            lampShader.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));      
            this.isOn = true;
            
        }
        
    }

    public float getAmperage()
    {
        return this.amperage;
    }

}
