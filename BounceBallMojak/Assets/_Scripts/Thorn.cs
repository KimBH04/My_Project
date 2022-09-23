using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Destroy(collision.gameObject);
            gameManager.GetComponent<GameManager>().StageRestart();
        }
    }
}
