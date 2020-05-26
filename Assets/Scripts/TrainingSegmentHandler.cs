using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSegmentHandler : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject gameManager;

    [HideInInspector]
    public bool right;

    void OnMouseDown()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }

    void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void OnMouseUpAsButton()
    {
        if (right)
        {
            gameManager.GetComponent<TrainingControl>().NextColors();
        }
        else
        {
            gameManager.GetComponent<TrainingControl>().gameState = GameControl.GameState.Lose;
        }
    }
}
