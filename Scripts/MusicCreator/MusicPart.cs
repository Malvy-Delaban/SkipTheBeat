using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum MusicPartDifficulty {
    Intro,      // 0
    Low,        // 1
    Medium,     // 2
    Hard,       // 3
    VeryHard,   // 4
    Outro,      // 5
    NotDefined  // 6
}

[Serializable]
public class MusicPart
{
    public int givingPoints;
    public MusicPartDifficulty difficulty;
    public Color color;
    public AudioClip songAsset;
    public int partId;
    public GameObject prefabPattern;
    public Sprite preview;

    public MusicPart() {
        givingPoints = 0;
        partId = -1;
        difficulty = MusicPartDifficulty.NotDefined;
        color = new Color32(255, 255, 255, 255);
        songAsset = null;
        prefabPattern = null;
        preview = null;
    }

    public static bool operator ==(MusicPart part1, MusicPart part2)
    {
        if (ReferenceEquals(part1, part2))
            return true;
        if (part1.givingPoints != part2.givingPoints)
            return false;
        if (part1.difficulty != part2.difficulty)
            return false;
        if (part1.color != part2.color)
            return false;
        if (part1.songAsset != part2.songAsset)
            return false;
        if (part1.partId != part2.partId)
            return false;
        return true;
    }
    public static bool operator !=(MusicPart part1, MusicPart part2)
    {
        if (part1.givingPoints != part2.givingPoints)
            return true;
        if (part1.difficulty != part2.difficulty)
            return true;
        if (part1.color != part2.color)
            return true;
        if (part1.songAsset != part2.songAsset)
            return true;
        if (part1.partId != part2.partId)
            return true;
        return false;
    }
}
