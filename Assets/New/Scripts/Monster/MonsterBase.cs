using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 상태 구별
public enum STATE 
{
	NONE, TRACE, ATTACK
}
public class MonsterBase : MonoBehaviour {
	[SerializeField]
	protected SkillBase[] skill;
	protected Dictionary<string, SkillBase> skills = new Dictionary<string, SkillBase>();

	protected CharacterBase character;
	// 몬스터 HP
	protected float curHP;
	protected float maxHP;
	protected STATE curState;

	public float CurHP
	{
		get { return curHP; }
		set { curHP = value; }
	}
	public float MaxHP
	{
		get { return maxHP; }
		set { maxHP = value; }
	}
	public STATE CurState
	{
		get { return curState; }
		set { curState = value; }
	}

	protected float moveSpeed;
	public float MoveSpeed 
	{
		get { return moveSpeed; }
		set { moveSpeed = value; }
	}
	private bool b_TouchCharacter = false;
	public bool bTouchCharacter 
	{
		get { return b_TouchCharacter; }
		set { b_TouchCharacter = value; }
	}

	public virtual void Initialize(Vector2 pos, float hp, CharacterBase _character) {}
	public virtual void Updated() {}

	public void Move(GameObject target)
	{
		Vector2 offset = target.transform.position - transform.position;
		offset.Normalize();
		transform.position = new Vector2(transform.position.x + offset.x * moveSpeed * Time.deltaTime,
										 transform.position.y + offset.y * moveSpeed * Time.deltaTime);
	}
}
