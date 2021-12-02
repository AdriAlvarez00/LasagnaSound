using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LasagnaSound
{
	[CustomEditor(typeof(MultiSound))]
	class MultiSoundEditor : Editor
	{
		void onEnable()
		{

		}

		public override void OnInspectorGUI()
		{
			//base.OnInspectorGUI();

			MultiSound mSound = (MultiSound)target;

            //mSound.SetIntensity(EditorGUILayout.Slider("Intensity", mSound.GetIntensity(), 0.0f, 1.0f));
            //SerializedObject nestedObject = new SerializedObject(mSound.bundle);
            //EditorGUILayout.PropertyField(nestedObject,  "Clip bundles");

            mSound.bundle = (ClipBundle)EditorGUILayout.ObjectField(mSound.bundle, typeof(ClipBundle), false);
            mSound.volume.ShowInEditor();
			mSound.maxSimultaneousPlaying.ShowInEditor();
			mSound.maxSimultaneousRepeat.ShowInEditor();
			mSound.minPlayInterval.ShowInEditor();
			mSound.maxPlayInterval.ShowInEditor();
		}
	}
}
