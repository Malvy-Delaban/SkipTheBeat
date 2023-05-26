using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointsCalculation : MonoBehaviour
{
    [SerializeField] private GameObject[] nums;
    [SerializeField] private Sprite[] assets;

    void Start() {
    }

    private void SetAsset(int digitToUse, int placement) {
        nums[placement].GetComponent<Image>().sprite = assets[digitToUse];
    }

    private int GetDecadeDigit(int number) {
        int decade = 0;
        if (number < 0)
            return 0;
        // if (number >= 0 && number <= 9)
        //     return number;
        while (number > 9) {
            number -= 10;
            decade++;
        }
        return decade;
    }

    private int GetDigit(int number) {
        if (number < 0)
            return 0;
        return number % 10;
    }

    public void SetNumber(int number, bool isLeftPart) {
        SetAsset(GetDecadeDigit(number), (isLeftPart ? 0 : 2));
        SetAsset(GetDigit(number), (isLeftPart ? 1 : 3));
    }
}
