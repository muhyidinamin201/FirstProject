using UnityEngine;
using System.Collections;

public class ActionBar : MonoBehaviour {

    public Texture2D actionBar;
    public Rect position;
    public SkillSlot[] skill;

    public float skillX;
    public float skillY;
    public float skillWidth;
    public float skillHeight;
    public float skillDistance;

    private int keyBindSlot = -1;

    // Use this for initialization
    void Start () {
        initialize();
    }
	
    void initialize()
    {
        SpesialAttack[] attacks = GameObject.FindGameObjectWithTag("Player").GetComponents<SpesialAttack>();
        skill = new SkillSlot[attacks.Length];
        for (int i = 0; i < attacks.Length; i++)
        {
            skill[i] = new SkillSlot();
            skill[i].skill = attacks[i];
        }
        skill[0].setKey(KeyCode.S);
        skill[1].setKey(KeyCode.D);
        skill[2].setKey(KeyCode.A);
    }
	// Update is called once per frame
	void Update () {
        updateSkillSlots();
	}

    void updateSkillSlots()
    {
        for(int i=0; i < skill.Length; i++)
        {
            skill[i].position.Set(skillX+i*(skillWidth+skillDistance), skillY, skillWidth, skillHeight);
        }
    }

    void OnGUI()
    {
        drawActionBar();
        drawSkillSlots();
    }

    void drawActionBar()
    {
        GUI.DrawTexture(getScreenRect(position), actionBar);
    }

    void drawSkillSlots()
    {
        for(int i = 0; i < skill.Length; i++)
        {
            GUI.DrawTexture(getScreenRect(skill[i].position), skill[i].skill.picture);
        }
    }

    void setKeyBindings()
    {
        for (int i = 0; i < skill.Length; i++)
        {
            if (Input.GetMouseButtonDown(0) && Event.current.isMouse && (skill[i].position).Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                if(keyBindSlot == -1)
                {
                    keyBindSlot = i;
                    skill[keyBindSlot].skill.activated = false;
                }
                else
                {
                    keyBindSlot = -1;
                }
            }
        }

        if(keyBindSlot != -1 && Event.current.isKey)
        {

            skill[keyBindSlot].setKey (Event.current.keyCode);
            skill[keyBindSlot].skill.activated = true;
            keyBindSlot = -1;

        }
    }

    Rect getScreenRect(Rect position)
    {
       return new Rect(Screen.width * position.x, Screen.height * position.y, Screen.width * position.width, Screen.height * position.height);
    }
}
