using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SkillBase {
	public override void Initialize()
	{
		coolTime = 10.0f;
		base.Initialize();
	}
	public override void Updated() 
	{
		base.Updated();	
	}
	public override void FireSkill(CharacterBase character)
	{
		// 캐릭터가 쉴드를 가지고 있지 않을 때만 발동하도록 함.
		// 쉴드에 사용되는 라이프값이 15
		// 따라서 15.1f 이상이어야 사용할 수 있도록 함.
		if (CanFireSkill() && !character.HasShield && character.CurLife >= 15.1f)
		{
			character.HasShield = true;
			effectObj.SetActive(true);
			StartCoroutine(SheildEffect(character));
			character.CurLife -= 15;
			CurTime = 0;
		}
	}
	// 캐릭터가 쉴드를 가지고 있지 않으면(= 캐릭터가 쉴드를 쓴 상태에서 피격을 당했을 때)
	// 이펙트 꺼줌.
	private IEnumerator SheildEffect(CharacterBase character)
	{
		while (true)
		{
			if (!character.HasShield)
			{
				effectObj.SetActive(false);
				break;
			}
			yield return null;
		}
	}
}
