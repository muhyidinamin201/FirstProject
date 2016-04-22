using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

    public float speed;
    public CharacterController controller;
    private Vector3 position;

    public AnimationClip run;
    public AnimationClip idle;

    public static bool attack;
    public static bool die;

	// Use this for initialization
	void Start () {
        position = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        //kondisi jika player tidak menyerang
        if (!attack&&!die)
        {
            if (Input.GetMouseButton(0))
            {
                locatePosition();
            }
            moveToPosition();
        }
        else
        {

        }
    }

    //method untuk mendeteksi kordinat pada saat mouse di klik. 
    void locatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
			if (hit.collider.tag != "Player"&&hit.collider.tag!="Enemy")
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
    }

    //method untuk mengerakkan player ke kordinat yang terdeteksi di method locatePosition
    void moveToPosition()
    {
        if(Vector3.Distance(transform.position, position) > 1)
        {
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);
            newRotation.x = 0f;
            newRotation.z = 0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
            controller.SimpleMove(transform.forward * speed);
            GetComponent<Animation>().CrossFade(run.name);
        }
        else
        {
            GetComponent<Animation>().CrossFade(idle.name);
        }
    }
}