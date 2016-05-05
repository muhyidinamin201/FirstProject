using UnityEngine;
using System.Collections;

public class LavelSystem : MonoBehaviour {

	public int level;
	public int exp;
    public Fighter player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		LevelUp ();
	}

	void LevelUp()
    {
        if (exp >= Mathf.Pow(level, 3) + 100){
            exp = exp - (int) (Mathf.Pow(level, 3) + 100);
            level = level + 1;
            LevelEffect();
		}
	}

    void LevelEffect()
    {
        player.maxHealth = player.maxHealth + 100;
        player.damage = player.damage + 50;
        player.health = player.maxHealth;     
    }
}