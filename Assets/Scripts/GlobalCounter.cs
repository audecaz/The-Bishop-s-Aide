using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalCounter : MonoBehaviour
{
    TextMeshProUGUI gold, faith, skill, artisan;

    void Start()
    {
        gold = GameObject.Find("GoldCount").GetComponent<TextMeshProUGUI>();
        faith = GameObject.Find("FaithCount").GetComponent<TextMeshProUGUI>();
        skill = GameObject.Find("SkillCount").GetComponent<TextMeshProUGUI>();
        artisan = GameObject.Find("ArtisanCount").GetComponent<TextMeshProUGUI>();
        //Debug.Log(gold);
    }
    void Update()
    {
        gold.SetText(MainManager.Instance.GoldCount.ToString());
        faith.SetText(MainManager.Instance.FaithCount.ToString());
        skill.SetText(MainManager.Instance.SkillCount.ToString());
        artisan.SetText(MainManager.Instance.ArtisanCount.ToString());
    }
}
