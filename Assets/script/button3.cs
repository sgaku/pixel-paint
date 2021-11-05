using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button3 : MonoBehaviour
{
    public GameObject original;
    public GameObject mini;
    GameObject grandchild1;
    GameObject grandchild2;

    public void OnClick()
    {





        Debug.Log("click");
        foreach (Transform child in original.transform)
        {
            if (child.gameObject.CompareTag("parent"))
            {
                grandchild1 = child.GetChild(0).gameObject;


                var collider = grandchild1.gameObject.GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                grandchild1.transform.localScale = new Vector3(1, 1, 1);

            }
            child.transform.localScale = new Vector3(1, 1, 1);

        }

        foreach (Transform child in mini.transform)
        {
            if (child.gameObject.CompareTag("parent"))
            {
                grandchild2 = child.GetChild(0).gameObject;
                var collider = grandchild2.gameObject.GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                grandchild2.transform.localScale = new Vector3(1, 1, 1);
            }

            child.transform.localScale = new Vector3(1, 1, 1);

        }

        FindObjectOfType<Controller4>().ChangePosition();
    }
  
}
