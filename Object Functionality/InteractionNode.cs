using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNode : MonoBehaviour
{
    public float scaleModifier;

    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    }
    // public void grow()
    // {
    //     this.gameObject.transform.LocalScale += new Vector3(0.75f,0.75f,0.75f);
    // }

    // public void shrink()
    // {
    //     this.gameObject.transform.LocalScale += new Vector3(0.75f,0.75f,0.75f);
    // }

    public void resetScale()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);   
    }

    void Update()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);   
    }

    public void scale( float factor)
    {
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f) + new Vector3(factor*scaleModifier,factor*scaleModifier,factor*scaleModifier);
    }
}
