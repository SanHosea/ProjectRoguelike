using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearFieldAttack : SkillBase {
	[SerializeField]
	private UserAttackArea AttackArea;
	public override void Initialize()
	{
		coolTime = 5.0f;
		base.Initialize();
		AttackArea.gameObject.SetActive(false);
	}
	public override void Updated() 
	{
		base.Updated();	
	}
	public override void FireSkill(CharacterBase character)
	{
		// 근거리 공격에 사용되는 수명값은 10
		// 따라서 10.1f 이상일 때 사용할 수 있도록 함.
		if (CanFireSkill() && character.CurLife >= 10.1f)
		{
			// 어택 에어리어에 값을 넣어서 체크를 해야하나...
			AttackArea.Initialize(character.AttackPerDamage * 1.5f);
			AttackArea.transform.position = character.transform.position + (Vector3)character.AttackVec;
			StartCoroutine(ObjectOnOff(AttackArea.gameObject, 0.2F));
			character.CurLife -= 10;
			CurTime = 0;
		}
	}
}
