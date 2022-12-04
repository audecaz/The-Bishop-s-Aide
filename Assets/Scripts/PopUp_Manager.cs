using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEditor.ShaderData;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.UIElements.UxmlAttributeDescription;

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
        MainManager.Instance.popupOpen = true;
    }

    public void Close(GameObject popUp)
    {
        Bg.SetActive(false);
        popUp.SetActive(false);
        IsActive = false;
        MainManager.Instance.popupOpen = false;
    }

    //POPUPS FACTS HISTORIQUES

    //POPUPS EVENEMENTS ALEATOIRES
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

    
    public void PopUpSpeVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Voleur");
        FactContent.SetText("Malheur ! Ayant entendu parler de la fameuse corne de licorne et la convoitant, un voleur se fait passer pour un pèlerin et vous dérobe la corne !  \r\n \r\n"+
            "Vous perdez " + chara.ressourceOne + " d’OR  \r\n"+
            "Vous perdez la Corne de Licorne de Saint Bertrand. \r\n" +
            "Vous devez désormais récupérer la corne avant de terminer l’Age ");
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
        Open(FactEvent);
    }

    //POP UP OBTENTION OBJETS SPECIAUX
    public void PopUpOrgueChoeur()
    {
        Debug.Log("test");

        ObjectTitle.SetText("Félicitations !");
        ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
        ObjectContent.SetText("Commandé par l'évêque Jean de Mauléon vers entre 1525 et 1550, \r\n" +
        "le chœur en bois sculpté trône au centre de la cathédrale Notre Dame de Saint Bertrand de Comminges.\r\n" +
        "La construction de l’orgue, également par Nicolas Bachelier, lui est postérieure.");

        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            Debug.Log("popup déjà ouverte !");
            StartCoroutine(Test(ObtentionObject));
        }
        else
        {
            Open(ObtentionObject);
        }

        
    }
    public IEnumerator Test(GameObject popupToOpen)
    {
        yield return new WaitUntil(() => !MainManager.Instance.popupOpen);
        Open(popupToOpen);
    }

    public void PopUpChoeur()
    {
        FactTitle.SetText("Le choeur de la cathédrale");
        FactContent.SetText("Il visait à séparer les chanoines du peuple et des pèlerins lors des célébrations en créant une zone fermée au sein même de la cathédrale. \r\n \r\n"+
        "Ce jubé est composé de 66 stalles sculptées particulièrement riches en détails.");
        Open(FactEvent);
    }

    public void PopUpOrgue()
    {
        FactTitle.SetText("L'orgue d'angle");
        FactContent.SetText("L’orgue de la cathédrale a la particularité d'être construit en angle, chose très rare.\r\n"+
        "En effet, le portail ne permettant pas de construire un orgue au-dessus, l’orgue a été construit sur le côté, en faisant un instrument unique. \r\n \r\n" +
        "Il est classé Monument Historique depuis 1840 et considéré comme l’un des plus beaux orgues d’Europe.");
        Open(FactEvent);
    }

    public void PopUpLicorneoOne()
    {
        ObjectTitle.SetText("Félicitations !");
        ObjectUnderTitle.SetText("Vous avez récupéré la corne de licorne !");
        ObjectContent.SetText("On dit que le bâton pastoral de Saint Bertrand aurait été directement sculpté dans une corne de licorne. \r\n \r\n" +
        "Il s'agit en réalité d'une corne de narval taillée.");
        Open(ObtentionObject);
    }

    public void PopUpLicorneTwo()
    {
        FactTitle.SetText("Le vol de la corne de licorne");
        FactContent.SetText("Suscitant les convoitises, la corne de licorne, ainsi que d’autres objets, sont dérobés à la fin du XVIème siècle lors d’une occupation de la cité. \r\n"+
        "La corne ne revenant pas, l’Eglise écrit alors une lettre réclamant l’intervention du roi Henri III. \r\n\r\n" +
        "C’est après l’intervention de Catherine de Médicis, mère du roi, que la corne est finalement restituée à l’église de Saint Bertrand de Comminges en 1601.");
        Open(FactEvent);
    }


    public void PopUpCrocoOne()
    {
        ObjectTitle.SetText("Félicitations !");
        ObjectUnderTitle.SetText("Vous avez obtenu le crocodile de St Bertrand !");
        ObjectContent.SetText("La légende raconte qu’il s’agirait d’une bête des marais apparu dans la ville et qui aurait terrorisé les jeunes filles de l’époque. \r\n"+
        "Saint Bertrand serait alors intervenu et aurait maîtrisé l’animal d’un coup de bâton pastoral.");
        Open(ObtentionObject);
    }

    public void PopUpCrocoTwo()
    {
        FactTitle.SetText("Le crocodile de St Bertrand");
        FactContent.SetText("On sait en réalité très peu de choses sur ce crocodile empaillé datant du XIVème siècle. \r\n  \r\n" +
        "Il s’agit probablement d’un trophée rapporté de Terre Sainte par un chevalier pour remercier Saint Bertrand de l’avoir protégé.");
        Open(FactEvent);
    }
}
