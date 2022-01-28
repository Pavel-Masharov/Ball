using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ball : MonoBehaviour
{
    public delegate void GameOver();
    public event GameOver GameOverEvent;

    public Script_UIController uiController;
    private void OnEnable()
    {
        uiController = FindObjectOfType<Script_UIController>();
    }
    private void Update()
    {
        CheckPlatform();
    }


    private void CheckPlatform()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            if(!gameObject.GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>();
            }

            Script_GameController.Instance.isGameOver = true;
            Invoke(nameof(GameIsLost), 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Script_Crystal>())
        {
            Destroy(other.gameObject);
            Script_GameController.Instance.CountCrystal++;
            uiController.OutputCountCrystal();
           
        }
    }

    private void GameIsLost()
    {
        GameOverEvent();
    }
}
