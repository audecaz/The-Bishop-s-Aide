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
    private Draggable hornDrag;
    private Draggable crocoDrag;

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
                choirDrag.transform.position = choirDrag.slot.transform.position;
                choirDrag.inSlot = true;
            }
            if (MainManager.Instance.IsOrganPlaced)
            {
                organDrag.transform.position = organDrag.slot.transform.position;
                organDrag.inSlot = true;
            }
        }
        else
        {
            choir.SetActive(false);
            organ.SetActive(false);
        }

        if (MainManager.Instance.HornStolen)
        {
            if (MainManager.Instance.HornRetrieved)
            {
                if (MainManager.Instance.IsHornPlaced)
                {
                    hornDrag.transform.position = hornDrag.slot.transform.position; //déjà à sa place
                    hornDrag.inSlot = true;
                }
                else
                {
                    hornDrag.transform.position = horn.transform.GetChild(0).transform.position; //Se place dans la box
                    horn.SetActive(true);
                }
                horn.transform.GetChild(0).GetComponent<Image>().enabled = true; //masque le visuel dans la box
            }
            else
            {
                horn.SetActive(false);
            }
        }
        else
        {
            hornDrag.transform.position = hornDrag.slot.transform.position; //déjà à sa place
            horn.transform.GetChild(0).GetComponent<Image>().enabled = false; //masque le visuel dans la box
            hornDrag.inSlot = true;

        }


        if (MainManager.Instance.IsCrocoHere)
        {
            croco.SetActive(true);

            if (MainManager.Instance.IsCrocoPlaced)
            {
                crocoDrag.transform.position = crocoDrag.slot.transform.position;
                crocoDrag.inSlot = true;
            }
        }
        else
        {
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
        }
        if (organDrag.inSlot && MainManager.Instance.IsOrganPlaced == false)
        {
            MainManager.Instance.IsOrganPlaced = true;
            PopUp_Manager.InstanceFact.PopUpOrgue();

        }
        if (hornDrag.inSlot && MainManager.Instance.IsHornPlaced == false)
        {
            MainManager.Instance.IsHornPlaced = true;
            PopUp_Manager.InstanceFact.PopUpLicorneTwo();

        }
        if (crocoDrag.inSlot && MainManager.Instance.IsCrocoPlaced == false)
        {
            MainManager.Instance.IsCrocoPlaced = true;
            PopUp_Manager.InstanceFact.PopUpCrocoTwo();
        }
    }
}
