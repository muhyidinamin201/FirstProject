using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillSlot {

    public SpesialAttack skill;
    public Rect position;
 
	// Use this for initialization
	public void setKey (KeyCode keyCode) {
	    if(skill != null)
        {
            skill.key = keyCode;
            
        }
	}
}
