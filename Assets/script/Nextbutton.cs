using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextbutton : MonoBehaviour
{
   
    public void OnClick()
    {
        SceneManager.LoadScene("third");
    }
   
}
