using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBoss : MonsterBase {
	private float phase4_time;
	private int attackType;
	public override void Initialize(Vector2 pos, float hp, CharacterBase _character)
	{
		transform.position = pos;
		curHP = hp;
		maxHP = curHP;
		curState = STATE.TRACE;

		moveSpeed = 1.0f;
		phase4_time = 0.0f;

		character = _character;
		attackType = 0;

		skills.Add("NearAttack", skill[0]);
		skills.Add("ArroundAttack", skill[1]);
		skills.Add("RushAttack", skill[2]);

		skills["NearAttack"].Initialize();
		skills["ArroundAttack"].Initialize();
		skills["RushAttack"].Initialize();
	}

	public override void Updated() 
	{
		// phase 1
		if (curHP <= 100 && curHP >= 76)
		{
			skills["NearAttack"].Updated();
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				skills["NearAttack"].FireSkill(character);
			}
		}
		// phase 2
		else if (curHP < 76 && curHP >= 51)
		{
			skills["ArroundAttack"].Updated();
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				skills["ArroundAttack"].FireSkill(character);
			}
		}
		// phase 3
		else if (curHP < 51 && curHP >= 24)
		{
			skills["RushAttack"].Updated();
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
			}
			else if (curState == STATE.ATTACK)
			{
				skills["RushAttack"].FireSkill(character);
			}
		}
		// phase 4
		// 기본 근접 공격을 사용. 
   		// 캐릭터와 닿을 시 근접, 주위 공격 둘 중 하나 사용. 
   		// 5초 동안 추적 시 돌진 공격 사용.
		else if (curHP < 24 && curHP > 0)
		{
			if (curState == STATE.TRACE)
			{
				Move(character.gameObject);
				phase4_time += Time.deltaTime;

				// 5초 이전에 부딪혔을 경우
				if (phase4_time < 5.0f && bTouchCharacter)
				{
					attackType = Random.Range(1, 3);
					curState = STATE.ATTACK;
				}
				// 부딪히지 않고 5초가 지난 경우
				else if (phase4_time >= 5.0f)
				{
					attackType = 3;
					curState = STATE.ATTACK;
				}
			}
			else if (curState == STATE.ATTACK)
			{
				if (attackType == 1) 	  skills["NearAttack"].FireSkill(character);
				else if (attackType == 2) skills["ArroundAttack"].FireSkill(character);
				else if (attackType == 3) skills["RushAttack"].FireSkill(character);

				phase4_time = 0;
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
