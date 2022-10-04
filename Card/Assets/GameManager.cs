using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _You;
    public GameObject _Aite;
    public GameObject _Draw;

    public Text CardsText;
    public Text Turn;

    public List<GameObject> Cards;

    private You you;
    private Aite aite;
    [HideInInspector]
    public GameObject StackCard;
    [HideInInspector]
    public bool URTrun, AitesTurn, ThrewOneCard;
    [HideInInspector]
    public bool consecutive = true;
    [HideInInspector]
    public bool MustGetCard = false;
    [HideInInspector]
    public int GetCardStack;


    private bool d;

    private void Awake()
    {
        StartCoroutine(Distribute(0));
        you = _You.GetComponent<You>();
        aite = _Aite.GetComponent<Aite>();

        aite.mgc2m = you.mgc2a = true;
        MustGetCard = false;    //원래 안 해도 되는데 버그인지 자꾸 true가 되서 넣음
        GetCardStack = 7;
    }

    void Update()
    {
        if (GetCardStack > 0)
        {
            MustGetCard = true;
        }

        CardsText.text = $" My : {you.MyCards.Count} " +
            $"Aite : {aite.AitesCards.Count} \n" +
            $"Remain : {Cards.Count} " +
            $"Total : {you.MyCards.Count + aite.AitesCards.Count + Cards.Count}";

        if (URTrun)
        {
            Turn.text = "Your turn";
        }
        else
        {
            Turn.text = "Aite's turn";
        }

        Debug.Log($"Your Turn : {URTrun} || Aite's Turn : {AitesTurn}");
        //Debug.Log(ThrewOneCard);
        //Debug.Log(GetCardStack);
        //Debug.Log(MustGetCard);
    }

    public void TurnEnd()
        //내 턴을 끝냈을 때
    {
        if (URTrun)
        {
            URTrun = false;
            AitesTurn = true;

            if (!ThrewOneCard)
                //내가 카드를 한 번이라도 내지 않았다면
            {
                if (!aite.mgc2m)
                {
                    GetCardStack = 1;
                }

                aite.mgc2m = true;
            }

            consecutive = true;
            ThrewOneCard = false;
            StartCoroutine(Distribute(0));

            //Debug.Log("여기 오긴 하니/");
        }
    }

    public void ChangeCard(GameObject card)
        //Resource 폴더에서 테이블에 놓여있던 카드를 찾아 리스트에 추가
    {
        GameObject StackedCard
            = Resources.Load<GameObject>(
                "Prefab/BackColor_Blue/" + transform.GetChild(0).gameObject.name);

        Destroy(transform.GetChild(0).gameObject);
            //놓여있던 카드는 파괴

        Cards.Add(StackedCard);

        StackCard = card;

        GameObject sc = Instantiate(StackCard, new Vector3(0f, .1f, 0f), Quaternion.identity);
        sc.transform.parent = gameObject.transform;
        sc.GetComponent<CardManager>().Their = true;
        //누군가 낸 카드를 테이블에 배치
    }

    public IEnumerator Distribute(int i)
    {
        yield return new WaitForSeconds(.3f);

        //Debug.Log(i);

        int j = Random.Range(0, Cards.Count);
        //0부터 카드 개수 사이의 수 랜덤

        if (i > GetCardStack * 2 - 1)
            //먹어야 하는 카드 수보다 큰 경우
        {
            //Debug.Log(MustGetCard);

            if (!d)
            {
                StackCard = Cards[j];
                GameObject sc = Instantiate(StackCard, new Vector3(0f, .1f, 0f), Quaternion.identity);
                sc.transform.parent = gameObject.transform;
                sc.GetComponent<CardManager>().Their = true;
                //게임 시작 시 테이블에 카드 한 장 배치

                Cards.RemoveAt(j);

                URTrun = true;
                d = true;
            }

            MustGetCard = false;
            aite.mgc2m = you.mgc2a = false;
            GetCardStack = 0;

            if (AitesTurn)
            {
                StartCoroutine(aite.AitesTurn(0));
            }
            else
            {
                URTrun = true;
            }

            yield break;
        }

        var v2 = new Vector2(-10f, 1.4f);

        if (i % 2 == 0 && aite.mgc2m)
            //나에게 카드 지급
        {
            Instantiate(_Draw, v2, Quaternion.identity).GetComponent<Card>().speed = -.7f;

            you.MyCards.Add(Cards[j]);

            StartCoroutine(you.PickMyCard(you.MyCards.Count - 1));

            Cards.RemoveAt(j);
        }
        else if (i % 2 == 1 && you.mgc2a)
            //상대에게 카드 지급
        {
            AitesTurn = false;

            Instantiate(_Draw, v2, Quaternion.identity).GetComponent<Card>().speed = .7f;

            aite.AitesCards.Add(Cards[j]);
            
            Cards.RemoveAt(j);
        }

        StartCoroutine(Distribute(++i));
    }






    public void SceneReload()
    {
        SceneManager.LoadScene("SampleScene");
    }
}