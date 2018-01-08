using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake() {
		DontDestroyOnLoad (gameObject);
		Debug.Log ("Don't destory on load: " + name);
	}
	
	void Start () {
		audioSource = GetComponent<AudioSource>();
        audioSource.clip = thisLevelMusic;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
	
	
	
	public void SetVolume (float volume) {
		audioSource.volume = volume;
	}
}
