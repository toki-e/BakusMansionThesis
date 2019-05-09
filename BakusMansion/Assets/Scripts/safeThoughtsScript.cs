using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class safeThoughtsScript : MonoBehaviour
{

    public Raycast raycastScript;
    public LaikaAudioScript laikaScript;

    private BoxCollider boxCollide;

    public bool triggerHit;

    // Start is called before the first frame update
    void Start()
    {
        triggerHit = false;
        boxCollide = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerHit)
        {
            boxCollide.enabled = false;

        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && laikaScript.successfullyHidden)
        {
            raycastScript.thoughtText.text = "That was nerve wracking...Now let's see if I can find anything new.";
            raycastScript.subTimer = 5;
            triggerHit = true;
        }
    }

}
