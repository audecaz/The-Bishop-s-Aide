using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject startScreen;
    public GameObject langageScreen;
    public GameObject disclaimerFR;
    public GameObject disclaimerENG;

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
            MainManager.Instance.Language = "fr";
            Debug.Log("Fran�ais !");
        }
        else
        {
            MainManager.Instance.Language = "eng";
            Debug.Log("Anglais !");
        }
    }

    public void NextPanel()
    {
        if (MainManager.Instance.Language != "0")
        {
            langageScreen.SetActive(false);

            if (MainManager.Instance.Language == "fr")
            {
                disclaimerFR.SetActive(true);
            }
            else
            {
                disclaimerENG.SetActive(true);
            }
        }
    }

    public void startGame()
    {
        
        SceneManager.LoadScene(1);
        
    }
}
