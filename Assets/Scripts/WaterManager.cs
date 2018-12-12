/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;

/// <summary>
/// Manages the isWater boolean, to indicate if water is present.
/// To be attached to the H20 tangible.
/// </summary>
public class WaterManager : MonoBehaviour {

    public bool isWater;

    void Start()
    {
        isWater = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "photosystem")
        {
            isWater = true;
            if (FindObjectOfType<PhotosystemManager>().isSunlight)
            {
                FindObjectOfType<HConcManager>().IncreaseLConc(2);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "photosystem")
        {
            isWater = false;
        }
    }

    public bool GetActiveStatus()
    {
        return isWater;
    }
   
}
