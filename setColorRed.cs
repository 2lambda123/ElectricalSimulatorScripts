using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setColorRed : MonoBehaviour
{
	Material mat;

    // Start is called before the first frame update
    void Start()
    {

		mat = this.gameObject.GetComponent<Renderer>().material;

		mat.SetColor("_Color",Color.red);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
