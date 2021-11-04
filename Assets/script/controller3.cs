using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller3 : MonoBehaviour
{
    public GameObject brownBlock;
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
    
    // Start is called before the first frame update
    void Start()
    {
        changeColor = false;

        brownBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
        whiteBlock.transform.localScale = new Vector3(0.95f, 0.95f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!changeColor)
        {
            outline.transform.position = new Vector3(0.7f, -5, 0);
        }
        else if (changeColor)
        {
            outline.transform.position = new Vector3(-0.5f, -5, 0);
        }

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
                if (hit.collider.gameObject.name == "checkbrown")
                {
                    changeColor = true;
                }
                if (hit.collider.gameObject.CompareTag("whiteblock") || hit.collider.gameObject.CompareTag("brownblock"))
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
                        GameObject pink1 = Instantiate(brownBlock, hit.collider.transform.position, Quaternion.identity);
                        pink1.transform.SetParent(parentObj2.transform, true);
                    }

                }


            }
        }

       
    }
    public void ChangePosition()
    {
        original.GetComponent<Animation>().Play("slide2");
        mini.GetComponent<Animation>().Play("leftdown2");

        Invoke("AddList", 2);
    }

    public void AddList()
    {
        foreach (Transform heartParent in original.transform)
        {
            if (heartParent.gameObject.CompareTag("parent"))
            {
                originalbl.Add(heartParent.gameObject);

            }
        }

        foreach (Transform miniParent in mini.transform)
        {
            if (miniParent.gameObject.CompareTag("parent"))
            {
                minibl.Add(miniParent.gameObject);
            }
        }
        CheckColor();
    }
    public void CheckColor()
    {
        stars.SetActive(true);
        for (int i = 0; i < 29; i++)
        {
            
            if (originalbl[i].transform.GetChild(0).gameObject.CompareTag("brownblock") && originalbl[i].transform.GetChild(0).gameObject.tag == minibl[i].transform.GetChild(0).gameObject.tag)
            {
                equelCount++;
            }
        }
        if (equelCount == 12)
        {
            starCount = 3;
            StartCoroutine("Star");

        }
        else if (equelCount < 12 && equelCount >= 7)
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
        for (int i = 0; i < starCount; i++)
        {
            GameObject star = stars.transform.GetChild(i).gameObject;
            star.GetComponent<Animation>().Play("move");
            yield return new WaitForSeconds(1);
        }
       
        GoalEffect();
       
    }
    void GoalEffect()
    {
        GameObject goal = Instantiate(goalEffect);
        goal.transform.SetParent(Camera.main.transform);

        goal.transform.localPosition = Vector3.up * 5f;
        goal.transform.eulerAngles = new Vector3(0, 100, 0);
      
    }

}
