using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public GameObject parent;
    GameObject parentPosition;

    private void Awake()
    {
        Array();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Array()
    {

        parentPosition = Instantiate(parent, new Vector3(0, 0, 0), Quaternion.identity);
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 6; y++)
            {
                Instantiate(block);
              
                block.transform.position = new Vector3(x, y, 0);
                block.transform.SetParent(parent.transform,true);

            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
