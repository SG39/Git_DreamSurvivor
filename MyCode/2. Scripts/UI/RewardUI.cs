using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public Image crystalImage;
    public TMP_Text grade;

    public Image characterImage;
    public TMP_Text dungeonName;
    public TMP_Text dungeonDifficulty;

    //public TMP_Text level;
    public TMP_Text power;
    public TMP_Text damage;
    public TMP_Text maxHp;
    //public TMP_Text recovery;
    public TMP_Text protection;
    public TMP_Text protectionProb;
    public TMP_Text criticalPower;
    public TMP_Text criticalProb;
    //public TMP_Text expIncrease;
    public TMP_Text goldIncrease;
    public TMP_Text coolDown;
    public TMP_Text attackRange;
    public TMP_Text attackDuration;
    public TMP_Text attackOfNumber;
    public TMP_Text projectorSpeed;
    //public TMP_Text currentHp;

    public GameObject skillSlot;
    public Transform slotPos;

    public Button closeButton;
    public DungeonSkillManager skillManager;
    public SpriteAtlas skillAtlas;
    public SpriteAtlas crystalAtlas;
    public DungeonPlayerStatus player;

    List<SkillData> datas = new List<SkillData>();
    List<GameObject> skillSlots = new List<GameObject>();

    private void Start()
    {
        UpdateUI();

        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("AutoMap_WS");
        });
    }
    private void OnEnable()
    {
        LoadSkillData();

        Time.timeScale = 0;

        for (int i = 0; i < skillManager.dungeonSkills.Count; i++)
        {
            skillSlots.Add(Instantiate(skillSlot, slotPos));
        }
        UpdateUI();
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
        //for (int i = 0; i < skillManager.dungeonSkills.Count; i++)
        //{
        //    Destroy(skillSlots[i]);
        //}
        //skillSlots.Clear();
    }
    private void LoadSkillData()
    {
        skillManager.LoadSkill();
        datas = skillManager.LoadSkillUIData();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < skillManager.dungeonSkills.Count; i++)
        {
            //획득한 스킬 내역
            int idx = datas.FindIndex(data => data.name == skillManager.dungeonSkills[i].name);
            //이미지
            skillSlots[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                = skillAtlas.GetSprite(datas[idx].sprite_name);
            //레벨
            skillSlots[i].transform.GetChild(2).GetComponent<TMP_Text>().text
                = skillManager.LevelToText(skillManager.dungeonSkills[i].dungeonLevel);
        }
        DungeonMapData mapData = new DungeonMapData();

        Debug.LogFormat("이미지번호 : {0}, 등급 : {1}", crystalAtlas.GetSprite(
            DreamInventoryManager.Instance.crystalImageNum), 
            DreamInventoryManager.Instance.Grade(skillManager.dungeonSkills.Count));
        crystalImage.sprite = crystalAtlas.GetSprite(
            DreamInventoryManager.Instance.crystalImageNum);

        //파편 등급에 임시 값을 부여함 => 합산 공식으로 변경될 예정 DreamInventoryUI.cs도 변경
        grade.text = DreamInventoryManager.Instance.Grade(skillManager.dungeonSkills.Count);

        //public Image characterImage;
        dungeonName.text = mapData.dungeonName.ToString();
        dungeonDifficulty.text = mapData.difficulty.ToString();

        //level = playerStatusData.level;
        power.text = player.power.ToString();
        damage.text = player.damage.ToString();
        maxHp.text = player.maxHp.ToString();
        //recovery = playerStatusData.recovery;
        protection.text = player.protection.ToString();
        protectionProb.text = player.protectionProb.ToString();
        criticalPower.text = player.criticalPower.ToString();
        criticalProb.text = player.criticalProb.ToString();
        //expIncrease = playerStatusData.expIncrease;
        goldIncrease.text = player.goldIncrease.ToString();
        coolDown.text = player.coolDown.ToString();
        attackRange.text = player.attackRange.ToString();
        attackDuration.text = player.attackDuration.ToString();
        attackOfNumber.text = player.attackOfNumber.ToString();
        projectorSpeed.text = player.projectorSpeed.ToString();
        //currentHp = playerStatusData.currentHp;
    }
}
