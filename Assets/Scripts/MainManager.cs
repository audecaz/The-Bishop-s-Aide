using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour //g?re la conservation de donn?es d'une sc?ne ? l'autre
{

    public static MainManager Instance;

    public string Language;


    public int GoldCount = 500;
    public int FaithCount = 400;
    public int SkillCount = 200;
    public int ArtisanCount = 0;

    public int ThievesCount = 0;
    public int PilgrinsCount = 0;

    public bool IsNicolasRecruted = false;
    public bool IsChoirGotten = false;
    public bool IsCrocoHere = false;
    public bool HornStolen = false;
    public bool HornRetrieved = false;

    public bool IsChoirPlaced = false;
    public bool IsOrganPlaced = false;
    public bool IsHornPlaced = false;
    public bool IsCrocoPlaced = false;

    public bool Incendie = false;

    public bool RessourceUI = false;  //false rien d'ouvert, true une ressource affichée

    public bool objectiveOpen = false;
    public bool popupOpen = false;
    public int tutoActive = 1; // 0 désactivé, 1 activé première partie, 2 activé retour dans Main, -1 sélection forcée de pèlerin, 3 après selection du pelerin, 4 au moment de toucher les objectifs

    public GameObject pelerinInfos; //Instance des informations sur les pelerins

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
