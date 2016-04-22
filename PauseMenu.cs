using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public bool paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("p")&& paused == false)
        {
            paused = true;
            Time.timeScale = 0;
        }
	    else if(Input.GetKeyDown("p")&& paused == true)
        {
            paused = false;
            Time.timeScale = 1;
        }
	}
}