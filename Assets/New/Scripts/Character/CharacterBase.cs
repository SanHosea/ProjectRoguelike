using UnityEngine;
using System;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour 
{
    [Flags]
    public enum DirState
    {
        NONE = 0,
        UP = 1,
        DOWN = 2,
        LEFT = 4,
        RIGHT = 8
    }

    [SerializeField]
    protected SkillBase[] skill;
    protected Dictionary<string, SkillBase> skills = new Dictionary<string, SkillBase>();
    // HP, LIFE
    private static int curHP = 0;
	private static int maxHP = 0;
	private static int curLife = 50;
	private static int maxLife = 50;

    public int CurHP
    {
        get { return curHP; }
        set { curHP = value; }
    }
    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }
    public int CurLife 
    {
        get { return curLife; }
        set { curLife = value;}
    }
    public int MaxLife 
    {
        get { return maxLife; }
        set { maxLife = value;}
    }
    // Status
    protected int _generation;
	protected int _attack;
	protected int _hp;
	protected int _life;
	protected int _totalPoint;
    protected float _moveSpeed;

    public int Stat_Generation 
    {
        get { return _generation; }
        set { _generation = value; }
    }
    public int Stat_Attack 
    {
        get { return _attack; }
        set { _attack = value; }
    }
    public int Stat_HP
    {
        get { return _hp; }
        set { _hp = value; }
    }
    public int Stat_Life 
    {
        get { return _life; }
        set { _life = value; }
    }
    public float Stat_MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    public int Stat_TotalPoint 
    {
        get { return _totalPoint; }
        set { _totalPoint = value; }
    }

    protected int _attackPerDamage;
    protected int _lifePerTime;
    protected int _hpPerCount;
    public int AttackPerDamage
    {
        get { return _attackPerDamage; }
        set { _attackPerDamage = value; }
    }
    public int LifePerTime 
    {
        get { return _lifePerTime; }
        set { _lifePerTime = value; }
    }
    public int HpPerCount
    {
        get { return _hpPerCount; }
        set { _hpPerCount = value; }
    }
    // 캐릭터가 쉴드 스킬을 썼는지 안썼는지
    protected bool hasShield;
    public bool HasShield
    {
        get { return hasShield; }
        set { hasShield = value; }
    }
    [SerializeField]
    protected GameObject charImg;
    [SerializeField]
    protected UserAttackArea attackArea;
    protected DirState dirState;
    protected Vector2 attackVec;
    public Vector2 AttackVec
    {
        get { return attackVec; }
        set { attackVec = value; }
    }

    [SerializeField]
    protected Animator animator;

    public virtual void Initialize(Vector2 pos) { }

	public virtual void Updated() 
    {
        if (Input.GetKeyDown(KeyCode.A))
            Skill_Key_A();

        else if (Input.GetKeyDown(KeyCode.S))
            Skill_Key_S();

        else if (Input.GetKeyDown(KeyCode.D))
            Skill_Key_D();

        else if (Input.GetKeyDown(KeyCode.Z))
            Attack();

        else if (Input.GetKeyDown(KeyCode.F))
            Skill_Key_F();

        else if (Input.GetKeyDown(KeyCode.Escape))
            Avoid();

        SetDirState();
        Move();
        DrawAnim();
    }
    // 이동
    public virtual void Move() 
    {
        switch (dirState)
        {
            case DirState.UP:
                transform.position = new Vector2(transform.position.x,
                                                 transform.position.y + _moveSpeed * Time.deltaTime);
                attackVec = Vector2.up;
                break;

            case DirState.DOWN:
                transform.position = new Vector2(transform.position.x,
                                                 transform.position.y - _moveSpeed * Time.deltaTime);
                attackVec = -Vector2.up;
                break;

            case DirState.LEFT:
                transform.position = new Vector2(transform.position.x - _moveSpeed * Time.deltaTime,
                                                 transform.position.y);
                attackVec = -Vector2.right;
                break;

            case DirState.RIGHT:
                transform.position = new Vector2(transform.position.x + _moveSpeed * Time.deltaTime,
                                                 transform.position.y);
                attackVec = Vector2.right;
                break;

            case DirState.UP | DirState.LEFT:
                transform.position = new Vector2(transform.position.x - _moveSpeed * Time.deltaTime,
                                                 transform.position.y + _moveSpeed * Time.deltaTime);

                attackVec = -Vector2.right;
                break;

            case DirState.UP | DirState.RIGHT:
                transform.position = new Vector2(transform.position.x + _moveSpeed * Time.deltaTime,
                                                 transform.position.y + _moveSpeed * Time.deltaTime);
                attackVec = Vector2.right;
                break;

            case DirState.DOWN | DirState.LEFT:
                transform.position = new Vector2(transform.position.x - _moveSpeed * Time.deltaTime,
                                                 transform.position.y - _moveSpeed * Time.deltaTime);
                attackVec = -Vector2.right;
                break;

            case DirState.DOWN | DirState.RIGHT:
                transform.position = new Vector2(transform.position.x + _moveSpeed * Time.deltaTime,
                                                 transform.position.y - _moveSpeed * Time.deltaTime);
                attackVec = Vector2.right;
                break;
        }
    }
    public void SetDirState() 
    {
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            dirState |= DirState.UP;
            dirState &= ~DirState.DOWN;
        }

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            dirState &= ~DirState.UP;
            dirState |= DirState.DOWN;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            dirState |= DirState.RIGHT;
            dirState &= ~DirState.LEFT;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            dirState &= ~DirState.RIGHT;
            dirState |= DirState.LEFT;
        }


        if (Input.GetKeyUp(KeyCode.UpArrow))
            dirState &= ~DirState.UP;

        if (Input.GetKeyUp(KeyCode.DownArrow))
            dirState &= ~DirState.DOWN;

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            dirState &= ~DirState.LEFT;

        if (Input.GetKeyUp(KeyCode.RightArrow))
            dirState &= ~DirState.RIGHT;
    }
	// 공격
	public virtual void Attack() {}
	// 회피
	public virtual void Avoid() {}
    // 스킬
    public virtual void Skill_Key_A() { }
	public virtual void Skill_Key_S() {}
	public virtual void Skill_Key_D() {}
    public virtual void Skill_Key_F() {}
    // 상호작용
	public virtual void Interaction() {}
    // Character Animation
    public virtual void DrawAnim() {}
}
