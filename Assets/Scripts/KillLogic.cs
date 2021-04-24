﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KillLogic : MonoBehaviour
{
    public TextMeshProUGUI KillCounter;
    public TextMeshProUGUI WaveCounter;
    public static int kills;
    public static int wave;

    public int killsForCommon = 1;
    public int killsForRare = 2;
    public int killsForLegendary = 3;

    public GameObject lockedCommon;
    public GameObject lockedRare;
    public GameObject lockedLegendary;

    public GameObject unlockedCommon;
    public GameObject unlockedRare;
    public GameObject unlockedLegendary;

    public bool doneCommonNotification;
    public bool doneRareNotification;
    public bool doneLegendaryNotification;
    void Start()
    {
        kills = 0;
        wave = 1;
        if (gameObject.tag == "Canvas")
        {
            //Debug.Log("test");
            lockedCommon.SetActive(true);
            lockedRare.SetActive(true);
            lockedLegendary.SetActive(true);

            unlockedCommon.SetActive(false);
            unlockedRare.SetActive(false);
            unlockedLegendary.SetActive(false);
        }
    }

    void Update()
    {
        if (gameObject.tag == "Canvas")
        {
            if (kills >= killsForCommon)
            {
                //Debug.Log("test2");
                //Debug.Log("can use common");
                unlockedCommon.SetActive(true);
                lockedCommon.SetActive(false);
            }
            if (kills >= killsForRare)
            {
                //Debug.Log("can use rare");
                unlockedRare.SetActive(true);
                lockedRare.SetActive(false);
            }
            if (kills >= killsForLegendary)
            {
                //Debug.Log("can use legendary");
                unlockedLegendary.SetActive(true);
                lockedLegendary.SetActive(false);
            }
        }
        KillCounter.text = kills.ToString();
        WaveCounter.text = wave.ToString();
    }
}
