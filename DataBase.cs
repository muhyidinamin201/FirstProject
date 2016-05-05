using UnityEngine;
using System.Collections;

public class DataBase : MonoBehaviour {
    public int interval = 120;
    int count; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(count == interval)
        {
            savePosition();
            count=0;
        }
        count = count +1;
	
	}
    
    void savePosition()
    {
        PlayerPrefs.SetFloat("x", ClickToMove.currentPosition.x);
        PlayerPrefs.SetFloat("y", ClickToMove.currentPosition.y);
        PlayerPrefs.SetFloat("z", ClickToMove.currentPosition.z);
    }

    public static Vector3 readPlayerPosition()
    {
        Vector3 position = new Vector3();
        position.x = PlayerPrefs.GetFloat("x");
        position.y = PlayerPrefs.GetFloat("y");
        position.z = PlayerPrefs.GetFloat("z");

        return position;
    } 
}
