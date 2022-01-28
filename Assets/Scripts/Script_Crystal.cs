using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Crystal : Script_MovingObjectGame
{
    void Update()
    {
        DeliteObject();
    }
    public override void DeliteObject()
    {
        if (transform.position.z < -Script_GameController.Instance.PositionForDelete || transform.position.x < -Script_GameController.Instance.PositionForAnimation)
        {
            Destroy(gameObject);
        }
    }
}
