using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterLead : MonoBehaviour
{
    public Transform meterTip;
    public reading meterReading;
    // Start is called before the first frame update
    void Start()
    {
        meterReading = new reading(' ', 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    reading FixedUpdate()
    {
		Collider[] hitColliders = Physics.OverlapSphere(meterTip.position,0.2f);
        for(int i = 0; i < hitColliders.Length; i++)
            if(hitColliders[i]!= this.gameObject.GetComponent<Collider>())
            {
                Debug.Log("Meter is reading something");
                //this.meterReading = null;
                Potential p =hitColliders[i].gameObject.GetComponent<Potential>();
                if(p!= null)
                {
                    this.meterReading = new reading(p.getPhase(), p.getPotential(), p.getAmperage());
                    Debug.Log("Meter reading: " + this.meterReading.toString());
                    Debug.Log("Meter reading2: " + p.getPotential().ToString());
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
