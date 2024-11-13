using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text Gold;
    [SerializeField] private TMP_Text Rune;

    #region  각 팝업창 프리팹 변수선언(SerializeField) - 추가시 하단에 추가
    [SerializeField] private GameObject GrowthUIGO;
    [SerializeField] private GameObject EquipmentUIGO;
    [SerializeField] private GameObject DundeonMenuUIGO;
    [SerializeField] private GameObject ShopUIGO;
    [SerializeField] private GameObject AchievementUIGO;
    [SerializeField] private GameObject DreamCrystallInventoryUIGO;
    #endregion

    #region  각 팝업창 버튼 변수 선언(SerializeField) - 추가시 하단에 추가
    [SerializeField] private Button growthButton;
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button dungeonMenuButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button achievementButton;
    [SerializeField] private Button DreamCrystallInventoryButton;
    [SerializeField] private Button wealthGoldButton;
    [SerializeField] private Button wealthRuneButton;
    #endregion

    #region 각 팝업창 활성화 상태
    private bool isGrowthUIGOActive = false;
    private bool isEquipmentUIGOActive = false;
    private bool isDungeonUIGOActive = false;
    private bool isShopUIGOActive = false;
    private bool isAchievementUIGOActive = false;
    private bool isDreamCrystallInventoryGOActive = false;
    #endregion 

    private void Awake()
    {
        // 시작시 모든 팝업창 닫기
        PopupwindowManagement();
    }
    
    private void Start()
    {
        OnButtonClick();
        var dreamCrystal = this.DreamCrystallInventoryUIGO.GetComponent<DreamInventoryUI>();
        dreamCrystal.dreamCrystalAction = () => {
            Debug.Log("꿈의 보석 인벤토리 터치");
            this.PopupwindowManagement(false, false, false, false, false, false);
            };
    }
    
    private void Update()
    {
        this.Gold.text = string.Format("{0:##,##0}", Main.Instance.PossessionGold);
        this.Rune.text = string.Format("{0:##,##0}", Main.Instance.PossessionRune);
    }

    #region 각 메뉴 팝업버튼 터치 이벤트
    // 터치 이벤트 등록
    private void OnButtonClick()
    {
        this.growthButton.onClick.AddListener(() => HandleButton(this.growthButton));
        this.equipmentButton.onClick.AddListener(() => HandleButton(this.equipmentButton));
        this.dungeonMenuButton.onClick.AddListener(() => HandleButton(this.dungeonMenuButton));
        this.shopButton.onClick.AddListener(() => HandleButton(this.shopButton));
        this.achievementButton.onClick.AddListener(() => HandleButton(this.achievementButton));
        this.DreamCrystallInventoryButton.onClick.AddListener(() => HandleButton(this.DreamCrystallInventoryButton));
        this.wealthGoldButton.onClick.AddListener(() => HandleButton(this.shopButton));
        this.wealthRuneButton.onClick.AddListener(() => HandleButton(this.shopButton));
    }

    // 각 버튼 터치시 팝업창 조작
    private void HandleButton(Button button)
    {
        switch (button)
        {
            case Button _ when button == this.growthButton:
                // 성장 버튼 터치 시
                Debug.Log("성장 버튼 터치");

                if(isGrowthUIGOActive)
                {
                    // 이미 열려있는 상태면 닫아라
                    isGrowthUIGOActive = ClosePopUP(GrowthUIGO);
                }
                else
                {
                    this.PopupwindowManagement(true, false, false, false, false, false);
                }

                break;

            case Button _ when button == this.equipmentButton:
                // 장비 버튼 터치 시
                Debug.Log("장비 버튼 터치");

                if(isEquipmentUIGOActive)
                {
                    isEquipmentUIGOActive = ClosePopUP(EquipmentUIGO);
                }
                else
                {
                    this.PopupwindowManagement(false, true, false, false, false, false);
                }

                break;

            case Button _ when button == this.dungeonMenuButton:
                // 던전 메뉴 버튼 터치 시
                Debug.Log("던전 버튼 터치");
                if(this.IsAllPopUpWindowClosed()) // 모든 팝업창이 닫혀있으면
                {
                    Debug.Log("던전 선택창 열기");
                    this.PopupwindowManagement(false, false, true, false, false, false);
                }
                else
                {
                    // 모든 창을 닫고 방치 화면으로 돌아온다.
                    this.PopupwindowManagement();
                }
                break;

            case Button _ when button == this.shopButton:
                // 상점 버튼 터치 시
                Debug.Log("상점 버튼 터치");

                if(isShopUIGOActive)
                {
                    isShopUIGOActive = ClosePopUP(ShopUIGO);
                }
                else
                {
                    this.PopupwindowManagement(false, false, false, true, false, false);
                }

                break;

            case Button _ when button == this.achievementButton:
                // 업적 버튼 터치 시
                Debug.Log("업적 버튼 터치");

                if(isAchievementUIGOActive)
                {
                    isAchievementUIGOActive = ClosePopUP(AchievementUIGO);
                }
                else
                {
                    this.PopupwindowManagement(false, false, false, false, true, false);
                }

                break;
            
            case Button _ when button == this.DreamCrystallInventoryButton:
                // 꿈의 보석 버튼 터치 시
                Debug.Log("꿈의 보석 인벤토리 터치");
                this.PopupwindowManagement(false, false, false, false, false, true);
                break;

            default:
                Debug.Log("설정 되지 않은 버튼 터치");
                break;
        }
    }
    #endregion

    #region 팝업창 관리 & 상태 메서드
    // 팝업창 관리 메서드
    private void PopupwindowManagement(bool growrth = false, bool equipment = false, bool dungeon = false, bool shop = false, bool achievement = false, bool dreamCrystall = false) 
    {
        // 열려있던 창 정보 저장하기
        SaveOpenPopupWindowData();

        // 상태
        this.isGrowthUIGOActive = growrth;
        this.isEquipmentUIGOActive = equipment;
        this.isDungeonUIGOActive = dungeon;
        this.isShopUIGOActive = shop;
        this.isAchievementUIGOActive = achievement;
        this.isDreamCrystallInventoryGOActive = dreamCrystall;

        // 팝업 관리
        this.GrowthUIGO.SetActive(growrth);
        this.EquipmentUIGO.SetActive(equipment);
        this.DundeonMenuUIGO.SetActive(dungeon);
        this.ShopUIGO.SetActive(shop);
        this.AchievementUIGO.SetActive(achievement);
        this.DreamCrystallInventoryUIGO.SetActive(dreamCrystall);
    }

    //팝업창 닫기
    private bool ClosePopUP(GameObject PopUpGo)
    {
        SaveOpenPopupWindowData();
        PopUpGo.SetActive(false);
        return false;
    }

    // 모든 팝업창이 닫혀있는가?
    private bool IsAllPopUpWindowClosed()
    {
        bool[] isPopUpwindows = {this.isGrowthUIGOActive, this.isEquipmentUIGOActive, this.isDungeonUIGOActive, 
            this.isShopUIGOActive, this.isAchievementUIGOActive, this.isDreamCrystallInventoryGOActive};
        
        return isPopUpwindows.All(element => !element);
    }

    // 열려있는 탭의 모든 정보 저장
    private void SaveOpenPopupWindowData()
    {
        if(this.isGrowthUIGOActive)
        {
            // 성장탭의 모든 정보 저장
            // 스테이터스 저장
            PlayerStatusManager.Instance.SaveStatus();
            Debug.Log($"<color=Yellow>Save Status</color>");
        }

        if(this.isEquipmentUIGOActive)
        {
            // 장비탭의 모든 정보 저장
        }

        if(this.isShopUIGOActive)
        {
            // 상점탭 모든 정보 저장
        }

        if(this.isAchievementUIGOActive)
        {
            // 업적탭 모든 데이터 저장
        }

        if(this.isDreamCrystallInventoryGOActive)
        {
            // 꿈의 파편 모든 데이터 저장
        }
    }
    #endregion



}
