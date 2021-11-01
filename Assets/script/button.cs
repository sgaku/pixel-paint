using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    public GameObject heart;
    public GameObject mini;
  GameObject grandchild1;
  GameObject grandchild2;

    public void OnClick()
    {
        FindObjectOfType<Controller2>().AddList();

       
        Debug.Log("click");
        foreach(Transform child in heart.transform)
        {
            if (child.gameObject.CompareTag("parent") )
            {
               grandchild1= child.GetChild(0).gameObject;

                Debug.Log(grandchild1);
                var collider = grandchild1.gameObject.GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                grandchild1.transform.localScale = new Vector3(1, 1, 1);

            }
            child.transform.localScale = new Vector3(1, 1, 1);
        
        }

        foreach(Transform child in mini.transform)
        {
            if (child.gameObject.CompareTag("parent"))
            {
                grandchild2 = child.GetChild(0).gameObject;
                grandchild2.transform.localScale = new Vector3(1, 1, 1);
            }
        
            child.transform.localScale = new Vector3(1, 1, 1);

        }

        
    }
}
