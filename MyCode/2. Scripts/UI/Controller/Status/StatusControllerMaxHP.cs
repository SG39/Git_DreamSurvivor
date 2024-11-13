using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusControllerMaxHP : MonoBehaviour
{

    [SerializeField] private TMP_Text statusLevel; // 스테이터스 레벨
    [SerializeField] private TMP_Text statusFigure; // 스테이터스 스탯값
    [SerializeField] private TMP_Text statusAmount; // 스테이터스 강화 진행도
    [SerializeField] private TMP_Text goldAmount; // 스테이터스 강화시 소모되는 골드
    [SerializeField] private Image gaugeBar; // 스테이터스 강화게이지
    [SerializeField] private Button statusButton; // 스테이터스 버튼

    private int iLevel = PlayerStatusManager.Instance.maxHPLevel;
    private int iAmount = PlayerStatusManager.Instance.maxHPAmount;
    private float fFigure = PlayerStatusManager.Instance.maxHPFigure;
    private List<float> goldList = PlayerStatusManager.Instance.maxHPGoldList;
    private float fMaxGoldAmount = PlayerStatusManager.Instance.maxHPGoldMaxAmount;

    [SerializeField]private float fFigureOrigen = 0;
    [SerializeField]private float fFigureIncrease = 1;
    [SerializeField]private float fGoldOrigen = 2000;
    [SerializeField]private float fGoldIncrease = 200;

    private bool isButtonDown = false; // 버튼이 눌렸는가
    private Coroutine coroutine = null; // 코루틴 시작 / 정지용 변수

    // Start is called before the first frame update
    void Awake()
    {
        // 이벤트 트리거 등록
        this.ButtonEvent(this.statusButton);
        // 각 스테이터스 업데이트
        this.UpdateStatus();
    }

    private void OnEnable()
    {
        Debug.Log($"버튼 활성화");   
        SetAllStatusText();
    }

#region 데이터 표시하기

    // 모든 데이터 표기하기
    private void SetAllStatusText()
    {
        this.statusLevel.text = "Lv. "+ iLevel;
        this.statusFigure.text = string.Format("{0:##,##0.00}", fFigure);
        this.statusAmount.text = iAmount.ToString() + " %";
        this.goldAmount.text = Math.Ceiling(goldList[iAmount]).ToString();
        this.gaugeBar.fillAmount = (float)iAmount / 100;
    }

    // 현재 강화수치와 필요 강화 골드 갱신
    private void SetStatusText()
    {
        this.statusAmount.text = iAmount.ToString() + " %";
        this.goldAmount.text = Math.Ceiling(goldList[iAmount]).ToString();
        this.gaugeBar.fillAmount = (float)iAmount / 100;
    }

#endregion

#region 버튼 이벤트

#region 이벤트 트리거 등록 
    
   private void ButtonEvent(Button statusBtn)
   {
        // 이벤트 트리거 컴포넌트 가져오기
        EventTrigger maxHpTrigger = statusBtn.gameObject.AddComponent<EventTrigger>();

        //? PointerDown 이벤트 리스너 추가(꾹 누를때)
        // 새로운 EventTrigger.Entry 객체생성
        EventTrigger.Entry buttonDownEntry = new EventTrigger.Entry();
        buttonDownEntry.eventID = EventTriggerType.PointerDown;

        // 이벤트 리스너 추가
        buttonDownEntry.callback.AddListener((data) => { OnButtonDown(); });
        maxHpTrigger.triggers.Add(buttonDownEntry);

        //? PointerUp 이벤트 리스너 추가(꾹 눌렀다 땟을때)
        EventTrigger.Entry buttonUpEntry = new EventTrigger.Entry();
        buttonUpEntry.eventID = EventTriggerType.PointerUp;
        buttonUpEntry.callback.AddListener((data) => { OnButtonUp(); });
        maxHpTrigger.triggers.Add(buttonUpEntry);

        //? PointerClick 이벤트 리스너 추가(단일클릭)
        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
        pointerClickEntry.eventID = EventTriggerType.PointerClick;
        pointerClickEntry.callback.AddListener((data) => { OnButtonClick(); });
        maxHpTrigger.triggers.Add(pointerClickEntry);
   }

#endregion

#region 버튼이벤트시 동작하는 메서드
    // 버튼이 일정시간 이상 눌렸을때
    private void OnButtonDown()
    {
        this.isButtonDown = true;

        if(this.coroutine == null)
        {
            this.coroutine = StartCoroutine(this.ButtonDown());
        }
        else
        {
            Debug.Log($"{this.name}<color=red>코루틴 비정상 작동</color> >> coroutine = {this.coroutine}");
        }
    }

    // 버튼이 일정시간 눌리고 떨어졌을 때
    private void OnButtonUp()
    {
        this.isButtonDown = false;
        StopCoroutine(this.coroutine); //강화 코루틴 정지
        this.coroutine = null; // 코루틴 초기화
    }

    // 버튼이 한번 클릭 되었을때
    private void OnButtonClick()
    {
        this.UpgradeStatus();
    }

    // 버튼이 일정 시간 이상 눌렸을때 자동으로 강화하는 메서드
    private IEnumerator ButtonDown()
    {
        // 처음 대기시간
        yield return new WaitForSeconds(0.3f);

        while(this.isButtonDown)
        {
            this.UpgradeStatus();
            // 0.1마다 증가
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 실제 스테이터스 강화 메서드
    private void UpgradeStatus()
    {
        // 만약 소지 골드가 강화골드 보다 같거나 많다면
        if(Main.Instance.PossessionGold >= goldList[iAmount])
        {
            // 골드 차감
            Main.Instance.AnDecreaseInGold(goldList[iAmount]);

            // 현재 강화 진행도 증가
            iAmount++;

            // 만약 진행도가 100%면 레벨업후 진행도 초기화
            if(iAmount == 100)
            {
                // 레벨업
                iLevel++;
                // 진행도 초기화
                iAmount = 0;
                // 레벨에 따른 각 스테이터스 갱신
                this.UpdateStatus();
                this.SetAllStatusText();

            }

            // 텍스트 갱신
            SetStatusText();

        }
        else
        {
            // 골드 부족
            Debug.Log($"Gold가 <color=yellow>{goldList[iAmount] - Main.Instance.PossessionGold}</color>부족합니다.");
        }
    }

#endregion
#endregion


#region 각 스테이터스 정보갱신
    private void UpdateStatus()
    {
        UIFunctionClass.Instance.LevelToStatus(iLevel, out fFigure, this.fFigureOrigen, this.fFigureIncrease, out fMaxGoldAmount, this.fGoldOrigen, this.fGoldIncrease);
        Debug.Log(fMaxGoldAmount);
        goldList = UIFunctionClass.Instance.ExpendableGoldList(fMaxGoldAmount);
    }
#endregion

}
