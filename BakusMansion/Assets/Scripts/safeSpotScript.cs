using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeSpotScript : MonoBehaviour
{
    public AudioSource heartSource;
    public AudioClip heartClip;

    public int timeSafe;
    public bool safe;

    public Raycast raycastScript;

    // Start is called before the first frame update
    void Start()
    {
        timeSafe = 0;
        safe = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(safe);

        if (timeSafe >= 1000)
        {
            safe = true;

        }

    }

    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            timeSafe++;

            if (!heartSource.isPlaying && !safe)
            {
                heartSource.PlayOneShot(heartClip, 1);
            }

        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            heartSource.Stop();

        }
    }
}
