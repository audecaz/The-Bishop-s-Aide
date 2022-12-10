using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackward : MonoBehaviour
{

    public GameObject cameraGame;
    Animator anim;

    private MeshCollider cathedrale;
    public Canvas panelCity;

    void Start()
    {
        anim = cameraGame.GetComponent<Animator>();
        cathedrale = GameObject.Find("Cathedrale").GetComponent<MeshCollider>();
    }

    public void Update()
    {
        if (anim.GetBool("Forward") && !MainManager.Instance.tutoActive)
        {
            cathedrale.enabled = true;
            panelCity.enabled = true;
        }
        else if(MainManager.Instance.tutoActive)
        {
            panelCity.enabled = false;
        }
        else
        {
            cathedrale.enabled = false;
            panelCity.enabled = false;
        }
    }

    public void CamBackward()
    {
        if (cameraGame != null)
        {
            if (anim != null && anim.GetBool("Forward"))
            {
                bool forward = anim.GetBool("Forward");
                anim.SetBool("Forward", !forward);
            }
        }


    }
}