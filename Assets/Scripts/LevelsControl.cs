using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsControl : MonoBehaviour
{
    public Button[] levels;

    void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Levels"); i++)
        {
            levels[i].interactable = true;
        }
    }
}
