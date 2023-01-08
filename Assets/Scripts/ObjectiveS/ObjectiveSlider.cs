using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveSlider : MonoBehaviour
{

    public GameObject ObjectivePanel;
    public GameObject opacity;
    public GameObject soundSlider;

    private float timerSlider = 0f;

    void Start()
    {
        opacity = GameObject.Find("Opacity");//.GetComponent<MeshRenderer>();
        opacity.SetActive(false);
    }

    private void Update()
    {
        if (timerSlider > 0)
        {
            timerSlider -= Time.deltaTime;
        }
    }

    public void ShowHideObjective()
    {

        if (timerSlider <= 0 && !PopUp_Manager.InstanceFact.IsActive && (MainManager.Instance.tutoActive == 0 || MainManager.Instance.tutoActive == 4) && !MainManager.Instance.paramOpen)
        {
            soundSlider.GetComponent<AudioSource>().Play();

            timerSlider = 1f; //initialise le cooldown du slider 
            if (ObjectivePanel != null)
            {

                Animator anim = ObjectivePanel.GetComponent<Animator>();
                if (anim != null)
                {
                    bool isOpen = anim.GetBool("Show");
                    anim.SetBool("Show", !isOpen);
                    MainManager.Instance.objectiveOpen = !isOpen;

                    if (MainManager.Instance.objectiveOpen == true)
                    {
                        opacity.SetActive(true);
                        if (GameObject.Find("Objectives").transform.GetChild(4).gameObject.activeSelf) //si une notification est activée
                        {
                            GameObject.Find("Objectives").transform.GetChild(4).gameObject.SetActive(false);// la désactive
                        }
                    }
                    else
                    {
                        opacity.SetActive(false);

                        if (GameObject.Find("ObjectivesList").transform.GetChild(4).gameObject.activeSelf) //si l'objectif est activé
                        {
                            GameObject.Find("ObjectivesList").transform.GetChild(4).GetChild(1).gameObject.SetActive(false);// désactive la notification
                        }

                        if (GameObject.Find("ObjectivesList").transform.GetChild(5).gameObject.activeSelf) //si l'objectif est activé
                        {
                            GameObject.Find("ObjectivesList").transform.GetChild(5).GetChild(1).gameObject.SetActive(false);// désactive la notification
                        }
                    }
                }
            }
        }
       
    }
}
