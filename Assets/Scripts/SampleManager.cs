using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleManager : MonoBehaviour
{
    public CharacterBase character;
    public BossBase monster;
    public UIManager uiMgr;

    [Header("[UI Object]")]
    [SerializeField]
    private GameObject uiObject;

    [SerializeField]
    private InputField width;
    [SerializeField]
    private InputField height;
    [SerializeField]
    private InputField speed;

	void Start ()
    {
        character.Intialize();
        monster.Initialize(character);
        uiMgr.Initialize(ref character, ref monster);

        uiObject.SetActive(false);

        width.text = (character.transform.localScale.x * 100).ToString();
        height.text = (character.transform.localScale.y * 100).ToString();
        speed.text = (character.speed).ToString();
    }
	
	void Update ()
    {
        character.Updated();
        monster.Updated();
        uiMgr.Updated();

        if(uiObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            uiObject.SetActive(false);
        }
        else if(!uiObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            uiObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ChangeAttribute();
        }
    }


    public void ChangeAttribute()
    {
        character.transform.localScale = new Vector3(float.Parse(width.text) * 0.01f, float.Parse(height.text) * 0.01f, 1);
        character.speed = float.Parse(speed.text);
    }
}
