using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPopup : MonoBehaviour {
	[SerializeField]
	private CharacterSimulationManager manager;
	public int curCheckSkillIndex;
	public void Button_GetSkillName(string skillName)
	{
		manager.text_skill[curCheckSkillIndex].text = skillName;
		gameObject.SetActive(false);
	}
}
