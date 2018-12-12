/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple refresh script mainly to reset Vuforia tracking.
/// To be attached to button behavior.
/// </summary>
public class Refresh : MonoBehaviour {

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            RefreshScene();
        }
    }

    public void RefreshScene() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
