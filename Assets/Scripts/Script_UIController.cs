using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_UIController : MonoBehaviour
{
    [SerializeField] private Text _textCountCrystal;
    public void OutputCountCrystal()
    {
        _textCountCrystal.text = "Кристалы  " + Script_GameController.Instance.CountCrystal.ToString();
    }
}
