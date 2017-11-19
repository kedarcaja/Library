using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DigitalRuby.SoundManagerNamespace
{
    public class SoundManagerDemo : MonoBehaviour
    {
#region deklarace
        public Slider SoundSlider;
        public InputField SoundCountTextBox;
        public Toggle PersistToggle;
        public AudioSource[] SoundAudioSources;
#endregion        

        private void PlaySound(int index)
        {
            int count;
            if (!int.TryParse(SoundCountTextBox.text, out count))
            {
                count = 1;
            }
            while (count-- > 0)
            {
                SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
            }
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlaySound(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlaySound(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlaySound(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlaySound(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PlaySound(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                PlaySound(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                PlaySound(6);
            }
        }

        public void SoundVolumeChanged()
        {
            SoundManager.SoundVolume = SoundSlider.value;
        }

       
    }
}
