using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterLead : MonoBehaviour
{
    public Transform meterTip;
    public reading meterReading;

    private bool debug;

    // Start is called before the first frame update
    void Start()
    {
        meterReading = new reading(' ', 0, 0);
    }
    

        // Where collisoin detection is
    reading FixedUpdate()
    {
            // Collision detection
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);

            // Itterating through imediate area
        for(int i = 0; i < hitColliders.Length; i++)
                // Verify not checking self
            if(hitColliders[i]!= this.gameObject.GetComponent<Collider>())
            {
                if(debug)
                    Debug.Log("Meter is reading something");
                
                Potential p =hitColliders[i].gameObject.GetComponent<Potential>();
                Neutral n = hitColliders[i].gameObject.GetComponent<Neutral>();

                if(p!= null)
                {
                    this.meterReading = new reading(p.getPhase(), p.getPotential(), p.getAmperage());

                    if(debug)
                    {
                        Debug.Log("Meter reading: " + this.meterReading.toString());
                        Debug.Log("Meter reading2: " + p.getPotential().ToString());
                    }
                }else if(n!=null)
                {
                    this.meterReading = new reading('n', 0, 0);
                }
                else
                    this.meterReading = new reading(' ',0,0);
            }else{
                    this.meterReading = new reading(' ',0,0);
            }

        return null;
    }

    public reading getReading()
    {
        return this.meterReading;
    }
}
