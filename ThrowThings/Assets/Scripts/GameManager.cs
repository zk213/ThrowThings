using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private Image shade;

    [SerializeField]
    private Text startText;

    [SerializeField]
    private AnimationCurve shadeCurve;

    private bool won;
    private float startTime = 0f;
    private bool starting;
    private Canvas canvas;

    private void OnEnable()
    {
        instance = this;
    }

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        startTime = -0.5f;
    }

    private void Update()
    {
        if (starting)
        {
            startTime += Time.deltaTime;
            Color color = shade.color;
            if (startTime >= 1.5f)
            {
                color.a = Mathf.Lerp(color.a, 0f, Time.smoothDeltaTime * 5f);
                canvas.enabled = false;
            }
            else
            {
                color.a = Mathf.Lerp(color.a, 1f, Time.smoothDeltaTime * 5f);
            }
            shade.color = color;
        }
        else
        {
            float t = startTime;
            shade.color = new Color(0f, 0f, 0f, shadeCurve.Evaluate(t));
            startTime += Time.deltaTime;

            if (startTime >= 3f)
            {
                startText.transform.localScale = Vector3.Lerp(startText.transform.localScale, Vector3.one, Time.smoothDeltaTime * 10f);
                startText.color = Mathf.RoundToInt(Time.time * 3.5f) % 2 == 0 ? Color.gray : Color.white;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    starting = true;
                    startTime = 0f;
                }
            }
            else
            {
                startText.transform.localScale = Vector3.zero;
            }
        }
    }

    public static void TeamWin(string team)
    {
        if (instance.won)
        {
            return;
        }

        Time.timeScale = 0.5f;
        if (instance.winScreen)
        {
            Win winScreen = Instantiate(instance.winScreen).GetComponent<Win>();
            winScreen.Initialize(team);
        }

        instance.won = true;
    }
}