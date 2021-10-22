using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float time;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        time = Time.time*10;
        scoreText.text = "Score: " + time.ToString("0");
    }
}
