using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You : MonoBehaviour
{
    [SerializeField]
    private GameObject gm;
    public List<GameObject> MyCards;

    [HideInInspector]
    public bool mgc2a;

    public void ChangeCard(string name)
    {
        //Debug.Log(name);

        for (int i = 0; i < MyCards.Count; i++)
            //내가 가진 카드 중 내가 낸 카드의 위치 찾기
        {
            //Debug.Log(MyCards[i].name);

            if (name == MyCards[i].name)
                //내가 가진 카드와 내가 낸 카드의 이름이 같으면
            {
                gm.GetComponent<GameManager>().ChangeCard(MyCards[i]);
                MyCards.RemoveAt(i);

                for (int j = i; j < MyCards.Count; j++)
                    //카드들을 옆으로 옮겨 빈자리 메꾸기
                {
                    GameObject card = GameObject.Find(MyCards[j].name);
                        card.transform.position = new Vector3
                        (
                  /*x*/ transform.position.x + 18.6f - j * 1.7f,
                  /*y*/ 3f - j * .01f,
                  /*z*/ -9.2f
                        );
                    //Debug.Log(j);
                }

                break;
            }
        }
    }

    public IEnumerator PickMyCard(int num)
        //내가 가진 카드를 화면에 보이기
    {
        yield return new WaitForSeconds(.7f);

        //Debug.Log(num);

        var v3 = new Vector3(
            transform.position.x + 18.6f - num * 1.7f, 
            3f - num * .01f, 
            -19f);

        Instantiate(MyCards[num], v3, Quaternion.identity).transform.parent = gameObject.transform;
    }
}
