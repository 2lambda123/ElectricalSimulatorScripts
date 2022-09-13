using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject lineSideInteractionNode;
    public GameObject loadSideInteractionNode;
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
        if( lineSideInteractionNode == null || loadSideInteractionNode == null)
            return;
            
        InteractionNode lineNode = lineSideInteractionNode.GetComponent<InteractionNode>();
        InteractionNode loadNode = loadSideInteractionNode.GetComponent<InteractionNode>();


        if( lineNode == null || loadNode == null )
        {
            isOn = false;
			lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);   
            return;
        }        

        Potential linePot = lineNode.getPotential();
        Potential loadPot = loadNode.getPotential();


        if( linePot == null || loadPot == null )
        {
            isOn = false;
			lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);   
            return;
        }        

        char linePha = lineNode.getPotential().getPhase();
        char loadPha = loadNode.getPotential().getPhase();

        if( linePha == null || loadPha == null )
        {
            isOn = false;
			lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);   
            return;
        }

        switch( linePha )
        {
            case 'a':
            case 'b':
            case 'c':
                if( loadPha == 'n' )
                {
                    //if( lineNode.getParentPotential().getPotential() == 120 && lineNode.getParentPotential().getPotential() == 0 )
                    lampShader.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));      
                    this.isOn = true;
                }else{
                    isOn = false;
                    lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);   
                }
                break;
            default:
                isOn = false;
                lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);   
                break;

        }

        // if( linePot == 'a' )
        // {
        //     isOn = false;
		// 	lampShader.GetComponent<Renderer>().material.SetColor("_Color",Color.white);      
        //     return;
        // }else 
        // {
        //         // Yellow\
        //     if( linePot.getParentPotential().getPotential() == 120 && linePot.getParentPotential().getPotential() == 0 )
        //     lampShader.GetComponent<Renderer>().material.SetColor("_Color",new Color(255/255.0f, 255/255.0f, 0));      
        //     this.isOn = true;
            
        // }
        
        if(this.amperageSearch)
            getAmperage();
    }

    public float getAmperage()
    {
        // Debug.Log("Amperage reading followed:");
        // Amperage a = lineSide.GetComponent<Amperage>();
        // if(a == null)
        // {
        //     Debug.Log("Line side has no amperage!");
        //     if( lineSide.GetComponent<Potential>() != null )
        //         a = lineSide.AddComponent<Amperage>();
        // }

        // Queue<GameObject> path = new Queue<GameObject>();
        // a.getAmperagePath( ref path, this.gameObject );

        // foreach(GameObject o in path)
        // {
        //     Amperage a2 = o.GetComponent<Amperage>();
        //     if( a2 == null )
        //         a2 = o.AddComponent<Amperage>();

        //     a2.addAmperageSource(this.gameObject);
        // }

        // this.amperageSearch = false;
        // Debug.Log("Total amperage: " + this.amperage);
        // return this.amperage;
        return 0;
    }

}
