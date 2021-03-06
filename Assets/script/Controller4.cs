using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller4 : MonoBehaviour
{

    public GameObject original;
    public GameObject mini;

    public List<GameObject> originalbl = new List<GameObject>();
    public List<GameObject> minibl = new List<GameObject>();



    public GameObject yellowblock;
    public GameObject brownblock;
    public GameObject grayblock;

    private int colorNumber;
    private int equelCount;
    private int starCount;

    public GameObject stars;
    public GameObject goalEffect;

    public GameObject outline;
    




    // Start is called before the first frame update
    void Start()
    {
        colorNumber = 1;
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
                if(hit.collider.gameObject.name == "checkyellow")
                {
                    if (outline.transform.position.x < -0.6f && outline.transform.position.x > -1)
                    {
                        outline.GetComponent<Animation>().Play("touchslide4");
                    }else if (outline.transform.position.x > 0.7f )
                    {
                        outline.GetComponent<Animation>().Play("touchslide6");
                    }
                    colorNumber = 1;
                }

                if(hit.collider.gameObject.name == "checkbrown")
                {
                    if (outline.transform.position.x < -2 )
                    {
                        outline.GetComponent<Animation>().Play("touchslide1");
                    }
                    else if (outline.transform.position.x > 0.7f)
                    {
                        outline.GetComponent<Animation>().Play("touchslide3");
                    }
                    colorNumber = 2;
                    

                }
                if(hit.collider.gameObject.name == "checkgray")
                {
                    if (outline.transform.position.x < -2)
                    {
                        outline.GetComponent<Animation>().Play("touchslide5");
                    }
                    else if (outline.transform.position.x < -0.6 && outline.transform.position.x > -1)
                    {
                        outline.GetComponent<Animation>().Play("touchslide2");
                    }
                    colorNumber = 3;
                    
                }

                if(hit.collider.gameObject.CompareTag("yellowblock") || hit.collider.gameObject.CompareTag("brownblock")
                    || hit.collider.gameObject.CompareTag("darkgrayblock")|| hit.collider.gameObject.CompareTag("whiteblock"))
                {
                    if(colorNumber == 1)
                    {
                        GameObject parentObj1 = hit.collider.transform.parent.gameObject;
                        Destroy(hit.collider.gameObject);
                        GameObject yellow = Instantiate(yellowblock, hit.collider.transform.position, Quaternion.identity);
                        yellow.transform.SetParent(parentObj1.transform, true);
                    }
                    else if(colorNumber == 2)
                    {
                        GameObject parentObj2 = hit.collider.transform.parent.gameObject;
                        Destroy(hit.collider.gameObject);
                        GameObject brown = Instantiate(brownblock, hit.collider.transform.position, Quaternion.identity);
                        brown.transform.SetParent(parentObj2.transform, true);

                    }else if(colorNumber == 3)
                    {
                        GameObject parentObj3 = hit.collider.transform.parent.gameObject;
                        Destroy(hit.collider.gameObject);
                        GameObject gray = Instantiate(grayblock, hit.collider.transform.position, Quaternion.identity);
                        gray.transform.SetParent(parentObj3.transform, true);
                    }

                }



            }
        }
    }

    public void ChangePosition()
    {
        original.GetComponent<Animation>().Play("slide3");
        mini.GetComponent<Animation>().Play("leftdown3");

        Invoke("Addlist", 2);
    }
    void Addlist()
    {
        foreach(Transform originalParent in original.transform)
        {
            if (originalParent.gameObject.CompareTag("parent"))
            {
                originalbl.Add(originalParent.gameObject);
            }
            
        }
        foreach(Transform miniParent in mini.transform)
        {
            if (miniParent.gameObject.CompareTag("parent"))
            {
                minibl.Add(miniParent.gameObject);
            }
        }
        checkColor();
    }

    void checkColor()
    {
        stars.SetActive(true);

        for(int i = 0; i < 39; i++)
        {
            if(originalbl[i].transform.GetChild(0).gameObject.tag == minibl[i].transform.GetChild(0).gameObject.tag)
            {
                equelCount++; 
            }
        }
        if(equelCount == 39)
        {
            starCount = 3;
            StartCoroutine("Star");
        }else if(equelCount<39 && equelCount >= 20)
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
