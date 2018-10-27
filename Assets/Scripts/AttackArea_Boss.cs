using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea_Boss : MonoBehaviour {
    public bool bCanAttack = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Character"))
            bCanAttack = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Character"))
            bCanAttack = false;
    }
}
