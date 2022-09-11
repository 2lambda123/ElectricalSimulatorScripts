using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{
    public GameObject [] children;
    public float time = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

     IEnumerator ExampleCoroutine()
    {
        foreach(GameObject o in children)
        {
            this.gameObject.transform.position = o.transform.position;
            yield return new WaitForSeconds(time);
        }
        StartCoroutine(ExampleCoroutine());
    }
}
