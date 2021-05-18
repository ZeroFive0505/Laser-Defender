using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private Text text;
    public static int score = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = "Your Score : " + score;
	}
    public static void Reset()
    {
        score = 0;
    }

    public void Score(int points)
    {
        score += points;
        text.text = "Your Score : " + score;
    }
}
