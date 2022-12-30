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

    public GameObject playStart;

    public void Start()
    {

    }

    public void playButton()
    {
        playStart.GetComponent<Animation>().Play("Button");
        StartCoroutine(PlayAfterAnimation());     
    }



    IEnumerator PlayAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        startScreen.SetActive(false);
        langageScreen.SetActive(true);
    }

    public void ChooseLanguage()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");

        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        string buttonTouched = EventSystem.current.currentSelectedGameObject.name;

        if (buttonTouched == "ButtonFrench")
        {
            MainManager.Instance.Language = "fr";
            Debug.Log("Français !");
        }
        else
        {
            MainManager.Instance.Language = "eng";
            Debug.Log("Anglais !");
        }
    }

    public void NextPanel()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        StartCoroutine(LanguageAfterAnimation());
   
    }
    IEnumerator LanguageAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);

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
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        StartCoroutine(StartAfterAnimation());

    }

    IEnumerator StartAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);

    }

    public static void ResetGame()
    {
        MainManager.Instance.GoldCount = 500;
        MainManager.Instance.FaithCount = 400;
        MainManager.Instance.SkillCount = 200;
        MainManager.Instance.ArtisanCount = 0;

        MainManager.Instance.ThievesCount = 0;
        MainManager.Instance.PilgrinsCount = 0;

        MainManager.Instance.IsNicolasRecruted = false;
        MainManager.Instance.IsChoirGotten = false;
        MainManager.Instance.IsCrocoHere = false;
        MainManager.Instance.HornStolen = false;
        MainManager.Instance.HornRetrieved = false;

        MainManager.Instance.IsChoirPlaced = false;
        MainManager.Instance.IsOrganPlaced = false;
        MainManager.Instance.IsHornPlaced = true;
        MainManager.Instance.IsCrocoPlaced = false;

        MainManager.Instance.Incendie = false;

        MainManager.Instance.RessourceUI = false;  //false rien d'ouvert, true une ressource affichée

        MainManager.Instance.objectiveOpen = false;
        MainManager.Instance.popupOpen = false;
        MainManager.Instance.tutoActive = 1; // 0 désactivé, 1 activé première partie, 2 activé retour dans Main, -1 sélection forcée de pèlerin, 3 après selection du pelerin, 4 au moment de toucher les objectifs, 10 end

        MainManager.Instance.allObjectives = false;
        MainManager.Instance.finished = false;
        MainManager.Instance.allPlaced = false;

        MainManager.Instance.notifIncendie = false;
        MainManager.Instance.notifHorn = false;

        MainManager.Instance.paramOpen = false;
        MainManager.Instance.placeSelected = false;
    }
}
