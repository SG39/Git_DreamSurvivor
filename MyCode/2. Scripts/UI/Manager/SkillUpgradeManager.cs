using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class SkillUpgradeManager : MonoBehaviour
{
    // 스킬 인포 불러오기
    public List<SkillInfo> skillInfo{get; private set;}

    [SerializeField] Button skillGrup0;
    [SerializeField] TMP_Text skillGrupText0;
    [SerializeField] float requiredForRune0 = 1000; //강화시필요 룬 초기값
    [SerializeField] Button skillGrup1;
    [SerializeField] TMP_Text skillGrupText1;
    [SerializeField] float requiredForRune1 = 1500;
    [SerializeField] Button skillGrup2;
    [SerializeField] TMP_Text skillGrupText2;
    [SerializeField] float requiredForRune2 = 2000;
    [SerializeField] Button skillGrup3;
    [SerializeField] TMP_Text skillGrupText3;
    [SerializeField] float requiredForRune3 = 2500;
    [SerializeField] Button skillGrup4;
    [SerializeField] TMP_Text skillGrupText4;
    [SerializeField] float requiredForRune4 = 3000;
    
    private void Awake()
    {
        skillInfo = SystemSkillManager.Instance.GetSystemSkillInfo(); // 스킬 인포 불러오기
    }

    void Start()
    {
        
        SetRuneTest();
        this.skillGrup0.onClick.AddListener(() => {

            if(this.requiredForRune0 <= Main.Instance.PossessionRune) // 소지 룬과 필요 룬비교
            {
                Main.Instance.AnDecreaseInRune(requiredForRune0); // 룬 감소
                // UIFunctionClass.Instance.SkillLevelUp();
                Debug.Log($"0번 스킬 번들 강화");
            }
            else
            {
                Debug.Log($"<color=Yellow>룬이 부족합니다.</color>");
            }

        });   
    }

#region  버튼 눌러 스킬 강화 하기
    private void OnClickSkillGrupButton(float requiredForRune)
    {
        if(requiredForRune <= Main.Instance.PossessionRune)
        {
            Main.Instance.AnDecreaseInRune(requiredForRune); // 룬 감소
            // UIFunctionClass.Instance.SkillLevelUp(); // 스킬 랜덤강화
            Debug.Log($"스킬 번들 강화");
        }
        else
        {
            Debug.Log($"<color=Yellow>룬이 부족합니다.</color>");
        }
    }
#endregion

# region UI정보 갱신 메서드
    // 룬강화시 소모룬 표기(스킬 추가시 각 리스트 뒤에 추가)
    private void SetRuneTest()
    {
        List<TMP_Text> texts = new List<TMP_Text>(){this.skillGrupText0, this.skillGrupText1, this.skillGrupText2, this.skillGrupText3, this.skillGrupText4};
        List<float> runes = new List<float>(){this.requiredForRune0, this.requiredForRune1, this.requiredForRune2, this.requiredForRune3, this.requiredForRune4};

        for(int i = 0; i < texts.Count; i++)
        {
            texts[i].text = runes[i].ToString();
            Debug.Log($"{i}번 스킬 번들 강화");
        }
    }

    // 스킬 정보 갱신 및 표기
    private void SetSkillInfo()
    {

    }
#endregion

}

