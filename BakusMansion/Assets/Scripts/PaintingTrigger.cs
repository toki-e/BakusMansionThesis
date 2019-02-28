using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingTrigger : MonoBehaviour
{

    public GameObject painting2;
    public GameObject painting3;

    public GameObject midCollider;
    public GameObject thirdCollider;

    public PickupObject pickUpScript;

    public bool midInPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "painting2") {

            pickUpScript.carrying = false;
            coll.gameObject.transform.position = transform.position;
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //coll.attachedRigidbody.useGravity = true;

            midInPlace = true;

            
            Debug.Log("hit!");  

        }

    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "painting2")
        {
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            midInPlace = false;

        }

    }

}
