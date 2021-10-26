using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    public GameObject heart;
 

   public void OnClick()
    {
        Debug.Log("click");
        foreach(Transform child in heart.transform)
        {
            if (child.gameObject.CompareTag("block"))
            {
                var collider = child.gameObject.GetComponent<PolygonCollider2D>();
                collider.enabled = false;
            }
          
            child.transform.localScale = new Vector3(1, 1, 1);
            child.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
