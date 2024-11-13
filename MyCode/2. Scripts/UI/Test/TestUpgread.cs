using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpgread : MonoBehaviour
{
    void Start()
    {
        float x = 100000f;
        int i = 1;
        List<float> divisions = GenerateAscendingDivisions(x);
        
        // 출력 확인
        foreach (float value in divisions)
        {
            Debug.Log($"{i}% >>>>>>> <color=yellow>{value}</color>");
            i++;
        }

        // 총합 확인
        float sum = 0;
        foreach (float value in divisions)
        {
            sum += value;
        }
        Debug.Log($"Sum of divisions >>>> {sum}");
    }

    List<float> GenerateAscendingDivisions(float x, int n = 100)
    {
        List<float> divisions = new List<float>();
        
        // 증가하는 배수 생성
        List<int> multipliers = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            multipliers.Add(i);
        }
        
        int totalMultiplier = 0;
        foreach (int m in multipliers)
        {
            totalMultiplier += m;
        }

        // 기본값 계산
        float baseValue = x / totalMultiplier;

        // 나눠진 리스트 생성
        foreach (int m in multipliers)
        {
            divisions.Add(baseValue * m);
        }

        return divisions;
    }
}
