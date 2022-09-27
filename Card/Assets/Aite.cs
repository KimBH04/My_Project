using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aite : MonoBehaviour
{
    public List<GameObject> AitesCards;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Card()
    {
        int i = 3;
        var v3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        foreach (var item in AitesCards)
        {
            Instantiate(item, new Vector3(v3.x + i, v3.y, v3.z), Quaternion.identity);
            i--; 
        }
    }
}
