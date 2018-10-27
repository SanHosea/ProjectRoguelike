using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Probability 
{
	public static int maxDeathCount = 30;
	public static int curDeathCount = 0;
	// 죽었을 때 골드와 스킬 레벨이 초기화될 확률
	public static float resetSkillLvPoint = 0;

	public static void Initialize()
	{
		curDeathCount = 0;
		resetSkillLvPoint = 0.25f;
	}

	public static float GetPlusPointPercent()
	{
		return Mathf.Pow(10, (1.0f / 15.0f) * (curDeathCount + 1));
	}
}
