using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAttackArea : MonoBehaviour {
	private float damage;
	public void Initialize(float _damage)
	{
		damage = _damage;
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Monster"))
		{
			// 몬스터 슈퍼클래스에 있는 HP를 참조해 체력 감소 시킴
			MonsterBase clone = other.GetComponent<MonsterBase>();
			clone.CurHP -= damage;
		}
	}
}
