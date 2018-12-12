/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;

/// <summary>
/// Manages the lumenToStroma boolean, to indicate if the concentration of h+ in the lumen is greater that that of the stroma.
/// To be attached to empty GameObject.
/// </summary>
public class HConcManager : MonoBehaviour {

    public TextMesh stromaText; //for displaying conc at stroma
    public TextMesh lumenText;  //for displaying conc at lumen

    int lumenConc;
    int stromaConc;

    bool lumenToStroma = false;

	void Start () {
        lumenConc = 5;
        stromaConc = 5;
	}
	
	void Update () {
        stromaText.GetComponent<TextMesh>().text = stromaConc.ToString();
        lumenText.GetComponent<TextMesh>().text = lumenConc.ToString();

        if (lumenConc > stromaConc) {
            lumenToStroma = true;
        } else {
            lumenToStroma = false;
        }
	}

    public void IncreaseSConc(int value) { //only for ATP Synthase
        if (FindObjectOfType<ATPSynthaseController>().GetActiveStatus()) 
        {
            stromaConc = stromaConc + value;
        }
    }
    public void DecreaseLConc(int value) //only for ATP Synthase
    {
        if (FindObjectOfType<ATPSynthaseController>().GetActiveStatus())
        {
            lumenConc = lumenConc - value;
        }
    }

    public void DecreaseSConc(int value) { //only for Cyto
        if (FindObjectOfType<CytochromeController>().GetActiveStatus()) 
        {
            stromaConc = stromaConc - value;
        }
    }
    public void IncreaseLConc(int value) //only for Cyto
    {
        if (FindObjectOfType<CytochromeController>().GetActiveStatus()) 
        {
            lumenConc = lumenConc + value;
        }
    }
   
    public bool GetActiveStatus()
    {
        return lumenToStroma;
    }
}
