using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInfos : MonoBehaviour
{
    public int job; // 0 = pelerin, 1 = artisan, 2 = voleur
    public int ressourceOne;
    public int ressourceTwo;

    //public PopUp_Manager popUp_Manager;

        public static void AddInfosToGlobal(GameObject chosenChara)
        {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();
        if (chara.job == 0)
        {
            MainManager.Instance.GoldCount += chara.ressourceOne;
            MainManager.Instance.FaithCount += chara.ressourceTwo;
        }
        else if(chara.job == 2) // voleur
        {
            //popUp_Manager.PopUpVoleur();
            MainManager.Instance.GoldCount -= chara.ressourceOne;
            MainManager.Instance.FaithCount -= chara.ressourceTwo;
        }else if (chara.job == 3) //pelerin special
        {
            MainManager.Instance.FaithCount += chara.ressourceOne;
            MainManager.Instance.GoldCount += chara.ressourceTwo;
        }
        else //artisan
        {
            MainManager.Instance.SkillCount += chara.ressourceOne;
            MainManager.Instance.GoldCount -= chara.ressourceTwo;
            MainManager.Instance.ArtisanCount++;
        }
    }
}


