using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMusic : MonoBehaviour {

    #region Variables
    
    public Slider MusicSlider;
    public bool PersistToggle;
    public AudioSource[] MusicAudioSources;
    public AudioSource Music;
    #endregion

    #region Unity Metod

    void Start () {
        Music.GetComponent<AudioSource>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            MusicAudioSources[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Music.Play(); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
           // PlayMusic(2);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
           // PlayMusic(3);
        }
    }

   
    #endregion
}
