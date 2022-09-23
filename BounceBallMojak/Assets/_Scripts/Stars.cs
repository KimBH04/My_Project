using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public GameObject stageManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            stageManager.GetComponent<StageManager>().StarCount();
            Destroy(gameObject);
        }
    }
}
