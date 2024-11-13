using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionClass
{
    public readonly static UIFunctionClass Instance = new UIFunctionClass();


#region  스테이터스 관리메서드

    #region 레벨에 따른 스탯 값 산출 메서드
    
    //! 일정하게 상승
    private float Figure(int level, float statusFigure, float statusIncreasedFigure)
    {
        float figure = statusFigure; // 초기값
        float increasedFigure = statusIncreasedFigure; // 증가수치

        for(int i = 1; i < level; i++)
        {
            figure += increasedFigure;
        }

        return figure;
    }

    //! 특정 레밸마다 추치증가
    private float Figure(int level, float statusFigure, float statusIncreasedFigure, int targetLevel, float targetIncreasedFiguer)
    {
        float figure = statusFigure; // 초기값
        float increasedFigure = statusIncreasedFigure; // 증가수치

        for(int i = 1; i < level; i++)
        {
            if(level % targetLevel == 0)
            {
                increasedFigure += targetIncreasedFiguer;
            }
            figure += increasedFigure;
        }

        return figure;
    }

    #endregion

    #region 최대 투자 수치에 따른 각 퍼센트별 필요 골드 리스트
    // 레벨업 마다 1번 호출(계속 호출할 이유가 없음, 처음 게임을 컬때는 불러와야 한다.)
    public List<float> ExpendableGoldList(float maxGoldAmount, int entireStepOfUpgread = 100)
    {
        List<float> expendableGoldList = new List<float>();
        
        // 증가하는 배수 생성
        List<int> multipliers = new List<int>();
        for (int i = 1; i <= entireStepOfUpgread; i++)
        {
            multipliers.Add(i);
        }
        
        int totalMultiplier = 0;
        foreach (int m in multipliers)
        {
            totalMultiplier += m;
        }

        // 기본값 계산
        float baseValue = maxGoldAmount / totalMultiplier;

        // 나눠진 리스트 생성
        foreach (int m in multipliers)
        {
            expendableGoldList.Add(baseValue * m);
        }

        // 총합 계산
        float sum = 0;
        foreach (float value in expendableGoldList)
        {
            sum += value;
        }

        // 총합과 현재 최총치와 비교
        if((int)sum == (int)maxGoldAmount)
        {
            Debug.Log($"필요 <color=Yellow>골드</color> 리스크 생성");
            return expendableGoldList;
        }
        else
        {
            Debug.Log($"<color=Yellow>MaxGoldAmount 와 ExpendableGoldList의 합계가 같지 않습니다.</color>");
            expendableGoldList[99] += (int)sum - (int)maxGoldAmount;
            return expendableGoldList;
        }

    }

    #endregion
    
    #region 스테이터스 레벨에 따른 수치상승 & 최대골드 수치
    public void LevelToStatus(int level, out float statusFigure, float originFigure, float increasedFigure, out float maxGoldAmount, float originGoldFigure, float increasedGoldFigure)
    {
        // 레벨에 따라 스탯 수치증가
        statusFigure = Figure(level, originFigure, increasedFigure); //반환

        // 레벨에 따라 최대 사용 골드 수치 증가
        maxGoldAmount = Figure(level, originGoldFigure, increasedGoldFigure); //반환
    }
    #endregion

#endregion


#region  스킬 강화 관리 메서드
    // 버튼을 눌렀을때 호출된다.(조건)
    // 4가지 스킬이 저장된 데이터 목록을 매개변수로 받는다.
    public void SkillLevelUp(SkillInfo skill1,SkillInfo skill2,SkillInfo skill3,SkillInfo skill4)
    {
        // 들어온 스킬중 시스템 레벨이 5이상을 제외하고 리스트로 만든다.
        List<SkillInfo> skills = new List<SkillInfo>(){skill1, skill2,skill3,skill4}; 
        foreach (SkillInfo skill in skills)
        {
            SkillInfo si = skill; 
            if(si.systemLevel >= 5)
            {
                skills.Remove(si);
            }
        }
        // 배열의 크기로 랜덤을 돌린다.
        int random = Random.Range(0, skills.Count);
        // 0-3까지 랜덤을 돌려 나온수의 배열에 있는 스킬 레벨을 증가 시킨다.
        skills[random].systemLevel++;
        Debug.Log($"스킬강화 >> <color=Yellow>{skills[random].name} </color>");
        // 스킬인포를 저장한다.
        Debug.Log($"<color=Yellow>Sucsses >> </color>Save Skill Info");
    }



#endregion
}
