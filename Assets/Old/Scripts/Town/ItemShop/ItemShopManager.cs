using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopManager : MonoBehaviour 
{
    private CharacterBase character;
    // 캐릭터 유아이
    [SerializeField]
    private CharacterUI characterUI;
	void Start () 
	{
		if (GameObject.FindWithTag("Character").GetComponent<CharacterBase>() != null)
		{
			character = GameObject.FindWithTag("Character").GetComponent<CharacterBase>();
        	character.Initialize(new Vector2(0, -2));

			characterUI.Initialize();
		}
	}
	
	void Update () 
	{
		character.Updated();
	}
}
