using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_VOLTMETER_TESTING : MonoBehaviour
{
    public GameObject negLead;
    public List<Transform> negPoints;
    bool isGrounded;

    public GameObject posLead;
    public List<Transform> posPoints;

    public int itt;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(animate());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator animate()
    {
        while(true)
        {
                foreach(Transform pos in posPoints)
                {
                    if(itt <18 )
                        negLead.transform.position = new Vector3(negPoints[3].position.x, negPoints[3].position.y, negPoints[3].position.z);
                    if(itt <12 )
                        negLead.transform.position = new Vector3(negPoints[2].position.x, negPoints[2].position.y, negPoints[2].position.z);
                    if(itt <9 )
                        negLead.transform.position = new Vector3(negPoints[1].position.x, negPoints[1].position.y, negPoints[1].position.z);
                    if(itt <6 )
                        negLead.transform.position = new Vector3(negPoints[0].position.x, negPoints[0].position.y, negPoints[0].position.z);
                    posLead.transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
                    yield return new WaitForSeconds(1);

                    if(itt >= 17)
                        itt = 0;
                    else
                        itt += 1;
                }

        }

    }
}
