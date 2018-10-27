using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour {
	public int maxHP;
	public int curHP;

	public int arrange;
	public int cooltime;
	public int attack;

	[SerializeField]
	private GameObject AttackArea;
	
	public void Initialize()
	{
		maxHP = 15;
		curHP = maxHP;

		arrange = 1;
		cooltime = 1;
		attack = 1;

		StartCoroutine(Attack());
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.name == "AttackArea")
		{
			CharacterBase character = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterBase>();
			curHP -= character.AttackPerDamage;
			if (curHP <= 0)
				Destroy(gameObject);
		}
	}

	private IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(cooltime);
			AttackArea.SetActive(true);
			AttackArea.transform.localScale = new Vector3(arrange + 1, arrange + 1, 1);
			yield return new WaitForSeconds(0.1f);
			AttackArea.SetActive(false);
		}
	}
}
