using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSlider : MonoBehaviour
{

    public GameObject ObjectivePanel;
    public GameObject opacity;

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
        if(timerSlider <= 0 && !PopUp_Manager.InstanceFact.IsActive && (MainManager.Instance.tutoActive == 0 || MainManager.Instance.tutoActive == 4))
        {
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
                    }
                    else
                    {
                        opacity.SetActive(false);
                    }
                }
            }
        }
       
    }
}