using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DragDrop_Script : MonoBehaviour
{
    public GameObject choir;
    public GameObject organ;
    public GameObject horn;
    public GameObject croco;

    private Draggable choirDrag;
    private Draggable organDrag;
    public static Draggable hornDrag;
    private Draggable crocoDrag;

    public GameObject choir3D;
    public GameObject organ3D;
    public GameObject horn3D;
    public GameObject croco3D;

    public GameObject choirSlot;
    public GameObject organSlot;
    public GameObject hornSlot;
    public GameObject crocoSlot;

    // Start is called before the first frame update
    void Start()
    {
        choirDrag = choir.transform.GetChild(1).GetComponent<Draggable>();
        organDrag = organ.transform.GetChild(1).GetComponent<Draggable>();
        hornDrag = horn.transform.GetChild(1).GetComponent<Draggable>();
        crocoDrag = croco.transform.GetChild(1).GetComponent<Draggable>();

        if (MainManager.Instance.IsChoirGotten)
        {
            if (MainManager.Instance.IsChoirPlaced)
            {
                choir3D.SetActive(true);
                choirSlot.SetActive(false);

                choir.transform.GetChild(1).GetComponent<Image>().enabled = false;
                choirDrag.inSlot = true;
                
            }
            if (MainManager.Instance.IsOrganPlaced)
            {
                organ3D.SetActive(true);
                organSlot.SetActive(false);

                organ.transform.GetChild(1).GetComponent<Image>().enabled = false;
                organDrag.inSlot = true;
            }
        }
        else
        {
            choir3D.SetActive(false);
            choirSlot.SetActive(true);

            organ3D.SetActive(false);
            organSlot.SetActive(true);

            choir.SetActive(false);
            organ.SetActive(false);
        }

        if (MainManager.Instance.HornStolen)
        {
            if (MainManager.Instance.HornRetrieved)
            {
                if (MainManager.Instance.IsHornPlaced)
                {
                    horn3D.SetActive(true);

                    horn.transform.GetChild(1).GetComponent<Image>().enabled = false;
                    hornDrag.inSlot = true;
                }
                else
                {
                    horn3D.SetActive(false);

                    hornDrag.transform.position = horn.transform.GetChild(0).transform.position; //Se place dans la box

                    horn.SetActive(true);
                    horn.transform.GetChild(0).GetComponent<Image>().enabled = true; //masque le visuel dans la box

                }
            }
            else
            {
                horn3D.SetActive(false);

                horn.SetActive(false);
            }
        }
        else //la corne n'a pas encore été volée
        {
            if(MainManager.Instance.tutoActive == 0) //hors tuto
            {
                //hornDrag.transform.position = hornDrag.slot.transform.position; //déjà à sa place
                horn3D.SetActive(true);

                horn.SetActive(false); //masque le visuel dans la box
                hornDrag.inSlot = true;
            }
            
            

        }


        if (MainManager.Instance.IsCrocoHere)
        {
            croco.SetActive(true);

            if (MainManager.Instance.IsCrocoPlaced)
            {
                croco3D.SetActive(true);

                croco.transform.GetChild(0).GetComponent<Image>().enabled = true; //masque le visuel dans la box
                crocoDrag.inSlot = true;
            }
        }
        else
        {
            croco3D.SetActive(false);
            croco.SetActive(false);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (choirDrag.inSlot && MainManager.Instance.IsChoirPlaced == false)
        {
            MainManager.Instance.IsChoirPlaced = true;
            PopUp_Manager.InstanceFact.PopUpChoeur();
            choir3D.SetActive(true);
            choirSlot.SetActive(false);
            choir.transform.GetChild(1).GetComponent<Image>().enabled = false;
        }
        if (organDrag.inSlot && MainManager.Instance.IsOrganPlaced == false)
        {
            MainManager.Instance.IsOrganPlaced = true;
            PopUp_Manager.InstanceFact.PopUpOrgue();
            organ3D.SetActive(true);
            organSlot.SetActive(false);
            organ.transform.GetChild(1).GetComponent<Image>().enabled = false;

        }
        if (hornDrag.inSlot && MainManager.Instance.IsHornPlaced == false)
        {
            MainManager.Instance.IsHornPlaced = true;
            PopUp_Manager.InstanceFact.PopUpLicorneTwo();
            horn3D.SetActive(true);
            horn.transform.GetChild(1).GetComponent<Image>().enabled = false;

        }
        if (crocoDrag.inSlot && MainManager.Instance.IsCrocoPlaced == false)
        {
            MainManager.Instance.IsCrocoPlaced = true;
            PopUp_Manager.InstanceFact.PopUpCrocoTwo();
            croco3D.SetActive(true);
            crocoSlot.SetActive(false);
            croco.transform.GetChild(1).GetComponent<Image>().enabled = false;

        }
    }
}
