using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject startScreen;
    public GameObject langageScreen;

    /*public void Start()
    {
        langageScreen.SetActive(false);
    }*/
    public void playButton()
    {
        startScreen.SetActive(false);
        langageScreen.SetActive(true);
    }

    public void ChooseLanguage()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        string buttonTouched = EventSystem.current.currentSelectedGameObject.name;

        if (buttonTouched == "ButtonFrench")
        {
            MainManager.Instance.Language = 1;
            Debug.Log("Français !");
        }
        else
        {
            MainManager.Instance.Language = 2;
            Debug.Log("Anglais !");
        }
    }

    public void startGame()
    {
        if(MainManager.Instance.Language != 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
