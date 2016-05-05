using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {
    public float CamSpeed = 0.05f;
    public float GUIsize = 25;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //blok script untuk mengerakkan kamera sebesar layar yang ditampilkan
        Rect recdown = new Rect(0, 0, Screen.width, GUIsize);
        Rect recup = new Rect(0, Screen.height - GUIsize, Screen.width, GUIsize);
        Rect recleft = new Rect(0, 0, GUIsize, Screen.height);
        Rect recright = new Rect(Screen.width - GUIsize, 0, GUIsize, Screen.height);
        if (recdown.Contains(Input.mousePosition))
            transform.Translate(0, 0, -CamSpeed, Space.World);
        if (recup.Contains(Input.mousePosition))
            transform.Translate(0, 0, CamSpeed, Space.World);
        if (recleft.Contains(Input.mousePosition))
            transform.Translate(-CamSpeed, 0, 0, Space.World);
        if (recright.Contains(Input.mousePosition))
            transform.Translate(CamSpeed, 0, 0, Space.World);
	}
}