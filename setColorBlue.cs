using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setColorBlue : MonoBehaviour
{
	Material mat;

    // Start is called before the first frame update
    void Start()
    {
		mat = this.gameObject.GetComponent<Renderer>().material;

		mat.SetColor("_Color",Color.blue);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
