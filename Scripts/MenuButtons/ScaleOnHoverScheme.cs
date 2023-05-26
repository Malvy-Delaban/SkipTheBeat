using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScaleOnHoverScheme : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // parent
    public SchemeList schemeList;

    // Detail panel visuals
    private Color detailPanelColor;

    // Given points
    private int givenPoints;

    // Remaining scheme
    private int remaining;

    // Button resize
    private Vector2 baseSize = new(60, 30);
    private Vector2 targetSize = new(70, 30);

    private void Start() {
        schemeList = transform.parent.GetComponent<SchemeList>();
        GetComponent<Button>().onClick.AddListener(ClickedButton);
    }

    private void ClickedButton() {
        MusicPartDifficulty difficulty = GetComponent<MusicPartContainer>().part.difficulty;
        schemeList.AddMusicPart(difficulty);
    }

    public void GetPartData() {
        MusicPart part = GetComponent<MusicPartContainer>().part;

        detailPanelColor = part.color;
        detailPanelColor.a = 1f;
        givenPoints = part.givingPoints;
        transform.GetChild(0).GetComponent<TMP_Text>().text = part.difficulty.ToString();
        GetComponent<Image>().color = part.color;
    }

    public void DeployTab() {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = targetSize;
        schemeList.ChangeDetailPanelData(detailPanelColor, GetComponent<MusicPartContainer>().part);
    }

    public void LeaveTab() {
        RectTransform rt = GetComponent (typeof (RectTransform)) as RectTransform;
        rt.sizeDelta = baseSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        schemeList.CloseAllTabs();
        DeployTab();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
