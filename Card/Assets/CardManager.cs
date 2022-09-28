using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject gm;
    public GameObject Na;
    [SerializeField]
    private AnimationCurve curve;


    [HideInInspector]
    public bool Their;
    private GameManager manager;
    private GameObject StackCard;
    private You you;
    private bool turn;

    float time;

    private void Awake()
    {
        gm = GameObject.Find("Game Manager");
        Na = GameObject.Find("You");

        you = Na.GetComponent<You>();
        manager = gm.GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        turn = manager.URTrun;
        StackCard = manager.StackCard;

        if (turn)
        {
            Debug.Log("is your turn");
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
        if (turn)
        {
            string[] StackCardName = StackCard.name.Split('_');
            string[] MyCardName = gameObject.name.Split('_');

            if (StackCardName[0] == MyCardName[0])
            {
                manager.StackCard = gameObject;

                you.ChangeCard(gameObject.name);

                manager.URTrun = false;
                Destroy(gameObject);
            }
            else if (StackCardName[1] == MyCardName[1])
            {

            }
            else if (StackCardName[2] == MyCardName[2] || StackCardName[2] == MyCardName[2])
            {

            }
            else
            {

            }
        }
        else
        {
            Debug.Log("isn't your turn");
        }
    }
}
