using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class TrackList : MonoBehaviour
{
    private List<MusicPart> tempPart;
    [SerializeField] private MusicCreator musicCreator;
    [SerializeField] private GameObject musicPartButtonPrefab;

    private bool isSamePartList() {
        if (tempPart.Count != musicCreator.parts.Count)
            return false;
        for (int i = 0; i < tempPart.Count; i++) {
            if (tempPart[i] != musicCreator.parts[i])
                return false;
        }
        return true;
    }

    private void CopyList() {
        if (tempPart.Count > 0)
            tempPart.Clear();
        foreach (MusicPart part in musicCreator.parts) {
            tempPart.Add(part);
        }
    }

    void ChangeVisualButtonBasedOnMusicPart(GameObject child) {
        MusicPart part = child.GetComponent<MusicPartContainer>().part;
        Image img = child.GetComponent<Image>();
        img.color = part.color;
        child.transform.GetChild(0).GetComponent<TMP_Text>().text = part.difficulty.ToString();
    }

    void UpdateVisualList() {
        int i = 0;
        int[] oldIdList = new int[transform.childCount];

        foreach (Transform child in transform) {
            if (child && child.GetComponent<MusicPartContainer>() != null)
                oldIdList[i] = child.GetComponent<MusicPartContainer>().part.partId;
            else
                oldIdList[i] = -1;
            Destroy(child.gameObject);
            i++;
        }
        foreach (MusicPart part in tempPart) {
            GameObject newPart = Instantiate(musicPartButtonPrefab, transform);
            newPart.AddComponent<MusicPartContainer>();
            newPart.GetComponent<MusicPartContainer>().SetValues(part);
            if (tempPart.Count > oldIdList.Length && !Array.Exists(oldIdList, element => element == part.partId))
                newPart.GetComponent<ButtonInTrackList>().AnimateSpawn();
            ChangeVisualButtonBasedOnMusicPart(newPart);
        }
    }

    void Start() {
        tempPart = new List<MusicPart>();
        CopyList();
    }

    void Update()
    {
        if (isSamePartList()) {
        } else {
            CopyList();
            UpdateVisualList();
        }
    }
}
