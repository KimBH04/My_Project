using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject gm;
    public GameObject Na;
    public GameObject Nu;
    [SerializeField]
    private AnimationCurve curve;

    [HideInInspector]
    public bool Their;
    private bool ThrewOneCard;
    private bool Consecutive;
    private bool turn;
    private GameManager manager;
    private GameObject StackCard;
    private You you;
    private Aite aite;

    float time;

    private void Awake()
    {
        gm = GameObject.Find("Game Manager");
        Na = GameObject.Find("You");
        Nu = GameObject.Find("Aite");

        you = Na.GetComponent<You>();
        aite = Nu.GetComponent<Aite>();
        manager = gm.GetComponent<GameManager>();
    }

    private void Start()
    {
        //소환한 카드의 "(Clone)"텍스트 제거
        int index = gameObject.name.IndexOf("(Clone)");
        gameObject.name = gameObject.name.Substring(0, index);
    }

    void FixedUpdate()
    {
        turn = manager.URTrun;
        StackCard = manager.StackCard;
        Consecutive = manager.consecutive;

        if (turn)
        {
            //Debug.Log("is your turn");
        }

        #region slideUp

        if (time > 1f || Their)
            return;

        time += Time.deltaTime;
        float z = curve.Evaluate(1f - time);

        transform.localPosition
            = new Vector3(
                transform.localPosition.x, 
                transform.localPosition.y, 
                transform.localPosition.z + z / 2.5f);

        #endregion
        //void 내 이 아래로 코드 작성 금지
        //Must not write code under this line in this void
    }

    private void OnMouseDown()
    {
        if (turn && transform.parent.name != "Game Manager")
        {
            #region Card Test

            var StackCardName = StackCard.name.Split('_');
            var MyCardName = gameObject.name.Split('_');

            if (manager.Seven)
            {
                StackCardName[0] = manager.shape;
            }

            if (manager.MustGetCard)
            {
                Debug.Log($"{StackCardName[0]} {MyCardName[0]} {StackCardName[1]} {MyCardName[1]}");

                if (StackCardName[0] == MyCardName[0] && Consecutive)
                //내 카드와 테이블에 놓인 카드의 모양이 같은 경우
                {
                    if (StackCardName[1] == "2" && MyCardName[1] == "1")
                        ThrowACard(MyCardName[0], int.Parse(MyCardName[1]));
                }
                else if (MyCardName[0] == "J")
                //내가 가진 카드가 조커인 경우
                {
                    if (MyCardName[2] == StackCardName[2] && Consecutive)
                    //내가 가진 카드와 테이블에 놓인 카드의 색이 같을 경우
                    {
                        ThrowACard(MyCardName[2], 14);
                    }
                    else if (StackCardName[0] == "J" && StackCardName[2] == "B")
                    {
                        if (MyCardName[2] == "C")
                        {
                            ThrowACard("C", 14);
                        }
                        else if (MyCardName[0] == "S" && MyCardName[1] == "4")
                        {
                            ThrowACard("S", 4);
                        }
                    }
                }
                else if (StackCardName[1] == MyCardName[1])
                //숫자가 같은 경우
                {
                    ThrowACard(MyCardName[0], int.Parse(MyCardName[1]));
                }
            }
            else
            {
                if (StackCardName[0] == MyCardName[0] && Consecutive)
                    //내 카드와 테이블에 놓인 카드의 모양이 같은 경우
                {
                    ThrowACard(MyCardName[0], int.Parse(MyCardName[1]));
                }
                else if (MyCardName[0] == "J")
                    //내가 가진 카드가 조커인 경우
                {
                    if (MyCardName[2] == StackCardName[2] && Consecutive)
                        //내가 가진 카드와 테이블에 놓인 카드의 색이 같을 경우
                    {
                        ThrowACard(MyCardName[2], 14);
                    }
                    else if (StackCardName[0] == "J"
                        && StackCardName[2] == "B" 
                        && MyCardName[2] == "C")
                        //내가 가진 카드가 컬러조커이고 테이블에 놓인 카드가 흑백조커인 경우
                    {
                        ThrowACard("C", 14);
                    }
                }
                else if (StackCardName[0] == "J")
                    //테이블에 놓인 카드가 조커인 경우
                {
                    if (MyCardName[2] == StackCardName[2] && Consecutive)
                          //테이블에 놓인 카드와 내가 가진 카드와 색이 같은 경우
                    {
                        ThrowACard(MyCardName[2], int.Parse(MyCardName[1]));
                    }
                }
                else if (StackCardName[1] == MyCardName[1])
                    //숫자가 같은 경우
                {
                    ThrowACard(MyCardName[0], int.Parse(MyCardName[1]));
                }
            }

            #endregion
        }
        else
        {
            //Debug.Log("isn't your turn");
        }
    }

    public void ThrowACard(string CardShape, int num)
    {
        #region Card's Type
        if (num == 1)
        {
            if (CardShape == "S")
            {
                manager.GetCardStack += 5;
            }
            else
            {
                manager.GetCardStack += 3;
            }
            aite.mgc2m = false;
            you.mgc2a = true;
        }
        else if(num == 2)
        {
            manager.GetCardStack += 2;

            aite.mgc2m = false;
            you.mgc2a = true;
        }
        else if (num == 14)
        {
            if (CardShape == "B")
            {
                manager.GetCardStack += 7;
            }
            else
            {
                manager.GetCardStack += 10;
            }
            aite.mgc2m = false;
            you.mgc2a = true;
        }
        #endregion

        manager.StackCard = gameObject;

        you.ChangeCard(gameObject.name);

        Destroy(gameObject);

        if (num != 11 && num != 12 && num != 13)
        {
            manager.consecutive = false;
            manager.ThrewOneCard = true;
        }

        manager.Seven = false;
    }
}
