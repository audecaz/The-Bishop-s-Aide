using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
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

    public void Start()
    {
        anim = GameObject.Find("Main Camera").GetComponent<Animator>();
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
                    //Debug.Log(hit.collider.gameObject.name);
                    
                    if (hit.collider.gameObject.CompareTag("Character") && MainManager.Instance.objectiveOpen == false && (!tutorial.activeSelf || MainManager.Instance.tutoActive <= 0))
                    {   
                        //Debug.Log(hit.collider.gameObject.name);
                        chosenChara = hit.collider.gameObject;//récupère le personnage sélectionné

                        if(MainManager.Instance.tutoActive < 0) //cas du choix forcé de pelerin pendant le tuto
                        {
                            if(chosenChara.name == "Pel 2")
                            {
                                CharacterInfos.AddInfosToGlobal(chosenChara);
                                MainManager.Instance.tutoActive = 3;

                                RandomCharacter.GenerateNewCharacter();
                            }
                        }
                        else
                        {
                            //Test si personnage spécial
                            if (chosenChara.GetComponent<CharacterInfos>().job == 2) // perso est voleur
                            {
                                MainManager.Instance.ThievesCount++;
                                if (MainManager.Instance.ThievesCount == 2) //voleur de corne
                                {
                                    PopUp_Manager.InstanceFact.PopUpSpeVoleur(chosenChara);
                                    MainManager.Instance.HornStolen = true;
                                    MainManager.Instance.IsHornPlaced = false;
                                    //Apparition de la mission liée dans le Update() de ObjectiveList.cs
                                    RandomCharacter.hornDiceNumber = 10; //réinitialise le compteur de dés
                                }
                                else
                                {
                                    PopUp_Manager.InstanceFact.PopUpVoleur(chosenChara);
                                }
                            }
                            else if (chosenChara.GetComponent<CharacterInfos>().job == 3)// perso est pelerin spécial
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

                            CharacterInfos.AddInfosToGlobal(chosenChara);

                            MainManager.Instance.PilgrinsCount++;

                            //Apparition des popups historiques
                            if (MainManager.Instance.PilgrinsCount >= 10)
                            {
                                if (MainManager.Instance.PilgrinsCount == 12)
                                {
                                    PopUp_Manager.InstanceFact.PopUpStBertrand();
                                }
                                else if (MainManager.Instance.PilgrinsCount == 24)
                                {
                                    PopUp_Manager.InstanceFact.PopUpBertrandGoth();
                                }
                                else if (MainManager.Instance.PilgrinsCount == 35)
                                {
                                    PopUp_Manager.InstanceFact.PopUpCathedrale();
                                }
                                else if (MainManager.Instance.PilgrinsCount == 46)
                                {
                                    PopUp_Manager.InstanceFact.PopUpCloitre();
                                }
                            }

                            //Apparition des events aléatoires, positifs & négatifs
                            if (MainManager.Instance.PilgrinsCount > 5) //Empêche de générer un event aléatoire dans les premiers pèlerins
                            {
                                PopUp_Manager.InstanceFact.EventAleatoire();
                            }

                            //Regenere de nouveaux persos
                            RandomCharacter.GenerateNewCharacter();
                        }

                    }
                    else if (hit.collider.gameObject.name == "Opacity" && MainManager.Instance.objectiveOpen && MainManager.Instance.tutoActive != 4) //Si le menu des objectifs est ouvert, le cache
                    {
                        slider.ShowHideObjective();
                    }
                    else if (hit.collider.gameObject.name == "City" && !anim.GetBool("Forward") && /*(MainManager.Instance.tutoActive == 1 || */MainManager.Instance.tutoActive == 0) 
                    {
                        //SceneManager.LoadScene(1);
                        //CameraForward.InstanceAnim.ForBackwardCam();
                        if (anim != null)
                        {
                            bool forward = anim.GetBool("Forward");
                            anim.SetBool("Forward", !forward);
                        }
                    }
                    else if(hit.collider.gameObject.name == "Cathedrale" && anim.GetBool("Forward") /*&& !MainManager.Instance.tutoActive*/)
                    {
                        SceneManager.LoadScene(2);

                    }

                }
            }

            //Test du touch des ressources de l'UI
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Ressource")) //si le touch touche un objet et tag ressource
            {
                UI_Buttons.TouchRessource(hit.collider.gameObject);
            }
            else //si touche juste l'écran ailleurs
            {
                UI_Buttons.CloseRessource();
            }
        }
    }
}

