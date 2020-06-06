using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void Continue()
    {
        if (PlayerPrefs.GetInt("Levels") == 0)
        {
            SceneManager.LoadScene("Training");
        }
        else
        {
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Levels"));
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Training()
    {
        SceneManager.LoadScene("Training");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void Level5()
    {
        SceneManager.LoadScene("Level5");
    }
    
    public void Level6()
    {
        SceneManager.LoadScene("Level6");
    }

    public void Level7()
    {
        SceneManager.LoadScene("Level7");
    }

    public void Level8()
    {
        SceneManager.LoadScene("Level8");
    }
}
