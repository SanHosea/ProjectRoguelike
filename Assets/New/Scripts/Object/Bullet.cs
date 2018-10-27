using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private float damage;
	private Vector2 attackVec;
	private float speed = 10.0f;
	public float Damage
	{
		get { return damage; }
		set { damage = value; }
	}

	public void Initialize(float _damage, Vector2 _attackVec)
	{
		damage = _damage;
		attackVec = _attackVec;

		StartCoroutine(Fire());
	}	
	
	private IEnumerator Fire()
	{
		float time = 0;
		while(true)
		{
			time += Time.deltaTime;
			transform.position = new Vector2(transform.position.x + attackVec.x * speed * Time.deltaTime,  
											 transform.position.y + attackVec.y * speed * Time.deltaTime);

			if (time >= 3.0f)
			{
				Destroy(this.gameObject);
			}
			yield return null;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Monster")) 
		{
			// 몬스터 슈퍼클래스에 있는 HP를 참조해 체력 감소 시킴
			MonsterBase clone = other.GetComponent<MonsterBase>();
			clone.CurHP -= damage;
			Destroy(gameObject);
		}	
	}
}
