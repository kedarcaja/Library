using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer      instance        = null;
    public AudioClip        StartClip;
    public AudioClip        GameClip;
    public AudioClip        EndClip;
    private AudioSource     music;

    
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip =        StartClip;
            music.loop =        true;
            music.Play();
		}	
	}
    void OnLevelWasLoaded(int level) {
        Debug.Log("Music player: loading Level");
        music.Stop();
        if (level == 0)             {music.clip = StartClip;}
        if (level == 1)             {music.clip = GameClip;}
        if (level == 2)             { music.clip = EndClip; }
        music.Play();
        music.loop = true;
    }
}
