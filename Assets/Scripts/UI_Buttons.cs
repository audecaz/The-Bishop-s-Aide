using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class UI_Buttons : MonoBehaviour
{
    static GameObject[] ressources;
        
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
}
