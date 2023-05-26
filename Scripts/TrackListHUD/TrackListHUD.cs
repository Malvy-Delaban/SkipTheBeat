using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrackListHUD : MonoBehaviour
{
    [SerializeField] private GameObject prefabTrackPart;
    public List<MusicPart> parts;

    void Start() {
        MusicCreator musicCreator = GameObject.Find("MusicCreator").GetComponent<MusicCreator>();
        parts = musicCreator.parts;
        GenerateTrackParts();
    }

    private void GenerateTrackParts() {
        foreach (MusicPart part in parts) {
            GameObject newPart = Instantiate(prefabTrackPart, Vector3.zero, Quaternion.identity);

            Vector3 currentScale = newPart.transform.localScale;
            newPart.transform.localScale = new Vector3(currentScale.x * part.songAsset.length * (1f / 6f), currentScale.y, currentScale.z);

            newPart.transform.SetParent(transform, false);
            newPart.GetComponent<Image>().color = part.color;
        }
    }
}
