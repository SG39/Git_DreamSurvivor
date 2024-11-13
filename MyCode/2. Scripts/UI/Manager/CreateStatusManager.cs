using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStatusManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerStatusManager.Instance.LoadPlayerStatus();    
    }
}
