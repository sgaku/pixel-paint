using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    LayerMask layer;
    public GameObject pinkBlock;
    public GameObject whiteBlock;
  

    // Start is called before the first frame update
    void Start()
    {
        
        layer = LayerMask.GetMask("ignore");
        pinkBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
        whiteBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        checkColor();

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, layer);
        if (Input.GetMouseButtonDown(0))
        {

          

            if (hit.collider != null )
            {
                if(hit.collider.gameObject.CompareTag("block") )
                {
                    if (checkColor())
                    {
                        Debug.Log("checkColor");
                        Destroy(hit.collider.gameObject);
                        Instantiate(whiteBlock, hit.collider.transform.position, Quaternion.identity);
                    }else if (!checkColor())
                    {Debug.Log("!checkColor");
                        Destroy(hit.collider.gameObject);
                        Instantiate(pinkBlock, hit.collider.transform.position, Quaternion.identity);
                    }

                }
               
                


            }
                
        }
         
           
    }
    bool checkColor()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, layer);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.gameObject.name == "checkwhite")
            {
                Debug.Log("t");
                return true;
            }
            else if (hit.collider != null && hit.collider.gameObject.name == "checkpink")
            {
                Debug.Log("f");
                return false;
            }
        }
        return false;

    }

}



