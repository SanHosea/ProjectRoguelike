using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSimulationManager : MonoBehaviour {
	#region UI Objects
	// Gold
	[Header("[Gold]")]
	[SerializeField]
	private Text text_gold;

	// Status
	[Header("[Status]")]
	[SerializeField]
	private Text text_generation;
	[SerializeField]
	private Text text_attack;
	[SerializeField]
	private Text text_movespeed;
	[SerializeField]
	private Text text_hp;
	[SerializeField]
	private Text text_life;

	// total stat point
	[Header("[Total stat point]")]
	[SerializeField]
	private Text text_totalPoint;
	[SerializeField]
	private Text text_pointuppercent;

	// player skill
	[Header("[Player skill]")]
	public Text[] text_skill;

	// skill level
	[Header("[Skill level]")]
	[SerializeField]
	private Text[] text_skill_lv;

	[SerializeField]
	private SkillPopup Popup;
	#endregion

	[SerializeField]
	private CharacterBase character;
	private int gold = 1000;

	private void Start() 
	{
		character.Initialize(new Vector2(-0.38f, -1.36f));
		Probability.Initialize();
		Skill.Initialize();
		Initialize();
	}

	public void Initialize()
	{
		gold = 1000;
		text_gold.text = gold.ToString();

		text_generation.text = character.Stat_Generation.ToString();
		text_attack.text = character.Stat_Attack.ToString();
		text_movespeed.text = character.Stat_MoveSpeed.ToString();
		text_hp.text = character.Stat_HP.ToString();
		text_life.text = character.Stat_Life.ToString();

		text_totalPoint.text = character.Stat_TotalPoint.ToString();
		text_pointuppercent.text = "(" +  Probability.GetPlusPointPercent().ToString("00.00") + "%)";

		for (int i = 0; i < text_skill.Length; i++)
			text_skill[i].text = "NONE";

		for (int i = 0; i < Skill.skill_Lv.Length; i++)
			text_skill_lv[i].text = Skill.skill_Lv[i].ToString();
	}

	public void Button_GoldUP()
	{
		gold += 1000;
		text_gold.text = gold.ToString();
	}

	public void Button_Reset()
	{
		character.Initialize(new Vector2(-0.38f, -1.36f));	// 캐릭터 스탯 리셋
		Probability.Initialize();	// 확률 리셋
		Skill.Initialize();			// 스킬 리셋
		Initialize();
	}

	public void Button_Choice_Skill(int Number)
	{
		Popup.curCheckSkillIndex = Number;
		Popup.gameObject.SetActive(true);
	}

	public void Button_NewGeneration()
	{
		// 제너레이션 1 업
		character.Stat_Generation++;
		text_generation.text = character.Stat_Generation.ToString();

		// 토탈 스탯 증가
		ReArrangeTotalPoint();
		// 현재 스탯 재분배
		ReArrangeStatus();
		// 스킬 레벨 재조정
		ReArrageSkillLv();
	}

	private void ReArrangeTotalPoint()
	{
		float rand = Random.Range(0.0f, 100.0f);
		// 확률이 맞아 포인트가 오를 때
		if (rand <= Probability.GetPlusPointPercent()) 
		{
			// 포인트 올려주고, 텍스트 변경해주고
			character.Stat_TotalPoint += 1;
			text_totalPoint.text = character.Stat_TotalPoint.ToString();
			// 인덱스 초기화
			Probability.curDeathCount = 0;
			text_pointuppercent.text = "(" +  Probability.GetPlusPointPercent().ToString("00.00") + "%)";
		}
		else
		{
			// 아니면 인덱스 하나 업
			if (Probability.curDeathCount != Probability.maxDeathCount)
			{
				Probability.curDeathCount++;
				text_pointuppercent.text = "(" +  Probability.GetPlusPointPercent().ToString("00.00") + "%)";
			}		
		}
	}
	private void ReArrageSkillLv()
	{
		for (int i = 0; i < text_skill.Length; i++)
		{
			// 스킬이 존재할 때
			if (text_skill[i].text != "NONE")
			{
				float rand = Random.Range(0.0f, 1.0f);
				if (Probability.resetSkillLvPoint >= rand && Skill.skill_Lv[i] >= 2)
				{
					Skill.skill_Lv[i]--;
					text_skill_lv[i].text = Skill.skill_Lv[i].ToString();
				}
			}
		}
	}
	private void ReArrangeStatus()
	{
		int stat1 = Random.Range(1, character.Stat_TotalPoint - 3);
		int stat2 = Random.Range(1, character.Stat_TotalPoint - 2 - stat1);
		int stat3 = Random.Range(1, character.Stat_TotalPoint - 1 - stat1 - stat2);
		int stat4 = character.Stat_TotalPoint - stat1 - stat2 - stat3;

		character.Stat_Attack = stat1;
		character.Stat_MoveSpeed = stat2;
		character.Stat_HP = stat3;
		character.Stat_Life = stat4;

		text_attack.text = character.Stat_Attack.ToString();
		text_movespeed.text = character.Stat_MoveSpeed.ToString();
		text_hp.text = character.Stat_HP.ToString();
		text_life.text = character.Stat_Life.ToString();
	}

	public void Button_SkillLvUp(int idx)
	{
		 if (gold >= Skill.skill_Lv[idx])
		 {
			 gold -= Skill.skill_Lv[idx];
			 Skill.skill_Lv[idx]++;
			 text_gold.text = gold.ToString();
			 text_skill_lv[idx].text = Skill.skill_Lv[idx].ToString();
		 }
	}
}
