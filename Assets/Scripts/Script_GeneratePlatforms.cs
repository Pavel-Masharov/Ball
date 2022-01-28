using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GeneratePlatforms : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    private Stack<Script_Platform> _arrayPlatformsNextPosition;
    private Queue<Script_Platform> _arrayPlatformsUpdate;
    private Script_StartPlatform _startPlatform;
    private Script_Ball _ball;

    private void Start()
    {
        _arrayPlatformsNextPosition = new Stack<Script_Platform>();
        _arrayPlatformsUpdate = new Queue<Script_Platform>();

        CreateScene();
    }

    private void UpdatePlatforms()
    {
        Destroy(_arrayPlatformsUpdate.Dequeue().gameObject);
        Script_Platform newPlatform = Instantiate(Script_GameController.Instance.PlatformPrefabs);
        CreateNewPlatform(newPlatform);
    }
    private void SpawnPlatforms()
    {
        for (int i = 0; i < 50; i++)
        {
            Script_Platform newPlatform = Instantiate(Script_GameController.Instance.PlatformPrefabs);
            CreateNewPlatform(newPlatform);
        }
    }

    private void CreateNewPlatform(Script_Platform platform)
    {
        platform.DefeatEvent += UpdatePlatforms;
        platform.transform.position = _arrayPlatformsNextPosition.Peek().transform.position + ChangeDirection(Script_GameController.Instance.Forward, Script_GameController.Instance.Right);
        platform.transform.parent = _parent.transform;
        _arrayPlatformsNextPosition.Clear();
        _arrayPlatformsNextPosition.Push(platform);
        _arrayPlatformsUpdate.Enqueue(platform);
        Script_GameController.Instance.PlatformNumber++;
        ChangePlatformForCrystal(platform);
    }

    private void ChangePlatformForCrystal(Script_Platform platform)
    {
        if (Script_GameController.Instance.isRandomSpawnCrystal == false)
        {
            if (Script_GameController.Instance.PlatformNumber % 6 == 0)
            {
                CreateCrystal(platform.transform.position);
            }
        }

        else
        {
            bool isSpawn = true;
            int a = Random.Range(0, 2);
            isSpawn = a == 0 ? isSpawn : !isSpawn;
            if (isSpawn)
            {
                CreateCrystal(platform.transform.position);
            }
        }
    }

    private Vector3 ChangeDirection(Vector3 f, Vector3 r)
    {
        Vector3 dir;
        int a = Random.Range(0, 2);
        dir = a == 0 ? f : r;
        return dir;

    }

    private void CreateCrystal(Vector3 point)
    {
        Vector3 offset = new Vector3(0, 1, 0);
        Script_Crystal newCrys = Instantiate(Script_GameController.Instance.CrystalPrefab, point + offset, Quaternion.identity);
        newCrys.transform.parent = _parent.transform;
    }

    public void StartGame()
    {
        Script_GameController.Instance.Popup.SetActive(false);
        Script_GameController.Instance.isGameOver = false;
        Script_GameController.Instance.PlatformNumber = 0;
        Script_Platform newPlatform = Instantiate(Script_GameController.Instance.PlatformPrefabs);
        newPlatform.transform.parent = _parent.transform;
        newPlatform.DefeatEvent += UpdatePlatforms;
        newPlatform.transform.position = new Vector3(0, 0, 0);
        _arrayPlatformsNextPosition.Push(newPlatform);
        _arrayPlatformsUpdate.Enqueue(newPlatform);
        SpawnPlatforms();
    }

    private void CreateScene()
    {
        ClearScene();
        _startPlatform = Instantiate(Script_GameController.Instance.StartPlatformPrefab, new Vector3(0, 0.01f, 0), Quaternion.identity);
        _startPlatform.transform.parent = _parent.transform;
        _ball = Instantiate(Script_GameController.Instance.BallPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        _ball.GameOverEvent += CreateScene;
        Script_GameController.Instance.CountCrystal = 0;
        _ball.uiController.OutputCountCrystal();
        Script_GameController.Instance.Popup.SetActive(true);
    }

    private void ClearScene()
    {
        _arrayPlatformsNextPosition.Clear();
        _arrayPlatformsUpdate.Clear();

        if (_ball != null)
        {
            Destroy(_ball.gameObject);
        }

        Script_MovingObjectGame[] allObject;
        allObject = FindObjectsOfType<Script_MovingObjectGame>();
        foreach (var item in allObject)
        {
            Destroy(item.gameObject);
        }
    }

}
