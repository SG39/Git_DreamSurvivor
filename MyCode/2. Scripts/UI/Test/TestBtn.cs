using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestBtn : MonoBehaviour
{
    public Button yourButton; // 드래그 앤 드롭으로 버튼을 할당하세요.
    public Text valueText; // 증가된 값을 표시할 UI 텍스트

    private int currentValue = 0;
    private bool isHolding = false;

    void Start()
    {
        EventTrigger trigger = yourButton.gameObject.AddComponent<EventTrigger>();

        // PointerDown 이벤트 리스너 추가
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown(); });
        trigger.triggers.Add(pointerDownEntry);

        // PointerUp 이벤트 리스너 추가
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp(); });
        trigger.triggers.Add(pointerUpEntry);

        // PointerClick 이벤트 리스너 추가
        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
        pointerClickEntry.eventID = EventTriggerType.PointerClick;
        pointerClickEntry.callback.AddListener((data) => { OnPointerClick(); });
        trigger.triggers.Add(pointerClickEntry);
    }

    public void OnPointerDown()
    {
        isHolding = true;
        StartCoroutine(IncreaseValue());
    }

    public void OnPointerUp()
    {
        isHolding = false;
    }

    public void OnPointerClick()
    {
        IncreaseOnce();
    }

    private IEnumerator IncreaseValue()
    {
        yield return new WaitForSeconds(0.3f); // 길게 누르기 인식 지연 시간
        while (isHolding)
        {
            currentValue++;
            valueText.text = currentValue.ToString();
            yield return new WaitForSeconds(0.1f); // 0.1초마다 값 증가
        }
    }

    private void IncreaseOnce()
    {
        currentValue++;
        valueText.text = currentValue.ToString();
    }
}
