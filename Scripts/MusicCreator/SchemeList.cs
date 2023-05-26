using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SchemeList : MonoBehaviour
{
    private int levelIndex;
    private List<MusicPart> availableParts;
    private List<MusicPartDifficulty> availableDifficulties;
    [SerializeField] private GameObject detailPanelReference;
    [SerializeField] private MusicPartsList musicPartList;
    [SerializeField] private MusicCreator musicCreator;
    [SerializeField] private GameObject prefabButton;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject smallCube;
    [SerializeField] private GameObject bigCube;
    [SerializeField] private GameObject dialogBox;
    private bool hasFirstBeenDeployed = false;

    void Start() {
        levelIndex = 0;

        GetAvailableParts(levelIndex);
    }

    void Update() {
    }

    private void GetAvailableParts(int levelIndex) {
        availableParts = new List<MusicPart>();
        LevelSong levelSong = musicPartList.levelSongs[levelIndex];

        foreach (MusicPart part in levelSong.parts)
            if (part.difficulty != MusicPartDifficulty.Intro && part.difficulty != MusicPartDifficulty.Outro && part.difficulty != MusicPartDifficulty.NotDefined)
                availableParts.Add(part);
        UpateDifficultyList();
        UpdateButtonsInSchemeList();
    }

    private void UpateDifficultyList() {
        availableDifficulties = new List<MusicPartDifficulty>();

        foreach (MusicPart part in availableParts)
            if (!availableDifficulties.Exists(e => e == part.difficulty))
                availableDifficulties.Add(part.difficulty);
    }

    private void UpdateButtonsInSchemeList() {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        foreach (MusicPartDifficulty difficulty in availableDifficulties) {
            var newButton = Instantiate(prefabButton, transform);
            newButton.GetComponent<MusicPartContainer>().SetValues(availableParts.Find(part => part.difficulty == difficulty));
            newButton.GetComponent<ScaleOnHoverScheme>().GetPartData();
            newButton.GetComponent<ScaleOnHoverScheme>().schemeList = this;
            if (!hasFirstBeenDeployed) {
                newButton.GetComponent<ScaleOnHoverScheme>().DeployTab();
                hasFirstBeenDeployed = true;
            }
        }
        if (availableDifficulties.Count == 0)
            detailPanelReference.SetActive(false);
        if (availableDifficulties.Count > 0)
            detailPanelReference.SetActive(true);
    }

    private void changeParticlesColor(Color color) {
        var col = particleSystem.colorOverLifetime;
        float amount = 0.3f;

        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(new Color(color.r - amount, color.g - amount, color.b - amount), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.7f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        col.color = grad;
    }

    private void changeCubeColor(Color color) {
        Color changedAlpha = color;
        changedAlpha.a = 0.05f;
        smallCube.GetComponent<Image>().color = changedAlpha;
        bigCube.GetComponent<Image>().color = changedAlpha;
    }

    public void ChangeDetailPanelData(Color color, MusicPart part) {
        changeParticlesColor(color);
        changeCubeColor(color);
        detailPanelReference.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = part.preview;
        detailPanelReference.GetComponent<Image>().color = color;
        int givenPoints = part.givingPoints;
        int remaining = 0;
        foreach (MusicPart e in availableParts)
            if (e.difficulty == part.difficulty)
                remaining++;

        string txtPoints = givenPoints + " Point" + (givenPoints > 1 ? "s" : "");
        string txtRemaining = "Remaining : " + remaining;

        detailPanelReference.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = txtPoints;
        detailPanelReference.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = txtRemaining;

        float H, S, V;
        Color.RGBToHSV(color, out H, out S, out V);
        V *= 0.7f;
        Color colorAddButton = Color.HSVToRGB(H, S, V);
        Transform AddButton = detailPanelReference.transform.GetChild(0).GetChild(1).GetChild(0);
        AddButton.GetComponent<Image>().color = colorAddButton;
        AddButton.GetComponent<AddSchemeButton>().SetActualPart(part);
    }

    public void AddMusicPart(MusicPartDifficulty type) {
        if (musicCreator.IsMusicFull()) {
            dialogBox.SetActive(true);
            return;
        }
        int index = availableParts.FindIndex(part => part.difficulty == type);
        MusicPartContainer tempContainer = new MusicPartContainer();

        tempContainer.SetValues(availableParts[index]);
        if (index < 0)
            return;
        musicCreator.AddPart(tempContainer);
        availableParts.RemoveAt(index);
        availableDifficulties.Clear();
        UpateDifficultyList();
        if (!availableDifficulties.Exists(e => e == type))
            hasFirstBeenDeployed = false;
        UpdateButtonsInSchemeList();
    }

    public void RemoveMusicPart(int partId) {
        List<MusicPart> levelSongParts = musicPartList.levelSongs[levelIndex].parts;

        availableParts.Add(levelSongParts.Find(e => e.partId == partId));
        musicCreator.RemovePart(partId);
        availableDifficulties.Clear();
        UpateDifficultyList();
        UpdateButtonsInSchemeList();
    }

    public void CloseAllTabs() {
        foreach (Transform child in transform)
            child.GetComponent<ScaleOnHoverScheme>().LeaveTab();
    }
}
