using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("ignore");
    }

    // Update is called once per frame
    void Update()
    {
       
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, layer);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
        
    }
}
