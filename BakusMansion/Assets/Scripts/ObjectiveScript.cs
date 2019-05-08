using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript : MonoBehaviour {

    public GameObject playerObj;

    public Text objectiveText;
    public bool viewingObjective;
    public bool objDisplayed;

    public GameObject woodenBox;

    public PaintingTrigger paintTriggerScript;
    public LastPaintingTrigger lastTriggerScript;
    
    public bool paintingComplete;

    public PedestalTrigger kingTriggerScript;
    public QueenPedestalTrigger queenTriggerScript;

    public bool statuesComplete;

    // Start is called before the first frame update
    void Start()
    {
        //painting puzzle
        objectiveText.text = "  ";
        paintingComplete = false;

        viewingObjective = false;
        objDisplayed = false;

        //statue puzzle
        statuesComplete = false;
    }

    // Update is called once per frame
    void Update()
    {  

        if (paintTriggerScript.midInPlace == true && lastTriggerScript.lastInPlace == true) {

            paintingComplete = true;
            Debug.Log("paintingsComplete");
        }

        if(paintingComplete == true)
        {
            woodenBox.SetActive(true);

        }

        if(kingTriggerScript.kingInPlace == true && queenTriggerScript.queenInPlace == true)
        {
            statuesComplete = true;
            Debug.Log("king and queen placed");

        }

    }
}
