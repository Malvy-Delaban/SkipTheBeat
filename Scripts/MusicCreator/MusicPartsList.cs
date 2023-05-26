using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSong {
    public List<MusicPart> parts;
    public string name;
    public string style;
    public int id;
    public int neededIdToPlay;
}

public class MusicPartsList : MonoBehaviour {
    [SerializeField] public LevelSong[] levelSongs;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}