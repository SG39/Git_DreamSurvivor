using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMain_Test : MonoBehaviour
{
    [SerializeField] private Button startButton;
    private void Awake()
    {
        PlayerStatusManager.Instance.LoadPlayerStatus();
        
        this.startButton.onClick.AddListener(() => {
            SceneManager.LoadScene(ConstNameClass.GAME_MAIN);
        });
    }
}
