using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aite : MonoBehaviour
{
    public List<GameObject> AitesCards;

    public GameObject gm;
    public GameObject _You;

    private You you;
    private GameManager manager;
    private GameObject StackCard;

    private bool Consecutive;
    private bool ThrewCard;
    private bool ThrewOneCard;
    [HideInInspector]
    public bool mgc2m;

    private void Awake()
    {
        manager = gm.GetComponent<GameManager>();
        you = _You.GetComponent<You>();
    }

    private void FixedUpdate()
    {
        StackCard = manager.StackCard;
        Consecutive = manager.consecutive;

        //Debug.Log(ThrewOneCard);
    }

    public IEnumerator AitesTurn(int i)
    {
        yield return new WaitForSeconds(.2f);

        //Debug.Log(i + "asdf");

        if (i >= AitesCards.Count)      //상대의 카드수보다 크면
        {
            if (!ThrewOneCard)      //한 번도 카드를 내지 않았다면
            {
                manager.GetCardStack = 1;       //카드를 한 장 뽑는다
                you.mgc2a = true;
                StartCoroutine(manager.Distribute(0));
            }

            manager.URTrun = true;      //나의 턴으로 변경
            manager.consecutive = true;
            manager.ThrewOneCard = false;
            ThrewOneCard = false;

            yield break;
        }

        string[] StackCardName = StackCard.name.Split('_');     //테이블에 놓인 카드의 이름
        string[] AitesCardName = AitesCards[i].name.Split('_');    //상대가 들고 있는 카드의 이름

        #region Card Test
        if (!manager.MustGetCard && StackCardName[0] == AitesCardName[0] && Consecutive)
            //모양이 같다면
        {
            ThrowACard(i, AitesCards[i], AitesCardName[0], int.Parse(AitesCardName[1]));
        }
        else if (AitesCardName[0] == "J")
            //상대가 들고 있는 카드가 조커라면
        {
            if (AitesCardName[2] == StackCardName[2] && Consecutive)
                //상대가 들고 있는 카드와 테이블에 있는 카드와 색이 같다면
            {
                ThrowACard(i, AitesCards[i], AitesCardName[2], 14);
            }
            else if (StackCardName[0] == "J"
                && StackCardName[2] == "B"
                && AitesCardName[2] == "C")
                //상대가 들고 있는 카드가 컬러조커이고 테이블에 놓인 카드가 흑백조커라면
            {
                ThrowACard(i, AitesCards[i], "C", 14);
            }
            
        }
        else if (StackCardName[0] == "J")
            //테이블에 놓인 카드가 조커라면
        {
            if (!manager.MustGetCard
                && AitesCardName[2] == StackCardName[2]
                && Consecutive)
                //테이블에 놓인 카드와 상대가 들고 있는 카드와 색이 같다면
            {
                ThrowACard(i, AitesCards[i], AitesCardName[2], int.Parse(AitesCardName[1]));
            }
        }
        else if (StackCardName[1] == AitesCardName[1])
            //숫자가 같다면
        {
            ThrowACard(i, AitesCards[i], AitesCardName[0], int.Parse(AitesCardName[1]));
        }
        else
        {
            ThrewCard = false;
        }
        #endregion

        //카드를 냈을 경우 ++i되어 '빈 곳을 채운 다음 카드'를 놓지지 않게 하기 위함
        if (ThrewCard)
        {
            StartCoroutine(AitesTurn(i));
        }
        else
        {
            StartCoroutine(AitesTurn(++i));
        }
    }

    public void ThrowACard(int card, GameObject Card, string CardShape, int sum)
    {
        #region Card's Type
        if (sum == 1)
        {
            if (CardShape == "S")
                //낸 카드가 Ace라면
            {
                manager.GetCardStack += 5;
            }
            else
                //그냥 A라면
            {
                manager.GetCardStack += 3;
            }

            you.mgc2a = false;
            mgc2m = true;
        }
        else if (sum == 2)
            //낸 카드가 2라면
        {
            manager.GetCardStack += sum;

            you.mgc2a = false;
            mgc2m = true;
        }
        else if (sum == 14)
        {
            if (CardShape == "B")
            // 낸 카드가 흑백조커라면
            {
                manager.GetCardStack += 7;
            }
            else
            //낸 카드가 컬러조커라면
            {
                manager.GetCardStack += 10;
            }

            you.mgc2a = false;
            mgc2m = true;
        }
        #endregion

        manager.ChangeCard(Card);

        AitesCards.RemoveAt(card);

        ThrewCard = true; 
        manager.consecutive = false;
        ThrewOneCard = true;
    }
}
