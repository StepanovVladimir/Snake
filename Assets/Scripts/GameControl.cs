using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public enum GameState
    {
        Play,
        Lose,
        Win
    }

    public static Color[] colors = {
        new Vector4(1f, 1f, 1f, 1f),
        new Vector4(0.15f, 0.15f, 0.15f, 1f),
        new Vector4(1f, 0f, 0f, 1f),
        new Vector4(0f, 1f, 0f, 1f),
        new Vector4(0f, 0f, 1f, 1f),
        new Vector4(1f, 1f, 0f, 1f),
    };

    public GameObject snake;
    public GameObject[] segments;
    public GameObject losePanel;
    public GameObject winPanel;
    public int nextLevel;

    [HideInInspector]
    public GameState gameState;

    private int offset;

    void Start()
    {
        gameState = GameState.Play;
        offset = 0;
        GenerateColors();
    }

    void FixedUpdate()
    {
        if (gameState == GameState.Lose)
        {
            PlayerLose();
        }
        else if (gameState == GameState.Win)
        {
            PlayerWin();
        }
    }

    public void NextColors()
    {
        for (int i = 0; i < segments[0].GetComponent<SegmentHandler>().cells.Length; i++)
        {
            snake.GetComponent<RandomSnake>().cells[i + offset].SetActive(false);
            snake.GetComponent<RandomSnake>().capsules[i + offset].SetActive(true);
        }

        offset += segments[0].GetComponent<SegmentHandler>().cells.Length;

        if (offset < snake.GetComponent<RandomSnake>().cells.Length)
        {
            GenerateColors();
        }
        else
        {
            gameState = GameState.Win;
        }
    }

    void GenerateColors()
    {
        int indexOfRightSegment = Random.Range(0, segments.Length);
        for (int i = 0; i < segments.Length; i++)
        {
            GameObject[] segmentCells = segments[i].GetComponent<SegmentHandler>().cells;
            GameObject[] snakeCells = snake.GetComponent<RandomSnake>().cells;
            if (i == indexOfRightSegment)
            {
                segments[i].GetComponent<SegmentHandler>().right = true;
                for (int j = 0; j < segmentCells.Length; j++)
                {
                    segmentCells[j].GetComponent<SpriteRenderer>().color = snakeCells[j + offset].GetComponent<SpriteRenderer>().color;
                }
            }
            else
            {
                segments[i].GetComponent<SegmentHandler>().right = false;
                int randomIndex = Random.Range(0, segmentCells.Length);
                for (int j = 0; j < segmentCells.Length; j++)
                {
                    if (j == randomIndex)
                    {
                        do
                        {
                            segmentCells[j].GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                        }
                        while (segmentCells[j].GetComponent<SpriteRenderer>().color == snakeCells[j + offset].GetComponent<SpriteRenderer>().color);
                    }
                    else
                    {
                        segmentCells[j].GetComponent<SpriteRenderer>().color = snakeCells[j + offset].GetComponent<SpriteRenderer>().color;
                    }
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

    void PlayerLose()
    {
        foreach (GameObject segment in segments)
        {
            segment.SetActive(false);
        }
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

        snake.GetComponent<RandomSnake>().Move();

        winPanel.SetActive(true);
    }
}
