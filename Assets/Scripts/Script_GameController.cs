using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_GameController : MonoBehaviour
{
    public static Script_GameController Instance { get; private set; }

    public Script_StartPlatform StartPlatformPrefab;
    public Script_Ball BallPrefab;
    [HideInInspector] public Script_Platform PlatformPrefabs;
    public Script_Crystal CrystalPrefab;
    public GameObject Popup;
    [SerializeField] private Script_Platform _easyPlatformPrefab;
    [SerializeField] private Script_Platform _mediumPlatformPrefab;
    [SerializeField] private Script_Platform _hardPlatformPrefab;

    private Vector3 _easyLevelForvard;
    private Vector3 _easyLevelRight;
    private Vector3 _mediumLevelForvard;
    private Vector3 _mediumLevelRight;
    private Vector3 _hardLevelForvard;
    private Vector3 _hardLevelRight;
    [HideInInspector] public Vector3 Forward;
    [HideInInspector] public Vector3 Right;
    [HideInInspector] public Vector3 DirectionPlatform;

    [HideInInspector] public int PlatformNumber;
    [HideInInspector] public int CountCrystal;
    [HideInInspector] public bool isGameOver;
    public float PositionForDelete { get; private set; }
    public float PositionForAnimation { get; private set; }

    [Header("Рандомное появление кристалов")] public bool isRandomSpawnCrystal;
    [Header("Скорость")] public float SpeedMove;
    [Header("Уровень сложности")] public LevelOfDifficulty difficulty = LevelOfDifficulty.Easy;
    public enum LevelOfDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard =2
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
       
        isGameOver = true;
        DirectionPlatform = Vector3.back;
        SpeedMove = 3;
        PositionForDelete = 10;
        PositionForAnimation = 3;

        _easyLevelForvard = new Vector3(0, 0, 3);
        _easyLevelRight = new Vector3(3, 0, 0);
        _mediumLevelForvard = new Vector3(0, 0, 2);
        _mediumLevelRight = new Vector3(2, 0, 0);
        _hardLevelForvard = new Vector3(0, 0, 1);
        _hardLevelRight = new Vector3(1, 0, 0);
    }


    private void Update()
    {
        ChangeDirectionMove();
        EnterOfDiddiculty();
    }


    private void ChangeDirectionMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (DirectionPlatform == Vector3.back)
            {
                DirectionPlatform = Vector3.left;
            }
            else
            {
                DirectionPlatform = Vector3.back;
            }
        }
    }

    private void EnterOfDiddiculty()
    {
        if (difficulty == LevelOfDifficulty.Easy)
        {
            PlatformPrefabs = _easyPlatformPrefab;
            Forward = _easyLevelForvard;
            Instance.Right = _easyLevelRight;
        }
        else if (difficulty == LevelOfDifficulty.Medium)
        {
            PlatformPrefabs = _mediumPlatformPrefab;
            Forward = _mediumLevelForvard;
            Right = _mediumLevelRight;
        }
        else
        {
            PlatformPrefabs = _hardPlatformPrefab;
            Forward = _hardLevelForvard;
            Right = _hardLevelRight;
        }
    }
}
