using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaikaAudioScript : MonoBehaviour
{

    public Raycast raycastScript;
    public safeSpotScript safeScript;

    public AudioSource spiritScreamSource;
    public AudioClip spiritClip;

    public AudioSource glassSource;
    public AudioClip glassClip;

    public AudioSource spookySource;
    public AudioClip spookyClip;

    public float timeTillLaika;

    public bool hit;
    public bool successfullyHidden;
    public bool dead;

    private BoxCollider boxCollide;

    //public Text thoughtText;

    // Start is called before the first frame update
    void Start()
    {
        timeTillLaika = 0f;
        boxCollide = GetComponent<BoxCollider>();
        successfullyHidden = false;
        dead = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(timeTillLaika);
        Debug.Log(successfullyHidden);

        if (raycastScript.subTimer <= 0)
        {
            raycastScript.thoughtText.text = "  ";
        }

        if (hit)
        {
            timeTillLaika += Time.deltaTime;

        }

        if(timeTillLaika > 30f && !safeScript.safe)
        {
            Debug.Log("dead");

            dead = true;
            SceneManager.LoadScene(1);
        }

        if(timeTillLaika > 30f && safeScript.safe)
        {
            successfullyHidden = true;
        }

        if (hit)
        {
            boxCollide.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {

            if (!spiritScreamSource.isPlaying)
            {
                spiritScreamSource.PlayOneShot(spiritClip, 1);
            }

            if (!glassSource.isPlaying)
            {
                glassSource.PlayOneShot(glassClip, 1);
            }

            if (!spookySource.isPlaying)
            {
                spookySource.PlayOneShot(spookyClip, 1);

            }

            raycastScript.subTimer = 5;
            raycastScript.thoughtText.text = "What was that? I'd better hide in here-!";

            hit = true;

        }
    }
}
