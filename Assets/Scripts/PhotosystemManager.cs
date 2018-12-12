/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the visual indicators of Photosystem activation(pulsing).
/// To be attached to Photosystems object.
/// </summary>
public class PhotosystemManager : MonoBehaviour {

    public bool isSunlight = false;

    //for pulsing effect
    Color oldColor;
    Color newColor;

	void Start () 
    {
        oldColor = Color.green;
        newColor = Color.yellow;
	}
   
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "sun") 
        {
            isSunlight = true;
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "sun")
        {
            StartCoroutine(ColorPulse());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sun")
        {
            isSunlight = false;
            Debug.Log(isSunlight);
            transform.GetComponent<Renderer>().material.color = oldColor;
        }
    }

    public bool GetActiveStatus(){
        return isSunlight;
    }

    /// <summary>
    /// Coroutine for changing color from oldColor to newColor and back.
    /// </summary>
    IEnumerator ColorPulse()
    {
        while (isSunlight)
        {
            transform.GetComponent<Renderer>().material.color = Color.Lerp(oldColor, newColor, Mathf.PingPong(Time.time, 1));
            yield return null;
        }

    }
}
