using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DetectTouchTest : MonoBehaviour
{
    public GameObject chosenChara;
    public ObjectiveSlider slider;
    //public CharacterInfos characterInfos;
    //public PopUp_Manager popUp_Manager;


    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if(hit.collider.gameObject.CompareTag("Character") && MainManager.Instance.objectiveOpen == false)
                    {
                        Debug.Log(hit.collider.gameObject.name);

                        chosenChara = hit.collider.gameObject;//r�cup�re le personnage s�lectionn�


                        if(chosenChara.GetComponent<CharacterInfos>().job == 2) // perso est voleur
                        {
                            //Debug.Log(popUp_Manager);
                            PopUp_Manager.InstanceFact.PopUpVoleur(chosenChara);
                        }
                        else if(chosenChara.GetComponent<CharacterInfos>().job == 3)// perso est pelerin sp�cial
                        { 
                            PopUp_Manager.InstanceFact.PopUpObjetSpe(chosenChara);
                        }
                        else if (chosenChara.GetComponent<CharacterInfos>().job == 4) // perso est nicolas bachelier
                        {
                            PopUp_Manager.InstanceFact.PopUpNicolas();
                        }
                        CharacterInfos.AddInfosToGlobal(chosenChara);
                            
                        RandomCharacter.GenerateNewCharacter();       
                    }
                    if (hit.collider.gameObject.name == "Opacity" && MainManager.Instance.objectiveOpen == true) //Si le menu des objectifs est ouvert
                    {
                        slider.ShowHideObjective();
                    }

                }
            }
        }
    }
} //Rechercher canvas et touch, pk raycast pas bloqu� par l'UI

