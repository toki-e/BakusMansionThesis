using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript : MonoBehaviour { 

    public Text objectiveText;
    public bool viewingObjective;
    public bool objDisplayed;

    public GameObject woodenBox;

    public PaintingTrigger paintTriggerScript;
    public LastPaintingTrigger lastTriggerScript;
    
    public bool paintingComplete;

    // Start is called before the first frame update
    void Start()
    {
        objectiveText.text = "  ";
        paintingComplete = false;
        viewingObjective = false;
        objDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (viewingObjective == false)
        {     
            if (Input.GetKey(KeyCode.Q))
            {
                objectiveText.text = "Objective: \n Restore the door.";              
            } else
            {
                objectiveText.text = "  ";
            }
        }
       

        if (paintTriggerScript.midInPlace == true && lastTriggerScript.lastInPlace == true) {

            paintingComplete = true;
            Debug.Log("paintingsComplete");
        }

        if(paintingComplete == true)
        {
            woodenBox.SetActive(true);

        }

    }
}
