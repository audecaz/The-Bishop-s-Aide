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
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class PopUp_Manager : MonoBehaviour
{
    public static PopUp_Manager InstanceFact;

    public GameObject Bg;
    public GameObject FactEvent;
    public GameObject ObtentionObject;

    private TextMeshProUGUI FactTitle;
    private TextMeshProUGUI FactContent;
    private Image FactPicto;

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
        FactPicto = gameObject.transform.GetChild(1).GetChild(3).GetComponent<Image>();

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
        FactPicto.enabled = false;

    }

    //POPUPS FACTS HISTORIQUES

    public void PopUpStBertrand()
    {
        
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpStBertrand));
        }
        else
        {
            FactTitle.SetText("Le saviez vous : Saint Bertrand");
            FactContent.SetText("Bertrand de l'Isle fut �v�que de la cit� de 1073 � sa mort et participa grandement � l'�l�vation de la ville de Lugdunum Convenarum.\r\n" +
            "Il fait construire la cath�drale romane et �rige le clo�tre.\r\n" +
            "Il y reste �v�que jusqu'� sa mort en 1123 et est canonis� en 1222. La ville prend alors son nom. \r\n \r\n" +
            "Saint Bertrand est connu pour sa bienveillance et sa volont� de faire r�gner la paix.");
            FactPicto.enabled = true;

            Open(FactEvent);
        }

    }

    public void PopUpBertrandGoth()
    {
        
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpBertrandGoth));
        }
        else
        {
            FactTitle.SetText("Bertrand de Goth");
            FactContent.SetText("�v�que de Comminges de 1294 � 1299, Bertrand de Goth participe � l'essor de la ville en �levant les reliques de Saint Bertrand " +
            "et soutenant l'afflux de p�lerins des chemins de St Jacques.Pour cela, il lance l'agrandissement de la cath�drale romane d�j� existante en y ajoutant un style gothique. \r\n \r\n" +
            "Saint Bertrand est connu pour sa bienveillance et sa volont� de faire r�gner la paix.");
            FactPicto.enabled = true;

            Open(FactEvent);
        }
    }

    public void PopUpCathedrale()
    {
        
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpCathedrale));
        }
        else
        {
            FactTitle.SetText("La cath�drale Notre Dame");
            FactContent.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat");
            FactPicto.enabled = true;

            Open(FactEvent);
        }
    }

    public void PopUpCloitre()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpCloitre));
        }
        else
        {
            FactTitle.SetText("Le clo�tre de la cath�drale");
            FactContent.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat");
            FactPicto.enabled = true;

            Open(FactEvent);
        }
    }


    // ----------------- POPUPS EVENEMENTS ALEATOIRES ---------------------------

    public void PopUpRecolte()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpRecolte));
        }
        else
        {
            int randomGold = Random.Range(18, 23);
            FactTitle.SetText("Bonne r�colte");
            FactContent.SetText("Le climat a �t� tr�s favorable cette ann�e et la r�colte est particuli�rement bonne.\r\n"+
            "Toute la cit� profite de cette heureuse nouvelle ! \r\n \r\n" +
            "Vous obtenez " + randomGold + " d�OR");

            Open(FactEvent);
            MainManager.Instance.GoldCount += randomGold;
        }
        Debug.Log("Recolte !");
    }

    public void PopUpMiracle()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpMiracle));
        }
        else
        {
            int randomFaith = Random.Range(18, 23);
            FactTitle.SetText("Miracle");
            FactContent.SetText("Quelqu�un a miraculeusement gu�ri d�s son entr�e dans la cath�drale !\r\n" +
            "La renomm�e de la cit� sur les chemins de Compostelle augmente grandement. \r\n \r\n" +
            "Vous obtenez " + randomFaith + " de FOI");

            Open(FactEvent);
            MainManager.Instance.FaithCount += randomFaith;
        }
        Debug.Log("Miracle !");
    }

    public void PopUpArtisanat()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpArtisanat));
        }
        else
        {
            int randomSkills = Random.Range(13, 17);
            FactTitle.SetText("Artisanat florissant");
            FactContent.SetText("Les artisans se plaisent dans la ville et un nouvel atelier a vu le jour.\r\n" +
            "Le ma�tre a pris 2 nouveaux apprentis ! \r\n \r\n" +
            "Vous obtenez " + randomSkills + " de FOI \r\n" +
            "Vous accueillez +2 artisans");

            Open(FactEvent);
            MainManager.Instance.SkillCount += randomSkills;
            MainManager.Instance.ArtisanCount += 2;
        }

        Debug.Log("Artisanat !");
    }

    public void PopUpIncendie()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpIncendie));
        }
        else
        {
            int randomGold = Random.Range(10, 15);
            FactTitle.SetText("Incendie");
            FactContent.SetText("Un feu a pris au c�ur de la cit� et s�est propag� sur plusieurs maisons alentour.\r\n" +
            "La reconstruction a un co�t. \r\n \r\n" +
            "Vous perdez " + randomGold + " d�OR \r\n"+
            "Vous avez besoin de +2 artisans pour reconstruire.");

            Open(FactEvent);
            MainManager.Instance.GoldCount -= randomGold;
        }
        Debug.Log("Incendie !");
    }

    public void PopUpFamine()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpFamine));
        }
        else
        {
            int randomGold = Random.Range(18, 23);
            FactTitle.SetText("Famine");
            FactContent.SetText("Les conditions de cette ann�e ont �t� particuli�rement mauvaises et la r�colte ne suffira pas pour nourrir la population.\r\n" +
            "Vous devez vous fournir ailleurs. \r\n \r\n" +
            "Vous perdez " + randomGold + " d�OR");

            Open(FactEvent);
            MainManager.Instance.GoldCount -= randomGold;
        }
        Debug.Log("Famine !");
    }

    public void PopUpEpidemie()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpEpidemie));
        }
        else
        {
            int randomFaith = Random.Range(18, 23);
            int randomSkill = Random.Range(13, 17);

            FactTitle.SetText("Epidemie");
            FactContent.SetText("Une soudaine maladie s�abat sur la ville. \r\n\r\n" +
            "Plusieurs personnes meurent et d�autres d�cident de quitter la cit� \r\n \r\n" +
            "Vous perdez " + randomFaith + " de FOI`\r\n"+
            "Vous perdez " + randomSkill + " de SAVOIR FAIRE");

            Open(FactEvent);
            MainManager.Instance.FaithCount -= randomFaith;
            MainManager.Instance.SkillCount -= randomSkill;
        }
        Debug.Log("Epidemie !");
    }

    public void PopUpVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        /*
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte, emp�che de se faire �craser
        {
            StartCoroutine(PopUpWait(PopUpVoleur));
        }
        else
        {
            FactTitle.SetText("Voleur");
            FactContent.SetText("Malheur ! " +
                "\r\nLe p�lerin que vous venez de recruter �tait en r�alit� un voleur d�guis�." +
                "\r\nLa rumeur se r�pand et les gens sont plus m�fiants." +
                "\r\n \r\nVous perdez " + chara.ressourceOne + " d'OR" +
                "\r\nVous perdez " + chara.ressourceTwo + "  de FOI");

            Open(FactEvent);
        }*/
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

        /*
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait(PopUpSpeVoleur(chosenChara));
        }
        else
        {
            FactTitle.SetText("Voleur");
            FactContent.SetText("Malheur ! Ayant entendu parler de la fameuse corne de licorne et la convoitant, un voleur se fait passer pour un p�lerin et vous d�robe la corne !  \r\n \r\n" +
                "Vous perdez " + chara.ressourceOne + " d�OR  \r\n" +
                "Vous perdez la Corne de Licorne de Saint Bertrand. \r\n" +
                "Vous devez d�sormais r�cup�rer la corne avant de terminer l�Age ");

            Open(FactEvent);
        }*/
        FactTitle.SetText("Voleur");
        FactContent.SetText("Malheur ! Ayant entendu parler de la fameuse corne de licorne et la convoitant, un voleur se fait passer pour un p�lerin et vous d�robe la corne !  \r\n \r\n" +
            "Vous perdez " + chara.ressourceOne + " d�OR  \r\n" +
            "Vous perdez la Corne de Licorne de Saint Bertrand. \r\n" +
            "Vous devez d�sormais r�cup�rer la corne avant de terminer l�Age ");

        Open(FactEvent);
    }

    public void PopUpObjetSpe(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        /*
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait("PopUpObjetSpe"));
        }
        else
        {
            FactTitle.SetText("Titre Objet");
            FactContent.SetText("Quelle chance !" +
                "\r\nLe p�lerin que vous venez de recruter vous offre un objet pr�cieux" +
                "\r\n \r\nVous obtenez " + chara.ressourceTwo + " d'OR");

            Open(FactEvent);
        }
        */
        FactTitle.SetText("Titre Objet");
        FactContent.SetText("Quelle chance !" +
            "\r\nLe p�lerin que vous venez de recruter vous offre un objet pr�cieux" +
            "\r\n \r\nVous obtenez " + chara.ressourceTwo + " d'OR");

        Open(FactEvent);

    }

    public void PopUpNicolas()
    {
        /*
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            StartCoroutine(PopUpWait("PopUpNicolas"));
        }
        else
        {
            FactTitle.SetText("Nicolas Bachelier");
            FactContent.SetText("Architecte et ing�nieur,\r\n" +
                "Nicolas Bachelier (1487-1556) est consid�r� comme l�un des plus grands architectes toulousains de la Renaissance. \r\n\r\n" +
                "Il a notamment particip� � la construction de l�h�tel d�Ass�zat, du Pont-Neuf ou encore du portail du Capitole de Toulouse.");

            Open(FactEvent);
        }*/
        FactTitle.SetText("Nicolas Bachelier");
        FactContent.SetText("Architecte et ing�nieur,\r\n" +
            "Nicolas Bachelier (1487-1556) est consid�r� comme l�un des plus grands architectes toulousains de la Renaissance. \r\n\r\n" +
            "Il a notamment particip� � la construction de l�h�tel d�Ass�zat, du Pont-Neuf ou encore du portail du Capitole de Toulouse.");

        Open(FactEvent);
    }


    //----------------------------- POP UPS OBTENTION OBJETS SPECIAUX --------------------------------------
    public void PopUpOrgueChoeur()
    {
        if (MainManager.Instance.popupOpen) //si une popup est d�j� ouverte
        {
            //Debug.Log("popup d�j� ouverte !");
            StartCoroutine(PopUpWait(PopUpOrgueChoeur));
        }
        else
        {
            ObjectTitle.SetText("F�licitations !");
            ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
            ObjectContent.SetText("Command� par l'�v�que Jean de Maul�on vers entre 1525 et 1550, \r\n" +
            "le ch�ur en bois sculpt� tr�ne au centre de la cath�drale Notre Dame de Saint Bertrand de Comminges.\r\n" +
            "La construction de l�orgue, �galement par Nicolas Bachelier, lui est post�rieure.");

            Open(ObtentionObject);
        }
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

    //Permet de ne pas superposer les popups
    public IEnumerator PopUpWait(Action functionName)
    {
        yield return new WaitUntil(() => !MainManager.Instance.popupOpen);
        functionName();
    }

    public void EventAleatoire()
    {
        int randomNumber = Random.Range(1, 20); //valeur max exclue
        Debug.Log(randomNumber);

        if(randomNumber == 19)
        {
            int randomEvent = Random.Range(1, 7); //valeur max exclue
            if (randomEvent <= 3) //event positif
            {
                if (randomEvent == 1)
                {
                    PopUpRecolte();
                }
                else if (randomEvent == 2)
                {
                    PopUpMiracle();
                }
                else
                {
                    PopUpArtisanat();
                }
            }
            else //event negatif
            {
                if (randomEvent == 4)
                {
                    PopUpFamine();
                }
                else if (randomEvent == 5)
                {
                    PopUpIncendie();
                }
                else
                {
                    PopUpEpidemie();
                }
            }
            
        }
    }
}
