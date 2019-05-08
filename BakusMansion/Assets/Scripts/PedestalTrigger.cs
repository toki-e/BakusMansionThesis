using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalTrigger : MonoBehaviour
{
    public bool kingInPlace;
    public GameObject kingStatue;

    // Start is called before the first frame update
    void Start()
    {
        kingInPlace = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "king")
        {
            coll.gameObject.transform.position = transform.position;
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //coll.gameObject.transform.rotation = Quaternion.Euler(0, 176, 0);

            kingStatue.transform.rotation = Quaternion.Euler(0, 180, 0); 

            kingInPlace = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "king")
        {
            coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            kingInPlace = false;

        }

    }
}
