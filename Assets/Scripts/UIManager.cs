using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject daysSurvived;
    public GameObject zombiesLeftG;

    public Image fadePanel;

    public GameObject crosshair;

    public TextMeshProUGUI daysSurvivedText;
    public TextMeshProUGUI zombiesLeft;
    public TextMeshProUGUI foodLeft;

    public GameObject foodLeftG;
    public GameObject canvas;

    private int dayNum;

    public GameObject spawner;

    public GameObject shop;
    public Camera mainCamera;

    public void Start()
    {
        daysSurvivedText = daysSurvived.GetComponentInChildren<TextMeshProUGUI>();
        zombiesLeft = zombiesLeftG.GetComponentInChildren<TextMeshProUGUI>();
        foodLeft = foodLeftG.GetComponentInChildren<TextMeshProUGUI>();
        spawner = GameObject.FindGameObjectWithTag("SpawnManager");
        spawner.SetActive(false);
    }

    public void ShopEnable()
    {
        StartCoroutine(Fade(Color.black, Color.clear, 1));
        daysSurvived.SetActive(false);
        foodLeftG.SetActive(true);
        shop.SetActive(true);
        crosshair.SetActive(false);
        mainCamera.enabled = false;
    }

    public void ShopDisable()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        daysSurvived.SetActive(true);
        crosshair.SetActive(true);
        foodLeftG.SetActive(false);
        shop.SetActive(false);
        mainCamera.enabled = true;
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime * speed;

            fadePanel.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateText()
    {
        daysSurvivedText.SetText("DAY " + dayNum.ToString());
    }

    public void UpdateZombiesLeft(int left)
    {
        zombiesLeft.SetText(left.ToString());
    }

    public void UpdateFood(int left)
    {
        foodLeft.SetText(left.ToString());
    }

    public void SetDayNumber(int dayNumber)
    {
        dayNum = dayNumber;
        UpdateText();
    }

    public void EnableDaysScreen()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        daysSurvived.SetActive(true);
        zombiesLeftG.SetActive(false);
        foodLeftG.SetActive(false);
        Cursor.visible = true;
        spawner.SetActive(false);
    }

    public void DisableDaysScreen()
    {
        StartCoroutine(Fade(Color.black, Color.clear, 1));
        daysSurvived.SetActive(false);
        zombiesLeftG.SetActive(true);
        foodLeftG.SetActive(true);
        Cursor.visible = false;
        spawner.SetActive(true);
    }
}
