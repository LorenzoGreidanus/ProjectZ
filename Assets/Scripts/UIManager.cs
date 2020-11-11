using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject daysSurvived;
    //public TextMeshPro daysSurvivedText;

    public TextMeshProUGUI daysSurvivedText;

    public GameObject canvas;

    private int dayNum;

    public void Start()
    {
        daysSurvivedText = daysSurvived.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        daysSurvivedText.SetText("DAY " + dayNum.ToString());
    }

    public void SetDayNumber(int dayNumber)
    {
        dayNum = dayNumber;
        UpdateText();
    }

    public void EnableDaysScreen()
    {
        canvas.SetActive(true);
    }

    public void DisableDaysScreen()
    {
        canvas.SetActive(false);
    }
}
