using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private void Awake()
    {
        transform.eulerAngles = new Vector3(180f, 0, 0);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);

        if (Mathf.Abs(transform.position.z) > 16)
            Destroy(gameObject);
    }
}
