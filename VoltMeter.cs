using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoltMeter : MonoBehaviour
{
    public GameObject meterBody;
    public MeterLead blackLead;
    public MeterLead redLead;

    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reading bl = blackLead.getReading();
        reading rl = redLead.getReading();

        if( bl.getPhase() == ' ' || rl.getPhase() == ' ')
        {
            this.text.text = "0v";
        }else if( bl.getPhase() == rl.getPhase() )
            this.text.text = "0v";
        else
        {
            if(bl.getPotential() == 0 )
                this.text.text = rl.getPotential() + "v";
            else if( rl.getPotential() == 0)
                this.text.text = bl.getPotential() + "v";

            if( bl.getPotential() == 120 && rl.getPotential() == 120)
                this.text.text = "208v";
        }


    }
}
