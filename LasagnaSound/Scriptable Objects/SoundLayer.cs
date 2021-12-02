using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LasagnaSound
{
    /// <summary>
    /// SoundLayer provides a start and finish point to be controlled by 
    /// a SoundLasagna using curves
    /// </summary>
    [CreateAssetMenu(fileName = "newSoundLayer", menuName = "LasagnaSound/SoundLayer")]
    class SoundLayer : ScriptableObject
    {
        [SerializeField] public MultiSound sound;
        [SerializeField] public AnimationCurve intensityCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        [SerializeField, Range(0.0f,1.0f)] public float inPoint = 0.0f, outPoint = 1.0f;
    }
}
