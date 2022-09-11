using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

/*
*
*	This class will be added to EVERYTHING that can conduct electricity. It will integrate all other scripts into one place.
*	Comments update: 31 Aug 22
*
*/
public class Conductor : MonoBehaviour
{
        // The two ends of the wire
    public GameObject A;
    public GameObject B;

        // All of these will be removed eventually
     public Potential potential;
    public Resistance resistance;
    public Amperage amperage;

        // Draw the "Insulation" that connects the wire
    private LineRenderer line;

        // DEVEOPING:

        // These will make it possible for each end of the wire to be touching multiple wires
        // Uses: Wire nut
    private List<GameObject> childrenA;
    private List<GameObject> childrenB;

    private float wireLength;
    private float wireSize;

    public bool isRemoteConnection;

    private bool debug;

    private bool canCheck;

    void Start()
    {
            // Must be called to setup 
        if( potential != null )
            setPotential(null);

            // Set up wire line
        //Draw a line
        line = this.gameObject.AddComponent<LineRenderer>();
        //SetColor works only when the material is set
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetVertexCount(2);// Set two points
        line.SetWidth(0.5f, 0.5f);// Set line width

    }

    void Update()
    {
        List<GameObject> la;
        List<GameObject> lb;

        Potential ts = findSource();

        if(!ts)
        {
            turnOff();
            // Debug.Log(gameObject.name + " did NOT FoundSource");
        }
        else
        {
            setPotential(ts);
            // Debug.Log(gameObject.name + " FoundSource!");
        }

            // Draw a visual to signify wire
        if( line != null )
        {
            line.SetPosition(0, A.transform.position);
            line.SetPosition(1, B.transform.position);
        }

        draw();
    }

    public Potential getPotential()
    {
        if(this.potential == null)
        Debug.Log("REUTRUNIGN NULL POTENTIAL");
        return this.potential;
    }

    public void setAsRemoteConnection(bool status)
    {
        this.isRemoteConnection = status;
    }

    // TODO: Problem here.
    public void setPotential(Potential p)
    {
        if( this.potential == null )
            this.potential = p;

        if( isNullSubPotential( A, B ) )
        {
            if(debug)
                Debug.Log("Added Sub Potential");
                // SubPotential is an object that is responsible for being able to pass between objects to make a seamless passing
            A.AddComponent<SubPotential>().setPotential(potential, this.gameObject);
            B.AddComponent<SubPotential>().setPotential(potential, this.gameObject);
        }else
            if(debug)
                Debug.Log("Did not add subpotential to objects");
    }

    public bool turnOff()
    {
        if( A.GetComponent<SubPotential>() != null )
        A.GetComponent<SubPotential>().removeSelf();
        
        if( B.GetComponent<SubPotential>() != null )
        B.GetComponent<SubPotential>().removeSelf();

        this.potential = null;
        return true;
    }

    void draw()
    {
        if( line == null )
        {
            if(debug)
                Debug.Log("Line fucking null??");
            line = this.gameObject.AddComponent<LineRenderer>();
            return;
        }

        if( this.potential == null )
        {
            //Debug.Log("No fucking Potential???");
            line.SetColors(Color.white, Color.white); // Line color settings
            return;
        }

        line.SetColors(this.potential.getColor(), this.potential.getColor()); // Line color settings
        //Set the start and end points of the indicator line
        line.SetPosition(0, A.transform.position);
        line.SetPosition(1, B.transform.position);
    }

    private bool isNull(GameObject A, GameObject B)
    {
        if( A == null || B == null )
            return true;
        return false;
    }

		// Recursive function to check if this object has a connection to an electrical source
	public Potential findSource()
	{

        WireTip subA = A.GetComponent<WireTip>();
        WireTip subB = B.GetComponent<WireTip>();

        if( subA == null )
        {
            subA = A.AddComponent<WireTip>();
            subA.setParent(this.gameObject);
        }
        if( subB == null )
        {
            subB = B.AddComponent<WireTip>();
            subA.setParent(this.gameObject);
        }

        
        List<GameObject> aList = subA.getConnections();
        List<GameObject> bList = subB.getConnections();

		foreach(GameObject obj in aList)
		{
			SubPotential sp = obj.GetComponent<SubPotential>();
            if( sp == null )
                continue;

            if( sp.checkIfSource() )
                return sp.getParentPotential();
            Potential p =  obj.GetComponent<WireTip>().getParentConductor().findSource();
            if( p != null )
                return p;
		}

		foreach(GameObject obj in bList)
		{
			SubPotential sp = obj.GetComponent<SubPotential>();
            if( sp == null )
                continue;

            if( sp.checkIfSource() )
                return sp.getParentPotential();
            Potential p =  obj.GetComponent<WireTip>().getParentConductor().findSource();
            if( p != null )
                return p;
		}
		return null;

	}

    private bool isNullSubPotential(GameObject A, GameObject B)
    {
            if( isNull( A, B ) )
            {
                    // SubPotential is an object that is responsible for being able to pass between objects to make a seamless passing
                A.AddComponent<SubPotential>().setPotential(potential, this.gameObject);
                B.AddComponent<SubPotential>().setPotential(potential, this.gameObject);
            }else
                if(debug)
                    Debug.Log("Did not add subpotential to objects");
        if( A.GetComponent<SubPotential>() == null || B.GetComponent<SubPotential>() == null )
            return true;
        return false;
    }

	public string toString()
	{
		var str = new StringBuilder();
	/*	str.Append("Is source: [");
		str.Append(isSource);
		str.Append("]\tPhase: [");
		str.Append(phase.ToString());
		str.Append("]\tmyPotential: [");
		str.Append(myPotential.ToString());
		str.Append("]");*/
		return str.ToString();
	}
}
