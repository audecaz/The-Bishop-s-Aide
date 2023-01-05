using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject objFive;
    public GameObject objSix;

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

    private static GameObject echaffautOne;
    private static GameObject echaffautTwo;
    private static GameObject contrefort;
    private static GameObject sparklesContrefort;

    void Start()
    {
        objOne = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        objTwo = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        objThree = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        objFour = gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        objFourOne = gameObject.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        objFourTwo = gameObject.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();
        objFourThree = gameObject.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();

        echaffautOne = GameObject.Find("City").transform.GetChild(5).gameObject;
        echaffautTwo = GameObject.Find("City").transform.GetChild(6).gameObject;
        contrefort = GameObject.Find("City").transform.GetChild(7).gameObject;
        sparklesContrefort = GameObject.Find("City").transform.GetChild(9).GetChild(5).gameObject;

        //objFive.SetActive(false);

        //objSix.SetActive(false); //invisible au début car corne pas encore volée

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
            objFour.fontStyle = TMPro.FontStyles.Strikethrough;

            if (objFourRempli == false && !MainManager.Instance.IsChoirGotten)
            {
                MainManager.Instance.IsChoirGotten = true;
                MainManager.Instance.GoldCount -= 20; //paye l'objet une fois
                PopUp_Manager.InstanceFact.PopUpOrgueChoeur();

                echaffautTwo.SetActive(true);

            }
            objFourRempli = true;


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
        if (MainManager.Instance.GoldCount >= 550)
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
            objFive.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Strikethrough;
            //objFive.fontStyle = TMPro.FontStyles.Strikethrough;
            objFiveRempli = true;
        }

        if (!objFive.activeSelf) //premier update depuis que la corne est volée
        {

            if (MainManager.Instance.HornStolen)
            {
                if (!MainManager.Instance.Incendie)
                {
                    objFive.transform.localPosition = new Vector3(480, 220, 0);
                    objSix.transform.localPosition = new Vector3(480, 90, 0);
                }

                objFive.SetActive(true);

                if (!MainManager.Instance.notifHorn)
                {
                    GameObject.Find("Objectives").transform.GetChild(4).gameObject.SetActive(true);//active la notification des objectifs

                    MainManager.Instance.notifHorn = true;
                }
                else
                {
                    objFive.transform.GetChild(1).gameObject.SetActive(false);
                }
                
            }
        }


        //Objective SIX
        if (!objSix.activeSelf) // premier update depuis l'incendie
        {
            
            if (MainManager.Instance.Incendie) // affiche l'objectif
            {
                if (!MainManager.Instance.HornStolen) // Si la corne n'a pas encore été volée et donc l'objectif pas apparu
                {
                    //inverse les position des deux objectifs 
                    objSix.transform.localPosition = new Vector3(480, 220, 0);
                    objFive.transform.localPosition = new Vector3(480, 20, 0);
                }

                objSix.SetActive(true);

                if (MainManager.Instance.ArtisanCount >= 5)
                {
                    objectiveSix = MainManager.Instance.ArtisanCount + 2;
                }
                else
                {
                    objectiveSix = 7;
                }


                if (!MainManager.Instance.notifIncendie)
                {
                    GameObject.Find("Objectives").transform.GetChild(4).gameObject.SetActive(true);//active la notification des objectifs

                    MainManager.Instance.notifIncendie = true;
                }
                else
                {
                    objSix.transform.GetChild(1).gameObject.SetActive(false);
                }

            }
        }
        
        if (MainManager.Instance.ArtisanCount >= objectiveSix && MainManager.Instance.Incendie && !objSixRempli)
        {
            objSix.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Strikethrough;
            //objSix.fontStyle = TMPro.FontStyles.Strikethrough;
            objSixRempli = true;

            if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
            {
                StartCoroutine(PopUpWait());
            }
            else
            {
                City_Effects.CityFxInstance.CityFireOff(); //masque les fx de l'incendie
            }
        }
        else if(MainManager.Instance.ArtisanCount < objectiveSix)
        {
            objSix.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
            objSixRempli = false;

        }

        

        //CONDITIONS DE FIN
        if (MainManager.Instance.allObjectives == false && !MainManager.Instance.finished)
        {
            if (MainManager.Instance.Incendie && MainManager.Instance.HornStolen) //les 2 objectifs facultatifs ont été débloqués
            {
                if (objOneRempli && objTwoRempli && objThreeRempli && (objFourRempli || (objFourOneRempli && objFourTwoRempli && objFourThreeRempli)) && objFiveRempli && objSixRempli)
                {
                    MainManager.Instance.allObjectives = true;

                    if (MainManager.Instance.IsCrocoHere) // + croco
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced && MainManager.Instance.IsCrocoPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");
                            EndManager.openEnd();
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }
                    else
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");
                            EndManager.openEnd();
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }

                    echaffautOne.SetActive(false);
                    echaffautTwo.SetActive(false);
                    contrefort.SetActive(true);
                    sparklesContrefort.SetActive(true);
                }

            }
            else if (MainManager.Instance.Incendie)
            {
                if (objOneRempli && objTwoRempli && objThreeRempli && (objFourRempli || (objFourOneRempli && objFourTwoRempli  && objFourThreeRempli)) && objSixRempli) //tous les objectifs de base + incendie
                {
                    MainManager.Instance.allObjectives = true;

                    if (MainManager.Instance.IsCrocoHere) // + croco
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced && MainManager.Instance.IsCrocoPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");

                            EndManager.openEnd();

                            //Animation de fin
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }
                    else //le croco n'a pas été récupéré
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");

                            EndManager.openEnd();

                            //Animation de fin
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }

                    echaffautOne.SetActive(false);
                    echaffautTwo.SetActive(false);
                    contrefort.SetActive(true);
                    sparklesContrefort.SetActive(true);

                }

            }
            else if (MainManager.Instance.HornStolen) 
            {
                if (objOneRempli && objTwoRempli && objThreeRempli && (objFourRempli || (objFourOneRempli && objFourTwoRempli && objFourThreeRempli)) && objFiveRempli)//la corne a été volé mais pas d'incendie
                {
                    MainManager.Instance.allObjectives = true;

                    if (MainManager.Instance.IsCrocoHere) // + croco
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced && MainManager.Instance.IsCrocoPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");

                            EndManager.openEnd();

                            //Animation de fin
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }
                    else
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");

                            EndManager.openEnd();

                            //Animation de fin
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }

                    echaffautOne.SetActive(false);
                    echaffautTwo.SetActive(false);
                    contrefort.SetActive(true);
                    sparklesContrefort.SetActive(true);

                }
            }
            else  // aucun des 2 objectifs facultatifs
            {
                if (objOneRempli && objTwoRempli && objThreeRempli && (objFourRempli || (objFourOneRempli && objFourTwoRempli && objFourThreeRempli)))
                {
                    MainManager.Instance.allObjectives = true;

                    if (MainManager.Instance.IsCrocoHere) // + croco
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsCrocoPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");

                            EndManager.openEnd();

                            //Animation de fin
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }
                    else
                    {
                        if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced) //Tous les objets sont placés
                        {
                            Debug.Log("affiche fin !");

                            EndManager.openEnd();

                            //Animation de fin
                        }
                        else //Il faut les placer
                        {
                            PopUp_Manager.InstanceFact.PopUpAllObjetives();
                            MainManager.Instance.allPlaced = false;
                        }
                    }

                    echaffautOne.SetActive(false);
                    echaffautTwo.SetActive(false);
                    contrefort.SetActive(true);
                    sparklesContrefort.SetActive(true);

                }
            }

            
        }

        if (MainManager.Instance.allPlaced == false)
        {
            if (MainManager.Instance.IsCrocoHere)
            {
                if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced && MainManager.Instance.IsCrocoPlaced)
                {
                    MainManager.Instance.allPlaced = true;
                }
            }
            else if (MainManager.Instance.IsChoirPlaced && MainManager.Instance.IsOrganPlaced && MainManager.Instance.IsHornPlaced)
            {
                MainManager.Instance.allPlaced = true;
            }
        }
        else
        {
            if (!MainManager.Instance.IsCrocoPlaced && MainManager.Instance.IsCrocoHere) //croco vient d'être obtenu mais pas encore placé
            {
                MainManager.Instance.allPlaced = false;
            }
        }


        if (MainManager.Instance.allObjectives && MainManager.Instance.allPlaced && SceneManager.GetActiveScene().name == "Main" && !MainManager.Instance.finished)
        {
            echaffautOne.SetActive(false);
            echaffautTwo.SetActive(false);
            contrefort.SetActive(true);
            sparklesContrefort.SetActive(true);


            EndManager.openEnd();
        }

    }

    public IEnumerator PopUpWait()
    {
        yield return new WaitUntil(() => !MainManager.Instance.popupOpen);
        City_Effects.CityFxInstance.CityFireOff(); //masque les fx de l'incendie
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
