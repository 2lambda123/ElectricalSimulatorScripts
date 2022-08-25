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

    public bool amperageSearch;
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
        
        if(this.amperageSearch)
            getAmperage();
    }

    public float getAmperage()
    {
        Debug.Log("Amperage reading followed:");
        Amperage a = lineSide.GetComponent<Amperage>();
        if(a == null)
        {
            Debug.Log("Line side has no amperage!");
            if( lineSide.GetComponent<Potential>() != null )
                a = lineSide.AddComponent<Amperage>();
        }

        Queue<GameObject> path = new Queue<GameObject>();
        a.getAmperagePath( ref path, this.gameObject );

        foreach(GameObject o in path)
        {
            Amperage a2 = o.GetComponent<Amperage>();
            if( a2 == null )
                a2 = o.AddComponent<Amperage>();

            a2.addAmperageSource(this.gameObject);
        }

        this.amperageSearch = false;
        Debug.Log("Total amperage: " + this.amperage);
        return this.amperage;
    }

}
