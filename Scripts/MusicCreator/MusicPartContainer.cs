using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPartContainer : MonoBehaviour
{
    [SerializeField] public MusicPart part;

    void Start() {
    }

    public void SetValues(MusicPart values) {
        part = new MusicPart();
        part.givingPoints = values.givingPoints;
        part.difficulty = values.difficulty;
        part.color = values.color;
        part.color.a = 1f;
        part.partId = values.partId;
        part.songAsset = values.songAsset;
        part.prefabPattern = values.prefabPattern;
        part.preview = values.preview;
    }
}
