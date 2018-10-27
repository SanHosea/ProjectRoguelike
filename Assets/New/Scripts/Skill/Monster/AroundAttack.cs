using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundAttack : SkillBase {
    [SerializeField]
	private MonsterBase monster;
	private CharacterBase character;
	[SerializeField]
	public MonsterAttackArea attackArea;
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	public override void Initialize()
	{
		coolTime = 3.0f;
		base.Initialize();
		character = GameObject.FindWithTag("Character").GetComponent<CharacterBase>();
		spriteRenderer.color = new Color (1, 1, 1, 0);
	}
	public override void Updated() 
	{
		base.Updated();	
		if (attackArea.IsIn && attackArea.CheckTime >= 0.5f)
		{
			monster.CurState = STATE.ATTACK;
		}
	}
	public override void FireSkill(CharacterBase character)
	{
		if (CanFireSkill())
		{
			CurTime = 0;
			StartCoroutine(Attack(character));
		}
	}
	public IEnumerator Attack(CharacterBase character)
	{
		// 공격범위 표시 
		spriteRenderer.color = new Color (1, 1, 1, 1);
		// 1초 후 공격
		yield return new WaitForSeconds(1.0f);
		if (attackArea.IsIn)
		{
			if (character.HasShield)
				character.HasShield = false;

			else
				character.CurHP -= 1;
		}

		spriteRenderer.color = new Color (1, 1, 1, 0);
		// 몬스터 다시 움직이도록
	    monster.CurState = STATE.TRACE;
	}
}
