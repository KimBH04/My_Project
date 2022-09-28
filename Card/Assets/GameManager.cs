using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _You;
    public GameObject _Aite;
    public GameObject _Draw2_U;
    public GameObject _Draw2Aite;

    public List<GameObject> Cards;

    private You you;

    [HideInInspector]
    public GameObject StackCard;
    
    [HideInInspector]
    public bool URTrun, AitesTurn;

    private void Awake()
    {
        StartCoroutine(Distribute(0));
        you = _You.GetComponent<You>();
    }

    void Update()
    {
        
    }

    public void ChangeCard(GameObject card)
    {
        Instantiate(StackCard, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Cards.Add(card);
    }

    IEnumerator Distribute(int i)
    {
        yield return new WaitForSeconds(.3f);

        int j = Random.Range(0, 54 - i);

        if (i > 13)
        {
            StackCard = Cards[j];
            Instantiate(Cards[j], new Vector3(0f, 1f, 0f), Quaternion.identity)
                .GetComponent<CardManager>().Their = true;
            Cards.RemoveAt(j);

            URTrun = true;

            yield break;
        }

        var v2 = new Vector2(-10f, 1.4f);

        if (i % 2 == 0)
        {
            Instantiate(_Draw2_U, v2, Quaternion.identity).GetComponent<Card>().speed = -.7f;

            you.MyCards.Add(Cards[j]);

            you.Card(i / 2);
        }
        else
        {
            Instantiate(_Draw2Aite, v2, Quaternion.identity).GetComponent<Card>().speed = .7f;

            _Aite.GetComponent<Aite>().AitesCards.Add(Cards[j]);
        }

        Cards.RemoveAt(j);

        StartCoroutine(Distribute(++i));
    }
}
