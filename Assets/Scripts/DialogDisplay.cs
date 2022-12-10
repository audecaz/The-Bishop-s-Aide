using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogDisplay : MonoBehaviour
{
    public Dialog dialog;

    public GameObject tutorial;
    public TextMeshProUGUI textDialog;

    public GameObject buttonTutorial;

    Animator anim;
    public MeshCollider cathedrale;
    private GameObject nextButton;
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

    private void Start()
    {   
        if (MainManager.Instance.tutoActive !=0)
        {
            //récupère les backgrounds du tuto
            fullBG = opacity.transform.GetChild(0).gameObject;
            if(MainManager.Instance.tutoActive == 1)
            {
                fullBG.SetActive(true);
            }
            pelerinBG = opacity.transform.GetChild(1).gameObject;
            pelerinBG.SetActive(false);
            coinsBG = opacity.transform.GetChild(2).gameObject;
            coinsBG.SetActive(false);
            ressourceBG = opacity.transform.GetChild(3).gameObject;
            ressourceBG.SetActive(false);
            objectiveBG = opacity.transform.GetChild(4).gameObject;
            objectiveBG.SetActive(false);


            if (MainManager.Instance.tutoActive != 0)
            {
                bertrandTwo = GameObject.Find("BertrandDialog 2");
                bertrandTwo.SetActive(false);
                if (MainManager.Instance.tutoActive == 2) //retour de la cathédrale
                {  
                    dialog = Resources.Load<Dialog>("Dialogue/Tuto 5");
                }
                AdvanceMonologue();    
            }
            //buttonTutorial = GameObject.Find("Question Button");
            buttonTutorial.SetActive(false);
            if(GameObject.Find("Cathedrale") != null)
            {
                

                //autre
                anim = GameObject.Find("Main Camera").GetComponent<Animator>();
                cathedrale = GameObject.Find("Cathedrale").GetComponent<MeshCollider>();
                nextButton = GameObject.Find("NextButton");
                nextButton.SetActive(false);
            }
            
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
                }else if(activeLineIndex == 7)
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
    public void LaunchTuto()
    {
        dialog = Resources.Load<Dialog>("Dialogue/Tuto");
        //Debug.Log(Resources.Load<Dialog>("Dialogue/Tuto"));
        activeLineIndex = 0;

    }

    public void NoLaunchTuto()
    {
        dialog = Resources.Load<Dialog>("Dialogue/NoTuto");
        activeLineIndex = 0;
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
        yield return new WaitUntil(() => Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved);
        nextButton.SetActive(true);
    }

    public IEnumerator WaitTouchCath()
    {
        yield return new WaitUntil(() => Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began);
        AdvanceMonologue();

        activeLineIndex = 0;
        dialog = Resources.Load<Dialog>("Dialogue/Tuto 4");

        bertrand.SetActive(true);
        fullBG.SetActive(true);
    }
    public IEnumerator WaitTouchPelerin()
    {
        yield return new WaitUntil(() => MainManager.Instance.tutoActive == 3);
        AdvanceMonologue();
        activeLineIndex = 0;

        dialog = Resources.Load<Dialog>("Dialogue/Tuto 6");
        bertrandTwo.SetActive(false);
        pelerinBG.SetActive(false);

        textDialog = bertrand.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        bertrand.SetActive(true);
        coinsBG.SetActive(true);
    }

    public IEnumerator WaitTouchObjectives()
    {
        yield return new WaitUntil(() => MainManager.Instance.objectiveOpen);
        AdvanceMonologue();
        activeLineIndex = 0;

        dialog = Resources.Load<Dialog>("Dialogue/Tuto 7");
        AdvanceMonologue();

        objectiveBG.SetActive(false);
    }

    //Après avoir fait tourné la cité, appuie sur le bouton, déclanche cette fonction
    public void TutoAfterTurn()
    {
        //activeLineIndex += 1;
        cathedrale.enabled = true;

        bertrand.SetActive(true);

        dialog = Resources.Load<Dialog>("Dialogue/Tuto 2");
        //Debug.Log(Resources.Load<Dialog>("Dialogue/Tuto"));
        activeLineIndex = 0;

    }

    
}

