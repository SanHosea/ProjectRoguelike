using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackArea : MonoBehaviour {
	private bool isIn;
	private float checkTime;
	public bool IsIn 
	{
		get { return isIn; }
		set { isIn = value; }
	}
	public float CheckTime
	{
		get { return checkTime; }
		set { checkTime = value; }
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Character"))
		{
			isIn = true;
			checkTime = 0.0f;
		}
	}

	private void OnTriggerStay2D(Collider2D other) 
	{
		if (other.CompareTag("Character"))
		{
			isIn = true;
			checkTime += Time.deltaTime;
		}
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if (other.CompareTag("Character"))
		{
			isIn = false;
		}
	}
}
