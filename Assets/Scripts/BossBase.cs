using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour {
    public float max_hp;
    public float cur_hp;

    public virtual void Initialize(CharacterBase _character) { }
    public virtual void Updated() { }
    public virtual void Move() { }
    public virtual void Attack() { }

}
