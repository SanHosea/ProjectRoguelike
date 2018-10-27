using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[FlagsAttribute]
public enum MoveState
{
    NONE = 0,
    UP = 1,
    DOWN = 2,
    LEFT = 4,
    RIGHT = 8
}

public class CharacterBase : MonoBehaviour
{
    public float speed;
    public MoveState moveState;
    public Vector2 attackVec;

    public float max_hp;
    public float max_life;

    public float cur_hp;
    public float cur_life;

    public virtual void Intialize() { }
    public virtual void Updated() { }

    public virtual void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveState |= MoveState.UP;
            moveState &= ~MoveState.DOWN;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {           
            moveState &= ~MoveState.UP;
            moveState |= MoveState.DOWN;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {            
            moveState |= MoveState.RIGHT;
            moveState &= ~MoveState.LEFT;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveState &= ~MoveState.RIGHT;
            moveState |= MoveState.LEFT;
        }


        if (Input.GetKeyUp(KeyCode.UpArrow))
            moveState &= ~MoveState.UP;

        if (Input.GetKeyUp(KeyCode.DownArrow))
            moveState &= ~MoveState.DOWN;

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveState &= ~MoveState.LEFT;

        if (Input.GetKeyUp(KeyCode.RightArrow))
            moveState &= ~MoveState.RIGHT;
    }
    public virtual void Attack() { }
    public virtual void CharacterAnimation() { }
}
