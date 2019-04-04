using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    public ObjectiveScript objScript;

    GameObject mainCamera;

    public KeyCode interactKey;

    public GameObject bookParticle;
    public GameObject door;
    public GameObject gem;

    public Text interactText;
    public Text thoughtText;
    public int bkInteractPg;

    public bool boxObserved;
    public bool gemCollected;
    public bool musicPlaying;

    public float subTimer;
    public float gemAppearTimer;

    public AudioSource appearSound;
    public AudioClip appearClip;

    public AudioSource recordPlayerSource;
    public AudioClip wickedClip;

    public AudioSource lockedSource;
    public AudioClip lockedClip;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        bookParticle.SetActive(false);

        interactKey = KeyCode.E;
        interactText.text = "  ";
        thoughtText.text = "  ";

        bkInteractPg = 0;

        boxObserved = false;
        gemCollected = false;
        gemAppearTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        subTimer -= Time.deltaTime;
        

        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "bookTrigger") {
                //Debug.Log("lookin' at book!");
                //interactText.text = "E: Read the blue book?";

                bookParticle.SetActive(true);

                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "I'll read later...";
                    interactText.text = "  ";                             
                }

            } else
            {
                bookParticle.SetActive(false);
                interactText.text = "  ";
            }

            if (hit.collider.gameObject.tag == "door" && boxObserved == false)
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "S-so small-! Is this a joke?";
                }

            }
            else

            if (hit.collider.gameObject.tag == "door" && gemCollected == true)
            {
                if (Input.GetKey(interactKey))
                { 
                     subTimer = 5;
                     thoughtText.text = "I'll use this. (Room Complete! Press R to play again.)";
                }
            }

            if(hit.collider.gameObject.tag == "woodBox")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "This wasn't here before... There's something inside.";
                    boxObserved = true;
                    
                }

            }

            if (hit.collider.gameObject.tag == "weapon")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Really? What does he need these for?";                
                }

            }

            if (hit.collider.gameObject.tag == "dresser")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "So much dust. Hasn't been used in decades most likely.";
                }

            }

            if (hit.collider.gameObject.tag == "music" && objScript.paintingComplete == false)
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Nothing's playing...";
                }

            }

            if (hit.collider.gameObject.tag == "music" && objScript.paintingComplete == true)
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Is he messing with me? Honestly, we have such a different taste in music.";
                }

            }

            if (hit.collider.gameObject.tag == "desk1")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "I highly doubt he sits and works at these.";
                }

            }

            if (hit.collider.gameObject.tag == "painting1")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Why is this sideways? The nameplate says 'Adam'.";
                }

            }

            if (hit.collider.gameObject.tag == "table1")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Nothing here... Let's see what's on the other one.";
                }

            }

            if (hit.collider.gameObject.tag == "nightstandR" || hit.collider.gameObject.tag == "nightstandL")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Can't open this...";

                    if (!lockedSource.isPlaying)
                    {
                        lockedSource.PlayOneShot(lockedClip, 1);

                    }
                }

            }

            if (hit.collider.gameObject.tag == "papers")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "Baku's notes about amplifiers. 'Gems that amplify magic. Can help you destroy barriers...'";
                }

            }

            if (hit.collider.gameObject.tag == "largeWall")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "What a waste of space. You can't keep me in here forever!";
                }

            }

            if (hit.collider.gameObject.tag == "gem")
            {
                if (Input.GetKey(interactKey))
                {
                    gemCollected = true;

                    if (!appearSound.isPlaying)
                    {
                        appearSound.PlayOneShot(appearClip, 1);
                    }
                }

            }

        }

        if (boxObserved == true)
        {
            door.transform.localScale = new Vector3(1.539f, 1.645f, 1.198f);
            door.transform.localPosition = new Vector3(11f, 3.779f, -17.599f);

            gemAppearTimer -= Time.deltaTime;

        }

        if(gemCollected == true)
        {
            gem.SetActive(false);

        }

        if(objScript.paintingComplete == true)
        {
            //subTimer -= Time.deltaTime * 60f;
            //thoughtText.text = "Nice work if I do say so myself.";
        }

        if (subTimer <= 0)
        {
            thoughtText.text = "  ";
        }

        if (gemAppearTimer <= -2f && gemCollected == false)
        {
            gem.SetActive(true);          
        }

        if (objScript.paintingComplete == true)
        {

            if (!musicPlaying)
            {
                if (!recordPlayerSource.isPlaying)
                {
                    recordPlayerSource.Play();

                    if (!appearSound.isPlaying)
                    {
                        appearSound.PlayOneShot(appearClip, 1);
                    }

                }

            }

        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    }

}
