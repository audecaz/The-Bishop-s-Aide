using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class UI_Buttons : MonoBehaviour
{
    static GameObject[] ressources;

    public static GameObject[] characters;

    //TextMeshPro du panneau de paramètre
    private TextMeshProUGUI title;
    private TextMeshProUGUI soundTitle;
    private TextMeshProUGUI sound;
    private TextMeshProUGUI music;
    private TextMeshProUGUI languageTitle;

    public void Start()
    {
        title = this.transform.GetChild(0).GetChild(3).transform.GetComponent<TextMeshProUGUI>();
        soundTitle = this.transform.GetChild(0).GetChild(4).transform.GetComponent<TextMeshProUGUI>();
        languageTitle = this.transform.GetChild(0).GetChild(5).transform.GetComponent<TextMeshProUGUI>();

    }

    //Sortie de la cathédrale
    public void BackToMain()
    {
        if (!MainManager.Instance.popupOpen)
        {
            SceneManager.LoadScene(1);
        }
    }

    //Touch les ressources dans l'UI
    public static void TouchRessource(GameObject touched)
    {
        if (MainManager.Instance.RessourceUI == true) //si une ressource déjà affichée, les ferme toutes
        {
            ressources = GameObject.FindGameObjectsWithTag("Ressource");
            for (int i = 0; i < ressources.Length; i++)
            {
                ressources[i].transform.GetChild(3).gameObject.SetActive(false);
                if (ressources[i].name == "Skills")
                {
                    ressources[i].transform.GetChild(4).gameObject.SetActive(false);
                }
            }

        }
        //puis réouvre celle touchée
        touched.transform.GetChild(3).gameObject.SetActive(true);

        if (touched.name == "Skills")
        {
            touched.transform.GetChild(4).gameObject.SetActive(true);
        }

        MainManager.Instance.RessourceUI = true;

    }

    public static void CloseRessource()
    {
        ressources = GameObject.FindGameObjectsWithTag("Ressource");
        for (int i = 0; i < ressources.Length; i++)
        {
            ressources[i].transform.GetChild(3).gameObject.SetActive(false);
            if (ressources[i].name == "Skills")
            {
                ressources[i].transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    //------------------------- PARAMETERS -----------------------------------

    public void OpenParam()
    {
        if(MainManager.Instance.tutoActive == 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            MainManager.Instance.paramOpen = true;
        }
        
    }

    public void CloseParam()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        MainManager.Instance.paramOpen = false;

    }

    public void BackMenu()
    {
        MenuManager.ResetGame();

        SceneManager.LoadScene(0);

    }

    public void ChangeLanguage()
    {
        string buttonTouched = EventSystem.current.currentSelectedGameObject.name;

        if (buttonTouched == "ButtonFrench")
        {
            MainManager.Instance.Language = "fr";
            //Debug.Log("Français !");

            title.SetText("Paramètres");
            /*
            soundTitle.SetText("Sons");
            languageTitle.SetText("Langues");*/

        }
        else
        {
            MainManager.Instance.Language = "eng";
            //Debug.Log("Anglais !");

            title.SetText("Parameters");
            /*
            soundTitle.SetText("Sounds");
            languageTitle.SetText("Languages");*/
        }

        LanguageManager.LanguageInstance.ChangeLanguage();

        characters = GameObject.FindGameObjectsWithTag("Character");


        //Récupère les infos existantes dans le GameObject du MainManager
        for (int i = 0; i < 4; i++)
        {
            Transform test = MainManager.Instance.pelerinInfos.transform.GetChild(i);
            characters[i].GetComponent<CharacterInfos>().job = test.GetComponent<CharacterInfos>().job;
            characters[i].GetComponent<CharacterInfos>().ressourceOne = test.GetComponent<CharacterInfos>().ressourceOne;
            characters[i].GetComponent<CharacterInfos>().ressourceTwo = test.GetComponent<CharacterInfos>().ressourceTwo;

            int jobLocal = characters[i].GetComponent<CharacterInfos>().job;
            int ressourceOneLocal = characters[i].GetComponent<CharacterInfos>().ressourceOne;
            int ressourceTwoLocal = characters[i].GetComponent<CharacterInfos>().ressourceTwo;

            TextMeshProUGUI jobTextLocal = characters[i].gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ROneTextLocal = characters[i].gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI RTwoTextLocal = characters[i].gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

            if (jobLocal == 0 || jobLocal == 2) //si pelerin
            {
                if (MainManager.Instance.Language == "fr")
                {
                    if (jobLocal == 2)
                    {
                        jobTextLocal.SetText("Voleur");
                    }
                    else
                    {
                        jobTextLocal.SetText("Pèlerin");
                    }
                    ROneTextLocal.SetText("OR : +" + ressourceOneLocal);
                    RTwoTextLocal.SetText("FOI : +" + ressourceTwoLocal);
                }
                else //eng
                {
                    if (jobLocal == 2)
                    {
                        jobTextLocal.SetText("Thief");
                    }
                    else
                    {
                        jobTextLocal.SetText("Pilgrim");
                    }
                    ROneTextLocal.SetText("GOLD : +" + ressourceOneLocal);
                    RTwoTextLocal.SetText("FAITH : +" + ressourceTwoLocal);
                }

            }
            else if (jobLocal == 4) //Nicolas Bachelier 
            {
                jobTextLocal.SetText("Nicolas Bachelier");

                if (MainManager.Instance.Language == "fr")
                {
                    RTwoTextLocal.SetText("Architecte");
                }
                else
                {
                    RTwoTextLocal.SetText("Architect");
                }
                ROneTextLocal.SetText("");

            }
            else if (jobLocal == 1)// Artisan
            {
                if (MainManager.Instance.Language == "fr")
                {
                    jobTextLocal.SetText("Artisan");
                    ROneTextLocal.SetText("SAVOIR FAIRE: +" + ressourceOneLocal);
                    RTwoTextLocal.SetText("OR : -" + ressourceTwoLocal);
                }
                else
                {
                    jobTextLocal.SetText("Craftman");
                    ROneTextLocal.SetText("SKILLS: +" + ressourceOneLocal);
                    RTwoTextLocal.SetText("GOLD : -" + ressourceTwoLocal);
                }

            }
            else //pelerin spécial
            {
                //nom pèlerin selon langue
                if (MainManager.Instance.Language == "fr")
                {
                    jobTextLocal.SetText("Pèlerin Spé");
                }
                else
                {
                    jobTextLocal.SetText("Special Pilgrim");
                }

                if (jobLocal == 3) //pèlerin avec objet classique
                {
                    if (MainManager.Instance.Language == "fr")
                    {
                        ROneTextLocal.SetText("FOI : +" + ressourceOneLocal);
                        if (ressourceTwoLocal == 12)
                        {
                            RTwoTextLocal.SetText("Calice en or : +12 d'OR");
                        }
                        else
                        {
                            RTwoTextLocal.SetText("Coffre précieux : +15 d'OR");
                        }
                    }
                    else //eng
                    {
                        ROneTextLocal.SetText("FAITH : +" + ressourceOneLocal);
                        if (ressourceTwoLocal == 12)
                        {
                            RTwoTextLocal.SetText("Golden Calice : +12 GOLD");
                        }
                        else
                        {
                            RTwoTextLocal.SetText("Precious Chest : +15 GOLD");
                        }
                    }
                }
                else if (jobLocal == 5) //pelerin licorne
                {
                    if (MainManager.Instance.Language == "fr")
                    {
                        ROneTextLocal.SetText("FOI : +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("Corne de licorne");
                    }
                    else //eng
                    {
                        ROneTextLocal.SetText("FAITH : +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("Unicorn Horn");
                    }
                }
                else if (jobLocal == 6)
                {
                    if (MainManager.Instance.Language == "fr")
                    {
                        ROneTextLocal.SetText("FOI : +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("Crocodile empaillé");
                    }
                    else // eng
                    {
                        ROneTextLocal.SetText("FAITH : +" + ressourceOneLocal);
                        RTwoTextLocal.SetText("Stuffed Crocodile");
                    }

                }
            }

        }

    }
}
