using UnityEngine;
using UnityEditor;

namespace LasagnaSound
{
    //We could use inheritance, but this way we can store the previous value
    //if the user wants to switch back
    [System.Serializable]
    public class FunctionValue
    {
        [SerializeField] private bool functionManaged = false;
        [SerializeField] private AnimationCurve curve = AnimationCurve.Constant(0.0f, 1.0f, 1.0f);
        [SerializeField] private float fixedValue = 1.0f;

        string fieldName;
        private float sliderMinimum, sliderMaximum;
        private bool isSlider;

        public FunctionValue(string name, float min=0.0f, float max=1.0f, bool slider = true)
        {
            fieldName = name;
            sliderMinimum = min;
            sliderMaximum = max;
            isSlider = slider;
        }

        public bool isConstant() { return !functionManaged; }

        public float getValue(float intensity)
        {
            return (functionManaged) ? curve.Evaluate(intensity) : fixedValue;
        }

        public void ShowInEditor()
        {
            EditorGUILayout.BeginHorizontal();
            if (functionManaged)
            {
                curve = EditorGUILayout.CurveField(fieldName + " function", curve);
            }
            else
            {
				if (isSlider)
                {
                    fixedValue = EditorGUILayout.Slider(fieldName, fixedValue, sliderMinimum, sliderMaximum);
				}
				else
                {
                    fixedValue = EditorGUILayout.FloatField(fieldName,  fixedValue);
                }
                
            }
            functionManaged = EditorGUILayout.Toggle(functionManaged);
            EditorGUILayout.EndHorizontal();
        }
    }
}
