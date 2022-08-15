using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterLead : MonoBehaviour
{
    public Transform meterTip;
    public reading potentialReading;

    private bool debug;

    private char slectedFunction;

    // Start is called before the first frame update
    void Start()
    {
        potentialReading = new reading(' ', 0, 0);
    }
    

        // Where collisoin detection is
    void FixedUpdate()
    {
        switch(slectedFunction)
        {
            case 'v':
                voltMeter();
                break;
            case 'o':
                //ohmMeter();
                break;
        }
    }

    public void setFunction(char newFunction)
    {
        this.slectedFunction = newFunction;
    }

    void voltMeter()
    {
            // Collision detection
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);

            // Itterating through imediate area
        foreach(Collider c in hitColliders)
                // Verify not checking self
            if(c != this.gameObject.GetComponent<Collider>())
            {
                if(debug)
                    Debug.Log("Meter is reading something");
                
                Potential p = c.gameObject.GetComponent<Potential>();
                Neutral n = c.gameObject.GetComponent<Neutral>();

                if(p!= null)
                {
                    this.potentialReading = new reading(p.getPhase(), p.getPotential(), p.getAmperage());

                    if(debug)
                    {
                        Debug.Log("Meter reading: " + this.potentialReading.toString());
                        Debug.Log("Meter reading2: " + p.getPotential().ToString());
                    }
                }else if(n!=null)
                {
                    this.potentialReading = new reading('n', 0, 0);
                }
                else
                    this.potentialReading = new reading(' ',0,0);
            }else{
                    this.potentialReading = new reading(' ',0,0);
            }
    }

    public bool ohmMeter()
    {
        bool found;
        if( this.gameObject.name == "PosLead")
            return false;

            // Collision detection
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);

            // Itterating through imediate area
        foreach(Collider col in hitColliders)
                // Verify not checking self
            if( !this.gameObject.Equals(col.gameObject))
            {
                
                Continuity con = col.gameObject.GetComponent<Continuity>();

                if(con != null)
                {
                    found = con.findPosLead(this.gameObject);
                    if( found )
                    {
                        Debug.Log("HAS CONTINUITY!!!");
                        return true;
                        break;
                    }
                }
            }

        return false;
    }

    public reading getReading()
    {
        return this.potentialReading;
    }

	// override object.Equals
	public bool Equals(GameObject obj)
	{
		//
		// See the full list of guidelines at
		//   http://go.microsoft.com/fwlink/?LinkID=85237
		// and also the guidance for operator== at
		//   http://go.microsoft.com/fwlink/?LinkId=85238
		//
		
		if (obj == null || GetType() != obj.GetType())
			return false;

		if( this.gameObject.name != obj.name )
			return false;

		if( this.gameObject.transform != obj.transform )
			return false;

		
		// TODO: write your implementation of Equals() here
		//throw new System.NotImplementedException();
		return true;
	}
}