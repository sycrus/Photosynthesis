/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the visual indicators of ATPSynthase activation(rotation, pulsing) and movement of H+ atoms.
/// To be attached to ATP top object for rotation.
/// </summary>
public class ATPSynthaseController : MonoBehaviour {

    public GameObject hAtom;
    public GameObject atpBottom;
    public bool isASActive = false;

    //for pulsing effect
    public Material oldMat;
    public Material newMat;


    bool lumenToStroma = false;
    bool isWater = false;

    float speed = 0.001f;
    float distance = 2.5f;

    Vector3 startPos;
    Vector3 endPos;

	void Start () {
        hAtom.gameObject.SetActive(false);
        startPos = new Vector3(hAtom.transform.position.x, hAtom.transform.position.y, hAtom.transform.position.z);
        endPos = new Vector3(hAtom.transform.position.x, hAtom.transform.position.y, hAtom.transform.position.z + distance);
	}
	
	void FixedUpdate () {
        lumenToStroma = FindObjectOfType<HConcManager>().GetActiveStatus();
        isWater = FindObjectOfType<WaterManager>().GetActiveStatus();

        StartCoroutine(ColorPulse());

        if (lumenToStroma) {
            transform.Rotate(Vector3.forward * Time.deltaTime * 500);
            hAtom.SetActive(true);
            isASActive = true;

            StartCoroutine(MoveTo(hAtom.transform, startPos, endPos, speed));

        } else {
            hAtom.SetActive(false);
            isASActive = false;
        }
	}

    /// <summary>
    /// Coroutine for moving H+ from startPos to endPos.
    /// </summary>
    IEnumerator MoveTo(Transform mover, Vector3 origin, Vector3 destination, float spd)
    {
        while (mover.position != destination)
        {
            mover.position = Vector3.MoveTowards(
                mover.position,
                destination,
                spd * Time.fixedDeltaTime);
            // Wait a frame and move again.
            yield return null;
        }

        Debug.Log("ATPSyntaseController: MoveTo");
        FindObjectOfType<HConcManager>().DecreaseLConc(1);
        FindObjectOfType<HConcManager>().IncreaseSConc(1);
        mover.position = origin;
    }

    /// <summary>
    /// Coroutine for changing color from oldMat to newMat and back.
    /// </summary>
    IEnumerator ColorPulse()
    {
        while (isASActive)
        {
            atpBottom.GetComponent<Renderer>().material.color = Color.Lerp(oldMat.color, newMat.color, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        atpBottom.GetComponent<Renderer>().material.color = oldMat.color;
    }

    public bool GetActiveStatus()
    {
        return isASActive;
    }

}
