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
            "\r\nLe p�lerin que vous venez de recruter �tait en r�alit� un voleur d�guis�." +
            "\r\nLa rumeur se r�pand et les gens sont plus m�fiants." +
            "\r\n \r\nVous perdez " + chara.ressourceOne + " d'OR" +
            "\r\nVous perdez " + chara.ressourceTwo + "  de FOI");
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
        MainManager.Instance.IsNicolasRecruted = true;
        Open(FactEvent);
    }

    public void PopUpOrgueChoeur()
    {
        //CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        //ObjectTitle.SetText("Les boiseries de la cath�drale");
        ObjectUnderTitle.SetText("Vous avez obtenu le choeur et l'orgue !");
        ObjectContent.SetText("Command� par l'�v�que Jean de Maul�on vers entre 1525 et 1550, \r\n" +
        "le ch�ur en bois sculpt� tr�ne au centre de la cath�drale Notre Dame de Saint Bertrand de Comminges.\r\n" +
        "La construction de l�orgue, �galement par Nicolas Bachelier, lui est post�rieure.");
        Open(ObtentionObject);
    }
}
