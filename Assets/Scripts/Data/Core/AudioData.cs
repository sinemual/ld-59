using System.Collections.Generic;
using Data.Base;
using TriInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Client.Data
{
    [CreateAssetMenu(menuName = "GameData/AudioData", fileName = "AudioData")]
    public class AudioData : BaseDataSO
    {
        public AudioMixer musicMixer;
        public AudioMixer effectsMixer;
        
        [ListDrawerSettings(ShowElementLabels = true)]
        public List<Sound> sounds;
        [ListDrawerSettings(ShowElementLabels = true)]
        public List<Sound> soundtrackSounds;
        [ListDrawerSettings(ShowElementLabels = true)]
        public List<Sound> uiSounds;
        [ListDrawerSettings(ShowElementLabels = true)]
        public List<Sound> shootingLevelSounds;
        [ListDrawerSettings(ShowElementLabels = true)]
        public List<Sound> battleSounds;
       
        public override void ResetData()
        {
        }
    }
}