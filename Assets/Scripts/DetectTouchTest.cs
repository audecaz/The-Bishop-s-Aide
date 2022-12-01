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

                        chosenChara = hit.collider.gameObject;
                        if(chosenChara.GetComponent<CharacterInfos>().job == 2)
                        {
                            //Debug.Log(popUp_Manager);
                            PopUp_Manager.InstanceFact.PopUpVoleur();
                        }
                        CharacterInfos.AddInfosToGlobal(chosenChara);
                            
                        RandomCharacter.GenerateNewCharacter();       
                    }
                    if (hit.collider.gameObject.name == "Opacity")
                    {
                        slider.ShowHideObjective();
                    }

                }
            }
        }
    }
} //Rechercher canvas et touch, pk raycast pas bloqué par l'UI

