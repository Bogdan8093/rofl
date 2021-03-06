﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUiScript : MonoBehaviour {
    // public Text AGI, STR, STA, INT;
    // agi str sta int
    // public Text[] stats = new Text[4];
    // public Text texts[5], getPoints;
    // private int[] statArr = new int[4];
    private int[] TmpStats = new int[5];
    public Text[] StatFields = new Text[6];
    IEnumerator Start() {
        yield return new WaitUntil(() => BigMom.DBF.Updating == false);
        reWriteStats();
    }
    // /*
    public IEnumerator Send() {
        if (int.Parse(StatFields[0].text) != BigMom.GSV.userData.points) {
            StatFields[5].text = "Updating please wait";
            List<string> requst = new List<string>();
            requst.Add("points");
            WWWForm form = new WWWForm();
            if (TmpStats[1] > 0) {
                form.AddField("AGI", TmpStats[1]);
                requst.Add("agility");
            }
            if (TmpStats[2] > 0) {
                form.AddField("INT", TmpStats[2]);
                requst.Add("intelligence");
            }
            if (TmpStats[3] > 0) {
                form.AddField("STA", TmpStats[3]);
                requst.Add("stamina");
            }
            if (TmpStats[4] > 0) {
                form.AddField("STR", TmpStats[4]);
                requst.Add("strength");
            }
            WWW w = BigMom.DBF.requst("update_stats", form);
            yield return new WaitUntil(() => w.isDone == true);
            Reset();
            // StartCoroutine(BigMom.GSV.userData.GetUserStats());
            StartCoroutine(BigMom.DBF.UpdateValue(requst)); // perevowy )
            yield return new WaitUntil(() => BigMom.DBF.Updating == false);
            reWriteStats();
            StatFields[5].text = "Success!";
            yield return new WaitForSeconds(2);
            StatFields[5].text = "";
        } else {
            StatFields[5].text = "Вы не распределили характеристики";
        }
    }
    // */
    void Reset() {
        for (int i = 0; i < TmpStats.Length; i++) {
            TmpStats[i] = 0;
        }
        foreach (Text item in StatFields) {
            item.color = Color.black;
        }
    }
    public void reWriteStats() {
        TmpStats[0] = BigMom.GSV.userData.points;
        StatFields[0].text = BigMom.GSV.userData.points.ToString();
        StatFields[1].text = BigMom.GSV.userData.agility.ToString();
        StatFields[2].text = BigMom.GSV.userData.intelligence.ToString();
        StatFields[3].text = BigMom.GSV.userData.stamina.ToString();
        StatFields[4].text = BigMom.GSV.userData.strength.ToString();
    }
    public void StatAssignment(bool direction, int record) {
        if (direction) {
            if (TmpStats[0] > 0) {
                TmpStats[record]++;
                StatFields[record].color = Color.blue;
                StatFields[record].text = (int.Parse(StatFields[record].text) + 1).ToString();
                StatFields[0].text = (int.Parse(StatFields[0].text) - 1).ToString();
            }
        } else {
            if (TmpStats[record] > 0) {
                TmpStats[record]--;
                StatFields[0].text = (int.Parse(StatFields[0].text) + 1).ToString();
                StatFields[record].text = (int.Parse(StatFields[record].text) - 1).ToString();
            }
            if (TmpStats[record] == 0) {
                StatFields[record].color = Color.black;
            }
        }
    }
}