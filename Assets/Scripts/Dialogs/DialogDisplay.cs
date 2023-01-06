using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;


public class DialogDisplay : MonoBehaviour
{
    public Dialog dialog;

    public GameObject tutorial;
    public TextMeshProUGUI textDialog;

    public GameObject buttonTutorial;
    private Image buttonYes;
    private Image buttonNo;

    Animator anim;
    public MeshCollider cathedrale;
    public GameObject nextButton;
    public GameObject bertrand;
    private GameObject bertrandTwo;

    //opacités tuto
    public GameObject opacity;
    private GameObject fullBG;
    private GameObject pelerinBG;
    private GameObject coinsBG;
    private GameObject ressourceBG;
    private GameObject objectiveBG;

    private int activeLineIndex = 0;

    //END
    public GameObject endButtonMenu;

    public string language;

    private void Start()
    {
        buttonYes = buttonTutorial.transform.GetChild(0).gameObject.GetComponent<Image>();
        buttonNo = buttonTutorial.transform.GetChild(1).gameObject.GetComponent<Image>();

        //Récupération de la langue de jeu sélectionnée
        if (MainManager.Instance.Language == "fr")// FR
        {
            language = "FR/";

            buttonYes.sprite = Resources.Load<Sprite>("Ui/oui");
            buttonNo.sprite = Resources.Load<Sprite>("Ui/non");

            nextButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/suivant");
        }
        else if(MainManager.Instance.Language == "eng") //ENG
        {
            language = "EN/";

            buttonYes.sprite = Resources.Load<Sprite>("Ui/yes");
            buttonNo.sprite = Resources.Load<Sprite>("Ui/no");

            nextButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/next");

        }



        if (MainManager.Instance.tutoActive !=0)
        {

            //récupère les backgrounds du tuto
            fullBG = opacity.transform.GetChild(0).gameObject; 

            if (SceneManager.GetActiveScene().name != "Cathedrale") //Seulement si dans ma
            {
                if(MainManager.Instance.tutoActive == 1) //Premier lancement du tuto, arrive dans Main
                {
                    dialog = Resources.Load<Dialog>("Dialogue/" + language + "Intro");
                }

                pelerinBG = opacity.transform.GetChild(1).gameObject;
                pelerinBG.SetActive(false);
                coinsBG = opacity.transform.GetChild(2).gameObject;
                coinsBG.SetActive(false);
                ressourceBG = opacity.transform.GetChild(3).gameObject;
                ressourceBG.SetActive(false);
                objectiveBG = opacity.transform.GetChild(4).gameObject;
                objectiveBG.SetActive(false);

                bertrandTwo = GameObject.Find("BertrandDialog 2");
                bertrandTwo.SetActive(false);

                AdvanceMonologue();

                //autre
                anim = GameObject.Find("Main Camera").GetComponent<Animator>();
                cathedrale = GameObject.Find("Cathedrale Sainte Marie").GetComponent<MeshCollider>();
                //nextButton = GameObject.Find("NextButton");
                nextButton.SetActive(false);
            }
            else if(MainManager.Instance.tutoActive == 1) { //dans la cathédrale
                dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto 3");

                //définit la corne posable

            }

            if (MainManager.Instance.tutoActive == 1)
            {
                fullBG.SetActive(true);
            }
            else if (MainManager.Instance.tutoActive == 2) //retour de la cathédrale
            {  
                dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto 5");
                AdvanceMonologue();

            }


            //buttonTutorial = GameObject.Find("Question Button");
            buttonTutorial.SetActive(false);
            
            
        }
        else
        {
            GameObject.Find("Tutorial - Canvas").SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                AdvanceMonologue();
            }
        }
        if(MainManager.Instance.tutoActive == 10)
        {
            dialog = Resources.Load<Dialog>("Dialogue/" + language + "End");
            AdvanceMonologue();
            MainManager.Instance.tutoActive = 11;
        }
        //Debug.Log(bertrand.activeSelf);

    }

    void AdvanceMonologue()
    {
        if(activeLineIndex < dialog.lines.Length)
        {

            if(dialog.name == "Tuto 5" )
            {
                if(activeLineIndex == 2)
                {
                    bertrandTwo.SetActive(true);
                    textDialog = bertrandTwo.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    bertrand.SetActive(false);
                    fullBG.SetActive(false);
                }else if(activeLineIndex == 4)
                {
                    MainManager.Instance.tutoActive = -1;
                    pelerinBG.SetActive(true);
                }   
            }else if(dialog.name == "Tuto 6")
            {
                if(activeLineIndex == 1)
                {
                    coinsBG.SetActive(false);
                    ressourceBG.SetActive(true);
                }else if(activeLineIndex == 2)
                {
                    ressourceBG.SetActive(false);
                    fullBG.SetActive(true);
                }else if(activeLineIndex == 8)
                {
                    fullBG.SetActive(false);
                    objectiveBG.SetActive(true);
                    MainManager.Instance.tutoActive = 4;
                }
            }
            DisplayLine();
            activeLineIndex += 1;
        }
        else 
        {
            if(dialog.name == "Tuto") //Enchainement au dernier text vers le zoom sur la cité
            {
                if (!anim.GetBool("Forward"))
                {
                    TutoZoomCity();
                }
            }else if(dialog.name == "Tuto 2") //après que le joueur ai fait tourner la cité,
            {

            }else if(dialog.name == "Tuto 3")//Attend que le joueur voit l'animation et touche l'écran pour continuer
            {
                bertrand.SetActive(false);
                fullBG.SetActive(false);
                StartCoroutine(WaitTouchCath());
            }else if(dialog.name == "Tuto 4") //fin de la partie tuto dans la cathédrale, att que le joueur ressorte
            {
                bertrand.SetActive(false);
                fullBG.SetActive(false);
                MainManager.Instance.tutoActive = 2;
            }
            else if (dialog.name == "Tuto 5") //attend que le joueur choisisse le pelerin
            {
                StartCoroutine(WaitTouchPelerin());
            }
            else if(dialog.name == "Tuto 6")
            {
                StartCoroutine(WaitTouchObjectives());
            }else if(dialog.name == "End")
            {
                tutorial.SetActive(false);
                endButtonMenu.SetActive(true);
            }
            else //sort du tuto
            {
                if (!dialog.lines[activeLineIndex-1].questionTuto)
                {
                    tutorial.SetActive(false);            
                    activeLineIndex = 0;
                    MainManager.Instance.tutoActive = 0;
                }
            }

            /*
            if (!dialog.lines[activeLineIndex].questionTuto)
            {
                tutorial.SetActive(false);
            }*/
        }
    }

    void DisplayLine()
    {
        Line line = dialog.lines[activeLineIndex];
        SetDialog(line.text);

        if (line.questionTuto)
        {
            buttonTutorial.SetActive(true);
        }
        else
        {
            buttonTutorial.SetActive(false);
        }

    }

    void SetDialog(string text)
    {
        textDialog.SetText(text);
    }

    //Première question Tuto yes no
    /*public void LaunchTuto()
    {
        dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto");
        //Debug.Log(Resources.Load<Dialog>("Dialogue/Tuto"));
        activeLineIndex = 0;
        MainManager.Instance.IsHornPlaced = false;

    }

    public void NoLaunchTuto()
    {
        dialog = Resources.Load<Dialog>("Dialogue/" + language + "NoTuto");
        activeLineIndex = 0;
    }*/

    public void LaunchTuto()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        StartCoroutine(TutoAfterAnimation("yes"));

    }
    public void NoLaunchTuto()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        StartCoroutine(TutoAfterAnimation("no"));
    }

    public IEnumerator TutoAfterAnimation(string tutoOrNot)
    {
        yield return new WaitForSeconds(0.4f);

        if (tutoOrNot == "yes")
        {
            dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto");
            //Debug.Log(Resources.Load<Dialog>("Dialogue/Tuto"));
            activeLineIndex = 0;
            MainManager.Instance.IsHornPlaced = false;
            AdvanceMonologue();

        }
        else
        {
            dialog = Resources.Load<Dialog>("Dialogue/" + language + "NoTuto");
            activeLineIndex = 0;
            AdvanceMonologue();

        }
    }



    public void TutoZoomCity()
    {
        //cathedrale.enabled = false;
        bertrand.SetActive(false);
        fullBG.SetActive(false);

        if (anim != null)
        {
            bool forward = true;
            anim.SetBool("Forward", forward);
        }

        StartCoroutine(WaitTurnCity());

        /*
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            nextButton.SetActive(true);
        }*/

    }

    public IEnumerator WaitTurnCity()
    {
        nextButton.SetActive(false);

        yield return new WaitUntil(() => Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved);
        nextButton.SetActive(true);
    }

    public IEnumerator WaitTouchCath()
    {
        yield return new WaitUntil(() => DragDrop_Script.hornDrag.inSlot == true);
        AdvanceMonologue();

        activeLineIndex = 0;
        dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto 4");

        bertrand.SetActive(true);
        fullBG.SetActive(true);
    }
    public IEnumerator WaitTouchPelerin()
    {
        yield return new WaitUntil(() => MainManager.Instance.tutoActive == 3);
        AdvanceMonologue();
        activeLineIndex = 0;

        dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto 6");
        bertrandTwo.SetActive(false);
        pelerinBG.SetActive(false);

        textDialog = bertrand.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        bertrand.SetActive(true);
        coinsBG.SetActive(true);

        CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount + 9, "gold");
        CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.FaithCount, MainManager.Instance.FaithCount + 10, "faith");
    }

    public IEnumerator WaitTouchObjectives()
    {
        yield return new WaitUntil(() => MainManager.Instance.objectiveOpen);
        AdvanceMonologue();
        activeLineIndex = 0;

        dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto 7");
        AdvanceMonologue();

        objectiveBG.SetActive(false);
    }

    //Après avoir fait tourné la cité, appuie sur le bouton, déclanche cette fonction
    public void TutoAfterTurn()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button"); //lance anim du touch button
        StartCoroutine(NextBertrandAfterAnimation());
    }
    public IEnumerator NextBertrandAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f); //attend 0.5s

        //continue le tutoriel
        cathedrale.enabled = true;

        bertrand.SetActive(true);

        dialog = Resources.Load<Dialog>("Dialogue/" + language + "Tuto 2");

        activeLineIndex = 0;
        AdvanceMonologue();

    }

}

