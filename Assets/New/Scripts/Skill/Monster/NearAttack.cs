using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttack : SkillBase 
{
	[SerializeField]
	private MonsterBase monster;
	[SerializeField]
	public MonsterAttackArea attackArea;
	public override void Initialize()
	{
		coolTime = 3.0f;
		base.Initialize();
		attackArea.gameObject.SetActive(false);
		// 처음에 어택에어리어 크기 줄이고, 알파값 0으로 둬서 
		// 이걸로 충돌체크 해버리자
		// 이후에 패턴 발동할 때는 어택에어리어 키우고, 알파값 넣어주고
		// 끝나면 다시 크기 줄이고 알파값 0
	}
	public override void Updated() 
	{
		base.Updated();	

		// 몬스터에가 닿은 경우
		if (monster.bTouchCharacter) 
		{
			monster.CurState = STATE.ATTACK;
			monster.bTouchCharacter = false;
		}
	}
	public override void FireSkill(CharacterBase character)
	{
		// 공격 트리거를 여기서 작성해야 컴포넌트 갈아끼우듯이 개발 가능함.
		if (CanFireSkill())
		{
			CurTime = 0;
			StartCoroutine(Attack(character));
		}
	}

	public IEnumerator Attack(CharacterBase character)
	{
		// 0.5초간 정지
		yield return new WaitForSeconds(0.5f);
		// 공격범위 표시 
		attackArea.gameObject.SetActive(true);
		// 1초 후 공격
		yield return new WaitForSeconds(1.0f);
		if (attackArea.IsIn)
		{
			if (character.HasShield)
				character.HasShield = false;

			else
				character.CurHP -= 1;
		}

		attackArea.gameObject.SetActive(false);
		monster.CurState = STATE.TRACE;
	}
}
