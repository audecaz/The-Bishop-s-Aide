using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;


public class EndManager : MonoBehaviour
{

    public static GameObject end;
    public static GameObject confettis;
    public GameObject bertrand;
    public GameObject tutorial;
    public Dialog dialog;
    public string language;

    public GameObject nextButton;

    private static GameObject echaffautOne;
    private static GameObject echaffautTwo;
    private static GameObject contrefort;


    private void Start()
    {
        confettis = this.transform.GetChild(0).gameObject;
        end = this.transform.GetChild(1).gameObject;

        

        Debug.Log(end);
        bertrand = GameObject.Find("BertrandDialog");

        if (MainManager.Instance.Language == "fr")// FR
        {
            language = "FR/";

            nextButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/suivant");

        }
        else if (MainManager.Instance.Language == "eng") //ENG
        {
            language = "EN/";

            nextButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/next");

        }
    }

    public static void openEnd()
    {
        end.SetActive(true);
        confettis.SetActive(true);

        MainManager.Instance.finished = true;
    }

    public void closeEnd()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();

        StartCoroutine(EndAfterAnimation());

    }

    IEnumerator EndAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f);

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
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();

        StartCoroutine(MenuAfterAnimation());
               
    }

    IEnumerator MenuAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        confettis.SetActive(false);

        MenuManager.ResetGame();
        SceneManager.LoadScene(0);
    }


}
