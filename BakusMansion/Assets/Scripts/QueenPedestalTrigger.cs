using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPedestalTrigger : MonoBehaviour
{


    public bool queenInPlace;

    // Start is called before the first frame update
    void Start()
    {
        queenInPlace = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "queen")
        {
            coll.gameObject.transform.position = transform.position;
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            queenInPlace = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "queen")
        {
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            queenInPlace = false;

        }

    }
}
