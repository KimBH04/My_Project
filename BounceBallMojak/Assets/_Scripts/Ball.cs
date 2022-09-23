using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float jumpPower;
    public float speed;

    private bool isCanonL, isCanonR;
    private bool isFarJump, isHighJump;
    private float time;
    private Rigidbody2D rigid2d;

    private float lastTouchTime;
    private float doubleTouchD;

    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        doubleTouchD = .5f;
        lastTouchTime = Time.time;
    }

    private void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");

        transform.position = new Vector2(transform.position.x + input * Time.deltaTime * speed, transform.position.y);

        if (isCanonL)
        {
            transform.localPosition = new Vector2(transform.localPosition.x - Time.deltaTime * 4f, transform.localPosition.y);
        }
        else if (isCanonR)
        {

        }
        else
        {
            rigid2d.gravityScale = 1f;
        }

        if (isFarJump && Input.touchCount == 0)
        {
            Debug.Log("ÅÍÄ¡");
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTouchTime < doubleTouchD)
                {
                    if (Input.GetTouch(0).position.x > Screen.width / 2f)
                    {
                        rigid2d.AddForce(Vector2.right * 200f);
                    }
                    else
                    {
                        rigid2d.AddForce(Vector2.right * -200f);
                    }

                    isFarJump = false;
                }
            }
        }
        if (isHighJump)
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject _gameObject = collision.gameObject;
        int colpoint = collision.contactCount;

        if (colpoint == 4)
        {
            if (_gameObject.name == "High Jump")
            {
                rigid2d.AddForce(Vector2.up * jumpPower * 2f);
            }
            else if (_gameObject.name == "Canon L")
            {
                transform.localPosition = new Vector2(transform.localPosition.x - 1f, (int)(transform.localPosition.y - 1.5f));
                rigid2d.gravityScale = 0f;
                isCanonL = true;
            }
            else if (_gameObject.name == "Canon R")
            {
                isCanonR = true;
            }
            else
            {
                rigid2d.AddForce(Vector2.up * jumpPower);
            }
        }
        else if (colpoint != 2)
        {
            //rigid2d.AddForce(collision.GetContact(0).normal * 100f);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x + 0.1f, transform.localPosition.y);
            //rigid2d.AddForce(collision.GetContact(0).normal * 10f);
        }

        Debug.Log(colpoint);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        time += Time.deltaTime;

        if (time > .2f && collision.contactCount == 3)
        {
            rigid2d.AddForce(Vector2.up * jumpPower / 2f);
        }

        Debug.Log($"{time} {collision.contactCount}");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        time = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Far")
        {
            isFarJump = true;
        }
        if (collision.gameObject.name == "High")
        {
            isHighJump = true;
        }

        Destroy(collision.gameObject);
    }
}
