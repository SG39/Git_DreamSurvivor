using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthUI : MonoBehaviour
{
    [SerializeField] private GameObject StatusGo;
    [SerializeField] private Button statusButton;
    [SerializeField] private GameObject CharacterGo;    
    [SerializeField] private Button characterButton;    
    [SerializeField] private GameObject SkillGo;
    [SerializeField] private Button skillButton;

    private bool isStatusWindowOpen = false;
    private bool isCharacterWindowOpen = false;
    private bool isSkillWindowOpen = false;

    private void OnEnable()
    {
        #region 성장 탭이 열릴때의 초기 값
        OpenTeb();
        #endregion

        #region 성장메뉴 버튼 조작
        this.statusButton.onClick.AddListener(() => {
            if(!isStatusWindowOpen)
            {
                OpenTeb();
            }
        }); 

        this.characterButton.onClick.AddListener(() => {
            if(!isCharacterWindowOpen)
            {
                OpenTeb(false, true, false);
            }
        });

        this.skillButton.onClick.AddListener(() => {
            if(!isSkillWindowOpen)
            {
                OpenTeb(false, false, true);
            }
        });
        #endregion
    }

    private void OpenTeb(bool status = true, bool character = false, bool skill = false)
    {
        // 열린창 닫기전 저장하기
        if(isStatusWindowOpen)
        {
            // 스테이터스 저장
        }
        else if(isCharacterWindowOpen)
        {
            // 캐릭터 저장
        }
        else if(isSkillWindowOpen)
        {
            // 스킬 저장
        }
        else
        {
            Debug.Log($"<color=Yellow>모든창은 닫혀있습니다.</color>");
        }

        // 열린창 상태 변경
        isStatusWindowOpen = status;
        isCharacterWindowOpen = character;
        isSkillWindowOpen = skill;

        // 열린창 닫고 터치한 창 열기
        this.StatusGo.SetActive(status);
        this.CharacterGo.SetActive(character);
        this.SkillGo.SetActive(skill);
    }

    



}
