using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

    public GameObject opponent;
    public AnimationClip attack;
    public AnimationClip die;

    public int damage;
    public int maxHealth;
    public int health;
    
    private double impactLength;
    public double impactMeter;
    public bool impacted;
    public bool inAction;

	public float range;
    bool started;

    public float combatEscapeTime;
    public float counter;
    public bool spesialAttack;

	// Use this for initialization
	void Start () {
        health = maxHealth;
		impactLength = (GetComponent<Animation>() [attack.name].length * impactMeter);
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.W)&&!spesialAttack)
        {
            inAction = true;
        }
        if (inAction)
        {
            if (attackMethod(0, 1, KeyCode.W, null, 0, true))
            {

            }
            else
            {
                inAction = false;
            }
        }
        deadMethod();
    }

    public bool attackMethod(int stunSeconds, double scaleDamage, KeyCode key, GameObject particleEffect, int projectile, bool opponentBased)
    {
        if (opponentBased)
        {
            if (Input.GetKey(key) && inRange())
            {
                GetComponent<Animation>().Play(attack.name);
                ClickToMove.attack = true;
                if (opponent != null)
                {
                    transform.LookAt(opponent.transform.position);
                }
            }
            else
            {
                if (Input.GetKey(key))
                {
                    GetComponent<Animation>().Play(attack.name);
                    ClickToMove.attack = true;
                    transform.LookAt(ClickToMove.cursorPosition);
                }
            }
        }

        if (GetComponent<Animation>()[attack.name].time > 0.9 * GetComponent<Animation>()[attack.name].length)
        {
            ClickToMove.attack = false;
            impacted = false;
            if (spesialAttack)
            {
                spesialAttack = false;
            }
            return false;
        }
        impact(stunSeconds, scaleDamage, particleEffect, projectile, opponentBased);
        return true;
    }

    public void resetAttack()
    {
        ClickToMove.attack = false;
        impacted = false;
        GetComponent<Animation>().Stop(attack.name);
    }

    //method agar kalkulasi penyerangan player teratur bukan per frame.
    void impact(int stunSeconds, double scaleDamage, GameObject particleEffect, int projectile, bool opponentBased)
    {
        if((!opponentBased || opponent!=null)&&GetComponent<Animation>().IsPlaying(attack.name)&&!impacted)
        {
			if ((GetComponent<Animation>()[attack.name].time) > impactLength&&(GetComponent<Animation>()[attack.name].time<0.9* GetComponent<Animation>()[attack.name].length))
            {
                counter = combatEscapeTime + 2;
                CancelInvoke("combatEscCountDown");
                InvokeRepeating("combatEscCountDown", 0, 1);
                opponent.GetComponent<Enemy>().getHit(damage * scaleDamage);
                opponent.GetComponent<Enemy>().getStun(stunSeconds);
                Quaternion rot = transform.rotation;
                rot.x = 0f;
                rot.z = 0f;
                if (projectile > 0)
                {
                    Instantiate(Resources.Load("Projectile"), new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), rot);
                }
                if (particleEffect != null) {
                    Instantiate(particleEffect, new Vector3(opponent.transform.position.x, opponent.transform.position.y, opponent.transform.position.z), Quaternion.identity);
                }
                impacted = true;    
            }
       }
    }

    void combatEscCountDown()
    {
        counter = counter - 1;
        if(counter == 0)
        {
            CancelInvoke("combatEscCountDown");
        }
    }

    public void getHit(int damage)
    {
        health = health - damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    //method agar health enemy berkurang pada saat diserang jarak dekat saja
    bool inRange(){
		if (opponent != null && Vector3.Distance (opponent.transform.position, transform.position) <= range) {
			return true;	
		} else {
			return false;
		}
	}

    public bool isDead()
    {
        if(health == 0) {
            return true;
        } else {
            return false;
        }
    }

    void deadMethod()
    {
        if (isDead())
        {
            if (!started)
            {
                ClickToMove.die = true;
                GetComponent<Animation>().Play(die.name);
                started = true;
            }
            if (started && !GetComponent<Animation>().IsPlaying(die.name))
            {
                Debug.Log("You have Dead");
                //ended = true;
                started = false;
                ClickToMove.die = false;
            }
        }
    }
}