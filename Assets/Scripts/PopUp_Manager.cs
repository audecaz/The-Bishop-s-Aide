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
            "\r\nLe p�lerin que vous venez de recruter �tait en r�alit� un voleur d�guis�." +
            "\r\nLa rumeur se r�pand et les gens sont plus m�fiants." +
            "\r\n \r\nVous perdez " + chara.ressourceOne + " d'OR" +
            "\r\nVous perdez " + chara.ressourceTwo + "  de FOI");
        Open(FactEvent);
    }

    
    public void PopUpSpeVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Voleur");
        FactContent.SetText("Malheur ! Ayant entendu parler de la fameuse corne de licorne et la convoitant, un voleur se fait passer pour un p�lerin et vous d�robe la corne !  \r\n \r\n"+
            "Vous perdez " + chara.ressourceOne + " d�OR  \r\n"+
            "Vous perdez la Corne de Licorne de Saint Bertrand. \r\n" +
            "Vous devez d�sormais r�cup�rer la corne avant de terminer l�Age ");
        Open(FactEvent);
    }

    public void PopUpObjetSpe(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Titre Objet");
        FactContent.SetText("Quelle chance !" +
            "\r\nLe p�lerin que vous venez de recruter vous offre un objet pr�cieux" +
            "\r\n \r\nVous obtenez " + chara.ressourceTwo + " d'OR");
        Open(FactEvent);
    }

    public void PopUpNicolas()
    {
        //CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactTitle.SetText("Nicolas Bachelier");
        FactContent.SetText("Architecte et ing�nieur,\r\n" +
            "Nicolas Bachelier (1487-1556) est consid�r� comme l�un des plus grands architectes toulousains de la Renaissance. \r\n\r\n" +
            "Il a notamment particip� � la construction de l�h�tel d�Ass�zat, du Pont-Neuf ou encore du portail du Capitole de Toulouse.");
        Open(FactEvent);
    }

    //POP UP OBTENTION OBJETS SPECIAUX
    public void PopUpOrgueChoeur()
    {
        Debug.Log("test");

        ObjectTitle.SetText("F�licitations !");
        ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
        ObjectContent.SetText("Command� par l'�v�que Jean de Maul�on vers entre 1525 et 1550, \r\n" +
        "le ch�ur en bois sculpt� tr�ne au centre de la cath�drale Notre Dame de Saint Bertrand de Comminges.\r\n" +
        "La construction de l�orgue, �galement par Nicolas Bachelier, lui est post�rieure.");

        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            Debug.Log("popup d�j� ouverte !");
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
        FactTitle.SetText("Le choeur de la cath�drale");
        FactContent.SetText("Il visait � s�parer les chanoines du peuple et des p�lerins lors des c�l�brations en cr�ant une zone ferm�e au sein m�me de la cath�drale. \r\n \r\n"+
        "Ce jub� est compos� de 66 stalles sculpt�es particuli�rement riches en d�tails.");
        Open(FactEvent);
    }

    public void PopUpOrgue()
    {
        FactTitle.SetText("L'orgue d'angle");
        FactContent.SetText("L�orgue de la cath�drale a la particularit� d'�tre construit en angle, chose tr�s rare.\r\n"+
        "En effet, le portail ne permettant pas de construire un orgue au-dessus, l�orgue a �t� construit sur le c�t�, en faisant un instrument unique. \r\n \r\n" +
        "Il est class� Monument Historique depuis 1840 et consid�r� comme l�un des plus beaux orgues d�Europe.");
        Open(FactEvent);
    }

    public void PopUpLicorneoOne()
    {
        ObjectTitle.SetText("F�licitations !");
        ObjectUnderTitle.SetText("Vous avez r�cup�r� la corne de licorne !");
        ObjectContent.SetText("On dit que le b�ton pastoral de Saint Bertrand aurait �t� directement sculpt� dans une corne de licorne. \r\n \r\n" +
        "Il s'agit en r�alit� d'une corne de narval taill�e.");
        Open(ObtentionObject);
    }

    public void PopUpLicorneTwo()
    {
        FactTitle.SetText("Le vol de la corne de licorne");
        FactContent.SetText("Suscitant les convoitises, la corne de licorne, ainsi que d�autres objets, sont d�rob�s � la fin du XVI�me si�cle lors d�une occupation de la cit�. \r\n"+
        "La corne ne revenant pas, l�Eglise �crit alors une lettre r�clamant l�intervention du roi Henri III. \r\n\r\n" +
        "C�est apr�s l�intervention de Catherine de M�dicis, m�re du roi, que la corne est finalement restitu�e � l��glise de Saint Bertrand de Comminges en 1601.");
        Open(FactEvent);
    }


    public void PopUpCrocoOne()
    {
        ObjectTitle.SetText("F�licitations !");
        ObjectUnderTitle.SetText("Vous avez obtenu le crocodile de St Bertrand !");
        ObjectContent.SetText("La l�gende raconte qu�il s�agirait d�une b�te des marais apparu dans la ville et qui aurait terroris� les jeunes filles de l��poque. \r\n"+
        "Saint Bertrand serait alors intervenu et aurait ma�tris� l�animal d�un coup de b�ton pastoral.");
        Open(ObtentionObject);
    }

    public void PopUpCrocoTwo()
    {
        FactTitle.SetText("Le crocodile de St Bertrand");
        FactContent.SetText("On sait en r�alit� tr�s peu de choses sur ce crocodile empaill� datant du XIV�me si�cle. \r\n  \r\n" +
        "Il s�agit probablement d�un troph�e rapport� de Terre Sainte par un chevalier pour remercier Saint Bertrand de l�avoir prot�g�.");
        Open(FactEvent);
    }
}
