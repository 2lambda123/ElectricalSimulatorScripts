using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Voltage : MonoBehaviour
{

    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        this.text.text = "The simulation of a voltmeter showcases its ability to read the potential between phases, neutral, and ground.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
