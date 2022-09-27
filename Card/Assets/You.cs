using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You : MonoBehaviour
{
    public List<GameObject> MyCards;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Card()
    {
        int i = MyCards.Count / 2;
        var v3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        foreach (var item in MyCards)
        {
            Instantiate(item, new Vector3(v3.x + i, v3.y - 10, v3.z + 10), Quaternion.identity);
            i--;
        }
    }
}
