using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickAct()
    {
      GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.3f);
      Destroy(gameObject, 0.05f);
    }
}
