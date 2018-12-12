/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the visual indicators of Cytochrome activation(pulsing) and movement of H+ atoms.
/// To be attached to Cytochrome object.
/// </summary>
public class CytochromeController : MonoBehaviour {

    public GameObject hAtom;
    public bool isCCActive = false;

    //for pulsing effect
    public Material oldMat;
    public Material newMat;

    float speed = 0.001f;

    Vector3 startPos;
    Vector3 endPos;
    float distance = 2.5f;

    bool isPSActive = false;
    bool isWater = false;


	void Start () {
        hAtom.gameObject.SetActive(false);
        startPos = new Vector3(hAtom.transform.position.x, hAtom.transform.position.y, hAtom.transform.position.z);
        endPos = new Vector3(hAtom.transform.position.x, hAtom.transform.position.y, hAtom.transform.position.z - distance);
	}
	
	void FixedUpdate () {
        
        isPSActive = FindObjectOfType<PhotosystemManager>().GetActiveStatus();
        isWater = FindObjectOfType<WaterManager>().isWater;

        StartCoroutine(ColorPulse());

        if (isPSActive && isWater)
        {
            isCCActive = true;
            hAtom.gameObject.SetActive(true);

            StartCoroutine(MoveTo(hAtom.transform, startPos, endPos, speed));
        } 
        else 
        {
            isCCActive = false;
            hAtom.gameObject.SetActive(false);
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
        FindObjectOfType<HConcManager>().DecreaseSConc(1);
        FindObjectOfType<HConcManager>().IncreaseLConc(1);
        mover.position = origin;
    }

    /// <summary>
    /// Coroutine for changing color from oldMat to newMat and back.
    /// </summary>
    IEnumerator ColorPulse()
    {
        while (isCCActive)
        {
            transform.GetComponent<Renderer>().material.color = Color.Lerp(oldMat.color, newMat.color, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GetComponent<Renderer>().material.color = oldMat.color;
    }

    public bool GetActiveStatus()
    {
        return isCCActive;
    }

}
