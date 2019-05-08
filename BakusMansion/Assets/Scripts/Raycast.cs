﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    public ObjectiveScript objScript;

    GameObject mainCamera;

    public KeyCode interactKey;
    public KeyCode closeKey;

    public GameObject bookParticle;
    public GameObject door;
    public GameObject gem;
    public GameObject barrier;

    public GameObject displayLid;
    public GameObject scratches;
    public GameObject dagger;
    public GameObject rope1; 
    public GameObject rope2;
    public GameObject trunkLid;

    public GameObject pianoParticle;
    public GameObject yellowGem;

    public GameObject laikaSoundTrigger;

    public Text interactText;
    public Text thoughtText;
    public int bkInteractPg;

    public Image bakusNote;
    public Image augustsRenNote;
    public Image augustsKotoNote;
    public Image augustsLaikaNote;

    public Image redGemIMG;
    public Image swordIMG;
    public Image yellowGemIMG;

    public bool boxObserved;
    public bool gemCollected;
    public bool musicPlaying;
    public bool barrierDispelled;
    public bool daggerCollected;
    public bool ropeCut;

    public bool yellowGemCollected;
    public bool yellowBarrierBroken;

    public float subTimer;
    public float gemAppearTimer;
    public float barrierDisappearTimer;

    public AudioSource appearSound;
    public AudioClip appearClip;

    public AudioSource appearSoundRm2;
    public AudioClip appearClipRm2;

    public AudioSource recordPlayerSource;
    public AudioClip wickedClip;

    public AudioSource lockedSource;
    public AudioClip lockedClip;

    public Animator gemAnimator;
    public Animator barrierAnimator;
    public Animation barrierBroken;
    public Animator doorAnimator;
    public Animator wallAnimator;
    public Animator boxAnimator;
    public Animator playerAnimator;
    public Animator studyDoorAnimator;
    public Animator armoryDoorAnimator;

    public Animator yellowGemAnimator;
    public Animator displayCaseAnimator;
    public Animator trunkAnimator;

    public Animator yellowBarrierAnimator;
    public Animator gateAnimator;

    // Start is called before the first frame update
    void Start()
    {

        playerAnimator.SetTrigger("lookAround");

        mainCamera = GameObject.FindWithTag("MainCamera");
        bookParticle.SetActive(false);

        interactKey = KeyCode.E;
        closeKey = KeyCode.Q;
        interactText.text = "  ";
        thoughtText.text = "  ";

        bkInteractPg = 0;

        boxObserved = false;
        gemCollected = false;
        gemAppearTimer = 1f;
        barrierDisappearTimer = 2f;

        gemAnimator = gem.GetComponent<Animator>();
        barrierAnimator = barrier.GetComponent<Animator>();
        doorAnimator = door.GetComponent<Animator>();
        displayCaseAnimator = displayLid.GetComponent<Animator>();
        trunkAnimator = trunkLid.GetComponent<Animator>();


        bakusNote.enabled = false;
        augustsKotoNote.enabled = false;
        augustsLaikaNote.enabled = false;
        augustsRenNote.enabled = false;

        redGemIMG.enabled = false;
        swordIMG.enabled = false;
        yellowGemIMG.enabled = false;

        daggerCollected = false;
        ropeCut = false;
        yellowGemCollected = false;
    }

    // Update is called once per frame
    void Update()
    {

        subTimer -= Time.deltaTime;
        

        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        //handle icons

        if (gemCollected)
        {
            redGemIMG.enabled = true;
        }

        if (yellowGemCollected)
        {
            yellowGemIMG.enabled = true;

        }

        if (daggerCollected)
        {
            swordIMG.enabled = true;

        }

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "bookTrigger") {
                //Debug.Log("lookin' at book!");
                //interactText.text = "E: Read the blue book?";

                //bookParticle.SetActive(true);

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "I'll read later...";
                    interactText.text = "  ";                             
                }

            } else
            {
                //bookParticle.SetActive(false);
                interactText.text = "  ";
            }

            if (hit.collider.gameObject.tag == "door" && boxObserved == false)
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "S-so small-! Is this a joke?";
                }

            }
            else

            if (hit.collider.gameObject.tag == "door" && gemCollected == true)
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                { 
                     subTimer = 5;
                     thoughtText.text = "I'll use this.";
                     barrierAnimator.SetTrigger("brokenTrigger");
                     barrierDispelled = true;
                }
            }

            //make barrier disappear

            if (barrierDispelled)
            {
                barrierDisappearTimer -= Time.deltaTime;
            }

            if(barrierDisappearTimer <= 0)
            {
                barrier.SetActive(false);

            }

            //openDoor after barrier's gone

            if (hit.collider.gameObject.tag == "door" && barrierDisappearTimer <= 0)
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    doorAnimator.SetTrigger("doorOpen");
                    wallAnimator.SetTrigger("slideUp");
                }
            }

            if(hit.collider.gameObject.tag == "woodBox")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "This wasn't here before... There's something inside.";
                    boxObserved = true;
                    boxAnimator.SetTrigger("boxShake");
                    
                }

            }

            if (hit.collider.gameObject.tag == "weapon")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Really? What does he need these for?";                
                }

            }

            if (hit.collider.gameObject.tag == "dresser")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "So much dust. Hasn't been used in decades most likely.";
                }

            }

            if (hit.collider.gameObject.tag == "music" && objScript.paintingComplete == false)
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Nothing's playing...";
                }

            }

            if (hit.collider.gameObject.tag == "music" && objScript.paintingComplete == true)
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Is he messing with me? Honestly, we have such a different taste in music.";
                }

            }

            if (hit.collider.gameObject.tag == "desk1")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "I highly doubt he sits and works here.";
                }

            }

            if (hit.collider.gameObject.tag == "painting1")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "The nameplate says 'Adam'.";
                }

            }

            if (hit.collider.gameObject.tag == "table1")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Nothing here... Let's see what's on the other one.";
                }

            }

            if (hit.collider.gameObject.tag == "nightstandR" || hit.collider.gameObject.tag == "nightstandL")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
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
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Baku's notes about amplifiers. 'Gems that amplify magic. Can help you destroy barriers...'";
                }

            }

            if (hit.collider.gameObject.tag == "largeWall")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "What a waste of space. You can't keep me in here forever!";
                }

            }

            if (hit.collider.gameObject.tag == "gem")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    gemCollected = true;

                    if (!appearSound.isPlaying)
                    {
                        appearSound.PlayOneShot(appearClip, 1);
                    }
                }

            }

            //hallway observation starts here

            if (hit.collider.gameObject.tag == "armor")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Tacky...";
                }

            }

            if (hit.collider.gameObject.tag == "blood")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "W-what happened here?";
                }

            }

            if (hit.collider.gameObject.tag == "studyDoor")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    studyDoorAnimator.SetTrigger("studyOpen");
                }

            }

            if (hit.collider.gameObject.tag == "armoryDoor")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    armoryDoorAnimator.SetTrigger("WeaponsRmDoorOpen");
                }

            }

            //rm2 observation starts here

            if (hit.collider.gameObject.tag == "LaikaWriting")
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {                 

                    subTimer = 5;
                    thoughtText.text = "Baku wouldn't write this on his own wall... This sounds like...Laika...";
                    


                }

            }

            if (hit.collider.gameObject.tag == "yellowBarrier" && yellowGemCollected)
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    yellowBarrierAnimator.SetTrigger("yellowBroken");

                    subTimer = 5;
                    thoughtText.text = "I can get through here now.";
                    yellowBarrierBroken = true; 

                    gateAnimator.SetTrigger("gateOpenTrigger");

                    
                }

            }

            if (hit.collider.gameObject.tag == "yellowGem")
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    yellowGem.SetActive(false);
                    subTimer = 5;
                    thoughtText.text = "Nice! I bet I could use this to open that other door.";
                    yellowGemCollected = true;

                    if (!appearSound.isPlaying)
                    {
                        appearSound.PlayOneShot(appearClip, 1);
                    }

                }

            }

            if (hit.collider.gameObject.tag == "piano" && objScript.statuesComplete)
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    
                    subTimer = 5;
                    thoughtText.text = "There's something in here too.";

                    yellowGemAnimator.SetTrigger("yellowPopOut");
               
                }

            }

            if (hit.collider.gameObject.tag == "trunkFront" && !daggerCollected)
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "I need something sharp to cut these ropes.";
                }

            }

            if (hit.collider.gameObject.tag == "dagger")
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {

                    
                    subTimer = 5;
                    thoughtText.text = "It's surprisingly light.";
                    dagger.SetActive(false);
                    daggerCollected = true;
                    
                    //added new
                    if (!appearSound.isPlaying)
                    {
                        appearSound.PlayOneShot(appearClip, 1);
                    }

                }

            }

            if (hit.collider.gameObject.tag == "case")
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {                 
                    subTimer = 5;
                    thoughtText.text = "There's something odd about this case. I should take a closer look.";                
                }

            }

            if (hit.collider.gameObject.tag == "scratches")
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {

                    Debug.Log("scratched");
                    subTimer = 5;
                    thoughtText.text = "The glass is loose on this side.";

                    displayCaseAnimator.SetTrigger("lidOpened");
                }

            }

            if (hit.collider.gameObject.tag == "pedestal")
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                 
                    subTimer = 5;
                    thoughtText.text = "Perfect for display. I wonder what used to sit here.";
                 
                }

            }

            if (hit.collider.gameObject.tag == "rope1" && daggerCollected == true)
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    rope1.SetActive(false);
                    ropeCut = true;
                    
                }

            }

            if (hit.collider.gameObject.tag == "rope2" && daggerCollected == true)
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    rope2.SetActive(false);

                }

            }

            if (hit.collider.gameObject.tag == "trunk" && daggerCollected == true && ropeCut == true)
            {

                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    trunkAnimator.SetTrigger("openTrunk");

                }

            }

            if (hit.collider.gameObject.tag == "lamp")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    subTimer = 5;
                    thoughtText.text = "Hm. It's not working...";
                }

            }

            if (hit.collider.gameObject.tag == "bakusNote")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    bakusNote.enabled = true;
                }

            }

            if(bakusNote.enabled == true)
            {
                if (Input.GetKey(closeKey))
                {
                    bakusNote.enabled = false;

                }

            }

            if (hit.collider.gameObject.tag == "augustsKotoNote")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    augustsKotoNote.enabled = true;
                }

            }

            if (augustsKotoNote.enabled == true)
            {
                if (Input.GetKey(closeKey))
                {
                    augustsKotoNote.enabled = false;

                }

            }

            if (hit.collider.gameObject.tag == "augustsLaikaNote")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    augustsLaikaNote.enabled = true;
                }

            }

            if (augustsLaikaNote.enabled == true)
            {
                if (Input.GetKey(closeKey))
                {
                    augustsLaikaNote.enabled = false;

                }

            }

            if (hit.collider.gameObject.tag == "augustsRenNote")
            {
                if (Input.GetKey(interactKey) || Input.GetMouseButton(0))
                {
                    augustsRenNote.enabled = true;
                }

            }

            if (augustsRenNote.enabled == true)
            {
                if (Input.GetKey(closeKey))
                {
                    augustsRenNote.enabled = false;

                }

            }

        }

        if (objScript.statuesComplete)
        {
            pianoParticle.SetActive(true);

        }



        if (boxObserved == true)
        {
            //door.transform.localScale = new Vector3(1.539f, 1.645f, 1.198f);
            //door.transform.localPosition = new Vector3(11f, 3.779f, -17.599f);

            gemAppearTimer -= Time.deltaTime;
            doorAnimator.SetTrigger("doorGrow");
            

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
            gemAnimator.SetTrigger("popOut");
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

        //Rm2 Code starts here

       

    }

}
