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
            if (attackMethod(0, 1, KeyCode.W))
            {

            }
            else
            {
                inAction = false;
            }
        }
        deadMethod();
    }

    public bool attackMethod(int stunSeconds, double scaleDamage, KeyCode key)
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
        impact(stunSeconds, scaleDamage);
        return true;
    }

    public void resetAttack()
    {
        ClickToMove.attack = false;
        impacted = false;
        //GetComponent<Animation>().Stop(attack.name);
    }

    //method agar kalkulasi penyerangan player teratur bukan per frame.
    void impact(int stunSeconds, double scaleDamage)
    {
        if(opponent!=null&&GetComponent<Animation>().IsPlaying(attack.name)&&!impacted)
        {
			if ((GetComponent<Animation>()[attack.name].time) > impactLength&&(GetComponent<Animation>()[attack.name].time<0.9* GetComponent<Animation>()[attack.name].length))
            {
                counter = combatEscapeTime + 2;
                CancelInvoke("combatEscCountDown");
                InvokeRepeating("combatEscCountDown", 0, 1);
                opponent.GetComponent<Enemy>().getHit(damage*scaleDamage);
                opponent.GetComponent<Enemy>().getStun(stunSeconds);
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
		if (Vector3.Distance (opponent.transform.position, transform.position) <= range) {
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