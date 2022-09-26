using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int stageNumber;

    private void Awake()
    {
        try
        {
            stageNumber = int.Parse(SceneManager.GetActiveScene().name);
        }
        catch (FormatException)
        {
            return;
        }
    }

    public void StagePass()
    {
        StartCoroutine(NextStage(stageNumber + 1));
    }

    public void StageRestart()
    {
        StartCoroutine(NextStage(stageNumber));
    }

    IEnumerator NextStage(int a)
    {
        yield return new WaitForSeconds(1f);

        if (a < 5)
        {
            SceneManager.LoadScene(a.ToString());

            int i = PlayerPrefs.GetInt("cs");
            if (i < a + 2)
                PlayerPrefs.SetInt("cs", a + 2);
        }
        else
        {
            SceneManager.LoadScene("Select");
        }

        StopCoroutine(NextStage(0));
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Select");
    }
    public void How()
    {
        
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
