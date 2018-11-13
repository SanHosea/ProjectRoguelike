using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : SkillBase {
	[SerializeField]
	private Bullet bulletPrefab;
	public override void Initialize()
	{
		coolTime = 1.0f;
		base.Initialize();
	}
	public override void Updated() 
	{
		base.Updated();	
	}
	public override void FireSkill(CharacterBase character)
	{
		// 원거리 공격에 사용되는 수명값은 2
		// 따라서 5.1f 이상일 때 사용할 수 있도록 함.
		if (CanFireSkill() && character.CurLife >= 2.1f)
		{
			// 오브젝트 세팅
			// 공격력은 캐릭터의 50%
			Bullet clone = Instantiate(bulletPrefab);
			clone.transform.position = character.transform.position;
			clone.Initialize(character.AttackPerDamage * 0.5f, character.AttackVec);

			// 캐릭터 수명 감소 후 쿨타임 시작
			character.CurLife -= 2;
			CurTime = 0;
		}
	}
}
