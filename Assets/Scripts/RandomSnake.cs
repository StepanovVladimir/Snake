using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSnake : MonoBehaviour
{
    public GameObject head;
    public GameObject[] cells;
    public GameObject[] capsules;
    public GameObject tail;

    //private Vector3 target;
    private Vector3 targetRotate1 = new Vector3(0, 0, 270);
    private Vector3 targetRotate2 = new Vector3(0, 0, 0);
    private Vector3 targetRotate3 = new Vector3(0, 0, 90);
    private Vector2 directionHead = new Vector2(5, 0);
    private Vector2 directionBody = new Vector2(0, 5);

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
            capsules[i].GetComponent<Renderer>().material.color = GameControl.colors[color];
            previousColor = color;
        }
    }

    public void Move()
    {
        head.transform.Translate(directionHead * Time.deltaTime);
        foreach (GameObject capsule in capsules)
        {
            capsule.transform.Translate(directionBody * Time.deltaTime);

            if (capsule.transform.localPosition.x < -6.42 && capsule.transform.eulerAngles.z > 0 && capsule.transform.eulerAngles.z <= 90)
            {
                capsule.transform.Rotate(Vector3.back * Time.deltaTime * 230);
                if (capsule.transform.eulerAngles.z > 90 || capsule.transform.eulerAngles.z == 0)
                {
                    capsule.transform.eulerAngles = targetRotate2;
                    capsule.transform.localPosition = new Vector3(-7.79f, capsule.transform.localPosition.y, 0);
                }
            }

            if (capsule.transform.localPosition.y > 2.5 && (capsule.transform.eulerAngles.z == 0 || capsule.transform.eulerAngles.z > 270))
            {
                capsule.transform.Rotate(Vector3.back * Time.deltaTime * 230);
                if (capsule.transform.eulerAngles.z <= 270)
                {
                    capsule.transform.eulerAngles = targetRotate1;
                    capsule.transform.localPosition = new Vector3(capsule.transform.localPosition.x, 3.77f, 0);
                }
            }
        }

        tail.transform.Translate(directionHead * Time.deltaTime);

        if (tail.transform.localPosition.x < -6.42 && tail.transform.eulerAngles.z > 90 && tail.transform.eulerAngles.z <= 180)
        {
            tail.transform.Rotate(Vector3.back * Time.deltaTime * 230);
            if (tail.transform.eulerAngles.z <= 90)
            {
                tail.transform.eulerAngles = targetRotate3;
                tail.transform.localPosition = new Vector3(-7.79f, tail.transform.localPosition.y, 0);
            }
        }

        if (tail.transform.localPosition.y > 2.5 && tail.transform.eulerAngles.z > 0 && tail.transform.eulerAngles.z <= 90)
        {
            tail.transform.Rotate(Vector3.back * Time.deltaTime * 230);
            if (tail.transform.eulerAngles.z > 90 || tail.transform.eulerAngles.z == 0)
            {
                tail.transform.eulerAngles = targetRotate2;
                tail.transform.localPosition = new Vector3(tail.transform.localPosition.x, 3.77f, 0);
            }
        }
    }
}
