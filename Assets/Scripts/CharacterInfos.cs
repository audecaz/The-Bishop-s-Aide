using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInfos : MonoBehaviour
{
    public int job; // 0 = pelerin, 1 = artisan, 2 = voleur
    public int ressourceOne;
    public int ressourceTwo;


    public static void AddInfosToGlobal(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();
        if (chara.job == 0)
        {
            MainManager.Instance.GoldCount += chara.ressourceOne;
            MainManager.Instance.FaithCount += chara.ressourceTwo;
        }
        else
        {
            MainManager.Instance.SkillCount += chara.ressourceOne;
            MainManager.Instance.GoldCount += chara.ressourceTwo;
            MainManager.Instance.ArtisanCount++;
        }
    }
}


