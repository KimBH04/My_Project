using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private float minmax = 80f;
    private float x;

    void FixedUpdate()
    {
        x += Input.GetAxis("Mouse Y") * 2f;

        if (x < -10f)
        {
            x -= x + 10f;
            Debug.Log("max");
        }
        else if (x > 30f)
        {
            x -= x - 30f;
            Debug.Log("min");
        }

        transform.localEulerAngles = new Vector3(-x, 0, 0);
    }
}
