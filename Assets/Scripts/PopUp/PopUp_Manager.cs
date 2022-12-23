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

        if (MainManager.Instance.allObjectives)
        {
            Debug.Log("fin du jeu");
        }
    }

    //POPUPS FACTS HISTORIQUES

    public void PopUpStBertrand()
    {
        
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpStBertrand));
        }
        else
        {
            if(MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Le saviez vous : Saint Bertrand");
                FactContent.SetText("<b>Bertrand de l'Isle</b> fut évêque de la cité de 1073 à sa mort et participa grandement à l'élévation de la ville de <i>Lugdunum Convenarum.</i>\r\n" +
                "Il fait construire la <b>cathédrale romane</b> et érige le <b>cloître.</b>\r\n" +
                "Il y reste évêque jusqu'à sa mort en 1123 et est canonisé en 1222. La ville prend alors son nom. \r\n \r\n" +
                "Saint Bertrand est connu pour <b>sa bienveillance</b> et sa volonté de <b>faire régner la paix.</b>");
            }
            else
            {
                FactTitle.SetText("Did you know: Saint Bertrand");
                FactContent.SetText("<b> Bertrand de l’Isle </b> was bishop of the city from 1073 until his death. He participated greatly to the elevation of the latin city of <i>Lugdunum Convenarum.</i>" +
                "He built the <b>romanesque cathedral</b> and the <b>cloister.</b>\r\n" +
                "He stayed bishop until his death in 1123 and was canonized in 1222. On that occasion, the city took his name \r\n \r\n" +
                "Saint Bertrand is well known for <b>his kindness</b> and <b>his desire to bring peace.</b>");
            }

            FactPicto.enabled = true;

            Open(FactEvent);
        }

    }

    public void PopUpBertrandGoth()
    {
        
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpBertrandGoth));
        }
        else
        {
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Bertrand de Goth");
                FactContent.SetText("Évêque de Comminges de <b>1294 à 1299</b>, Bertrand de Goth participe à l'essor de la ville en élevant les reliques de Saint Bertrand " +
                "et soutenant l'afflux de pèlerins <b>des chemins de St Jacques.</b> Pour cela, il lance l'<b>agrandissement de la cathédrale romane</b> déjà existante en y ajoutant un style gothique. \r\n \r\n" +
                "Il devient archevêque de Bordeaux en 1300 et est <b>sacré pape</b> en 1305 sous le nom de <b>Clément V.</b>\r\n");
            }
            else
            {
                FactTitle.SetText("Bertrand de Goth");
                FactContent.SetText("Bishop of Comminges from <b>1294 to 1299</b>, Bertrand de Goth participates in the development of the city by elevating Saint Bertrand’s relics " +
                "and supporting the amount of pilgrims coming to the city through the <b>Way of Saint James.</b> To achieve it, he starts the <b>enlargement and embellishment of the already existing romanesque cathedral</b> with a more gothic style.\r\n \r\n" +
                "He becomes archbishop of Bordeaux in 1300 and is <b>crowned pope</b> in 1305 under the name of <b>Clement V.</b>\r\n");
            }
            FactPicto.enabled = true;

            Open(FactEvent);
        }
    }

    public void PopUpCathedrale()
    {
        
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpCathedrale));
        }
        else
        {
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("La cathédrale Notre Dame");
                FactContent.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat");
            }
            else
            {
                FactTitle.SetText("The cathedral Notre Dame");
                FactContent.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat");
            }
                
            FactPicto.enabled = true;

            Open(FactEvent);
        }
    }

    public void PopUpCloitre()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpCloitre));
        }
        else
        {
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Le cloître de la cathédrale");
                FactContent.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat");
            }
            else
            {
                FactTitle.SetText("The cathedral's cloister");
                FactContent.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat");
            }
            
            FactPicto.enabled = true;

            Open(FactEvent);
        }
    }


    // ----------------- POPUPS EVENEMENTS ALEATOIRES ---------------------------

    public void PopUpRecolte()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpRecolte));
        }
        else
        {
            int randomGold = Random.Range(18, 23);
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Bonne récolte");
                FactContent.SetText("Le climat a été très favorable cette année et la récolte est particulièrement bonne.\r\n" +
                "Toute la cité profite de cette heureuse nouvelle ! \r\n \r\n" +
                "<b>Vous obtenez " + randomGold + " d’OR</b>");
            }
            else
            {
                FactTitle.SetText("Excellent harvest");
                FactContent.SetText("Climate has been very favorable this year and the harvest is particularly good.\r\n" +
                "The whole city benefits from this lucky event ! \r\n \r\n" +
                "<b>You gain " + randomGold + " GOLD</b>");
            }

            Open(FactEvent);
            MainManager.Instance.GoldCount += randomGold;
        }
        Debug.Log("Recolte !");
    }

    public void PopUpMiracle()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpMiracle));
        }
        else
        {
            int randomFaith = Random.Range(18, 23);
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Miracle");
                FactContent.SetText("Quelqu’un a miraculeusement guéri dès son entrée dans la cathédrale !\r\n" +
                "La renommée de la cité sur les chemins de Compostelle augmente grandement. \r\n \r\n" +
                "<b>Vous obtenez " + randomFaith + " de FOI</b>");
            }
            else
            {
                FactTitle.SetText("Miracle");
                FactContent.SetText("Someone miraculously healed as soon as they entered the cathedral !\r\n" +
                "The renown of the city on the Way of Saint James greatly increases. \r\n \r\n" +
                "<b>You gain " + randomFaith + " FAITH</b>");
            }

            Open(FactEvent);
            MainManager.Instance.FaithCount += randomFaith;
        }
        Debug.Log("Miracle !");
    }

    public void PopUpArtisanat()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpArtisanat));
        }
        else
        {
            int randomSkills = Random.Range(13, 17);

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Artisanat florissant");
                FactContent.SetText("Les artisans se plaisent dans la ville et un nouvel atelier a vu le jour.\r\n" +
                "Le maître a pris 2 nouveaux apprentis ! \r\n \r\n" +
                "<b>Vous obtenez " + randomSkills + " de FOI </b>\r\n" +
                "<b>Vous accueillez +2 artisans</b>");
            }
            else
            {
                FactTitle.SetText("Flourishing craftsmanship");
                FactContent.SetText("The craftsmen are happy in the city and a new workshop has opened. \r\n" +
                "The master craftsman took 2 new apprentices! \r\n \r\n" +
                "<b>You gain " + randomSkills + " FAITH </b>\r\n" +
                "<b>You recruit 2 new craftsmen</b>");
            }

            Open(FactEvent);
            MainManager.Instance.SkillCount += randomSkills;
            MainManager.Instance.ArtisanCount += 2;
        }

        Debug.Log("Artisanat !");
    }

    public void PopUpIncendie()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpIncendie));
        }
        else
        {
            int randomGold = Random.Range(10, 15);

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Incendie");
                FactContent.SetText("Un feu a pris au cœur de la cité et s’est propagé sur plusieurs maisons alentour.\r\n" +
                "La reconstruction a un coût. \r\n \r\n" +
                "<b>Vous perdez " + randomGold + " d’OR </b> \r\n" +
                "<b>Vous avez besoin de +2 artisans pour reconstruire.</b>");
            }
            else
            {
                FactTitle.SetText("Fire");
                FactContent.SetText("A fire started in the heart of the city and spread to multiple houses around.\r\n" +
                "Rebuilding has a cost. \r\n \r\n" +
                "<b>You lose " + randomGold + " GOLD </b>\r\n" +
                "<b>You need +2 more craftsmen to rebuild. </b>");
            }

            MainManager.Instance.Incendie = true;
            
            /*
            if (!MainManager.Instance.HornStolen) // Si la corne n'a pas encore été volée et donc l'objectif pas apparu
            {
                //inverse les position des deux objectifs 
                GameObject.Find("ObjectiveSix").transform.localPosition = new Vector3(480, 220, 0);
                GameObject.Find("ObjectiveFive").transform.localPosition = new Vector3(480, 20, 0);
            }*/

            Open(FactEvent);
            MainManager.Instance.GoldCount -= randomGold;
        }
        Debug.Log("Incendie !");
    }

    public void PopUpFamine()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpFamine));
        }
        else
        {
            int randomGold = Random.Range(18, 23);

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Famine");
                FactContent.SetText("Les conditions de cette année ont été particulièrement mauvaises et la récolte ne suffira pas pour nourrir la population.\r\n" +
                "Vous devez vous fournir ailleurs. \r\n \r\n" +
                "<b>Vous perdez " + randomGold + " d’OR</b>");
            }
            else
            {
                FactTitle.SetText("Famine");
                FactContent.SetText("This year’s climate was particularly bad and the harvest will not be enough to feed people. \r\n" +
                "You have to buy food elsewhere. \r\n \r\n" +
                "<b>You lose " + randomGold + " GOLD</b>");
            }

            Open(FactEvent);
            MainManager.Instance.GoldCount -= randomGold;
        }
        Debug.Log("Famine !");
    }

    public void PopUpEpidemie()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpEpidemie));
        }
        else
        {
            int randomFaith = Random.Range(18, 23);
            int randomSkill = Random.Range(13, 17);

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Epidemie");
                FactContent.SetText("Une soudaine maladie s’abat sur la ville. \r\n" +
                "Plusieurs personnes meurent et d’autres décident de quitter la cité… \r\n \r\n" +
                "<b>Vous perdez " + randomFaith + " de FOI</b>\r\n" +
                "<b>Vous perdez " + randomSkill + " de SAVOIR FAIRE</b>");
            }
            else
            {
                FactTitle.SetText("Disease outbreak");
                FactContent.SetText("A sudden disease strikes the city. \r\n" +
                "Few people die and others decide to leave. \r\n \r\n" +
                "<b>You lose " + randomFaith + " FAITH</b>\r\n" +
                "<b>You lose " + randomSkill + " SKILLS</b>");
            }

            Open(FactEvent);
            MainManager.Instance.FaithCount -= randomFaith;
            MainManager.Instance.SkillCount -= randomSkill;
        }
        Debug.Log("Epidemie !");
    }

    public void PopUpVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Voleur");
            FactContent.SetText("Malheur ! " +
            "\r\nLe pèlerin que vous venez de recruter était en réalité un voleur déguisé." +
            "\r\nLa rumeur se répand et les gens sont plus méfiants." +
            "\r\n \r\n<b>Vous perdez " + chara.ressourceOne + " d'OR</b>" +
            "\r\n<b>Vous perdez " + chara.ressourceTwo + "  de FOI</b>");
        }
        else
        {
            FactTitle.SetText("Thief");
            FactContent.SetText("Bad luck ! " +
            "\r\nThe pilgrim you just recruited was in fact a thief in disguise." +
            "\r\nThe rumor spreads and people are more wary.\r\n" +
            "\r\n \r\n<b>You lose " + chara.ressourceOne + " GOLD</b>" +
            "\r\n<b>You lose " + chara.ressourceTwo + "  FAITH</b>");
        }

        Open(FactEvent);
    }

    
    public void PopUpSpeVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        /*
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpSpeVoleur(chosenChara));
        }
        else
        {
            FactTitle.SetText("Voleur");
            FactContent.SetText("Malheur ! Ayant entendu parler de la fameuse corne de licorne et la convoitant, un voleur se fait passer pour un pèlerin et vous dérobe la corne !  \r\n \r\n" +
                "Vous perdez " + chara.ressourceOne + " d’OR  \r\n" +
                "Vous perdez la Corne de Licorne de Saint Bertrand. \r\n" +
                "Vous devez désormais récupérer la corne avant de terminer l’Age ");

            Open(FactEvent);
        }*/
        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Voleur");
            FactContent.SetText("Malheur ! Ayant entendu parler de la fameuse corne de licorne et la convoitant, un voleur se fait passer pour un pèlerin et vous dérobe la corne !  \r\n \r\n" +
            "<b>Vous perdez " + chara.ressourceOne + " d’OR  </b>\r\n" +
            "<b>Vous perdez la Corne de Licorne de Saint Bertrand. </b>\r\n" +
            "<b>Vous devez désormais récupérer la corne avant de terminer l’Age </b>");
        }
        else
        {
            FactTitle.SetText("Thief");
            FactContent.SetText("Bad luck ! " +
            "\r\nThe pilgrim you just recruited was in fact a thief in disguise." +
            "\r\nHaving heard about the famous unicorn’s horn, he steals it." +
            "\r\n \r\n<b>You lose " + chara.ressourceOne + " GOLD</b>\r\n" +
            "<b>You lose the Unicorn’s Horn of Saint Bertrand. </b>\r\n" +
            "<b>You have to retrieve the horn to finish the era.</b>");
        }

        Open(FactEvent);
    }

    public void PopUpObjetSpe(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        /*
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait("PopUpObjetSpe"));
        }
        else
        {
            FactTitle.SetText("Titre Objet");
            FactContent.SetText("Quelle chance !" +
                "\r\nLe pèlerin que vous venez de recruter vous offre un objet précieux" +
                "\r\n \r\nVous obtenez " + chara.ressourceTwo + " d'OR");

            Open(FactEvent);
        }
        */
        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Titre Objet");
            FactContent.SetText("Quelle chance !" +
            "\r\nLe pèlerin que vous venez de recruter vous offre un objet précieux" +
            "\r\n \r\nVous obtenez " + chara.ressourceTwo + " d'OR");
        }
        else
        {
            FactTitle.SetText("Object Title");
            FactContent.SetText("Great news !" +
            "\r\nThe pilgrim you just recruited gives you a precious item" +
            "\r\n \r\n <b>You gain " + chara.ressourceTwo + " GOLD </b>");
        }

        Open(FactEvent);

    }

    public void PopUpNicolas()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            StartCoroutine(PopUpWait(PopUpNicolas));
        }
        else
        {
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Nicolas Bachelier");
                FactContent.SetText("Architecte et ingénieur,\r\n" +
                "Nicolas Bachelier (1487-1556) est considéré comme l’un des plus grands architectes toulousains de la Renaissance. \r\n\r\n" +
                "Il a notamment participé à la construction de l’hôtel d’Assézat, du Pont-Neuf ou encore du portail du Capitole de Toulouse.");
            }
            else
            {
                FactTitle.SetText("Nicolas Bachelier");
                FactContent.SetText("Architect and engineer,\r\n" +
                "Nicolas Bachelier (1487-1556) is considered one of the greatest architects of the Renaissance in Toulouse’s area. \r\n\r\n" +
                "In  particular, he participated in the construction of the <b>Assezat Hotel, Pont-Neuf and Capitole’s gate</b> in Toulouse.");
            }

            Open(FactEvent);
        }
        
    }


    //----------------------------- POP UPS OBTENTION OBJETS SPECIAUX --------------------------------------
    public void PopUpOrgueChoeur()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            //Debug.Log("popup déjà ouverte !");
            StartCoroutine(PopUpWait(PopUpOrgueChoeur));
        }
        else
        {
            if (MainManager.Instance.Language == "fr")
            {
                ObjectTitle.SetText("Félicitations !");
                ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
                ObjectContent.SetText("Commandé par <b>l'évêque Jean de Mauléon</b> vers entre 1525 et 1550, \r\n" +
                "le chœur en bois sculpté trône <b>au centre de la cathédrale Notre Dame</b> de Saint Bertrand de Comminges.\r\n" +
                "La construction de l’orgue, également par Nicolas Bachelier, lui est postérieure.");
            }
            else
            {
                ObjectTitle.SetText("Congratulations !");
                ObjectUnderTitle.SetText("You have obtained the choir and the organ !");
                ObjectContent.SetText("The choir was ordered by <b>bishop Jean de Mauléon</b> between the years 1525 and 1550. \r\n" +
                "The construction of the organ came after the choir. <b>It was also made by Nicolas Bachelier.</b>");
            }

            Open(ObtentionObject);
        }
    }

    public void PopUpChoeur()
    {
        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Le choeur de la cathédrale");
            FactContent.SetText("Il visait à <b>séparer les chanoines du peuple et des pèlerins</b> lors des célébrations en créant une zone fermée au sein même de la cathédrale. \r\n \r\n" +
            "Ce jubé est composé de <b>66 stalles sculptées </b> particulièrement riches en détails.");
        }
        else
        {
            FactTitle.SetText("The choir");
            FactContent.SetText("It was made <b>to separate the canons from the people and the pilgrims</b> during celebrations by creating a closed area within the cathedral.\r\n \r\n" +
            "This jube is composed of <b>66 carved stalls.</b> They are particularly rich in details. ");
        }

        Open(FactEvent);
    }

    public void PopUpOrgue()
    {
        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("L'orgue d'angle");
            FactContent.SetText("L’orgue de la cathédrale a la particularité <b>d'être construit en angle</b>, chose très rare.\r\n" +
            "En effet, le portail ne permettant pas de construire un orgue au-dessus, l’orgue a été construit sur le côté, en faisant un <b>instrument unique.</b> \r\n \r\n" +
            "Il est <b>classé Monument Historique</b> depuis 1840 et considéré comme <b>l’un des plus beaux orgues d’Europe./b>");
        }
        else
        {
            FactTitle.SetText("The corner organ");
            FactContent.SetText("The organ of the cathedral has the particularity of being <b>built to go in a corner</b>, which is very rare.\r\n" +
            "Indeed, the portal does not allow an organ to be built above it, it was built on the side, turning it into an <b>unique instrument.</b> \r\n \r\n" +
            "It is <b>listed as a historic monument by UNESCO</b> since 1840 and is considered as <b>one of the most beautiful organs of Europe.</b>");
        }

        Open(FactEvent);
    }

    public void PopUpLicorneoOne()
    {
        if (MainManager.Instance.Language == "fr")
        {
            ObjectTitle.SetText("Félicitations !");
            ObjectUnderTitle.SetText("Vous avez récupéré la corne de licorne !");
            ObjectContent.SetText("On dit que le bâton pastoral de Saint Bertrand aurait été <b>directement sculpté dans une corne de licorne.</b> \r\n \r\n" +
            "Il s'agit en réalité d'une corne de narval taillée.");
        }
        else
        {
            ObjectTitle.SetText("Congratulations !");
            ObjectUnderTitle.SetText("You have retrieved the Unicorn horn !");
            ObjectContent.SetText("It is said that the pastoral staff of Saint Bertrand <b>was carved directly in a unicorn’s horn.</b> \r\n \r\n" +
            "It is in reality a carved narwhal’s tusk. ");
        }

        Open(ObtentionObject);


    }

    public void PopUpLicorneTwo()
    {
        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Le vol de la corne de licorne");
            FactContent.SetText("Suscitant les convoitises, <b>la corne de licorne</b>, ainsi que d’autres objets, sont <b>dérobés à la fin du XVIème siècle</b> lors d’une occupation de la cité. \r\n" +
            "La corne ne revenant pas, l’Eglise écrit alors une lettre réclamant l’intervention du roi Henri III. \r\n\r\n" +
            "C’est après <b>l’intervention de Catherine de Médicis</b>, mère du roi, que la corne est <b>finalement restituée</b> à l’église de Saint Bertrand de Comminges <b>en 1601.</b>");
        }
        else
        {
            FactTitle.SetText("The theft of the unicorn’s horn");
            FactContent.SetText("When the city was occupied <b>at the end of the 16th century</b>, <b>the coveted unicorn’s horn</b>, as well as other objects, <b>were stolen</b>.\r\n" +
            "Seeing the horn not coming back, the Church wrote a letter asking for King Henri III to interfere. \r\n\r\n" +
            "It was only after <b>the intervention of Catherine de’ Medici</b>, mother of the King, that the horn was <b>finally returned</b> to the church of Saint Bertrand de Comminges <b>in 1601.</b> \r\n");
        }

        Open(FactEvent);
    }


    public void PopUpCrocoOne()
    {
        if (MainManager.Instance.Language == "fr")
        {
            ObjectTitle.SetText("Félicitations !");
            ObjectUnderTitle.SetText("Vous avez obtenu le crocodile de St Bertrand !");
            ObjectContent.SetText("La légende raconte qu’il s’agirait d’<b>une bête des marais apparu dans la ville</b> et qui aurait terrorisé les jeunes filles de l’époque. \r\n" +
            "<b>Saint Bertrand serait alors intervenu et aurait maîtrisé l’animal d’un coup de bâton pastoral.</b>");
        }
        else
        {
            ObjectTitle.SetText("Congratulations !");
            ObjectUnderTitle.SetText("You have obtained the crocodile of St Bertrand !");
            ObjectContent.SetText("The legend tells that it is <B>a swamp beast that appeared in the city</b> and that terrorized young ladies. \r\n" +
            "<b>Saint Bertrand would then have intervened and killed the animal with his pastoral staff</b>");
        }

        Open(ObtentionObject);
    }

    public void PopUpCrocoTwo()
    {
        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Le crocodile de St Bertrand");
            FactContent.SetText("On sait en réalité très peu de choses sur ce crocodile empaillé datant <b>du XIVème siècle. </b>\r\n  \r\n" +
            "Il s’agit probablement d’<b>un trophée rapporté de Terre Sainte par un chevalier pour remercier Saint Bertrand de l’avoir protégé.</b>");
        }
        else
        {
            FactTitle.SetText("The crocodile of Saint Bertrand");
            FactContent.SetText("We actually know very little about this stuffed crocodile <b>from the 14th century.</b> \r\n  \r\n" +
            "It is probably<b> a trophy from the crusades brought back from the Holy Land by a knight to thank Saint Bertrand for his protection.</b>");
        }

        Open(FactEvent);
    }

    public void PopUpAllObjetives()
    {
        MainManager.Instance.allObjectives = true;

        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            //Debug.Log("popup déjà ouverte !");
            StartCoroutine(PopUpWait(PopUpAllObjetives));
        }
        else
        {
            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Félicitations !");
                FactContent.SetText("Tous les objectifs ont été remplis et vous avez obtenu tous les objets liés à cet Age ! \r\n  \r\n" +
                "Rendez vous dans la cathédrale, <b>placez les éléments à leur place et revenez ici pour compléter l'Age. \r\n</b>");
            }
            else
            {
                FactTitle.SetText("Congratulations !");
                FactContent.SetText("All the objectives have been completed and you’ve obtained all the important objects of this Age. \r\n  \r\n" +
                "Now go to the cathedral, <b>put every object into place and come back here to complete the Age.</b>");
            }

            Open(FactEvent);

        }
    }

    //Permet de ne pas superposer les popups
    public IEnumerator PopUpWait(Action functionName)
    {
        yield return new WaitUntil(() => !MainManager.Instance.popupOpen);
        functionName();
    }

    public void EventAleatoire()
    {
        int randomNumber = Random.Range(1, 16); //valeur max exclue
        Debug.Log(randomNumber);

        //if(randomNumber == 15)
        if (randomNumber < 19)
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
                //if (randomEvent == 4)
                if (randomEvent < 2)
                {
                    PopUpFamine();
                }
                //else if (randomEvent == 5)
                else if (randomEvent <2)
                {
                    PopUpEpidemie();

                }
                else if(!MainManager.Instance.Incendie)
                {
                    PopUpIncendie();
                    City_Effects.CityFxInstance.CityFireOn();
                }
            }
            
        }
    }
}
