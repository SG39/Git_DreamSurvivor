using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private TMP_Text startButtonText;
    private byte alpha = 255;
    private bool isZero = false;

    private void FixedUpdate()
    {
        ChanageText();
    }

    void ChanageText()
    {
        if(!isZero)
        {
            this.alpha -= 5;

            if(this.alpha <= 0)
            {
                isZero = true;
            }
        }
        else if(isZero)
        {
            this.alpha += 5;

            if(this.alpha >= 255)
            {
                isZero = false;
            }
        }
        // 텍스트 반영
        this.startButtonText.color = new Color32(255, 255, 255, alpha);

    }
}
