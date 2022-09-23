using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int stageNumber;

    private void Awake()
    {
        stageNumber = int.Parse(SceneManager.GetActiveScene().name);
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

        SceneManager.LoadScene(a.ToString());
        StopCoroutine(NextStage(0));
    }
}
