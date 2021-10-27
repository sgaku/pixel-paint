using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    public GameObject heart;
    public GameObject mini;
   
 

   public void OnClick()
    {
        FindObjectOfType<Controller>().AddList();

       
        Debug.Log("click");
        foreach(Transform child in heart.transform)
        {
            if (child.gameObject.CompareTag("whiteblock")|| child.gameObject.CompareTag("pinkblock"))
            {
                var collider = child.gameObject.GetComponent<PolygonCollider2D>();
                collider.enabled = false;
            }
          
            child.transform.localScale = new Vector3(1, 1, 1);
        
        }

        foreach(Transform child in mini.transform)
        {
           
            child.transform.localScale = new Vector3(1, 1, 1);

        }

        
    }
}
