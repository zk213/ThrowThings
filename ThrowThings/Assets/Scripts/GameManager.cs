using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private GameObject winScreen;

    private float winTime = 0f;
    private float startTime = 0f;
    private bool won;
    private Texture2D blackImage;

    private void OnEnable()
    {
        instance = this;
    }

    private void Awake()
    {
        startTime = -0.5f;
        blackImage = new Texture2D(1, 1);
        blackImage.SetPixel(0, 0, Color.black);
        blackImage.Apply();
    }

    private void Update()
    {
        if (won)
        {
            winTime += Time.unscaledDeltaTime;
            if (winTime >= 5f)
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1f;
            }
        }

        startTime += Time.deltaTime;
    }

    private void OnGUI()
    {
        float fadeTime = 1.5f;
        if (winTime >= 5f - fadeTime)
        {
            float t = 1f - ((5f - winTime) / fadeTime);
            GUI.color = new Color(1f, 1f, 1f, Mathf.Clamp01(t * 5f));
            GUI.DrawTexture(new Rect(-10f, -10f, Screen.width + 10f, Screen.height + 10f), blackImage, ScaleMode.StretchToFill);
        }
        else if (startTime < fadeTime)
        {
            float t = 1f - (startTime / fadeTime);
            GUI.color = new Color(1f, 1f, 1f, Mathf.Clamp01(t * 2f));
            GUI.DrawTexture(new Rect(-10f, -10f, Screen.width + 10f, Screen.height + 10f), blackImage, ScaleMode.StretchToFill);
        }
    }

    public static void TeamWin(string team)
    {
        Time.timeScale = 0.5f;
        if (instance.winScreen)
        {
            GameObject winScreen = Instantiate(instance.winScreen);
        }

        instance.won = true;
    }
}