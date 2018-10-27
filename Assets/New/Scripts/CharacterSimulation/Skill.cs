using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Skill 
{
	public enum SkillName
	{
		A = 0, B, C, D, E, F, G, H, I, J, K, L
	}
	public static int[] skill_Lv = new int[12];

	public static void Initialize()
	{
		for (int i = 0; i < skill_Lv.Length; i++)
			skill_Lv[i] = 0;
	}
}
