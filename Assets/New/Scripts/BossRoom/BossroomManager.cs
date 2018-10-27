using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스킬 이펙트 넣어줘야 함(회복, 쉴드)
// 보스룸 UI 제작
// 보스 제작 및 스킬 패턴 제작
// 캐릭터 근거리, 원거리 공격 제작
public class BossroomManager : MonoBehaviour 
{
	[SerializeField]
	private CharacterBase character;
	[SerializeField]
	private MonsterBase monster;
	[Header("[UI Object]")]
	[SerializeField]
	private Text curHP;
	[SerializeField]
	private Text curLife;
	[Header("[Monster UI]")]
	[SerializeField]
	private InputField monsterHP;
	[SerializeField]
	private InputField monsterSpeed;
	[SerializeField]
	private Text curState;
	[Header ("- Skill")]
	[SerializeField]
	private NearAttack skill1;
	[SerializeField]
	private AroundAttack skill2;
	[SerializeField]
	private RushAttack skill3;

	[Header("- Patter 1")]
	[SerializeField]
	private InputField pt1_x;
	[SerializeField]
	private InputField pt1_y;
	[SerializeField]
	private InputField pt1_CoolTime;
	[Header("- Patter 2")]
	[SerializeField]
	private InputField pt2_x;
	[SerializeField]
	private InputField pt2_y;
	[SerializeField]
	private InputField pt2_CoolTime;
	[Header("- Patter 3")]
	[SerializeField]
	private InputField pt3_x;
	[SerializeField]
	private InputField pt3_y;
	[SerializeField]
	private InputField pt3_CoolTime;
	[SerializeField]
	private InputField pt3_moveSpeed;

	void Start () 
	{
		character.Initialize(new Vector2(0, -2.8f));
		monster.Initialize(Vector2.zero, 40.0f, character);	

		monsterHP.text = monster.CurHP.ToString();
		monsterSpeed.text = monster.MoveSpeed.ToString();
		curState.text = monster.CurState.ToString();
		
		pt1_x.text = skill1.attackArea.gameObject.transform.localScale.x.ToString();
		pt1_y.text = skill1.attackArea.gameObject.transform.localScale.y.ToString();
		pt1_CoolTime.text = skill1.CoolTime.ToString();

		pt2_x.text = skill2.attackArea.gameObject.transform.localScale.x.ToString();
		pt2_y.text = skill2.attackArea.gameObject.transform.localScale.y.ToString();
		pt2_CoolTime.text = skill2.CoolTime.ToString();

		pt3_x.text = skill3.attackArea.gameObject.transform.localScale.x.ToString();
		pt3_y.text = skill3.attackArea.gameObject.transform.localScale.y.ToString();
		pt3_CoolTime.text = skill3.CoolTime.ToString();
		pt3_moveSpeed.text = skill3.rushSpeed.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		character.Updated();	
		monster.Updated();

		curHP.text = "HP : " + character.CurHP.ToString();
		curLife.text = "LIFE : " + character.CurLife.ToString();

		monsterHP.text = monster.CurHP.ToString();
		monster.MoveSpeed = float.Parse(monsterSpeed.text);
		curState.text = monster.CurState.ToString();

		skill1.attackArea.gameObject.transform.localScale = new Vector3(float.Parse(pt1_x.text), 
																		float.Parse(pt1_y.text), 1);
		skill1.CoolTime = float.Parse(pt1_CoolTime.text);

		skill2.attackArea.gameObject.transform.localScale = new Vector3(float.Parse(pt2_x.text), 
																		float.Parse(pt2_y.text), 1);
		skill2.CoolTime = float.Parse(pt2_CoolTime.text);

		skill3.attackArea.gameObject.transform.localScale = new Vector3(float.Parse(pt3_x.text), 
																		float.Parse(pt3_y.text), 1);
		skill3.CoolTime = float.Parse(pt2_CoolTime.text);
		skill3.rushSpeed = float.Parse(pt3_moveSpeed.text);
	}
}
