using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class ObjectiveSlider : MonoBehaviour
{

    public GameObject ObjectivePanel;
    public MeshRenderer opacity;

    private float timerSlider = 0f;

    void Start()
    {
        opacity = GameObject.Find("Opacity").GetComponent<MeshRenderer>();
        opacity.enabled = false;
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
            timerSlider = 1f; //initialise le cooldown du saut 
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
                        opacity.enabled = true;
                    }
                    else
                    {
                        opacity.enabled = false;
                    }
                }
            }
        }
       
    }
}
