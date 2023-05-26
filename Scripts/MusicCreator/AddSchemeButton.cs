using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddSchemeButton : MonoBehaviour
{
    private MusicPart part;

    void Start() {
        GetComponent<Button>().onClick.AddListener(AddPart);
    }

    public void SetActualPart(MusicPart _part) {
        part = _part;
    }

    public void AddPart() {
        GameObject.Find("SchemesList").GetComponent<SchemeList>().AddMusicPart(part.difficulty);
    }
}
