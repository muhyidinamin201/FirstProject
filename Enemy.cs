using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed;
    public float range;
    public CharacterController control;
    public Transform player;
    public LavelSystem playerLevel;
    private Fighter opponent;

    public AnimationClip attackClip;
    public AnimationClip run;
    public AnimationClip idle;
	public AnimationClip die;

    public double impactMeter = 0.36;

    public int maxHealth;
    public int health;
    public int damage;

    private bool impacted;
    public int stunTime;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        opponent = player.GetComponent<Fighter>();
	}
	
	// Update is called once per frame
	void Update () {
        //blok program kondisi jika enemy not dead dan sebaliknya
        if (!isDead())
        {
            if (stunTime <= 0)
            {
                if (!inRange())
                {
                    chase();
                }
                else
                {
                    //GetComponent<Animation> ().CrossFade (idle.name);
                    GetComponent<Animation>().Play(attackClip.name);
                    attack();
                    if (GetComponent<Animation>()[attackClip.name].time > 0.9 * GetComponent<Animation>()[attackClip.name].length)
                    {
                        impacted = false;
                    }
                }
            }
            else
            {

            }
        }
        else
        {
            dieMethod();
        }
    }

    void attack()
    {
        if (GetComponent<Animation>()[attackClip.name].time > GetComponent<Animation>()[attackClip.name].length * impactMeter && !impacted && GetComponent<Animation>()[attackClip.name].time < 0.9 * GetComponent<Animation>()[attackClip.name].length)
        {
            opponent.getHit(damage);
            impacted = true;
        }
    }

    //method agar enemy bisa bergerak sampai pada jarak tertentu sesuai settingan
    bool inRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //mengurangi health enemy pada saat diserang player
    public void getHit(double damage)
    {
        health = health - (int) damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void getStun(int seconds)
    {
        stunTime = seconds;
        InvokeRepeating("stunCountDown", 0f, 1f);
    }

    void stunCountDown()
    {
        stunTime = stunTime - 1;
        if (stunTime <= 0)
        {
            CancelInvoke("stunCountDown");
        }
    }

    //method agar enemy mendekati player
    void chase()
    {
        transform.LookAt(player.position);
        control.SimpleMove(transform.forward * speed);
        GetComponent<Animation>().CrossFade(run.name);
    }

    //method untuk menhilangkan enemy jika animation die berakhir 
    void dieMethod()
    {
        GetComponent<Animation>().Play(die.name);
        if (GetComponent<Animation>()[die.name].time > GetComponent<Animation>()[die.name].length * 0.9)
        {
			playerLevel.exp = playerLevel.exp + 100;
            Destroy(gameObject);
        }
    }
    //method agar enemy mati pada saat health mencapai 0 
	bool isDead(){
		if (health <= 0) {
			return true;
		} else {
			return false;
		}
	}
    
    //menkonvert enemy menjadi gameobject pada saat mouse berada di atas enemy
    void OnMouseOver()
    {
        player.GetComponent<Fighter>().opponent = gameObject;
    }
}