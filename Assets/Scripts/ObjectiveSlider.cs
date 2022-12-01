using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class ObjectiveSlider : MonoBehaviour
{

    public GameObject ObjectivePanel;
    public GameObject opacity;

    private float timerSlider = 0f;

    void Start()
    {
        opacity = GameObject.Find("Opacity");
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
        if(timerSlider <= 0 && !PopUp_Manager.InstanceFact.IsActive)
        {
            timerSlider = 1.5f; //initialise le cooldown du saut 
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
