using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class MusicManager : MonoBehaviour
    {
        [Header("Objects to serialize")]
        [SerializeField] private ScoreData scoreData = default;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource themeAudio = default;
        [SerializeField] private AudioSource[] phaseAudios = new AudioSource[3];
        [SerializeField] private AudioSource transitionAudio = default;
        [SerializeField] private AudioSource additionalMelody = default;

        //Private variables
        private int currentLevelIndex = 0;
        private int nextLevelToTriggerMusic = 1;

        private void Update()
        {
            currentLevelIndex = scoreData.currentWorldLevelCount;

            if (currentLevelIndex == nextLevelToTriggerMusic)
            {
                nextLevelToTriggerMusic++;

                if (currentLevelIndex == 1)
                {
                    transitionAudio.Play();
                    phaseAudios[1].mute = false;
                    phaseAudios[0].mute = true;
                }
                else if (currentLevelIndex == 2)
                {
                    transitionAudio.Play();
                    phaseAudios[2].mute = false;
                    phaseAudios[1].mute = true;
                }
            }
        }
    }
}