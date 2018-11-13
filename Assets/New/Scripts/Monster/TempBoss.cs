using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBoss : MonsterBase {
	 public override void Initialize(Vector2 pos, float hp, CharacterBase _character)
	 {
		transform.position = pos;
		curHP = hp;
		maxHP = curHP;
		curState = STATE.TRACE;

		moveSpeed = 1.0f;

		character = _character;

		skills.Add("skill_1", skill[0]);
		skills.Add("skill_2", skill[1]);
		skills.Add("skill_3", skill[2]);

		skills["skill_1"].Initialize();
		skills["skill_2"].Initialize();
		skills["skill_3"].Initialize();
	 }

	 public override void Updated() 
	 {
		// phase 1
		if (curHP <= 100 && curHP >= 76)
		{
			skills["skill_1"].Updated();
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				skills["skill_1"].FireSkill(character);
			}
		}
		// phase 2
		else if (curHP < 76 && curHP >= 51)
		{
			skills["skill_2"].Updated();
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				skills["skill_2"].FireSkill(character);
			}
		}
		// phase 3
		else if (curHP < 51 && curHP >= 24)
		{
			skills["skill_3"].Updated();
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				skills["skill_3"].FireSkill(character);
			}
		}
		// phase 4
		else if (curHP < 24 && curHP > 0)
		{
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				
			}
		}

		else if (curHP <= 0) 
		{
			Destroy(gameObject);
		}
	 }

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Character"))
		{
			other.gameObject.GetComponent<CharacterBase>().CurHP -= 1; 
			bTouchCharacter = true;
		}
	}
}
