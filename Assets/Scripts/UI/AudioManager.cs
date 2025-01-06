using UnityEngine;
using UnityEngine.Audio;
using EventHandler = Utilities.EventHandler;

namespace UI
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        [Header("Audio Mixer")]
        public AudioMixer audioMixer;
        [Header("audio clip")]
        public AudioClip bgmClips;
        public AudioClip jumpClips;
        public AudioClip longJumpClips;
        public AudioClip deadClips;
        [Header("audio Source")]
        public AudioSource bgmSource;
        public AudioSource fx;
        void Awake()
        {
            if (instance==null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this);
            bgmSource.clip = bgmClips;
            PlayMusic();
        
        }

        private void OnEnable()
        {
            EventHandler.GameOverEvent+= OnGameOverEvent;
        }

        private void OnGameOverEvent()
        {
            fx.clip = deadClips;
            fx.Play();
        }

        private void OnDisable()
        {
            EventHandler.GameOverEvent-= OnGameOverEvent;
        }

        /// <summary>
        /// 跳跃音效
        /// </summary>
        /// <param name="type">0 小跳 1大跳</param>
        public void SetJumpClip(int type)
        {
            switch (type)
            {
                case 0:
                    fx.clip = jumpClips;
                    break;
                case 1:
                    fx.clip = longJumpClips;
                    break;
            }
        }

        public void PlayJumpFX()
        {
            fx.Play();
        }

        public void PlayMusic()
        {
            if (!bgmSource.isPlaying)
            {
                bgmSource.Play();
            }
        }

        public void ToggleAudio(bool isOn)
        {
            if (isOn)
            {
                audioMixer.SetFloat("masterVolume", 0);
            }
            else
            {
                audioMixer.SetFloat("masterVolume", -80);
            }
        }
    }
}
