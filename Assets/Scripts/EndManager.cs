using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{

    public static GameObject end;
    public GameObject bertrand;
    public GameObject tutorial;
    public Dialog dialog;
    public string language;



    private void Start()
    {
        end = this.transform.GetChild(0).gameObject;
        Debug.Log(end);
        bertrand = GameObject.Find("BertrandDialog");

        if (MainManager.Instance.Language == "fr")// FR
        {
            language = "FR/";
        }
        else if (MainManager.Instance.Language == "eng") //ENG
        {
            language = "EN/";
        }
    }

    public static void openEnd()
    {
        end.SetActive(true);
        MainManager.Instance.finished = true;
    }

    public void closeEnd()
    {
        //Debug.Log("Fin !");
        end.SetActive(false);
        tutorial.SetActive(true);
        tutorial.transform.GetChild(0).gameObject.SetActive(false); //bouton retour
        tutorial.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false); //backgrounds inutiles
        tutorial.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false); //backgrounds inutiles
        tutorial.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.SetActive(false); //backgrounds inutiles
        tutorial.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.SetActive(false); //backgrounds inutiles
        tutorial.transform.GetChild(1).GetChild(2).gameObject.SetActive(false); //BertrandDialog 2
        tutorial.transform.GetChild(1).GetChild(3).gameObject.SetActive(false); //bouton question

        MainManager.Instance.tutoActive = 10;
    }

    public void backMenu()
    {
        SceneManager.LoadScene(0);
    }

}
