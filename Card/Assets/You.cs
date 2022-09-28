using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You : MonoBehaviour
{
    [SerializeField]
    private GameObject gm;
    public List<GameObject> MyCards;

    private void FixedUpdate()
    {
        
    }










    public int ChangeCard(string name)
    {
        for (int i = 0; i < MyCards.Count; i++)
        {
            if (name == MyCards[i].name)
            {
                gm.GetComponent<GameManager>().ChangeCard(MyCards[i]);
                MyCards.RemoveAt(i);

                for (int j = i; j < MyCards.Count; j++)
                {
                    MyCards[j].transform.position
                        = new Vector3
                        (
                  /*x*/ transform.position.x + 14f - j * 1.1f,
                  /*y*/ 3f - j * .01f,
                  /*z*/ -20f
                        );
                }

                Debug.Log("에엒따");
                break;
            }
        }

        return 0;
    }

    public void Card(int num)
    {
        StartCoroutine(PickMyCard(num));
    }

    IEnumerator PickMyCard(int num)
    {
        yield return new WaitForSeconds(.7f);

        var v3 = new Vector3(
            transform.position.x + 14f - num * 1.1f, 
            3f - num * .01f, 
            -20f);

        Instantiate(MyCards[num], v3, Quaternion.identity).transform.parent = gameObject.transform;
    }
}
