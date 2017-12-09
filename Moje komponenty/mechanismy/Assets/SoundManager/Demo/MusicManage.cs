using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace DigitalRuby.SoundManagerNamespace
{
    public class MusicManage : MonoBehaviour
    {
        public Slider MusicSlider;
        public bool PersistToggle;
        public AudioSource[] MusicAudioSources;

        private void PlayMusic(int index)
        {
            MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, PersistToggle=true);
        }

      

        private void Update()
        {
           

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                PlayMusic(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                PlayMusic(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                PlayMusic(2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                PlayMusic(3);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (PersistToggle=false)
                {
                    SoundManager.StopAll();
                }
            }
        }

        public void MusicVolumeChanged()
        {
            SoundManager.MusicVolume = MusicSlider.value;
        }
    }
}