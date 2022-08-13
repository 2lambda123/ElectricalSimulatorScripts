using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
  public char phase;
  public int voltage;
    // Start is called before the first frame update
    void Start()
    {
 	    //Potential p = new Potential(this.gameObject, false, phase,voltage);   
      Potential p = this.gameObject.AddComponent<Potential>();
      p.setParams( true, phase,voltage);
  	  Debug.Log(p.toString()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
