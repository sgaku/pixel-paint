using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{

    public GameObject pinkBlock;
    public GameObject whiteBlock;
    bool changeColor;
    public GameObject original;
    public GameObject mini;
    public List<GameObject> originalbl = new List<GameObject>();
    public List<GameObject> minibl = new List<GameObject>();
    private int equelCount;
    private int starCount;
   
    public GameObject stars;
    public GameObject goalEffect;
    public GameObject outline;

    public GameObject button;
    public GameObject next;

    // Start is called before the first frame update
    void Start()
    {
        changeColor = false;
     
        pinkBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
        whiteBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {



            if (hit.collider != null)
            {

                if (hit.collider.gameObject.name == "checkwhite")
                {
                    if(outline.transform.position.x < 0)
                    {
                        outline.GetComponent<Animation>().Play("movewhite");
                    }
                    changeColor = false;
                   
                }
                if (hit.collider.gameObject.name == "checkpink")
                {
                    if(outline.transform.position.x > 0)
                    {
                        outline.GetComponent<Animation>().Play("movepink");
                    }
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
   public  void ChangePosition()
    {
        original.GetComponent<Animation>().Play("slide");
        mini.GetComponent<Animation>().Play("leftdown");

        Invoke("AddList", 2);
    }

    public void AddList()
    {
        foreach (Transform originalParent in original.transform)
        {
            if (originalParent.gameObject.CompareTag("parent") )
            {
                originalbl.Add(originalParent.gameObject);

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
        stars.SetActive(true);
        for(int i = 0; i < 22; i++)
        {
            if(originalbl[i].transform.GetChild(0).gameObject.tag == minibl[i].transform.GetChild(0).gameObject.tag)
            {
                equelCount++;
            }
        }
        if(equelCount == 22)
        { 
            starCount = 3;
            StartCoroutine("Star");

        }else if(equelCount < 22 && equelCount >= 11)
        {
            starCount = 2;
            StartCoroutine("Star");
        }
        else
        {
            starCount = 1;
            StartCoroutine("Star");
           
        }

        
    }

    IEnumerator Star()
    {
        for(int i = 0; i < starCount; i++)
        {
            GameObject star = stars.transform.GetChild(i).gameObject;
            star.GetComponent<Animation>().Play("move");
            yield return new WaitForSeconds(1);
        }
        Destroy(button);
        GoalEffect();  
    }

     void GoalEffect()
    {
        GameObject goal = Instantiate(goalEffect);
        goal.transform.SetParent(Camera.main.transform);

        goal.transform.localPosition = Vector3.up * 5f;
        goal.transform.eulerAngles = new Vector3(0,100,0);
        next.SetActive(true);
    }

    













}








