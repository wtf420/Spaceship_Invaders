using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AudioManager: MonoBehaviour
    {
        static AudioManager instance;
        public static AudioManager Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField]
        AudioSource BackGroundMusic;
        [SerializeField]
        AudioSource EffectMusic;

        [SerializeField]
        List<AudioClip> Audios;

        public Slider AudioVolume;

        float volume = 1f;

        private void Awake()
        {
            // this will exist throughout all scenes
            DontDestroyOnLoad(this);

            if (instance == null)
            {
                instance = this;
                PlayMenuAudioBackGround();
            }
            else
                Destroy(gameObject);

        }

        public void GetComponentAudioVolume()
        {
            AudioVolume = GameObject.Find("VolumeSlider").GetComponent<Slider>();
            Debug.Log(AudioVolume); 
            if (AudioVolume != null)
                AudioVolume.value = volume;
        }

        public void SettingVolume(float volume)
        {
            this.volume = volume;
            BackGroundMusic.volume = volume / 2.0f;
            EffectMusic.volume = volume;
        }

        public void PlayMenuAudioBackGround()
        {
            if (Audios.Count > 0 && BackGroundMusic)
            {
                BackGroundMusic.clip = Audios[0];
                if(!BackGroundMusic.isPlaying)
                    BackGroundMusic.Play();
            }
        }


        public void PlayPlayerShooting()
        {
            if (Audios.Count > 1 && EffectMusic)
            {
                EffectMusic.clip = Audios[1];
                EffectMusic.PlayOneShot(Audios[1]);
            }
        }

        public void PlayAsteroidExplosion()
        {
            if (Audios.Count > 2 && EffectMusic)
            {
                EffectMusic.clip = Audios[2];
                EffectMusic.PlayOneShot(Audios[2]);
            }
        }

        public void PlayEnemyExplosion()
        {
            if (Audios.Count > 3 && EffectMusic)
            {
                EffectMusic.clip = Audios[3];
                EffectMusic.PlayOneShot(Audios[3]);
            }
        }

        public void PlayPlayerExplosion()
        {
            if (Audios.Count > 4 && EffectMusic)
            {
                EffectMusic.clip = Audios[4];
                EffectMusic.PlayOneShot(Audios[4]);
            }
        }    

    }
}
