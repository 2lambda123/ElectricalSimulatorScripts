using System;
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

    public char selectedFunction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch( this.selectedFunction )
        {
            case 'v':
                blackLead.setFunction(selectedFunction);
                redLead.setFunction(selectedFunction);
                volts();
                break;
            case 'o':
                blackLead.setFunction(selectedFunction);
                redLead.setFunction(selectedFunction);
                ohmMeter();
                break;
            case 'r':
                blackLead.setFunction(selectedFunction);
                redLead.setFunction(selectedFunction);
                break;
        }
    }

    void ohmMeter()
    {
        if( blackLead.ohmMeter() )
            this.text.text = "0";
        else
            this.text.text = "OL";
    }

    void volts()
    {
        reading bl = blackLead.getReading();
        reading rl = redLead.getReading();

            // Meter reading logic
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

            else if( bl.getPotential() == 120 && rl.getPotential() == 120)
                this.text.text = "208v";

            else if( bl.getPotential() == 277 && rl.getPotential() == 277)
                this.text.text = "480v";
            else
                this.text.text = "?v";
                //this.text.text = Mathf.Abs(rl.getPotential() - bl.getPotential()) + "v";
            

        }
    }
}
