








using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

namespace Pixelplacement
{
    public class Initialization : MonoBehaviour
    {
        
        StateMachine _stateMachine;
        DisplayObject _displayObject;

        
        void Awake()
        {
            
            InitializeSingleton();

            
            _stateMachine = GetComponent<StateMachine>();
            _displayObject = GetComponent<DisplayObject>();

            
            if (_displayObject != null) _displayObject.Register();

            
            if (_stateMachine != null) _stateMachine.Initialize();
        }

        void Start()
        {
            
            if (_stateMachine != null) _stateMachine.StartMachine();
        }

        
        void OnDisable()
        {
            if (_stateMachine != null)
            {
                if (!_stateMachine.returnToDefaultOnDisable || _stateMachine.defaultState == null) return;

                if (_stateMachine.currentState == null)
                {
                    _stateMachine.ChangeState(_stateMachine.defaultState);
                    return;
                }

                if (_stateMachine.currentState != _stateMachine.defaultState)
                {
                    _stateMachine.ChangeState(_stateMachine.defaultState);
                }
            }
        }

        
        void InitializeSingleton()
        {
            foreach (Component item in GetComponents<Component>())
            {
                string baseType;

#if NETFX_CORE
                baseType = item.GetType ().GetTypeInfo ().BaseType.ToString ();
#else
                baseType = item.GetType().BaseType.ToString();
#endif

                if (baseType.Contains("Singleton") && baseType.Contains("Pixelplacement"))
                {
                    MethodInfo m;

#if NETFX_CORE
                    m = item.GetType ().GetTypeInfo ().BaseType.GetMethod ("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);
#else
                    m = item.GetType().BaseType.GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);
#endif

                    m.Invoke(item, new Component[] { item });
                }
            }
        }
    }
}