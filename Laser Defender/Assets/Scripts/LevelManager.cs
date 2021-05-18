using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    static bool SecondStage = false;
    static bool ThirdStage = false;

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
        if(name == "Start Menu")
        {
            Reset();
        }
		Application.LoadLevel(name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void NextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void Check()
    {
        if(ScoreText.score>2000 && !SecondStage)
        {
            SecondStage = true;
            NextLevel();
        }

        if(ScoreText.score>5000 && !ThirdStage)
        {
            ThirdStage = true;
            NextLevel();
        }
    }

    public void Reset()
    {
        ScoreText.score = 0;
        SecondStage = false;
        ThirdStage = false;
    }

}
