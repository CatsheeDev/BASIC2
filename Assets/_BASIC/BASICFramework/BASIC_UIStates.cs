#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BASIC.UI.States
{
    public class BASIC_UIStates
    {
        private IUIState currentState;

        public BASIC_UIStates(IUIState initialState)
        {
            currentState = initialState;
        }

        public void SetState(IUIState newState)
        {
            currentState = newState;
        }

        public void RenderCurrentState()
        {
            currentState.RenderUI();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("BASIC v2.2.0 - Something", EditorStyles.centeredGreyMiniLabel); 
        }
    }



    public interface IUIState
    {
        void RenderUI();
    }

}
#endif