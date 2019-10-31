using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField]
    private float loadTime = 8f;

    [SerializeField]
    private Image shade;

    [SerializeField]
    private Text text;

    [SerializeField]
    private AnimationCurve textCurve;

    [SerializeField]
    private AnimationCurve shadeCurve;

    private float time = 0f;

    private void Update()
    {
        time += Time.unscaledDeltaTime;
        if (time >= loadTime)
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        float t = time / loadTime;
        shade.color = new Color(0f, 0f, 0f, shadeCurve.Evaluate(t));
        text.color = new Color(text.color.r, text.color.g, text.color.b, textCurve.Evaluate(t));
    }

    public void Initialize(string team)
    {
        text.text = team + " wins!";
    }
}