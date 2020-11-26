using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Animation")]
    public Image fadePanel;
    public GameObject canvas;

    [Header("To Update")]
    public TextMeshProUGUI daysSurvivedText;
    public TextMeshProUGUI zombiesLeft;
    private int dayNum;

    [Header("Enable and Disable")]
    public GameObject spawner;
    public Camera mainCamera;
    public GameObject crosshair;

    [Header("UI Catagory")]
    public GameObject daysSurvived;
    public GameObject zombiesLeftG;
    public GameObject shop;
    public GameObject shopBack;

    public void Start()
    {
        daysSurvivedText = daysSurvived.GetComponentInChildren<TextMeshProUGUI>();
        zombiesLeft = zombiesLeftG.GetComponentInChildren<TextMeshProUGUI>();
        spawner = GameObject.FindGameObjectWithTag("SpawnManager");
        spawner.SetActive(false);
    }

    public void ShopEnable()
    {
        StartCoroutine(Fade(Color.black, Color.clear, 1));
        daysSurvived.SetActive(false);
        shop.SetActive(true);
        crosshair.SetActive(false);
        mainCamera.enabled = false;
        shopBack.SetActive(true);
    }

    public void ShopDisable()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 0.0001f));
        daysSurvived.SetActive(true);
        crosshair.SetActive(true);
        shop.SetActive(false);
        shopBack.SetActive(false);
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
        Cursor.visible = true;
        spawner.SetActive(false);
    }

    public void DisableDaysScreen()
    {
        StartCoroutine(Fade(Color.black, Color.clear, 1));
        daysSurvived.SetActive(false);
        zombiesLeftG.SetActive(true);
        Cursor.visible = false;
        spawner.SetActive(true);
    }
}
