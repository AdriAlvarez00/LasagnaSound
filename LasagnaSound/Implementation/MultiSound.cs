﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LasagnaSound
{
    [CreateAssetMenu(fileName = "newMultiSound", menuName = "LasagnaSound/MultiSound")]
    [System.Serializable]
    class MultiSound : ScriptableObject
    {
        [SerializeField] public ClipBundle bundle;
        [SerializeField] public FunctionValue volume = new FunctionValue("Volume");
        [SerializeField] public FunctionValue maxSimultaneousPlaying = new FunctionValue("Max simultaneous sounds",0.0f,100.0f,false);
        [SerializeField] public FunctionValue maxSimultaneousRepeat = new FunctionValue("Max sound repeats", 0.0f, 100.0f, false);
        [SerializeField] public FunctionValue minPlayInterval = new FunctionValue("Min play interval", 0.1f, 100.0f, true);
        [SerializeField] public FunctionValue maxPlayInterval = new FunctionValue("Max play interval", 0.1f, 100.0f, true);
        [SerializeField] public float pitchModifier = 0.0f;
        [SerializeField] public FunctionValue pitchModVariance = new FunctionValue("Pitch variance interval", 0.0f, 10f, true);
        [SerializeField] public AnimationCurve intensityCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        [SerializeField, Range(0.0f, 1.0f)] public float inPoint = 0.0f, outPoint = 1.0f;
    }
}
