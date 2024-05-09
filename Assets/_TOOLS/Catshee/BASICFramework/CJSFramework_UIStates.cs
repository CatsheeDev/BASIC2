#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CJS.UI.States
{
    public class CJSFramework_UIStates
    {
        private CJS.UI.States.UIState currentState;

        public CJSFramework_UIStates(CJS.UI.States.UIState initialState)
        {
            currentState = initialState;
        }

        public void SetState(CJS.UI.States.UIState newState)
        {
            currentState = newState;
        }

        public void RenderCurrentState()
        {
            currentState.RenderNewUI();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Catshees Map Tools", EditorStyles.centeredGreyMiniLabel); 
        }
    }



    public interface UIState
    {
        void RenderNewUI();
    }

}
#endif