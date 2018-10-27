using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private CharacterBase character;
    private BossBase boss;
    [Header("[UI Objects]")]
    [Header("[- Character]")]
    [SerializeField]
    private Image curHP;
    [SerializeField]
    private Text curHPText;
    [SerializeField]
    private Image curLife;
    [SerializeField]
    private Text curLifeText;
    [Header("[- Boss]")]
    [SerializeField]
    private Image curBossHP;
    [SerializeField]
    private Text curBossHPText;

    public void Initialize(ref CharacterBase _character, ref BossBase _boss)
    {
        character = _character;
        boss = _boss;

        curHP.fillAmount = 1;
        curHPText.text = character.cur_hp.ToString() + " / " + character.max_hp.ToString();
        curLife.fillAmount = 1;
        curLifeText.text = character.cur_life.ToString() + " / " + character.max_life.ToString();

        curBossHP.fillAmount = 1;
        curBossHPText.text = boss.cur_hp.ToString() + " / " + boss.max_hp.ToString();
    }

    public void Updated()
    {
        curHP.fillAmount = character.cur_hp / character.max_hp;
        curLife.fillAmount = character.cur_life / character.max_life;
        curBossHP.fillAmount = boss.cur_hp / boss.max_hp;

        curHPText.text = character.cur_hp.ToString() + " / " + character.max_hp.ToString();
        curLifeText.text = character.cur_life.ToString() + " / " + character.max_life.ToString();
        curBossHPText.text = boss.cur_hp.ToString() + " / " + boss.max_hp.ToString();
    }
}
