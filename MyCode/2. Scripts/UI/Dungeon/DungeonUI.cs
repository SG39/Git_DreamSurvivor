using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUI : MonoBehaviour
{
    public GameObject dungeonSkillUI;
    public Image exp;
    public TMP_Text level;
    public TMP_Text playTime;
    public TMP_Text[] questTexts;
    public Button pause;
    public GameObject option;

    void Start()
    {
        pause.onClick.AddListener(() => { 
            Debug.Log($"<color=Yellow>On Clik PauseButton</color>");
            option.SetActive(true); });
        UpdateEXPUI(0);
        UpdateLevelUI(1);
        UpdateQuestUI(1, true);
    }
    public void UpdateEXPUI(float exp)
    {
        this.exp.fillAmount = exp;
    }

    public void UpdateLevelUI(int level)
    {
        this.level.text = level.ToString();
    }

    public void UpdateTimeUI(float time)
    {
        float min = (int)time / 60;
        float sec = (int)time % 60;
        playTime.text = min.ToString("00") + " : " + sec.ToString("00");
    }

    public void UpdateQuestUI(int num, bool cul, int amount = 0)
    {
        if (num == 2)
        {
            questTexts[num + 1].text = $"( {amount} / 0 )";
        }
        else if (num == 4)
        {
            questTexts[num + 1].text = $"( {amount} / 3 )";
        }
        if (!cul)
        {
            questTexts[num].fontStyle = FontStyles.Strikethrough;
            questTexts[num].color = Color.red;
            questTexts[num + 1].fontStyle = FontStyles.Strikethrough;
            questTexts[num + 1].color = Color.red;
        }
    }

    public void SeletSkill()
    {
        dungeonSkillUI.SetActive(true);
    }
}
