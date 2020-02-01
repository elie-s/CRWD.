using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public class SfxManager : MonoBehaviour
    {
        [Header("Objects to serialize")]
        [SerializeField] private ScoreData scoreData = default;

        [Header("Audio Sources")]
        [SerializeField] private RandomSource nextPart = default;

        //Private variables
        private int currentLevelIndex = 0;
        private int nextLevelToTriggerSfx = 1;

        private void Update()
        {
            currentLevelIndex = scoreData.currentWorldLevelCount;

            if (currentLevelIndex == nextLevelToTriggerSfx)
            {
                nextLevelToTriggerSfx++;

                nextPart.Play();
            }
        }
    }
}