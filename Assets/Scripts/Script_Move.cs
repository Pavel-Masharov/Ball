using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Move : MonoBehaviour
{
    void Update()
    {
        MovePlatform(Script_GameController.Instance.DirectionPlatform);
    }

    private void MovePlatform(Vector3 dir)
    {
        if (!Script_GameController.Instance.isGameOver)
        {
            transform.Translate(dir * Script_GameController.Instance.SpeedMove * Time.deltaTime);
        }

    }
}
