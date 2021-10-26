using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    LayerMask layer;
    public GameObject pinkBlock;
    public GameObject whiteBlock;
    bool checkColor;
    public GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        checkColor = true;
        layer = LayerMask.GetMask("ignore");
        pinkBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
        whiteBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
    }

    // Update is called once per frame
    void Update()
    {
       

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, layer);
        if (Input.GetMouseButtonDown(0))
        {

          

            if (hit.collider != null )
            {

                if(hit.collider.gameObject.name == "checkwhite")
                {
                    checkColor = true;
                }
                if(hit.collider.gameObject.name == "checkpink")
                {
                    checkColor = false;
                }

                if(hit.collider.gameObject.CompareTag("block") )
                {
                    if (checkColor)
                    {
                       
                        Destroy(hit.collider.gameObject);
                        GameObject white1 = Instantiate(whiteBlock, hit.collider.transform.position, Quaternion.identity);
                        white1.transform.SetParent(heart.transform, true);

                    }else if (!checkColor)
                    {
                        Destroy(hit.collider.gameObject);
                       GameObject pink1 =  Instantiate(pinkBlock, hit.collider.transform.position, Quaternion.identity);
                        pink1.transform.SetParent(heart.transform, true);
                    }

                }
               
                


            }
                
        }
         
           
    }
   

}


