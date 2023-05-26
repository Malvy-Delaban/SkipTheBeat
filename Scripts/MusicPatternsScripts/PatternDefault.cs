using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum posList {
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    CenterCenter,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight,
}

public class PatternDefault : MonoBehaviour
{
    protected GameObject player;
    protected MusicPart musicPart;
    private List<GameObject> positionEmpties = new List<GameObject>();

    void Awake() {
        transform.parent = GameObject.Find("PatternGenerator").transform;
        GetEmptyPosition();
        player = GameObject.Find("Player");
        musicPart = transform.parent.GetComponent<PatternHandler>().GetCurrentPart();
    }

    private void GetEmptyPosition() {
        foreach (string i in System.Enum.GetNames(typeof(posList)))
            positionEmpties.Add(GameObject.Find(i));
    }

    protected Vector3 GetPosOf(posList pos) {
        return positionEmpties[(int)pos].transform.position;
    }

    protected Vector3 GetMiddlePosOf(posList pos1, posList pos2) {
        return Vector3.Lerp(positionEmpties[(int)pos1].transform.position, positionEmpties[(int)pos2].transform.position, 0.5f);
    }
}
