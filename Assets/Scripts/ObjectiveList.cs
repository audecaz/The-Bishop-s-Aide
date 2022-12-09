using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ObjectiveList : MonoBehaviour
{

    public static ObjectiveList InstanceObjectives;

    public static TextMeshProUGUI objOne;
    public static TextMeshProUGUI objTwo;
    public static TextMeshProUGUI objThree;
    public static TextMeshProUGUI objFour;
    public static TextMeshProUGUI objFourOne;
    public static TextMeshProUGUI objFourTwo;
    public static TextMeshProUGUI objFourThree;
    public static TextMeshProUGUI objFive;
    public static TextMeshProUGUI objSix;

    public int objectiveSix;

    public bool objOneRempli = false;
    public bool objTwoRempli = false;
    public bool objThreeRempli = false;

    public bool objFourRempli = false;
    public bool objFourOneRempli = false;
    public bool objFourTwoRempli = false;
    public bool objFourThreeRempli = false;

    public bool objFiveRempli = false;
    public bool objSixRempli = false;

    int test = -1;


    void Start()
    {
        objOne = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        objTwo = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        objThree = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        objFour = gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        objFourOne = gameObject.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        objFourTwo = gameObject.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();
        objFourThree = gameObject.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();
        objFive = gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        objSix = gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();


        objFive.enabled = false; //invisible au début car corne pas encore volée
        objFive.transform.GetChild(0).GetComponent<Image>().enabled = false; //correspond au point à côté invisible aussi

        objSix.enabled = false; //invisible au début car corne pas encore volée
        objSix.transform.GetChild(0).GetComponent<Image>().enabled = false; //correspond au point à côté invisible aussi

        /*
        objOne.SetText("Obtenir 100 d'OR");
        objTwo.SetText("Obtenir 70 de FOI");
        objThree.SetText("Atteindre 50 de SAVOIR FAIRE");
        */

    }

    // Update is called once per frame
    void Update()
    {
        //Objective ONE
        if (MainManager.Instance.GoldCount >= 650)
        {
            IfComplete(objOne, objOneRempli);
            objOneRempli = true;


        }
        else
        {
            objOneRempli = false;
            objOne.fontStyle = TMPro.FontStyles.Normal;

        }

        //Objective TWO
        if (MainManager.Instance.FaithCount >= 550)
        {
            IfComplete(objTwo, objTwoRempli);
            objTwoRempli = true;

        }
        else
        {
            objTwoRempli = false;
            objTwo.fontStyle = TMPro.FontStyles.Normal;

        }

        //Objective THREE
        if (MainManager.Instance.SkillCount >= 275)
        {
            IfComplete(objThree, objThreeRempli);
            objThreeRempli = true;
        }
        else
        {
            objThreeRempli = false;
            objThree.fontStyle = TMPro.FontStyles.Normal;

        }

        //Objective FOUR
        if (objFourOneRempli == true && objFourTwoRempli == true && objFourThreeRempli == true) //les 3 sous objectifs sont remplis
        {
            if (objFourRempli == false && !MainManager.Instance.IsChoirGotten)
            {
                objFour.fontStyle = TMPro.FontStyles.Strikethrough;
                MainManager.Instance.GoldCount -= 20; //paye l'objet une fois
                MainManager.Instance.IsChoirGotten = true;
                PopUp_Manager.InstanceFact.PopUpOrgueChoeur();            
                objFourRempli = true;
            
            }

        }


        //Objective FOUR ONE
        if (MainManager.Instance.IsNicolasRecruted) //Nicolas Bachelier recruté
        {
            IfComplete(objFourOne, objFourOneRempli);
            objFourOneRempli = true;

        }

        //Objective FOUR TWO
        if (MainManager.Instance.ArtisanCount >= 5)
        {
            IfComplete(objFourTwo, objFourTwoRempli);
            objFourTwoRempli = true;

        }

        //Objective FOUR THREE
        if (MainManager.Instance.GoldCount >= 520)
        {
            IfComplete(objFourThree, objFourThreeRempli);
            objFourThreeRempli = true;
        }
        else
        {
            objFourThreeRempli = false;
            objFourThree.fontStyle = TMPro.FontStyles.Normal;
        }

        //OBJECTIVE FIVE
        if (MainManager.Instance.HornRetrieved && !objFiveRempli) //si première update où la corne est là
        {
            objFive.fontStyle = TMPro.FontStyles.Strikethrough;
            objFiveRempli = true;
        }


        if (MainManager.Instance.HornStolen)
        {
            objFive.enabled = true;
            objFive.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        //Objective SIX
        if (MainManager.Instance.Incendie) // affiche l'objectif
        {
            objSix.enabled = true;
            objSix.transform.GetChild(0).GetComponent<Image>().enabled = true;

            if (test == -1) // premiere fois que Incendie passe en true et donc dans le if 
            {
                test = MainManager.Instance.ArtisanCount; //récupère la valeur actuelle de ArtisanCount

                if (MainManager.Instance.IsChoirGotten)
                {
                    objectiveSix = test + 2;
                }
                else
                {
                    objectiveSix = 7;
                }
            }
        }
        
        if (MainManager.Instance.ArtisanCount >= objectiveSix)
        {
            objSix.fontStyle = TMPro.FontStyles.Strikethrough;
            objSixRempli = true;
        }
        else
        {
            objSix.fontStyle = TMPro.FontStyles.Normal;
            objSixRempli = false;

        }
    }

    void IfComplete(TextMeshProUGUI objective, bool isComplete)
    {
        if(isComplete == false)
        {
            objective.fontStyle = TMPro.FontStyles.Strikethrough;
        }

    }
    /*
    void IfNotComplete(TextMeshProUGUI objective, bool isComplete)
    {
        isComplete = false;
        objective.fontStyle = TMPro.FontStyles.Normal;
    }*/
}
