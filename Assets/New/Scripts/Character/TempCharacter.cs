using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TempCharacter : CharacterBase 
{
    public override void Initialize(Vector2 pos) 
    {
        // 캐릭터 포지션, 움직임 상태 초기화
        transform.position = pos;
        attackArea.gameObject.SetActive(false);

        dirState &= ~DirState.UP;
        dirState &= ~DirState.DOWN;
        dirState &= ~DirState.LEFT;
        dirState &= ~DirState.RIGHT;

        // 스킬 등록 및 초기화
        skills.Add("skill_1", skill[0]);
        skills.Add("skill_2", skill[1]);
        skills.Add("skill_3", skill[2]);
        skills.Add("skill_4", skill[3]);

        skills["skill_1"].Initialize();
        skills["skill_2"].Initialize();
        skills["skill_3"].Initialize();
        skills["skill_4"].Initialize();

        // 캐릭터 스탯 초기화
        _generation = 1;
        _attack = 3;
        _hp = 3;
        _life = 3;
        _moveSpeed = 3;
        _totalPoint = 12;
        
        _attackPerDamage = 1 * _attack;
        _lifePerTime = 60;
        _hpPerCount = 1;

        MaxHP = _hp * _hpPerCount;
        CurHP = MaxHP;
        MaxLife = _life * _lifePerTime;
        CurLife = MaxLife;

        hasShield = false;
        attackArea.Initialize(_attackPerDamage);

        // 캐릭터 애니메이션 초기화
        animator.SetBool("isMove", false);

        // 캐릭터 라이프 감소 코루틴(1초에 1씩 감소)
        StartCoroutine(_Life());
    }

    public override void Updated() 
    {
        base.Updated();

        // 스킬 업데이트
        // 딕셔너리에 들어가는 키값들을 따로 분류해서
        // 그 키값들을 사용하는 것이 좀 더 효율적일 듯 하다.
        // 스킬 리스트라는 클래스를 만들어서 변수 관리하는 게 어떨까 싶기도 함.
        skills["skill_1"].Updated();
        skills["skill_2"].Updated();
        skills["skill_3"].Updated();
        skills["skill_4"].Updated();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {
        StartCoroutine(_Attack());
    }

    public override void Avoid()
    {

    }

    public override void Skill_Key_A()
    {
        // 근거리
        skills["skill_1"].FireSkill(this);
    }

    public override void Skill_Key_S()
    {
        // 원거리
        skills["skill_2"].FireSkill(this);
    }

    public override void Skill_Key_D()
    {
        // 쉴드
        skills["skill_3"].FireSkill(this);
    }

    public override void Skill_Key_F() 
    {
        // 회복
        skills["skill_4"].FireSkill(this);
    }

    public override void Interaction()
    {
        base.Interaction();
    }

    public override void DrawAnim()
    {
        if (dirState == DirState.NONE)
            animator.SetBool("isMove", false);

        else
        {
            animator.SetBool("isMove", true);

            switch(dirState)
            {
                case DirState.LEFT:
                case DirState.UP | DirState.LEFT:
                case DirState.DOWN | DirState.LEFT:
                    charImg.transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                    break;

                case DirState.RIGHT:
                case DirState.UP | DirState.RIGHT:
                case DirState.DOWN | DirState.RIGHT:
                    charImg.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    break;
            }
        }
    }
    private IEnumerator _Attack()
    {
        attackArea.transform.localPosition = attackVec;
        attackArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackArea.gameObject.SetActive(false);
    }

    private IEnumerator _Life()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            CurLife--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.name == "ScarecrowAttackArea")
        {
            Scarecrow clone = GameObject.FindWithTag("Enemy").GetComponent<Scarecrow>();
            CurHP -= clone.attack;
        }
    }
}
