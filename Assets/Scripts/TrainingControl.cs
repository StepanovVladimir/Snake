using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingControl : MonoBehaviour
{
    public GameObject snake;
    public GameObject[] segments;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject hintPanel;
    public GameObject hint1;
    public GameObject hint2;
    public int nextLevel;
    public GameObject pause;
    public GameObject pausePanel;

    [HideInInspector]
    public GameControl.GameState gameState;

    private int offset;
    private Vector3 target;

    void Start()
    {
        gameState = GameControl.GameState.Play;
        offset = 0;
        target = new Vector3(20, snake.transform.position.y, snake.transform.position.z);
        GenerateColors(1, 0);
    }

    void Update()
    {
        if (gameState == GameControl.GameState.Lose)
        {
            PlayerLose();
        }
        else if (gameState == GameControl.GameState.Win)
        {
            PlayerWin();
        }
    }

    public void NextColors()
    {
        for (int i = 0; i < segments[0].GetComponent<TrainingSegmentHandler>().cells.Length; i++)
        {
            snake.GetComponent<TrainingSnake>().cells[i + offset].SetActive(false);
        }

        offset += segments[0].GetComponent<TrainingSegmentHandler>().cells.Length;

        if (offset < snake.GetComponent<TrainingSnake>().cells.Length)
        {
            if (offset == 3)
            {
                hint1.SetActive(false);
                hint2.SetActive(true);
                GenerateColors(2, 1);
            }
            else
            {
                hint2.SetActive(false);
                foreach (GameObject segment in segments)
                {
                    segment.SetActive(false);
                }
                pause.SetActive(false);
                hintPanel.SetActive(true);
            }
        }
        else
        {
            gameState = GameControl.GameState.Win;
        }
    }

    public void OnPause()
    {
        foreach (GameObject segment in segments)
        {
            segment.SetActive(false);
        }
        pause.SetActive(false);
        hint1.SetActive(false);
        hint2.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OffPause()
    {
        pausePanel.SetActive(false);
        foreach (GameObject segment in segments)
        {
            segment.SetActive(true);
        }

        if (offset == 0)
        {
            hint1.SetActive(true);
        }
        else if (offset == 3)
        {
            hint2.SetActive(true);
        }
        pause.SetActive(true);
    }

    public void TrySelf()
    {
        hintPanel.SetActive(false);
        foreach (GameObject segment in segments)
        {
            segment.SetActive(true);
        }
        pause.SetActive(true);
        GenerateColors(Random.Range(0, segments.Length), 1);
    }

    void GenerateColors(int indexOfRightSegment, int rightOverturned)
    {
        for (int i = 0; i < segments.Length; i++)
        {
            GameObject[] segmentCells = segments[i].GetComponent<TrainingSegmentHandler>().cells;
            GameObject[] snakeCells = snake.GetComponent<TrainingSnake>().cells;
            if (i == indexOfRightSegment)
            {
                segments[i].GetComponent<TrainingSegmentHandler>().right = true;
                for (int j = 0; j < segmentCells.Length; j++)
                {
                    segmentCells[j].GetComponent<SpriteRenderer>().color = snakeCells[j + offset].GetComponent<SpriteRenderer>().color;
                }

                if (rightOverturned == 1)
                {
                    segments[i].GetComponent<Transform>().rotation = new Quaternion(0, 0, 180, 0);
                }
                else
                {
                    segments[i].GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
                }
            }
            else
            {
                segments[i].GetComponent<TrainingSegmentHandler>().right = false;
                int randomIndex = Random.Range(0, segmentCells.Length);
                for (int j = 0; j < segmentCells.Length; j++)
                {
                    if (j == randomIndex)
                    {
                        do
                        {
                            segmentCells[j].GetComponent<SpriteRenderer>().color = GameControl.colors[Random.Range(0, GameControl.colors.Length)];
                        }
                        while (segmentCells[j].GetComponent<SpriteRenderer>().color == snakeCells[j + offset].GetComponent<SpriteRenderer>().color);
                    }
                    else
                    {
                        segmentCells[j].GetComponent<SpriteRenderer>().color = snakeCells[j + offset].GetComponent<SpriteRenderer>().color;
                    }
                }

                int overturned = Random.Range(0, 2);
                if (overturned == 1)
                {
                    segments[i].GetComponent<Transform>().rotation = new Quaternion(0, 0, 180, 0);
                }
                else
                {
                    segments[i].GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
                }
            }

            
        }
    }

    void PlayerLose()
    {
        foreach (GameObject segment in segments)
        {
            segment.SetActive(false);
        }
        pause.SetActive(false);
        losePanel.SetActive(true);
    }

    void PlayerWin()
    {
        if (nextLevel != 0 && PlayerPrefs.GetInt("Levels") < nextLevel)
        {
            PlayerPrefs.SetInt("Levels", nextLevel);
        }

        foreach (GameObject segment in segments)
        {
            segment.SetActive(false);
        }
        pause.SetActive(false);

        snake.transform.position = Vector3.MoveTowards(snake.transform.position, target, Time.deltaTime * 5);

        winPanel.SetActive(true);
    }
}
