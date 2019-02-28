using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPaintingTrigger : MonoBehaviour
{

    public GameObject painting2;
    public GameObject painting3;

    public GameObject midCollider;
    public GameObject thirdCollider;

    public PickupObject pickUpScript;

    public bool lastInPlace;

    // Start is called before the first frame update
    void Start()
    {
        lastInPlace = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "painting3") {

            pickUpScript.carrying = false;
            coll.gameObject.transform.position = transform.position;
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //coll.gameObject.isStatic = true;
            //coll.attachedRigidbody.useGravity = false;

            lastInPlace = true;
             
            Debug.Log("hit!");  

        }

    }

    private void OnTriggerExit(Collider coll)
    {
        lastInPlace = false;
        coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

}
