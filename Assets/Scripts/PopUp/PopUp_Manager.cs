using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Image FactImage;
    private Image FactPicto;
    private AudioSource FactSound;

    private Sprite factPictoFr;
    private Sprite factPictoEng;

    private TextMeshProUGUI ObjectTitle;
    private TextMeshProUGUI ObjectUnderTitle;
    private TextMeshProUGUI ObjectContent;
    public bool IsActive = false;

    private Sprite illuRecolte;
    private Sprite illuMiracle;
    private Sprite illuArtisanat;
    private Sprite illuFamine;
    private Sprite illuIncendie;
    private Sprite illuEpidemie;
    private Sprite illuVoleur;
    private Sprite illuCoffre;
    private Sprite illuCalice;
    private Sprite illuNicolas;
    private Sprite illuStBertrand;
    private Sprite illuBertrand;
    private Sprite illuCathedrale;
    private Sprite illuCloitre;
    private Sprite illuCorne;
    private Sprite illuChoeur;
    private Sprite illuOrgue;
    private Sprite illuCroco;
    private Sprite illuAllObjects;

    public GameObject ObtentionCroco;
    public GameObject ObtentionCorne;
    public GameObject ObtentionOrgue;

    private AudioClip soundRecolte;
    private AudioClip soundMiracle;
    private AudioClip soundArtisanat;
    private AudioClip soundFamine;
    private AudioClip soundIncendie;
    private AudioClip soundEpidemie;
    private AudioClip soundVoleur;

    public void Start()
    {
        //Debug.Log(FactEvent);
        InstanceFact = this;

        FactTitle = gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        FactContent = gameObject.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        FactImage = gameObject.transform.GetChild(1).GetChild(2).GetComponent<Image>();
        FactPicto = gameObject.transform.GetChild(1).GetChild(3).GetComponent<Image>();
        FactSound = gameObject.transform.GetChild(1).GetComponent<AudioSource>();

        ObjectTitle = gameObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        ObjectUnderTitle = gameObject.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        ObjectContent = gameObject.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();


        illuRecolte = Resources.Load<Sprite>("Illus/bonne_recolte");
        illuMiracle = Resources.Load<Sprite>("Illus/miracle");
        illuArtisanat = Resources.Load<Sprite>("Illus/artisanat");
        illuFamine = Resources.Load<Sprite>("Illus/famine");
        illuIncendie = Resources.Load<Sprite>("Illus/incendie");
        illuEpidemie = Resources.Load<Sprite>("Illus/epidemie");
        illuVoleur = Resources.Load<Sprite>("Illus/voleur");
        illuCoffre = Resources.Load<Sprite>("Illus/coffre");
        illuCalice = Resources.Load<Sprite>("Illus/calice");
        illuNicolas = Resources.Load<Sprite>("Illus/nicolas");
        illuStBertrand = Resources.Load<Sprite>("Illus/stbertrand");
        illuBertrand = Resources.Load<Sprite>("Illus/bertrand");
        illuCathedrale = Resources.Load<Sprite>("Illus/cathedrale");
        illuCloitre = Resources.Load<Sprite>("Illus/cloitre");
        illuCorne = Resources.Load<Sprite>("Illus/licorne");
        illuChoeur = Resources.Load<Sprite>("Illus/choeur");
        illuOrgue = Resources.Load<Sprite>("Illus/orgue");
        illuCroco = Resources.Load<Sprite>("Illus/croco");
        illuAllObjects = Resources.Load<Sprite>("Illus/all");

        factPictoFr = Resources.Load<Sprite>("Ui/sceau_fr");
        factPictoEng = Resources.Load<Sprite>("Ui/sceau_eng");

        soundRecolte = Resources.Load<AudioClip>("Sounds/Popups/recolte");
        soundMiracle = Resources.Load<AudioClip>("Sounds/Popups/miracle");
        soundArtisanat = Resources.Load<AudioClip>("Sounds/Popups/artisanat");
        soundFamine = Resources.Load<AudioClip>("Sounds/Popups/famine");
        soundIncendie = Resources.Load<AudioClip>("Sounds/Popups/fire");
        soundEpidemie = Resources.Load<AudioClip>("Sounds/Popups/epidemie");
        soundVoleur = Resources.Load<AudioClip>("Sounds/Popups/thief");
}

    public void Open(GameObject popUp) 
    {
        Bg.SetActive(true);
        popUp.SetActive(true);
        IsActive = true;
        MainManager.Instance.popupOpen = true;
    }

    public void Close(GameObject popUp)
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Animation>().Play("Button");
        EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();

        StartCoroutine(MenuAfterAnimation(popUp));
    }

    IEnumerator MenuAfterAnimation(GameObject popUp)
    {
        yield return new WaitForSeconds(0.4f);


        Bg.SetActive(false);
        popUp.SetActive(false);
        ObtentionObject.SetActive(false);
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
            FactImage.sprite = illuStBertrand;

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Le saviez vous : Saint Bertrand");
                FactContent.SetText("<b>Bertrand de l'Isle</b> fut évêque de la cité de 1073 à sa mort et participa grandement à l'élévation de la ville de <i>Lugdunum Convenarum.</i>\r\n" +
                "Il fit construire la <b>cathédrale romane</b> et ériger le <b>cloître.</b>\r\n" +
                "Il y resta évêque jusqu'à sa mort en 1123 et fut canonisé en 1222. La ville prit alors son nom. \r\n \r\n" +
                "Saint Bertrand est connu pour <b>sa bienveillance</b> et sa volonté de <b>faire régner la paix.</b>");
                FactPicto.sprite = factPictoFr;
            }
            else
            {
                FactTitle.SetText("Did you know: Saint Bertrand");
                FactContent.SetText("<b> Bertrand de l’Isle </b> was bishop of the city from 1073 until his death. He participated greatly to the elevation of the latin city of <i>Lugdunum Convenarum.</i>" +
                "He built the <b>romanesque cathedral</b> and the <b>cloister.</b>\r\n" +
                "He stayed bishop until his death in 1123 and was canonized in 1222. On that occasion, the city took his name \r\n \r\n" +
                "Saint Bertrand is well known for <b>his kindness</b> and <b>his desire to bring peace.</b>");
                FactPicto.sprite = factPictoEng;

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
            FactImage.sprite = illuBertrand;

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Bertrand de Goth");
                FactContent.SetText("Évêque de Comminges de <b>1294 à 1299</b>, Bertrand de Goth participa à l'essor de la ville en élevant les reliques de Saint Bertrand " +
                "et soutenant l'afflux de pèlerins <b>des chemins de St-Jacques.</b> Pour cela, il lança l'<b>agrandissement de la cathédrale romane</b> déjà existante dans le style gothique. \r\n \r\n" +
                "Il devint archevêque de Bordeaux en 1300 et fut <b>sacré pape</b> en 1305 sous le nom de <b>Clément V.</b>\r\n");

                FactPicto.sprite = factPictoFr;

            }
            else
            {
                FactTitle.SetText("Bertrand de Goth");
                FactContent.SetText("Bishop of Comminges from <b>1294 to 1299</b>, Bertrand de Goth participates in the development of the city by elevating Saint Bertrand’s relics " +
                "and supporting the amount of pilgrims coming to the city through the <b>Way of Saint James.</b> To achieve it, he starts the <b>enlargement and embellishment of the already existing romanesque cathedral</b> with a more gothic style.\r\n \r\n" +
                "He becomes archbishop of Bordeaux in 1300 and is <b>crowned pope</b> in 1305 under the name of <b>Clement V.</b>\r\n");

                FactPicto.sprite = factPictoEng;

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
            FactImage.sprite = illuCathedrale;

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("La cathédrale Notre Dame");
                FactContent.SetText("La cathédrale romane Sainte Marie a été bâtie <b>sur l’acropole de l’ancienne cité romaine</b> <i>Lugdunum Convernarum</i> aux environs de l’an 1100. \r\n"
                +"C’est <b>Bertrand de l’Isle</b> qui la fit construire."
                +"Elle resta <b>la cathédrale du diocèse de Comminges</b> jusqu’en 1801, ensuite raccordée au diocèse de Bayonne et l'archidiocèse de Toulouse. ");

                FactPicto.sprite = factPictoFr;

            }
            else
            {
                FactTitle.SetText("The cathedral Notre Dame");
                FactContent.SetText("The romanesque cathedral of Saint Mary was built <b>on the acropolis of the ancient roman city</b> <i>Lugdunum Convernarum</i> around 1100.\r\n"
                +"It was <b>Bertrand de l’Isle</b> who ordered it.\r\n"
                +"The cathedral stayed <b>the official church of the Comminges</b> diocese up to 1801 until it became linked to the diocese of Bayonne and to the archdiocese of Toulouse.");

                FactPicto.sprite = factPictoEng;

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
            FactImage.sprite = illuCloitre;

            if (MainManager.Instance.Language == "fr")
            {
                FactTitle.SetText("Le cloître de la cathédrale");
                FactContent.SetText("Le cloître fut <b>construit au 12ème siècles pour les chanoines</b>. \r\n"
                +"De cette période, il ne reste aujourd’hui plus que <b>les arcades romanes</b> de la galerie sud avec leurs arcs en plein cintre.\r\n" 
                +"La galerie nord quant à elle fut construite <b>au 15ème siècle et est de style gothique</b> avec des croisées d’ogives caractéristiques de ce mouvement architectural. ");

                FactPicto.sprite = factPictoFr;

            }
            else
            {
                FactTitle.SetText("The cathedral's cloister");
                FactContent.SetText("The cloister was <b>built in the 12th century for the canons.</b>\r\n"
                +"From this period, only the southern gallery’s <b>romanesque arcades</b> remain with their semicircular arches.\r\n" 
                +"The north gallery was built <b>in the 15th century</b>. It is characteristic of the Gothic architectural movement with its cross-ribs.");

                FactPicto.sprite = factPictoEng;
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
            FactImage.sprite = illuRecolte;
            FactSound.clip = soundRecolte;

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
            FactSound.Play();


            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount + randomGold, "gold");

            //MainManager.Instance.GoldCount += randomGold;
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
            FactImage.sprite = illuMiracle;

            FactSound.clip = soundMiracle;

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
            FactSound.Play();


            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.FaithCount, MainManager.Instance.FaithCount + randomFaith, "faith");

            //MainManager.Instance.FaithCount += randomFaith;
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
            FactImage.sprite = illuArtisanat;

            FactSound.clip = soundArtisanat;

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
            FactSound.Play();

            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.SkillCount, MainManager.Instance.SkillCount + randomSkills, "skill");
            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.ArtisanCount, MainManager.Instance.ArtisanCount + 2, "artisan");

            //MainManager.Instance.SkillCount += randomSkills;
            //MainManager.Instance.ArtisanCount += 2;
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
            FactImage.sprite = illuIncendie;

            FactSound.clip = soundIncendie;

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
            FactSound.Play();

            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount - randomGold, "gold");

            //MainManager.Instance.GoldCount -= randomGold;
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
            FactImage.sprite = illuFamine;

            FactSound.clip = soundFamine;

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
            FactSound.Play();


            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount - randomGold, "gold");
            //MainManager.Instance.GoldCount -= randomGold;
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
            FactImage.sprite = illuEpidemie;

            FactSound.clip = soundEpidemie;

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
            FactSound.Play();


            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.FaithCount, MainManager.Instance.FaithCount - randomFaith , "faith");
            CharacterInfos.InstanceCharaInfos.UpdateValue(MainManager.Instance.SkillCount, MainManager.Instance.SkillCount - randomSkill, "skill");
            //MainManager.Instance.FaithCount -= randomFaith;
            //MainManager.Instance.SkillCount -= randomSkill;
        }
        Debug.Log("Epidemie !");
    }

    public void PopUpVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactImage.sprite = illuVoleur;

        FactSound.clip = soundVoleur;

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
        FactSound.Play();

    }


    public void PopUpSpeVoleur(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        FactImage.sprite = illuVoleur;

        FactSound.clip = soundVoleur;

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
        FactSound.Play();

    }

    public void PopUpObjetSpe(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        
        if (MainManager.Instance.Language == "fr")
        {
            if(chara.objectName == "coffre")
            {
                FactImage.sprite = illuCoffre;
                FactTitle.SetText("Coffre précieux");
            }
            else
            {
                FactImage.sprite = illuCalice;
                FactTitle.SetText("Calice en or");
            }
            FactContent.SetText("Quelle chance !" +
            "\r\nLe pèlerin que vous venez de recruter vous offre un objet précieux" +
            "\r\n \r\n<b>Vous obtenez " + chara.ressourceTwo + " d'OR</b>");
        }
        else
        {
            if (chara.objectName == "coffre")
            {
                FactImage.sprite = illuCoffre;
                FactTitle.SetText("Precious chest");
            }
            else
            {
                FactImage.sprite = illuCalice;
                FactTitle.SetText("Golden calice");
            }
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
            FactImage.sprite = illuNicolas;

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
    //Quand le choeur et l'orgue sont obtenus
    public void PopUpOrgueChoeur()
    {
        if (MainManager.Instance.popupOpen) //si une popup est déjà ouverte
        {
            //Debug.Log("popup déjà ouverte !");
            StartCoroutine(PopUpWait(PopUpOrgueChoeur));
        }
        else
        {
            ObtentionCorne.SetActive(false);
            ObtentionCroco.SetActive(false);

            ObtentionOrgue.SetActive(true);

            if (MainManager.Instance.Language == "fr")
            {
                ObjectTitle.SetText("Félicitations !");
                ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
                ObjectContent.SetText("Commandé par <b>l'évêque Jean de Mauléon</b> vers entre 1525 et 1550, \r\n" +
                "le chœur en bois sculpté trône <b>au centre de la cathédrale Notre Dame</b> de Saint-Bertrand de Comminges.\r\n" +
                "La construction de l’orgue, également par Nicolas Bachelier, est postérieure.");
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

    //Quand le choeur est placé
    public void PopUpChoeur()
    {
        FactImage.sprite = illuChoeur;

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
            "This jube is composed of <b>66 carved stalls. </b> They are particularly rich in details. ");
        }

        Open(FactEvent);
    }

    //Quand l'orgue est placé
    public void PopUpOrgue()
    {
        FactImage.sprite = illuOrgue;

        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("L'orgue d'angle");
            FactContent.SetText("L’orgue de la cathédrale a la particularité <b>d'être construit en angle</b>, chose très rare.\r\n" +
            "En effet, le portail ne permettant pas de construire un orgue au-dessus, l’orgue a été construit sur le côté, ce qui en fait un <b>instrument unique.</b> \r\n \r\n" +
            "Il est <b>classé Monument Historique</b> depuis 1840 et considéré comme <b>l’un des plus beaux orgues d’Europe.</b>");
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
    
    //Quand la corne est récupérée
    public void PopUpLicorneoOne()
    {
        ObtentionOrgue.SetActive(false);
        ObtentionCroco.SetActive(false);

        ObtentionCorne.SetActive(true);

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

    //Quand la corne est placée
    public void PopUpLicorneTwo()
    {
        FactImage.sprite = illuCorne;

        if (MainManager.Instance.Language == "fr")
        {
            FactTitle.SetText("Le vol de la corne de licorne");
            FactContent.SetText("Suscitant les convoitises, <b>la corne de licorne</b>, ainsi que d’autres objets, ont été <b>dérobés à la fin du XVIème siècle</b> lors d’une occupation de la cité. \r\n" +
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

    //Quand le croco est obtenu
    public void PopUpCrocoOne()
    {
        ObtentionOrgue.SetActive(false);
        ObtentionCorne.SetActive(false);

        ObtentionCroco.SetActive(true);

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

    //Quand le croco est placé
    public void PopUpCrocoTwo()
    {
        FactImage.sprite = illuCroco;

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
            FactImage.sprite = illuAllObjects;

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
        //Debug.Log(randomNumber);

        if(randomNumber >= 14)
        //if (randomNumber < 19)
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
                //if (randomEvent < 2)
                {
                    PopUpFamine();
                }
                else if (randomEvent == 5)
                //else if (randomEvent <2)
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
