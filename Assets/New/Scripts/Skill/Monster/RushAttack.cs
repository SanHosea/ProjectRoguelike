using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttack : SkillBase {
	[SerializeField]
	private MonsterBase monster;
	
	private CharacterBase character;
	[SerializeField]
	public MonsterAttackArea attackArea;

	private float checkTime;
	private float distance;
	// 돌진하는 방향
	private Vector2 rushDir;
	private Vector3 characterPos;
	private Vector3 monsterPos;
	// 돌진 스피드
	public float rushSpeed = 3;
	public override void Initialize()
	{
		coolTime = 3.0f;
		distance = 1.0f;
		base.Initialize();
		character = GameObject.FindWithTag("Character").GetComponent<CharacterBase>();
		attackArea.gameObject.SetActive(false);
	}
	public override void Updated() 
	{
		base.Updated();	

		if (Mathf.Abs((Vector2.SqrMagnitude(character.transform.position - monster.transform.position))) >= distance 
					&& monster.CurState == STATE.TRACE)
		{
			checkTime += Time.deltaTime;
			
			if (checkTime >= 5.0f)
			{
				characterPos = character.transform.position;
				monsterPos = monster.transform.position;
				monster.CurState = STATE.ATTACK;
			}
		}

		else 
		{
			checkTime = 0;
		}
	}
	public override void FireSkill(CharacterBase character)
	{
		if (CanFireSkill())
		{
			StartCoroutine(Attack(character));
			CurTime = 0;
			checkTime = 0;
		}
	}

	// 1. 돌진 방향이 이상함
	// 2. 돌진이 끝난 이후에 판정처리가 들어가서 느림
	public IEnumerator Attack(CharacterBase character)
	{
		rushDir = characterPos - monsterPos;
		float theta = ContAngle(-monster.transform.up, rushDir);	
		// 공격범위 표시 
		attackArea.gameObject.SetActive(true);
		// 캐릭터가 몬스터 왼쪽에 있을 경우
		// 회전각에 마이너스 값을 곱해줘서 반대방향으로 올바르게 돌 수 있도록 한다.
		if (characterPos.x < monsterPos.x)
		{
			theta *= -1;
		}

		attackArea.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, theta));
		// 1초 후 공격
		yield return new WaitForSeconds(1.0f);
		// 돌진
		// 여기서부터 만들어야 함
		attackArea.gameObject.SetActive(false);
		StartCoroutine(Rush(monsterPos, rushDir.normalized * 3));
	}

	private IEnumerator Rush(Vector2 start, Vector2 end)
	{
		float time = 0;
		while (true)
		{
			time += Time.deltaTime * rushSpeed;
			monster.transform.position = Vector2.Lerp(start, end, time);

			if (time >= 1.0f)
			{
				monster.CurState = STATE.TRACE;
				break;
			}

			yield return null;
		}
	}

	// 두 벡터의 회전각은 0 ~ 180 사이의 값밖에 나오지 않는다.
	// 왼쪽이든 오른쪽이든 0 ~ 180만 나온다는 이야기.
	private float ContAngle(Vector3 fwd, Vector3 targetDir) 
	{
		float angle = Vector3.Angle(fwd, targetDir);

		if (AngleDir(fwd, targetDir, Vector3.up) == -1)
		{
			angle = 360.0f - angle;
			if (angle > 359.9999f)
				angle -= 360.0f;
			
			return angle;
		}
		else 
			return angle;
	}

	private int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) 
	{
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0.0f)
			return 1;
		else if (dir < 0.0f) 
			return -1;
		else 
			return 0;
	}
}
