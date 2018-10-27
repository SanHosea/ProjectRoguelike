using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : SkillBase {
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
		// HP가 1일 때 쓰면 죽기 때문에 2 이상일 때만 쓸 수 있도록 함.
		if (CanFireSkill() && character.CurHP >= 2)
		{
			StartCoroutine(ObjectOnOff(effectObj, 2.0f));
			character.CurHP -= 1;
			character.CurLife += 30;
			CurTime = 0;
		}
	}
}
