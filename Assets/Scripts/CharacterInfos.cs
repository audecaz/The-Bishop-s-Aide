using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class CharacterInfos : MonoBehaviour
{
    public static CharacterInfos InstanceCharaInfos;

    public TextMeshProUGUI gold, faith, skill, artisan;

    public int job; // 0 = pelerin, 1 = artisan, 2 = voleur
    public int ressourceOne;
    public int ressourceTwo;

    public string objectName;

    //public PopUp_Manager popUp_Manager;
    public void Start()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            InstanceCharaInfos = this;

            gold = GameObject.Find("GoldCount").GetComponent<TextMeshProUGUI>();
            faith = GameObject.Find("FaithCount").GetComponent<TextMeshProUGUI>();
            skill = GameObject.Find("SkillCount").GetComponent<TextMeshProUGUI>();
            artisan = GameObject.Find("ArtisanCount").GetComponent<TextMeshProUGUI>();
        }

        
    }
    public void AddInfosToGlobal(GameObject chosenChara)
    {
        CharacterInfos chara = chosenChara.GetComponent<CharacterInfos>();

        if (chara.job == 0)
        {
            UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount + chara.ressourceOne, "gold");
            UpdateValue(MainManager.Instance.FaithCount, MainManager.Instance.FaithCount + chara.ressourceTwo, "faith");

            /*
            MainManager.Instance.GoldCount += chara.ressourceOne;
            MainManager.Instance.FaithCount += chara.ressourceTwo;*/
        }
        else if(chara.job == 2) // voleur
        {
            UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount - chara.ressourceOne, "gold");
            UpdateValue(MainManager.Instance.FaithCount, MainManager.Instance.FaithCount - chara.ressourceTwo, "faith");

            //MainManager.Instance.GoldCount -= chara.ressourceOne;
            //MainManager.Instance.FaithCount -= chara.ressourceTwo;
        }
        else if (chara.job == 3 || chara.job == 5) //pelerin special
        {
            UpdateValue(MainManager.Instance.FaithCount, MainManager.Instance.FaithCount + chara.ressourceOne, "faith");
            UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount + chara.ressourceTwo, "gold");

            //MainManager.Instance.FaithCount += chara.ressourceOne;
            //MainManager.Instance.GoldCount += chara.ressourceTwo;
        }
        else if(chara.job == 1) //artisan
        {
            UpdateValue(MainManager.Instance.SkillCount, MainManager.Instance.SkillCount + chara.ressourceOne, "skill");
            UpdateValue(MainManager.Instance.GoldCount, MainManager.Instance.GoldCount - chara.ressourceTwo, "gold");
            UpdateValue(MainManager.Instance.ArtisanCount, MainManager.Instance.ArtisanCount + 1, "artisan");

            //MainManager.Instance.SkillCount += chara.ressourceOne;
            //MainManager.Instance.GoldCount -= chara.ressourceTwo;
            //MainManager.Instance.ArtisanCount++;
        }
    }

    public void UpdateValue(int oldValue, int newValue, string ressourceName)
    {
        StartCoroutine(Counter(oldValue, newValue, ressourceName));
    }

    public IEnumerator Counter(int oldValue, int newValue, string ressourceName)
    {
       

        int stepAmount;
        //Debug.Log(oldValue);
        //Debug.Log(newValue);

        if (newValue - oldValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - oldValue) * 0.1f);
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - oldValue) * 0.1f);
        }

        Debug.Log(stepAmount);

        if (oldValue < newValue)
        {

            while (oldValue < newValue)
            {

                oldValue += stepAmount;
                if (oldValue > newValue)
                {
                    oldValue = newValue;
                    
                }

                if (ressourceName == "gold")
                {
                    MainManager.Instance.GoldCount = oldValue;
                    //Debug.Log(oldValue);
                    gold.color = new Color(0, 1, 0);
                    gold.SetText(MainManager.Instance.GoldCount.ToString());
                }
                else if (ressourceName == "faith")
                {
                    MainManager.Instance.FaithCount = oldValue;
                    faith.color = new Color(0, 1, 0);
                    faith.SetText(MainManager.Instance.FaithCount.ToString());
                }
                else if (ressourceName == "skill")
                {
                    MainManager.Instance.SkillCount = oldValue;
                    skill.color = new Color(0, 1, 0);
                    skill.SetText(MainManager.Instance.SkillCount.ToString());
                }
                else
                {
                    MainManager.Instance.ArtisanCount = oldValue;
                    artisan.color = new Color(0, 1, 0);

                    artisan.SetText(MainManager.Instance.ArtisanCount.ToString());

                }

                yield return new WaitForSeconds(1*0.01f);

            }

            if (ressourceName == "gold")
            {
                gold.color = new Color(1, 1, 1);
            }
            else if (ressourceName == "faith")
            {
                faith.color = new Color(1, 1, 1);
            }
            else if (ressourceName == "skill")
            {
                skill.color = new Color(1, 1, 1);
            }
            else
            {
                artisan.color = new Color(1, 1, 1);
            }
        }
        else
        {

            while (oldValue > newValue)
            {
                oldValue += stepAmount;
                if (oldValue < newValue)
                {
                    oldValue = newValue;

                    
                }

                if (ressourceName == "gold")
                {
                    gold.SetText(MainManager.Instance.GoldCount.ToString());
                    gold.color = new Color(1, 0, 0);

                    MainManager.Instance.GoldCount = oldValue;

                }
                else if (ressourceName == "faith")
                {
                    faith.SetText(MainManager.Instance.FaithCount.ToString());
                    faith.color = new Color(1, 0, 0);

                    MainManager.Instance.FaithCount = oldValue;

                }
                else if (ressourceName == "skill")
                {
                    skill.SetText(MainManager.Instance.SkillCount.ToString());
                    skill.color = new Color(1, 0, 0);

                    MainManager.Instance.SkillCount = oldValue;

                }
                else
                {
                    MainManager.Instance.ArtisanCount = oldValue;
                    artisan.color = new Color(1, 0, 0);

                    artisan.SetText(MainManager.Instance.ArtisanCount.ToString());

                }

                yield return new WaitForSeconds(1* 0.01f);
            }

            if (ressourceName == "gold")
            {
                gold.color = new Color(1, 1, 1);
            }
            else if (ressourceName == "faith")
            {
                faith.color = new Color(1, 1, 1);
            }
            else if (ressourceName == "skill")
            {
                skill.color = new Color(1, 1, 1);
            }
            else
            {
                artisan.color = new Color(1, 1, 1);
            }
        }
    }
}


