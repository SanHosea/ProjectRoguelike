using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour {

    private CharacterBase character;
    // 캐릭터 유아이
    [SerializeField]
    private CharacterUI characterUI;

    public delegate void EnterItemShop();

	void Start () 
    {
        character = GameObject.FindWithTag("Character").GetComponent<CharacterBase>();
        character.Initialize(Vector2.zero);

        characterUI.Initialize();
	}
	
	void Update () 
    {
        character.Updated();
	}

    public void Enter_ItemShop() 
    {
        SceneManager.LoadScene("itemshop");
    }
}
