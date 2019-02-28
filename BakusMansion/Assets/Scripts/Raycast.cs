using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    public ObjectiveScript objScript;

    GameObject mainCamera;

    public KeyCode interactKey;

    public GameObject bookParticle;
    public GameObject door;

    public Text interactText;
    public Text thoughtText;
    public int bkInteractPg;
    public bool boxObserved;

    public float subTimer;

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

            if(hit.collider.gameObject.tag == "door" && boxObserved == false)
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "S-so small-! Is this a joke?";
                }

            }

            if(hit.collider.gameObject.tag == "woodBox")
            {
                if (Input.GetKey(interactKey))
                {
                    subTimer = 5;
                    thoughtText.text = "This wasn't here before...";
                    boxObserved = true;
                }

            }          

        }

        if (boxObserved == true)
        {
            door.transform.localScale = new Vector3(1.539f, 1.645f, 1.198f);
            door.transform.localPosition = new Vector3(11f, 3.779f, -17.599f);

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

    }
}
