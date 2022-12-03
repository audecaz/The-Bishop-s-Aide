using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PopUp_Manager : MonoBehaviour
{
    public static PopUp_Manager InstanceFact;

    public GameObject Bg;
    public GameObject FactEvent;
    public GameObject ObtentionObject;

    private TextMeshProUGUI FactTitle;
    private TextMeshProUGUI FactContent;
    private TextMeshProUGUI ObjectTitle;
    private TextMeshProUGUI ObjectUnderTitle;
    private TextMeshProUGUI ObjectContent;
    public bool IsActive = false;

    
    public void Start()
    {
        //Debug.Log(FactEvent);
        InstanceFact = this;

        FactTitle = gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        FactContent = gameObject.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

        ObjectTitle = gameObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        ObjectUnderTitle = gameObject.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        ObjectContent = gameObject.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void Open(GameObject popUp) {
        Bg.SetActive(true);
        popUp.SetActive(true);
        IsActive = true;
    }

    public void Close(GameObject popUp)
    {
        Bg.SetActive(false);
        popUp.SetActive(false);
        IsActive = false;
    }

    public void PopUpVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Voleur");
        FactContent.SetText("Malheur ! " +
            "\r\nLe pèlerin que vous venez de recruter était en réalité un voleur déguisé." +
            "\r\nLa rumeur se répand et les gens sont plus méfiants." +
            "\r\n \r\nVous perdez " + chara.ressourceOne + " d'OR" +
            "\r\nVous perdez " + chara.ressourceTwo + "  de FOI");
        Open(FactEvent);
    }

    public void PopUpObjetSpe(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Titre Objet");
        FactContent.SetText("Quelle chance !" +
            "\r\nLe pèlerin que vous venez de recruter vous offre un objet précieux" +
            "\r\n \r\nVous obtenez " + chara.ressourceTwo + " d'OR");
        Open(FactEvent);
    }

    public void PopUpNicolas()
    {
        //CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Nicolas Bachelier");
        FactContent.SetText("Architecte et ingénieur,\r\n" +
            "Nicolas Bachelier (1487-1556) est considéré comme l’un des plus grands architectes toulousains de la Renaissance. \r\n\r\n" +
            "Il a notamment participé à la construction de l’hôtel d’Assézat, du Pont-Neuf ou encore du portail du Capitole de Toulouse.");
        MainManager.Instance.IsNicolasRecruted = true;
        Open(FactEvent);
    }

    public void PopUpOrgueChoeur()
    {
        //CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        //ObjectTitle.SetText("Les boiseries de la cathédrale");
        ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
        ObjectContent.SetText("Commandé par l'évêque Jean de Mauléon vers entre 1525 et 1550, \r\n" +
        "le chœur en bois sculpté trône au centre de la cathédrale Notre Dame de Saint Bertrand de Comminges.\r\n" +
        "La construction de l’orgue, également par Nicolas Bachelier, lui est postérieure.");
        Open(ObtentionObject);
    }
}
