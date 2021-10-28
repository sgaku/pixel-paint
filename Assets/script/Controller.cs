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
    public GameObject mini;
   public List<GameObject> heartbl = new List<GameObject>();
   public List<GameObject> minibl = new List<GameObject>();



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

                if(hit.collider.gameObject.CompareTag("whiteblock") || hit.collider.gameObject.CompareTag("pinkblock"))
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

    public void AddList()
    {
        foreach(Transform heartblock in heart.transform)
        {
            if (heartblock.gameObject.CompareTag("whiteblock")||heartblock.gameObject.CompareTag("pinkblock"))
            {
                

                
                heartbl.Add(heartblock.gameObject);
            }
        }

        foreach(Transform miniblock in mini.transform)
        {
            if (miniblock.gameObject.CompareTag("whiteblock")||miniblock.gameObject.CompareTag("pinkblock"))
            {
                minibl.Add(miniblock.gameObject); 
            }
        }
        CheckClear();
    }

    public void CheckClear()
    {
       foreach(GameObject heartblock in heartbl)
        {
            CheckTag1(heartblock);
        }

       foreach(GameObject miniblock in minibl)
        {
            CheckTag2(miniblock);
        } 

    }

    void CheckTag1(GameObject h)
    {

    }
    void CheckTag2(GameObject m)
    {

    }
   

}



