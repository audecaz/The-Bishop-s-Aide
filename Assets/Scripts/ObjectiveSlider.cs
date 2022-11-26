using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class ObjectiveSlider : MonoBehaviour
{

    public GameObject ObjectivePanel;
    public GameObject opacity;

    void Start()
    {
        opacity = GameObject.Find("Opacity");
        opacity.SetActive(false);

    }

    public void ShowHideObjective()
    {
        if(ObjectivePanel != null)
        {

            Animator anim = ObjectivePanel.GetComponent<Animator>();
            if(anim != null)
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
