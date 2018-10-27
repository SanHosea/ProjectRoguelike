using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestRoomManager : MonoBehaviour 
{
	[SerializeField]
	private CharacterBase character;

	#region UI Object
	[Header("Input Field")]
	[SerializeField]
	private InputField input_attack;
	[SerializeField]
	private InputField input_moveSpeed;
	[SerializeField]
	private InputField input_hp;
	[SerializeField]
	private InputField input_life;
	[SerializeField]
	private InputField input_damage;
	[SerializeField]
	private InputField input_hpPerCount;
	[SerializeField]
	private InputField input_lifePerTime;
	[SerializeField]
	private GameObject ui_Scarecrow;
	[SerializeField]
	private Image img_scarecrow_hp;
	[SerializeField]
	private Text txt_scarecrow_hp;
	[SerializeField]
	private Text txt_curHP;
	[SerializeField]
	private Text txt_curLife;
	#endregion

	[Header("Scarecrow")]
	[SerializeField]
	private Scarecrow scarecrow;
	private Scarecrow curScareCrow;
	[SerializeField]
	private InputField input_scarecrow_attack;
	[SerializeField]
	private InputField input_scarecrow_arrange;
	[SerializeField]
	private InputField input_scarecrow_cooltime;

	private void Start() 
	{
		character.Initialize(new Vector2(0, -1.8f));	
	
		input_attack.text = character.Stat_Attack.ToString();
		input_moveSpeed.text = character.Stat_MoveSpeed.ToString();
		input_hp.text = character.Stat_HP.ToString();
		input_life.text = character.Stat_Life.ToString();

		input_damage.text = character.AttackPerDamage.ToString();
		input_hpPerCount.text = character.HpPerCount.ToString();
		input_lifePerTime.text = character.LifePerTime.ToString();

		txt_curHP.text = "CurHP = " + character.CurHP.ToString();
		txt_curLife.text = "CurLife = " + character.CurLife.ToString();
	}

	private void Update() 
	{
		if (character != null)
		{
			if (character.CurHP > 0)
			{
				character.Updated();	

				character.Stat_Attack = int.Parse(input_attack.text);
				character.Stat_MoveSpeed = int.Parse(input_moveSpeed.text);
				character.Stat_HP = int.Parse(input_hp.text);
				character.Stat_Life = int.Parse(input_life.text);

				character.AttackPerDamage = int.Parse(input_damage.text);
				character.HpPerCount = int.Parse(input_hpPerCount.text);
				character.LifePerTime = int.Parse(input_lifePerTime.text);

				txt_curHP.text = "CurHP = " + character.CurHP.ToString();
				txt_curLife.text = "CurLife = " + character.CurLife.ToString();
			}
			else 
			{
				Destroy(character.gameObject);
			}
		}

		

		if (curScareCrow != null)
		{
			ui_Scarecrow.SetActive(true);
			img_scarecrow_hp.fillAmount = (float)curScareCrow.curHP / (float)curScareCrow.maxHP;
			txt_scarecrow_hp.text = curScareCrow.curHP + " / " + curScareCrow.maxHP;

			curScareCrow.arrange = int.Parse(input_scarecrow_arrange.text);
			curScareCrow.attack = int.Parse(input_scarecrow_attack.text);
			curScareCrow.cooltime = int.Parse(input_scarecrow_cooltime.text);
		}
		else
		{
			ui_Scarecrow.SetActive(false);
		}
	}

	public void Button_Create_Scarecrow()
	{
		if (curScareCrow == null)
		{
			Scarecrow clone = Instantiate(scarecrow);
			clone.transform.position = Vector2.zero;
			clone.Initialize();
			curScareCrow = clone;

			input_scarecrow_arrange.text = curScareCrow.arrange.ToString();
			input_scarecrow_attack.text = curScareCrow.attack.ToString();
			input_scarecrow_cooltime.text = curScareCrow.cooltime.ToString();
		}
	}
}
