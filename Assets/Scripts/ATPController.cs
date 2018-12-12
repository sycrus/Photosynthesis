/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the movement of the ADP and ATP objects.
/// To be attached to an empty GameObject.
/// </summary>
public class ATPController : MonoBehaviour {
    public GameObject adp;
    public GameObject atp;

    public GameObject location1;
    public GameObject location2;
    public GameObject location3;

    bool isATPActive;
    bool adpMove01;
    bool adpMove12;
    bool atpMove;

    Vector3[] positions = new[] { new Vector3(0f,0f,0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) };

    float spd = 0.0008f;

	void Start () {
        
        isATPActive = false;

        adpMove01 = true;
        adpMove12 = false;
        atpMove = false;

        positions[0] = location1.transform.position;
        positions[1] = location2.transform.position;
        positions[2] = location3.transform.position;

        adp.transform.position = positions[0];
        atp.transform.position = positions[1];


        adp.gameObject.SetActive(true);
        atp.gameObject.SetActive(false);
	}

    void Update()
    {
        if(adpMove01){
            StartCoroutine(AdpMove01());
            StopCoroutine(AdpMove12());
            StopCoroutine(AtpMove());
        }
        if (adpMove12)
        {
            StartCoroutine(AdpMove12());
            StopCoroutine(AdpMove01());
            StopCoroutine(AtpMove());
        }
        if (atpMove)
        {
            StartCoroutine(AtpMove());
            StopCoroutine(AdpMove01());
            StopCoroutine(AdpMove12());
        }

    }

   /// <summary>
   /// Moves ADP from position 0 to 1
   /// </summary>
    IEnumerator AdpMove01()
    {
        while (adp.transform.position != positions[1]) {
            if (adpMove01) {
                adp.transform.position = Vector3.MoveTowards(adp.transform.position, positions[1], spd * Time.deltaTime);
            }
            yield return null;
        }

        isATPActive = FindObjectOfType<ATPSynthaseController>().GetActiveStatus();
        if (isATPActive)
        {
            adpMove01 = false;
            adpMove12 = false;
            atpMove = true;

            atp.gameObject.SetActive(true);
            adp.gameObject.SetActive(false);
        }
        else
        {
            adpMove01 = false;
            adpMove12 = true;
            atpMove = false;
        }
    }

    /// <summary>
    /// Moves ADP from position 1 to 2
    /// </summary>
    IEnumerator AdpMove12()
    {
        while(adp.transform.position != positions[2]) {
            if (adpMove12)
            {
                adp.transform.position = Vector3.MoveTowards(adp.transform.position, positions[2], spd * Time.deltaTime);
            }
            yield return null;
        }

        adpMove01 = true;
        adpMove12 = false;
        atpMove = false;
        adp.transform.position = positions[0];

    }

    /// <summary>
    /// Moves ATP from position 1 to 2
    /// </summary>
   IEnumerator AtpMove()
    {
        while (atp.transform.position != positions[2])
        {
            if (atpMove)
            {
                atp.transform.position = Vector3.MoveTowards(atp.transform.position, positions[2], spd * Time.deltaTime);
            }
            yield return null;
        }

        adpMove01 = true;
        adpMove12 = false;
        atpMove = false;
        adp.transform.position = positions[0];
        atp.transform.position = positions[1];
        atp.gameObject.SetActive(false);
        adp.gameObject.SetActive(true);
    }
}
