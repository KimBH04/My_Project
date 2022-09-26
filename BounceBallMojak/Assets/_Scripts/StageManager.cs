using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject gameManager;
    private int stars;

    private void Awake()
    {
        stars = GameObject.FindGameObjectsWithTag("Star").Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Select");
        }
    }

    public void StarCount()
    {
        if (--stars == 0)
        {
            gameManager.GetComponent<GameManager>().StagePass();
        }
    }
}
