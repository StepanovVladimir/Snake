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
        new Vector4(0f, 0f, 0f, 1f),
        new Vector4(1f, 0f, 0f, 1f),
        new Vector4(0f, 1f, 0f, 1f),
        new Vector4(0f, 0f, 1f, 1f),
        new Vector4(1f, 1f, 0f, 1f),
    };

    public GameObject snake;
    public GameObject[] segments;
    public GameObject losePanel;
    public GameObject winPanel;
    //public float time;
    //public Text timer;
    public int nextLevel;
    public GameObject snakePart1;
    public GameObject snakePart2;
    public GameObject pause;
    public GameObject pausePanel;

    [HideInInspector]
    public GameState gameState;

    private int offset;
    private Vector3 target;
    private Vector3 targetRotate1 = new Vector3(0, 0, 270);
    private Vector3 targetRotate2 = new Vector3(0, 0, 180);

    void Start()
    {
        //timer.text = time.ToString();
        gameState = GameState.Play;
        offset = 0;
        target = new Vector3(30, snake.transform.position.y, snake.transform.position.z);
        GenerateColors();
    }

    void Update()
    {
        /*if (time > 0 && gameState == GameState.Play)
        {
            time -= Time.deltaTime;
            timer.text = Mathf.Round(time).ToString();
        }
        else if (time <= 0 || gameState == GameState.Lose)
        {
            PlayerLose();
        }
        else
        {
            PlayerWin();
        }*/
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

    public void OnPause()
    {
        foreach (GameObject segment in segments)
        {
            segment.SetActive(false);
        }
        pause.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OffPause()
    {
        pausePanel.SetActive(false);
        foreach (GameObject segment in segments)
        {
            segment.SetActive(true);
        }
        pause.SetActive(true);
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
        if (snakePart1.transform.eulerAngles.z == 0 || snakePart1.transform.eulerAngles.z > 270)
        {
            snakePart1.transform.Rotate(Vector3.back * Time.deltaTime * 50);
        }
        else
        {
            snakePart1.transform.eulerAngles = targetRotate1;
        }

        if (snakePart2 != null)
        {
            if (snakePart2.transform.eulerAngles.z == 0 || snakePart2.transform.eulerAngles.z > 180)
            {
                snakePart2.transform.Rotate(Vector3.back * Time.deltaTime * 50);
            }
            else
            {
                snakePart2.transform.eulerAngles = targetRotate2;
            }
        }
        
        winPanel.SetActive(true);
    }
}
