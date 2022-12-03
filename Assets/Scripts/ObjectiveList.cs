using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ObjectiveList : MonoBehaviour
{
    public static TextMeshProUGUI objOne;
    public static TextMeshProUGUI objTwo;
    public static TextMeshProUGUI objThree;
    public static TextMeshProUGUI objFour;
    public static TextMeshProUGUI objFourOne;
    public static TextMeshProUGUI objFourTwo;
    public static TextMeshProUGUI objFourThree;

    public bool objOneRempli = false;
    public bool objTwoRempli = false;
    public bool objThreeRempli = false;

    public bool objFourRempli = false;
    public bool objFourOneRempli = false;
    public bool objFourTwoRempli = false;
    public bool objFourThreeRempli = false;

    void Start()
    {
  
        objOne = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        objTwo = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        objThree = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        objFour = gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        objFourOne = gameObject.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        objFourTwo = gameObject.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();
        objFourThree = gameObject.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();

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
        if(MainManager.Instance.GoldCount >= 700)
        {
            IfComplete(objOne, objOneRempli);
            objOneRempli = true;

            /*
            if(objOneRempli == false)
            {
                objOne.fontStyle = TMPro.FontStyles.Strikethrough;
            }
            */
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

            /*
            if (objTwoRempli == false)
            {
                objTwo.fontStyle = TMPro.FontStyles.Strikethrough;
            }
            */
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

            /*
            if (objThreeRempli == false)
            {
                objThree.fontStyle = TMPro.FontStyles.Strikethrough;
            }
            */
        }
        else
        {
            objThreeRempli = false;
            objThree.fontStyle = TMPro.FontStyles.Normal;
            
        }

        //Objective FOUR
        if (objFourOneRempli == true && objFourTwoRempli == true && objFourThreeRempli == true) //les 3 sous objectifs sont remplis
        {
            if (objFourRempli == false)
            {
                objFour.fontStyle = TMPro.FontStyles.Strikethrough;
                MainManager.Instance.GoldCount -= 20; //paye l'objet une fois
                PopUp_Manager.InstanceFact.PopUpOrgueChoeur();
            }
            objFourRempli = true;

        }


        //Objective FOUR ONE
        if (MainManager.Instance.IsNicolasRecruted) //Nicolas Bachelier recruté
        {
            IfComplete(objFourOne, objFourOneRempli);
            objFourOneRempli = true;

            /*
            if (objFourOneRempli == false)
            {
                objFourOne.fontStyle = TMPro.FontStyles.Strikethrough;
            }
            */


        }

        //Objective FOUR TWO
        if (MainManager.Instance.ArtisanCount >= 5)
        {
            IfComplete(objFourTwo, objFourTwoRempli);
            objFourTwoRempli = true;

            /*
            if (objFourTwoRempli == false)
            {
                objFourTwo.fontStyle = TMPro.FontStyles.Strikethrough;
            }*/

        }

        //Objective FOUR THREE
        if (MainManager.Instance.GoldCount >= 20)
        {
            IfComplete(objFourThree, objFourThreeRempli);
            objFourThreeRempli = true;

            /*
            if (objFourThreeRempli == false)
            {
                objFourThree.fontStyle = TMPro.FontStyles.Strikethrough;
            }
            */
        }
        else
        {
            objFourThreeRempli = false;
            objFourThree.fontStyle = TMPro.FontStyles.Normal;
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
