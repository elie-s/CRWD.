using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class MusicManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource[] phaseAudios = new AudioSource[3];
        [SerializeField] private AudioSource transitionAudio = default;

        //Private
        private int audioIndex = 0;

        public void ChangeLevelTheme()
        {
            transitionAudio.Play();
            phaseAudios[audioIndex + 1].mute = false;
            phaseAudios[audioIndex].mute = true;
            audioIndex++;
        }
    }
}