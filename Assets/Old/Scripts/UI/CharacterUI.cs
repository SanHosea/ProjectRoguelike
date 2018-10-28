using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour 
{
    private CharacterBase character;
    [SerializeField]
    private Image curHP;
    [SerializeField]
    private Image curLife;
    [SerializeField]
    private Text curHPText;
    [SerializeField]
    private Text curLiftText;

    public void Initialize()
    {
        character = GameObject.FindWithTag("Character").GetComponent<CharacterBase>();

        curHP.fillAmount = (float)character.CurHP / (float)character.MaxHP;
        curLife.fillAmount = (float)character.CurLife / (float)character.MaxLife;

        curHPText.text = string.Format("{0} / {1}", character.CurHP, character.MaxHP);
        curLiftText.text = string.Format("{0} / {1}", character.CurLife, character.MaxLife);
    }
}
