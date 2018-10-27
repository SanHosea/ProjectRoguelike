using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBoss : BossBase
{
    public enum MoveState { NONE, RIGHT, LEFT, UP, DOWN }

    public MoveState curState;
    private bool bStartPattern1;
    private bool bStartPattern2;
    private bool bStartPattern3;
    private bool bStartPattern4;
    public CharacterBase character;
    public float speed;     // 스피드
    private float time_1;
    private float time_2;
    private float time_3;
    private float time_4;

    private bool bAttack;

    private Vector2 originPos;
    private Vector2 originCharacterPos;

    [SerializeField]
    private AttackArea_Boss[] attackArea_patterns;


    public override void Initialize(CharacterBase _character)
    {
        max_hp = 100;
        bAttack = false;
        //cur_hp = 100;
        bStartPattern1 = false;
        bStartPattern2 = false;
        bStartPattern3 = false;
        bStartPattern4 = false;
        character = _character;
        time_1 = 0;
        time_2 = 0;
        time_3 = 0;
        time_4 = 0;

        for (int i = 0; i < attackArea_patterns.Length; i++)
        {
            attackArea_patterns[i].gameObject.SetActive(false);
        }

        curState = MoveState.NONE;
    }

    public override void Updated()
    {
        if (cur_hp >= 76)
        {
            Pattern_1();
        }
        else if (cur_hp < 76 && cur_hp >= 51)
        {
            Pattern_2();
        }
        else if (cur_hp < 51 && cur_hp >= 26)
        {
            Pattern_3();
        }
        else if (cur_hp < 26)
        {
            Pattern_4();
        }

        if (cur_hp <= 0) cur_hp = 0; 
    }

    // 1. 내려치기
    public void Pattern_1()
    {
        // 캐릭터와 충돌 안했을 때
        if(!bStartPattern1)
        {
            Vector3 offset = character.transform.position - transform.position;

            // 움직이는 방향 체크 -> 공격 방향 설정 위함
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
            {
                if (offset.x > 0)
                    curState = MoveState.RIGHT;
                else
                    curState = MoveState.LEFT;
            }
            else
            {
                if (offset.y > 0)
                    curState = MoveState.UP;
                else
                    curState = MoveState.DOWN;
            }

            offset.Normalize();
            transform.position += offset * speed * Time.deltaTime;
            time_1 = 0;
        }
        // 충돌하고 나서
        else
        {
            // 시간 측정
            time_1 += Time.deltaTime;

            // 3초 지났을 때 -> 1초로 수정
            if (time_1 > 1)
            {
                // 공격 범위 생성
                attackArea_patterns[0].gameObject.SetActive(true);

                // 캐릭터가 부딪힌 방향 쪽으로 범위와 포지션 재설정
                switch(curState)
                {
                    case MoveState.DOWN:
                        attackArea_patterns[0].transform.localScale = new Vector3(1.5f, 2, 1);
                        attackArea_patterns[0].transform.position = new Vector2(transform.position.x, transform.position.y - 1.75f);
                        break;

                    case MoveState.UP:
                        attackArea_patterns[0].transform.localScale = new Vector3(1.5f, 2, 1);
                        attackArea_patterns[0].transform.position = new Vector2(transform.position.x, transform.position.y + 1.75f);
                        break;

                    case MoveState.LEFT:
                        attackArea_patterns[0].transform.localScale = new Vector3(2, 1.5f, 1);
                        attackArea_patterns[0].transform.position = new Vector2(transform.position.x - 1.75f, transform.position.y);
                        break;

                    case MoveState.RIGHT:
                        attackArea_patterns[0].transform.localScale = new Vector3(2, 1.5f, 1);
                        attackArea_patterns[0].transform.position = new Vector2(transform.position.x + 1.75f, transform.position.y);
                        break;
                }
                
            }

            // 공격 범위 생성 후 1초 지났을 때 
            if (time_1 > 2)
            {
                // 범위 안에 캐릭터 있으면
                // 캐릭터 현재 체력 20 감소
                if (attackArea_patterns[0].bCanAttack)
                {
                    character.cur_hp -= 20;
                }
                attackArea_patterns[0].gameObject.SetActive(false);
                bStartPattern1 = false;
            }
        }
    }

    // 2. 주위 공격
    public void Pattern_2()
    {
        // 충돌체크 영역은 켜고 공격 범위는 꺼둔 상태
        attackArea_patterns[1].gameObject.SetActive(true);
        attackArea_patterns[1].transform.position = transform.position;

        // 영역 내에 있을 경우
        if (attackArea_patterns[1].bCanAttack && !bStartPattern2)
        {
            // 시간 측정
            time_2 += Time.deltaTime;

            // 1.5초 지났을 때
            if(time_2 > 1.5)
            {
                attackArea_patterns[1].GetComponent<SpriteRenderer>().enabled = true;
                bStartPattern2 = true;
            }
        }
        else if(!attackArea_patterns[1].bCanAttack && !bStartPattern2)
        {
            // 공격 범위는 꺼둠
            attackArea_patterns[1].GetComponent<SpriteRenderer>().enabled = false;
            time_2 = 0;
        }

        if(bStartPattern2)
        {
            time_2 += Time.deltaTime;
            if (time_2 > 3)
            {
                attackArea_patterns[1].GetComponent<SpriteRenderer>().enabled = false;

                if(attackArea_patterns[1].bCanAttack)
                    character.cur_hp -= 20;

                time_2 = 0;
                bStartPattern2 = false;
            }
        }
    }

    // 3. 투척 공격
    public void Pattern_3()
    {
        Vector3 offset = character.transform.position - transform.position;

        if (!bStartPattern3)
        {
            // 캐릭터와의 거리 계산해서 범위 밖에 있을 경우
            if (offset.magnitude >= 3)
            {
                time_3 += Time.deltaTime;

                // 1.5초 지났을 때
                if (time_3 >= 1.5)
                {
                    if (!attackArea_patterns[2].gameObject.activeInHierarchy)
                    {
                        // 투석 위치 캐릭터 위치로 변경
                        attackArea_patterns[2].transform.position = character.transform.position;
                        // 공격 범위 표시
                        attackArea_patterns[2].gameObject.SetActive(true);
                        // 공격 패턴 시작
                        bStartPattern3 = true;
                    }
                }
            }
            else
            {
                time_3 = 0;
            }

        }

        else if (bStartPattern3)
        {
            time_3 += Time.deltaTime;

            // 공격 범위 표시되고 1초 지났을 때
            if (time_3 >= 2.5)
            {
                // 캐릭터가 그 범위 안에 아직 있다면 체력 감소
                if (attackArea_patterns[2].bCanAttack)
                    character.cur_hp -= 20;

                // 공격 범위 다시 끔
                attackArea_patterns[2].gameObject.SetActive(false);
                time_3 = 0;
                bStartPattern3 = false;
            }
        }
    }

    // 4. 돌진 공격
    public void Pattern_4()
    {
        Vector3 offset = character.transform.position - transform.position;

        if (!bStartPattern4)
        {
            // 거리 밖에 있을 때
            if (offset.magnitude > 2)
            {
                time_4 += Time.deltaTime;

                // 3초 넘어가면
                if (time_4 >= 3)
                {
                    originPos = transform.position;
                    originCharacterPos = character.transform.position;
                    // 패턴 시작
                    bStartPattern4 = true;
                }
            }

            else
            {
                time_4 = 0;
            }
        }
 
        else if (bStartPattern4)
        {
            // 돌진 속도
            time_4 += Time.deltaTime * 2;
            transform.position = Vector2.Lerp(originPos, originCharacterPos, time_4 - 5);
            attackArea_patterns[3].gameObject.SetActive(true);
            attackArea_patterns[3].transform.position = transform.position;

            // 범위 내에 부딪혔을 때
            if (attackArea_patterns[3].bCanAttack)
            {
                character.cur_hp -= 20;
                attackArea_patterns[3].bCanAttack = false;
                attackArea_patterns[3].gameObject.SetActive(false);
                bStartPattern4 = false;
            }

            if (time_4 >= 6)
            {
                time_4 = 0;
                bStartPattern4 = false;
                attackArea_patterns[3].gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            bStartPattern1 = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "AttackArea")
        {
            if(cur_hp <= 100)
                cur_hp -= 1;
        }
    }
}
