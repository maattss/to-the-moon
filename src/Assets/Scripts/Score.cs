using UnityEngine;
using TMPro;
using System.Linq;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float time;
    // Start is called before the first frame update
    MoonManager moonManager;

    void Start()
    {
        moonManager = FindObjectOfType<MoonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + moonManager.Score.ToString("0");
    }
}
