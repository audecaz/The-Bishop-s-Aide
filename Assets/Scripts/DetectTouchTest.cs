using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class DetectTouchTest : MonoBehaviour
{
    public GameObject chosenChara;
    public ObjectiveSlider slider;
    //public CharacterInfos characterInfos;
    //public PopUp_Manager popUp_Manager;

    Animator anim;
    public GameObject tutorial;

    public GameObject bertrand;

    public GameObject nomBatiment;
    public GameObject enterBtn;
    public GameObject lockImage;

    public GameObject cathedral;
    public GameObject maisonBrid;
    public GameObject cloitre;

    public Animation animPelerinBG;


    public void Start()
    {
        anim = GameObject.Find("Main Camera").GetComponent<Animator>();
        animPelerinBG = GameObject.Find("animationPelerin").GetComponent<Animation>();
        //animPelerinBG.Play("pelerinAnimation");
        Debug.Log("test !" +animPelerinBG);

        //bertrand = GameObject.Find("BertrandDialog");

        maisonBrid.GetComponent<Animation>().Stop();
        cathedral.GetComponent<Animation>().Stop();
        cloitre.GetComponent<Animation>().Stop();

    }


    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    
                    if (hit.collider.gameObject.CompareTag("Character") && !MainManager.Instance.objectiveOpen && !MainManager.Instance.paramOpen && (!tutorial.activeSelf || MainManager.Instance.tutoActive <= 0) && !MainManager.Instance.finished)
                    {   
                        //Debug.Log(hit.collider.gameObject.name);
                        chosenChara = hit.collider.gameObject;//r�cup�re le personnage s�lectionn�
                        hit.collider.gameObject.GetComponent<AudioSource>().Play();
                        animPelerinBG.Play("animPilgrimtest");

                        if (MainManager.Instance.tutoActive < 0) //cas du choix forc� de pelerin pendant le tuto
                        {
                            if(chosenChara.name == "Pel 2")
                            {
                                //CharacterInfos.InstanceCharaInfos.AddInfosToGlobal(chosenChara);
                                MainManager.Instance.tutoActive = 3;

                                RandomCharacter.GenerateNewCharacter();
                            }
                        }
                        else
                        {
                            //Test si personnage sp�cial
                            if (chosenChara.GetComponent<CharacterInfos>().job == 2) // perso est voleur
                            {
                                MainManager.Instance.ThievesCount++;
                                if (MainManager.Instance.ThievesCount == 2) //voleur de corne
                                {
                                    PopUp_Manager.InstanceFact.PopUpSpeVoleur(chosenChara);
                                    MainManager.Instance.HornStolen = true;
                                    MainManager.Instance.IsHornPlaced = false;
                                    //Apparition de la mission li�e dans le Update() de ObjectiveList.cs
                                    RandomCharacter.hornDiceNumber = 10; //r�initialise le compteur de d�s
                                }
                                else
                                {
                                    PopUp_Manager.InstanceFact.PopUpVoleur(chosenChara);
                                }
                            }
                            else if (chosenChara.GetComponent<CharacterInfos>().job == 3)// perso est pelerin sp�cial
                            {
                                PopUp_Manager.InstanceFact.PopUpObjetSpe(chosenChara);
                            }
                            else if (chosenChara.GetComponent<CharacterInfos>().job == 5) //pelerin special avec corne
                            {
                                PopUp_Manager.InstanceFact.PopUpLicorneoOne();
                                MainManager.Instance.HornRetrieved = true;
                            }
                            else if (chosenChara.GetComponent<CharacterInfos>().job == 6) //pelerin special avec croco
                            {
                                PopUp_Manager.InstanceFact.PopUpCrocoOne();
                                MainManager.Instance.IsCrocoHere = true;
                            }
                            else if (chosenChara.GetComponent<CharacterInfos>().job == 4) // perso est nicolas bachelier
                            {
                                PopUp_Manager.InstanceFact.PopUpNicolas();
                                MainManager.Instance.IsNicolasRecruted = true;
                            }

                            CharacterInfos.InstanceCharaInfos.AddInfosToGlobal(chosenChara);

                            MainManager.Instance.PilgrinsCount++;

                            //Apparition des popups historiques
                            if (MainManager.Instance.PilgrinsCount >= 5)
                            {
                                if (MainManager.Instance.PilgrinsCount == 5)
                                {
                                    PopUp_Manager.InstanceFact.PopUpStBertrand();
                                }
                                else if (MainManager.Instance.PilgrinsCount == 12)
                                {
                                    PopUp_Manager.InstanceFact.PopUpBertrandGoth();
                                }
                                else if (MainManager.Instance.PilgrinsCount == 20)
                                {
                                    PopUp_Manager.InstanceFact.PopUpCathedrale();
                                }
                                else if (MainManager.Instance.PilgrinsCount == 28)
                                {
                                    PopUp_Manager.InstanceFact.PopUpCloitre();
                                }
                            }

                            //Apparition des events al�atoires, positifs & n�gatifs
                            if (MainManager.Instance.PilgrinsCount > 5) //Emp�che de g�n�rer un event al�atoire dans les premiers p�lerins
                            {
                                PopUp_Manager.InstanceFact.EventAleatoire();
                            }

                            chosenChara.GetComponent<Animation>().Play("Pelerin");
                            StartCoroutine(ChangePilgrimAfterAnimation());
                        }

                        
                    

                    }
                    else if (hit.collider.gameObject.name == "City" && !anim.GetBool("Forward") && /*(MainManager.Instance.tutoActive == 1 || */MainManager.Instance.tutoActive == 0 && !MainManager.Instance.paramOpen) 
                    {
                        //SceneManager.LoadScene(1);
                        //CameraForward.InstanceAnim.ForBackwardCam();
                        if (anim != null)
                        {
                            bool forward = anim.GetBool("Forward");
                            anim.SetBool("Forward", !forward);
                        }
                    }
                    else if(hit.collider.gameObject.CompareTag("Batiment") && anim.GetBool("Forward") && (bertrand.activeSelf || MainManager.Instance.tutoActive == 0)) // touche un b�timent de la ville
                    {
                        if (MainManager.Instance.tutoActive == 1 && hit.collider.gameObject.name == "Cathedrale Sainte Marie") //dans le tuto
                        {
                            SceneManager.LoadScene(2);
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<AudioSource>().Play();

                            MainManager.Instance.placeSelected = true;

                            nomBatiment.SetActive(true);

                            if (hit.collider.gameObject == cathedral)
                            {
                                if (MainManager.Instance.Language == "fr")
                                {
                                    nomBatiment.GetComponent<TextMeshProUGUI>().SetText("Cath�drale Sainte Marie");
                                }
                                else //anglais
                                {
                                    nomBatiment.GetComponent<TextMeshProUGUI>().SetText("Saint Mary's Cathedral");
                                }

                                cloitre.GetComponent<Animation>().Stop();
                                maisonBrid.GetComponent<Animation>().Stop();
                                cathedral.GetComponent<Animation>().Play("SelectedCathedral");

                                enterBtn.SetActive(true);
                                lockImage.SetActive(false);
                            }
                            else
                            {
                                lockImage.SetActive(true);
                                enterBtn.SetActive(false);

                                if (hit.collider.gameObject == maisonBrid)
                                {
                                    if (MainManager.Instance.Language == "fr")
                                    {
                                        nomBatiment.GetComponent<TextMeshProUGUI>().SetText("Maison Bridault");
                                    }
                                    else //anglais
                                    {
                                        nomBatiment.GetComponent<TextMeshProUGUI>().SetText("House Bridault");
                                    }

                                    cloitre.GetComponent<Animation>().Stop();
                                    cathedral.GetComponent<Animation>().Stop();
                                    maisonBrid.GetComponent<Animation>().Play("SelectedBrid");

                                }
                                else //cloitre
                                {
                                    if (MainManager.Instance.Language == "fr")
                                    {
                                        nomBatiment.GetComponent<TextMeshProUGUI>().SetText("Clo�tre");
                                    }
                                    else //anglais
                                    {
                                        nomBatiment.GetComponent<TextMeshProUGUI>().SetText("Cloister");
                                    }

                                    maisonBrid.GetComponent<Animation>().Stop();
                                    cathedral.GetComponent<Animation>().Stop();
                                    cloitre.GetComponent<Animation>().Play("SelectedCloitre");

                                }
                            }
                        }



                        //hit.collider.gameObject.name == "Cathedrale" && anim.GetBool("Forward") && (bertrand.activeSelf || MainManager.Instance.tutoActive == 0)
                        //SceneManager.LoadScene(2);

                    }

                    if (hit.collider.gameObject.name != "Plane" && MainManager.Instance.objectiveOpen && MainManager.Instance.tutoActive != 4) //Si le menu des objectifs est ouvert, le cache
                    {
                        slider.ShowHideObjective();
                    }
                    

                }
            }

            //Test du touch des ressources de l'UI
            if(MainManager.Instance.tutoActive == 0)
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Ressource")) //si le touch touche un objet et tag ressource
                {
                    UI_Buttons.TouchRessource(hit.collider.gameObject);
                }
                else //si touche juste l'�cran ailleurs
                {
                    UI_Buttons.CloseRessource();
                }
            }
            
        }
    }

    IEnumerator ChangePilgrimAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        RandomCharacter.GenerateNewCharacter();

    }
}

