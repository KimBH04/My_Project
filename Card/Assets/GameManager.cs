using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _You;
    public GameObject _Aite;

    public List<GameObject> Cards;

    private void Awake()
    {
        StartCoroutine(Distribute());
    }

    void Update()
    {
        
    }

    IEnumerator Distribute()
    {
        yield return new WaitForSeconds(.5f);

        int j;

        for (int i = 0; i < 13;)
        {
            j = Random.Range(0, 55 - i++);
            _You.GetComponent<You>().MyCards.Add(Cards[j]);
            Cards.RemoveAt(j);

            j = Random.Range(0, 55 - i++);
            _Aite.GetComponent<Aite>().AitesCards.Add(Cards[j]);
            Cards.RemoveAt(j);
        }

        _You.GetComponent<You>().Card();
        _Aite.GetComponent<Aite>().Card();
    }
}
