using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharacter : CharacterBase
{
    [SerializeField]
    private GameObject attackarea;
    private bool isAttack;
    [SerializeField]
    private TextMesh curState;

    public override void Intialize()
    {
        max_hp = 100;
        max_life = 50;
        cur_hp = 100;
        cur_life = 50;

        attackarea.SetActive(false);
        isAttack = false;
        curState.text = "정지";

        moveState |= MoveState.NONE;
        InvokeRepeating("Discount_Life", 1, 5);
    }

    public override void Updated()
    {
        Move();
        CharacterAnimation();
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (!isAttack && Input.GetKey(KeyCode.Z))
            Attack();
    }

    public override void Move()
    {
        base.Move();

        switch (moveState)
        {
            case MoveState.UP:
                transform.position = new Vector2(transform.position.x,
                                                 transform.position.y + speed * Time.deltaTime);
                attackVec = Vector2.up;
                break;

            case MoveState.DOWN:
                transform.position = new Vector2(transform.position.x,
                                                 transform.position.y - speed * Time.deltaTime);
                attackVec = -Vector2.up;
                break;

            case MoveState.LEFT:
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,
                                                 transform.position.y);
                attackVec = -Vector2.right;
                break;

            case MoveState.RIGHT:
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime,
                                                 transform.position.y);
                attackVec = Vector2.right;
                break;

            case MoveState.UP | MoveState.LEFT:
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,
                                                 transform.position.y + speed * Time.deltaTime);

                attackVec = -Vector2.right;
                break;

            case MoveState.UP | MoveState.RIGHT:
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime,
                                                 transform.position.y + speed * Time.deltaTime);
                attackVec = Vector2.right;
                break;

            case MoveState.DOWN | MoveState.LEFT:
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,
                                                 transform.position.y - speed * Time.deltaTime);
                attackVec = -Vector2.right;
                break;

            case MoveState.DOWN | MoveState.RIGHT:
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime,
                                                 transform.position.y - speed * Time.deltaTime);
                attackVec = Vector2.right;
                break;
        }
    }

    public override void Attack()
    {
        attackarea.transform.localPosition = new Vector2(transform.position.x + attackVec.x * 0.32f, transform.position.y + attackVec.y * 0.32f);
        attackarea.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, attackVec.x * 90));

        StartCoroutine(_Attack());
    }

    public override void CharacterAnimation()
    {
        switch (moveState)
        {
            case MoveState.NONE:
                curState.text = "정지";
                break;

            case MoveState.UP:
                curState.text = "상";
                break;

            case MoveState.DOWN:
                curState.text = "하";
                break;

            case MoveState.LEFT:
                curState.text = "좌";
                break;

            case MoveState.RIGHT:
                curState.text = "우";
                break;

            case MoveState.UP | MoveState.LEFT:
                curState.text = "좌상";
                break;

            case MoveState.UP | MoveState.RIGHT:
                curState.text = "우상";
                break;

            case MoveState.DOWN | MoveState.LEFT:
                curState.text = "좌하";
                break;

            case MoveState.DOWN | MoveState.RIGHT:
                curState.text = "우하";
                break;
        }
    }

    private IEnumerator _Attack()
    {
        isAttack = true;
        attackarea.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        attackarea.SetActive(false);
        isAttack = false;
    }

    public void Discount_Life()
    {
        cur_life--;

        if (cur_life <= 0)
        {
            cur_life = 0;
            CancelInvoke("Discount_Life");
        }
    }
}
