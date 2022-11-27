using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour //g?re la conservation de donn?es d'une sc?ne ? l'autre
{

    public static MainManager Instance;

    public int GoldCount = 0;
    public int FaithCount = 0;
    public int SkillCount = 0;
    public int ArtisanCount = 0;

    public bool IsNicolasRecruted = false;

    public bool objectiveOpen = false;

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
