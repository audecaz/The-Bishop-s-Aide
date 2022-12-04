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
                    if (hit.collider.gameObject.CompareTag("Character") && MainManager.Instance.objectiveOpen == false)
                    {
                        //Debug.Log(hit.collider.gameObject.name);

                        chosenChara = hit.collider.gameObject;//récupère le personnage sélectionné

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

                        if (MainManager.Instance.PilgrinsCount >= 10)
                        {
                            if(MainManager.Instance.PilgrinsCount == 10)
                            {
                                PopUp_Manager.InstanceFact.PopUpStBertrand();
                            }
                            else if(MainManager.Instance.PilgrinsCount == 18)
                            {
                                PopUp_Manager.InstanceFact.PopUpBertrandGoth();
                            }
                            else if(MainManager.Instance.PilgrinsCount == 28)
                            {
                                PopUp_Manager.InstanceFact.PopUpCathedrale();
                            }
                            else if(MainManager.Instance.PilgrinsCount == 37){
                                PopUp_Manager.InstanceFact.PopUpCloitre();
                            }
                        }

                        //StopAllCoroutines();
                        RandomCharacter.GenerateNewCharacter();
                    }
                    else if (hit.collider.gameObject.name == "Opacity" && MainManager.Instance.objectiveOpen == true) //Si le menu des objectifs est ouvert
                    {
                        slider.ShowHideObjective();
                    }
                    else if (hit.collider.gameObject.name == "City")
                    {
                        SceneManager.LoadScene(1);
                    }

                }
            }
        }
    }
}

