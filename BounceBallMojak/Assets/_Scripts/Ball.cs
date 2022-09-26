using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ball : MonoBehaviour
{
    public float jumpPower;
    public float speed;
    public Tilemap ball;

    private bool isWall;
    private bool isCanonL, isCanonR;
    private bool isFarJump, isHighJump;
    private float time;
    private Rigidbody2D rigid2d;

    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");

        transform.position = new Vector2(transform.position.x + input * Time.deltaTime * speed, transform.position.y);

        if (isCanonL)
        {
            transform.localPosition = new Vector2(transform.localPosition.x - Time.deltaTime * 4f, transform.localPosition.y);

            if (input != 0)
            {
                isCanonL = false;
            }
        }
        else if (isCanonR)
        {
            transform.localPosition = new Vector2(transform.localPosition.x - Time.deltaTime * -4f, transform.localPosition.y);

            if (input != 0)
            {
                isCanonR = false;
            }
        }
        else
        {
            rigid2d.gravityScale = 1f;
        }


        if (isFarJump)
        {
            Debug.Log("far");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (input > 0)
                {
                    rigid2d.AddForce(Vector2.right * 250f);
                    isFarJump = false;
                }
                else if (input < 0)
                {
                    rigid2d.AddForce(Vector2.right * -250f);
                    isFarJump = false;
                }
                ball.color = Color.white;
            }
        }
        
        if (isHighJump)
        {
            Debug.Log("high");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (input != 0)
                {
                    rigid2d.AddForce(Vector2.up * 9f, ForceMode2D.Impulse);
                    ball.color = Color.white;
                    isHighJump = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject _gameObject = collision.gameObject;
        int colpoint = collision.contactCount;

        if (colpoint == 4)
        {
            isWall = false;

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
                transform.localPosition = new Vector2(transform.localPosition.x + 1f, (int)(transform.localPosition.y - 1.5f));
                rigid2d.gravityScale = 0f;
                isCanonR = true;
            }
            else
            {
                rigid2d.AddForce(Vector2.up * jumpPower);
            }
        }
        /*
        else if (colpoint != 2)
        {
            rigid2d.AddForce(collision.GetContact(0).normal * 100f);
        }*/
        else
        {
            isWall = true;

            Vector2 v2 = collision.GetContact(0).normal;
            transform.localPosition = new Vector2(transform.localPosition.x + v2.x * .1f, transform.localPosition.y + v2.y * .1f);

            //rigid2d.AddForce(collision.GetContact(0).normal * 10f);
        }

        Debug.Log(colpoint);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        time += Time.deltaTime;

        Debug.Log(collision.contactCount);

        if (time > .1f && collision.contactCount == 3 && !isWall)
        {
            rigid2d.AddForce(Vector2.up * jumpPower / 2f);
        }

        //Debug.Log($"{time} {collision.contactCount}");
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
            ball.color = Color.red;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "High")
        {
            isHighJump = true;
            ball.color = Color.green;
            Destroy(collision.gameObject);
        }
    }
}
