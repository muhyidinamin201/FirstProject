using UnityEngine;
using System.Collections;

public class SpesialAttack : MonoBehaviour {

    public Fighter player;
    public Texture2D picture;
    public KeyCode key;
    public double damagePercentage;
    public int stunTime;
    public bool inAction;
    public GameObject particleEffect;
    public int projectile;
    public bool opponentBased;
    public bool activated = true; 

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (activated && Input.GetKeyDown(key) && !player.spesialAttack)
        {
            player.resetAttack();
            player.spesialAttack = true;
            inAction = true;
        }
        if (inAction && player.spesialAttack)
        {
            if(player.attackMethod(stunTime, damagePercentage, key, particleEffect, projectile, opponentBased))
            {

            }
        }
        else
        {
            inAction = false;
        }
    }
}