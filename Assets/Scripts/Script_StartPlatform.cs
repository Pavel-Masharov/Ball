using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_StartPlatform : Script_MovingObjectGame
{
    private Animator anim;
    static readonly int isDeleteStateHash = Animator.StringToHash("isDelete");

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        DeliteObject();
    }

    public override void DeliteObject()
    {
        if (transform.position.z < -Script_GameController.Instance.PositionForAnimation || transform.position.x < -Script_GameController.Instance.PositionForAnimation)
        {
            anim.SetTrigger(isDeleteStateHash);
        }

        if (transform.position.z < -Script_GameController.Instance.PositionForDelete)
        {
            Destroy(gameObject);
        }
    }
}
