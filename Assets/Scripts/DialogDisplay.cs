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
    public GameObject opacity;

    private int activeLineIndex = 0;

    private void Start()
    {
        if (MainManager.Instance.tutoActive)
        {
            AdvanceMonologue();
            //buttonTutorial = GameObject.Find("Question Button");
            buttonTutorial.SetActive(false);
            anim = GameObject.Find("Main Camera").GetComponent<Animator>();
            cathedrale = GameObject.Find("Cathedrale").GetComponent<MeshCollider>();
            nextButton = GameObject.Find("NextButton");
            nextButton.SetActive(false);
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
            }else if(dialog.name == "Tuto 2")
            {

            }
            else //sort du tuto
            {
                if (!dialog.lines[activeLineIndex-1].questionTuto)
                {
                    tutorial.SetActive(false);            
                    activeLineIndex = 0;
                    MainManager.Instance.tutoActive = false;
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
        opacity.SetActive(false);

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

