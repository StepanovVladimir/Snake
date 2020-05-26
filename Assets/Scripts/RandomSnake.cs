using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSnake : MonoBehaviour
{
    public GameObject[] cells;

    void Awake()
    {
        int previousColor = -1;
        int forbiddenColor = -1;
        for (int i = 0; i < cells.Length; i++)
        {
            int color = Random.Range(0, GameControl.colors.Length);
            if (color == forbiddenColor)
            {
                while (color == forbiddenColor)
                {
                    color = Random.Range(0, GameControl.colors.Length);
                }
                forbiddenColor = -1;
            }
            else if (color == previousColor)
            {
                forbiddenColor = color;
            }
            cells[i].GetComponent<SpriteRenderer>().color = GameControl.colors[color];
            previousColor = color;
        }
    }
}
