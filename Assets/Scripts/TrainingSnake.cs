using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSnake : MonoBehaviour
{
    public GameObject[] cells;

    void Awake()
    {
        cells[0].GetComponent<SpriteRenderer>().color = GameControl.colors[2];
        cells[1].GetComponent<SpriteRenderer>().color = GameControl.colors[3];
        cells[2].GetComponent<SpriteRenderer>().color = GameControl.colors[4];
        cells[3].GetComponent<SpriteRenderer>().color = GameControl.colors[5];
        cells[4].GetComponent<SpriteRenderer>().color = GameControl.colors[0];
        cells[5].GetComponent<SpriteRenderer>().color = GameControl.colors[1];
        cells[6].GetComponent<SpriteRenderer>().color = GameControl.colors[1];
        cells[7].GetComponent<SpriteRenderer>().color = GameControl.colors[4];
        cells[8].GetComponent<SpriteRenderer>().color = GameControl.colors[0];
    }
}
