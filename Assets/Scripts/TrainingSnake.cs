using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSnake : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject[] capsules;

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

        capsules[0].GetComponent<Renderer>().material.color = GameControl.colors[2];
        capsules[1].GetComponent<Renderer>().material.color = GameControl.colors[3];
        capsules[2].GetComponent<Renderer>().material.color = GameControl.colors[4];
        capsules[3].GetComponent<Renderer>().material.color = GameControl.colors[5];
        capsules[4].GetComponent<Renderer>().material.color = GameControl.colors[0];
        capsules[5].GetComponent<Renderer>().material.color = GameControl.colors[1];
        capsules[6].GetComponent<Renderer>().material.color = GameControl.colors[1];
        capsules[7].GetComponent<Renderer>().material.color = GameControl.colors[4];
        capsules[8].GetComponent<Renderer>().material.color = GameControl.colors[0];
    }
}
