using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour {

    private Text myText;
    
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myText.text = ScoreText.score.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
