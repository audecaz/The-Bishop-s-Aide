using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI_Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}