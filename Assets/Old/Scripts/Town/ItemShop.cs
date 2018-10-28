using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnterItemShopDelegate = TownManager.EnterItemShop;

public class ItemShop : MonoBehaviour
{
    [SerializeField]
    private TownManager townMgr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            EnterItemShopDelegate enterItemShop = new EnterItemShopDelegate(townMgr.Enter_ItemShop);
            enterItemShop();
        }
    }
}
