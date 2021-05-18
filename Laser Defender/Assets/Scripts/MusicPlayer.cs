using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;
    public AudioClip BossClip;
    public AudioClip Complete;

    private AudioSource music;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("MusicPlayer : Loaded Level " + scene.name);
        music = GetComponent<AudioSource>();
        
        if(scene.name == "Start Menu")
        {
            music.Stop();
            music.clip = startClip;
            music.Play();
        }
        if(scene.name == "Level_01")
        {
            music.clip = gameClip;
            music.Play();
        }
        if(scene.name == "Level_03")
        {
            music.Stop();
            music.clip = BossClip;
            music.Play();
        }
        if(scene.name == "Win")
        {
            music.clip = Complete;
            music.Play();
        }
        if(scene.name == "Lose")
        {
            music.clip = endClip;
            music.Play();
        }
        
        Loop_Test(scene.name);
    }

    void Loop_Test(string name)
    {
        if (name == "Win" || name == "Lose")
        {
            music.loop = false;
        }
        else
        {
            music.loop = true;
        }
    }
	
	/*void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
	}*/
}
