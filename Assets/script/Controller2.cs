using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{
 //   LayerMask layer;
    public GameObject pinkBlock;
    public GameObject whiteBlock;
    bool changeColor;
    public GameObject heart;
    public GameObject mini;
    public List<GameObject> heartbl = new List<GameObject>();
    public List<GameObject> minibl = new List<GameObject>();
    private int count;
    public GameObject clearText;
    public GameObject failText;

    
    // Start is called before the first frame update
    void Start()
    {
        changeColor = false;
       // layer = LayerMask.GetMask("ignore");
        pinkBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
        whiteBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
    }

    // Update is called once per frame
    void Update()
    {


        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {



            if (hit.collider != null)
            {

                if (hit.collider.gameObject.name == "checkwhite")
                {
                    changeColor = false;
                }
                if (hit.collider.gameObject.name == "checkpink")
                {
                    changeColor = true;
                }

                if (hit.collider.gameObject.CompareTag("whiteblock") || hit.collider.gameObject.CompareTag("pinkblock"))
                {
                   
                    if (!changeColor)
                    {
                        GameObject parentObj1 = hit.collider.transform.parent.gameObject;
                        Destroy(hit.collider.gameObject);
                        GameObject white1 = Instantiate(whiteBlock, hit.collider.transform.position, Quaternion.identity);
                        white1.transform.SetParent(parentObj1.transform, true);
                       

                    }
                    else if (changeColor)
                    {
                        GameObject parentObj2 = hit.collider.transform.parent.gameObject;
                        Destroy(hit.collider.gameObject);
                        GameObject pink1 = Instantiate(pinkBlock, hit.collider.transform.position, Quaternion.identity);
                        pink1.transform.SetParent(parentObj2.transform, true);
                    }

                }




            }

        }




    }

    public void AddList()
    {
        foreach (Transform heartParent in heart.transform)
        {
            if (heartParent.gameObject.CompareTag("parent") )
            {
                heartbl.Add(heartParent.gameObject);

            }
        }

        foreach (Transform miniParent in mini.transform)
        {
            if (miniParent.gameObject.CompareTag("parent") )
            {
                minibl.Add(miniParent.gameObject);
            }
        }
        CheckColor();  
    }

    public void CheckColor()
    {
        for(int i = 0; i < 22; i++)
        {
            if(heartbl[i].transform.GetChild(0).gameObject.tag == minibl[i].transform.GetChild(0).gameObject.tag)
            {
                count++;
            }
        }

        if(count == 22)
        {
            clearText.SetActive(true);
        }else if(count < 22)
        {
            failText.SetActive(true);
        }
    }













}








