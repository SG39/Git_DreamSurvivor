using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] DungeonPlayerStatus status;
    public float pushForce = 1.05f; // �о�� ���� ũ��
    public float pushRadius = 1.05f; // �о���� ����
    Dungeon dungeon;
    float exp = 0;
    float maxEXP = 5;
    private Camera mainCamera;
    private PlayerStatusManager statusManager = PlayerStatusManager.Instance;
    public MyCollider col;

    void Start()
    {
        mainCamera = Camera.main;
        col = GetComponent<MyCollider>();
        dungeon = FindAnyObjectByType<Dungeon>();
        Init();
    }


    void Update()
    {
        MovePlayer();
        //Controller();
    }
    private void Init()
    {
        LoadPlayerStatus();
        LevelUp();
    }

    private void LoadPlayerStatus()
    {
        status.level = 0;
        status.power = statusManager.powerFigure;
        status.damage = statusManager.damageFigure;
        status.maxHp = statusManager.maxHPFigure;
        status.recovery = statusManager.recoveryFigure;
        status.protection = statusManager.protectionFigure;
        status.protectionProb = statusManager.protectionFigure;
        status.criticalPower = statusManager.criticalPowerFigure;
        status.criticalProb = statusManager.criticalPowerFigure;
        status.expIncrease = statusManager.expIncreaseGainFigure;
        status.goldIncrease = statusManager.goldGainFigure;
        status.coolDown = statusManager.coolDownFigure;
        status.attackRange = statusManager.attackRangeFigure;
        status.attackDuration = statusManager.attackDurationFigure;
        status.attackOfNumber = (int)statusManager.attackOfNumberFigure;
        status.projectorSpeed = statusManager.projectorSpeedFigure;
        status.currentHp = status.maxHp;
    }

    private void Controller()
    {
        switch (GameManager.Instance.GameType)
        {
            case (GameType.Main):
                {
                    Debug.Log("GameScene�Դϴ�.");
                    break;
                }
            case (GameType.Dungeon):
                {
                    Debug.Log("DungeonScene�Դϴ�.");
                    MovePlayer();
                    break;
                }
        }

    }
    private void MovePlayer()
    {
        float dirH = Input.GetAxisRaw("Horizontal");
        float dirV = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(dirH, dirV).normalized;

        PushAwayMonsters(dir);
        //if (dir != Vector2.zero)
        //{
        //    pushDirection = dir; // ���������� �Էµ� ������ ����
        //}

        transform.Translate(dir * moveSpeed * Time.fixedDeltaTime);
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void PushAwayMonsters(Vector2 dir)
    {
        MyCollider[] colliders = QuadTreeManager.Instance.Retrieve(CameraFrustum.ViewRect()).ToArray();
        foreach (var collider in colliders)
        {
            if (collider != this && collider.colliderType == ColliderType.Monster)
            {
                Vector2 directionToMonster = (collider.GetCenter() - col.GetCenter()).normalized;
                float distance = Vector2.Distance(col.GetCenter(), collider.GetCenter());

                // �о�� ����� ������ ������ ��ġ�ϴ� ��쿡�� �о
                if (Vector2.Dot(directionToMonster, dir) > 0 && distance < pushRadius)
                {
                    collider.transform.position += (Vector3)(directionToMonster * pushForce * (pushRadius - distance) / pushRadius);
                }
            }
        }
    }
    public void LevelUp()
    {
        exp = 0;
        dungeon.LevelUp(++status.level);
    }

    public void EXPUp()
    {
        float currentEXP = ++exp / maxEXP;
        if (currentEXP >= 1)
        {
            LevelUp();
            maxEXP++;
            currentEXP = 0;
        }
        dungeon.EXPUp(currentEXP);
    }
}
