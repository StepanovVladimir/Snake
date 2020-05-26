using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentHandler : MonoBehaviour
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
            gameManager.GetComponent<GameControl>().NextColors();
        }
        else
        {
            gameManager.GetComponent<GameControl>().gameState = GameControl.GameState.Lose;
        }
    }
}
