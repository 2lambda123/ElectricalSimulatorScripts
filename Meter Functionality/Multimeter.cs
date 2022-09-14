using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multimeter : MonoBehaviour
{
    public MeterLead blackLead;
    public MeterLead redLead;

    public TMP_Text meterOutput;
    public TMP_Text selectedFeature;

    public char selectedFunction;
    public int mode;

    private bool clicked;

    void Start()
    {
        mode = -1;
        clicked = false;
        changeMode();
    }

    void Update()
    {
        changeMode();
    }

    // Update is called once per frame
    void OnMouseUp()
    {
        if(++mode > 2)
            mode = 0;
    }

    private void changeMode()
    {
        blackLead.setFunction(this.mode);
        redLead.setFunction(this.mode);

        switch( this.mode )
        {
            case 0:
                setSelectedFeatureText("V");
                volts();
                break;
            case 1:
                setSelectedFeatureText("O");
                ohmMeter();
                break;
            case 2:
                setSelectedFeatureText("R");
                resistance();
                break;
        }
    }

    void resistance()
    {
        float res = blackLead.getResistanceReading();
        if( res == -1 )
            setMeterOutputText("OL");
        else
            setMeterOutputText(res.ToString());

    }

    void ohmMeter()
    {
        if( blackLead.ohmMeter() )
            setMeterOutputText("0");
        else
            setMeterOutputText("OL");
    }

    void volts()
    {
        reading bl = blackLead.getReading();
        reading rl = redLead.getReading();

            // Meter reading logic
        if( bl.getPhase() == ' ' || rl.getPhase() == ' ')
        {
            setMeterOutputText("0v");
        }else if( bl.getPhase() == rl.getPhase() )
            setMeterOutputText("0v");
        else
        {
            if(bl.getPotential() == 0 )
                setMeterOutputText(rl.getPotential() + "v");
            else if( rl.getPotential() == 0)
                setMeterOutputText(bl.getPotential() + "v");

            else if( bl.getPotential() == 120 && rl.getPotential() == 120)
                setMeterOutputText("208v");

            else if( bl.getPotential() == 277 && rl.getPotential() == 277)
                setMeterOutputText("480v");
            else
                setMeterOutputText("?v");
                //this.text.text = Mathf.Abs(rl.getPotential() - bl.getPotential()) + "v";
            

        }
    }

    private bool setSelectedFeatureText(string txt)
    {
        if( this.selectedFeature == null )
            return false;
        
        this.selectedFeature.text = txt;
        return true;
    }

    private bool setMeterOutputText(string txt)
    {
        if( this.meterOutput == null )
            return false;
        
        this.meterOutput.text = txt;
        return true;
    }
}