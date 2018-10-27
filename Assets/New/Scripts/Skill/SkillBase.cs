using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour {
	// 스킬 쿨타임
	protected float coolTime;
	public float CoolTime 
	{
		get { return coolTime; }
		set { coolTime = value; }
	}
	private float curTime;
	protected float CurTime
	{
		get { return curTime; }
		set { curTime = value; }
	}
	// 스킬 레벨
	protected int level;
	public int Level
	{
		get { return level; }
		set { level = value; }
	}

	[SerializeField]
	protected GameObject effectObj;
	// 스킬 쿨타임이 나중에 바뀔 수도 있을 거 같은데...
	// 다른 캐릭터가 좀 더 짧은 스킬 쿨타임을 가질 수도 있고...
	// 이거 캐릭터나 몬스터에서 쿨타임을 정해줘야하나 아니면 스킬 컴포넌트에서 해줘야 하나..
	public virtual void Initialize() 
	{
		// 쿨타임만큼 초기에 시간을 설정해줘야 
		// 바로 스킬 사용할 수 있음.
		curTime = coolTime;
		if (effectObj != null) effectObj.SetActive(false);
	}
	public virtual void Updated() 
	{
		curTime += Time.deltaTime;
	}
	public virtual void FireSkill(CharacterBase character)
	{

	}
	protected bool CanFireSkill()
	{
		if (curTime >= coolTime) return true;
		else return false;
	}
	protected IEnumerator ObjectOnOff(GameObject obj, float time)
	{
		obj.SetActive(true);
		yield return new WaitForSeconds(time);
		obj.SetActive(false);
	}
}
