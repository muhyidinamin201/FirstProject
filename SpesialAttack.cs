using UnityEngine;
using System.Collections;

public class SpesialAttack : MonoBehaviour {

    public Fighter player;
    public KeyCode key;
    public double damagePercentage;
    public int stunTime;
    public bool inAction;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !player.spesialAttack)
        {
            player.resetAttack();
            player.spesialAttack = true;
            inAction = true;
        }
        if (inAction && player.spesialAttack)
        {
            if(player.attackMethod(stunTime, damagePercentage, key))
            {

            }
        }
        else
        {
            inAction = false;
        }
    }
}