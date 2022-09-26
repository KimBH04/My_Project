using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject canvas;

    private int ComplatedStageNum;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("cs"))
        {
            PlayerPrefs.SetInt("cs", 2);
        }

        ComplatedStageNum = PlayerPrefs.GetInt("cs") - 2;

        LevelSet(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void OnReset()
    {
        PlayerPrefs.SetInt("cs", 2);

        LevelSet(false);
    }

    public void S1()
    {
        if (PlayerPrefs.GetInt("cs") >= 2)
            SceneManager.LoadScene("0");
    }
    public void S2()
    {
        if (PlayerPrefs.GetInt("cs") >= 3)
            SceneManager.LoadScene("1");
    }
    public void S3()
    {
        if (PlayerPrefs.GetInt("cs") >= 4)
            SceneManager.LoadScene("2");
    }
    public void S4()
    {
        if (PlayerPrefs.GetInt("cs") >= 5)
            SceneManager.LoadScene("3");
    }
    public void S5()
    {
        if (PlayerPrefs.GetInt("cs") >= 6)
            SceneManager.LoadScene("4");
    }

    public void LevelSet(bool set)
    {
        for (int i = 0; i <= ComplatedStageNum; i++)
        {
            try
            {
                canvas.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(set);
            }
            catch (UnityException)
            {
                Awake();
            }
        }
    }
}
