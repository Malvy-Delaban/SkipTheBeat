using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongDataFetcher : MonoBehaviour
{
    [SerializeField] private MusicCreator musicCreator;
    private int lastPointsNeeded = 0;
    private int lastCurrentPoints = 0;

    void Update() {
        DisplayDuration();
        DisplayPoints();
    }

    private void DisplayPoints() {
        int newCurrentPoints = musicCreator.GetCurrentPoints();
        int newPointsNeeded = musicCreator.GetPointsNeeded();
        
        if (newCurrentPoints != lastCurrentPoints) {
            lastCurrentPoints = newCurrentPoints;
            transform.GetChild(0).GetChild(1).GetComponent<PointsCalculation>().SetNumber(newCurrentPoints, true);
        }
        if (newPointsNeeded != lastPointsNeeded) {
            lastPointsNeeded = newPointsNeeded;
            transform.GetChild(0).GetChild(1).GetComponent<PointsCalculation>().SetNumber(newPointsNeeded, false);
        }
    }

    private void DisplayDuration() {
        TimeSpan time = TimeSpan.FromSeconds(musicCreator.GetCurrentDuration());
        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = string.Format("Duration : {0:0}:{1:00}", time.Minutes, time.Seconds);
    }
}
