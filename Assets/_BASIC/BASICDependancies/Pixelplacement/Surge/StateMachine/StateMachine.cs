









#pragma warning disable 168

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace Pixelplacement
{
    [RequireComponent (typeof (Initialization))]
    public class StateMachine : MonoBehaviour 
    {
        
        public GameObject defaultState;
        public GameObject currentState;
        public bool _unityEventsFolded;

        
        
        
        [Tooltip("Should log messages be thrown during usage?")]
        public bool verbose;

        
        
        
        [Tooltip("Can States within this StateMachine be reentered?")]
        public bool allowReentry = false;

        
        
        
        [Tooltip("Return to default state on disable?")]
        public bool returnToDefaultOnDisable = true;

        
        public GameObjectEvent OnStateExited;
        public GameObjectEvent OnStateEntered;
        public UnityEvent OnFirstStateEntered;
        public UnityEvent OnFirstStateExited;
        public UnityEvent OnLastStateEntered;
        public UnityEvent OnLastStateExited;

        
        
        
        
        public bool CleanSetup 
        { 
            get; 
            private set; 
        }

        
        
        
        public bool AtFirst
        {
            get
            {
                return _atFirst;
            }

            private set
            {
                if (_atFirst)
                {
                    _atFirst = false;
                    if (OnFirstStateExited != null) OnFirstStateExited.Invoke ();
                } else {
                    _atFirst = true;
                    if (OnFirstStateEntered != null) OnFirstStateEntered.Invoke ();
                }
            }
        }

        
        
        
        public bool AtLast
        {
            get
            {
                return _atLast;
            }

            private set
            {
                if (_atLast)
                {
                    _atLast = false;
                    if (OnLastStateExited != null) OnLastStateExited.Invoke ();
                } else {
                    _atLast = true;
                    if (OnLastStateEntered != null) OnLastStateEntered.Invoke ();
                }
            }
        }

        
        bool _initialized;
        bool _atFirst;
        bool _atLast;

        
        
        
        
        public GameObject Next (bool exitIfLast = false)
        {
            if (currentState == null) return ChangeState (0);
            int currentIndex = currentState.transform.GetSiblingIndex();
            if (currentIndex == transform.childCount - 1)
            {
                if (exitIfLast)
                {
                    Exit();
                    return null;
                }
                else
                {
                    return currentState;	
                }
            }else{
                return ChangeState (++currentIndex);
            }
        }

        
        
        
        public GameObject Previous (bool exitIfFirst = false)
        {
            if (currentState == null) return ChangeState(0);
            int currentIndex = currentState.transform.GetSiblingIndex();
            if (currentIndex == 0)
            {
                if (exitIfFirst)
                {
                    Exit();
                    return null;
                }
                else
                {
                    return currentState;
                }
            }
            else{
                return ChangeState(--currentIndex);
            }
        }

        
        
        
        public void Exit ()
        {
            if (currentState == null) return;
            Log ("(-) " + name + " EXITED state: " + currentState.name);
            int currentIndex = currentState.transform.GetSiblingIndex ();

            
            if (currentIndex == 0) AtFirst = false;

            
            if (currentIndex == transform.childCount - 1) AtLast = false;	

            if (OnStateExited != null) OnStateExited.Invoke (currentState);
            currentState.SetActive (false);
            currentState = null;
        }

        
        
        
        public GameObject ChangeState (int childIndex)
        {
            if (childIndex > transform.childCount-1)
            {
                Log("Index is greater than the amount of states in the StateMachine \"" + gameObject.name + "\" please verify the index you are trying to change to.");
                return null;
            }

            return ChangeState(transform.GetChild(childIndex).gameObject);
        }

        
        
        
        public GameObject ChangeState (GameObject state)
        {
            if (currentState != null)
            {
                if (!allowReentry && state == currentState)
                {
                    Log("State change ignored. State machine \"" + name + "\" already in \"" + state.name + "\" state.");
                    return null;
                }
            }

            if (state.transform.parent != transform)
            {
                Log("State \"" + state.name + "\" is not a child of \"" + name + "\" StateMachine state change canceled.");
                return null;
            }

            Exit();
            Enter(state);

            return currentState;
        }

        
        
        
        public GameObject ChangeState (string state)
        {
            Transform found = transform.Find(state);
            if (!found)
            {
                Log("\"" + name + "\" does not contain a state by the name of \"" + state + "\" please verify the name of the state you are trying to reach.");
                return null;
            }

            return ChangeState(found.gameObject);
        }

        
        
        
        public void Initialize()
        {
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        
        
        
        public void StartMachine ()
        {
            
            if (Application.isPlaying && defaultState != null) ChangeState (defaultState.name);
        }

        
        void Enter (GameObject state)
        {
            currentState = state;
            int index = currentState.transform.GetSiblingIndex ();

            
            if (index == 0)
            {
                AtFirst = true;
            }

            
            if (index == transform.childCount - 1)
            {
                AtLast = true;	
            }

            Log( "(+) " + name + " ENTERED state: " + state.name);
            if (OnStateEntered != null) OnStateEntered.Invoke (currentState);
            currentState.SetActive (true);
        }

        void Log (string message)
        {
            if (!verbose) return;
            Debug.Log (message, gameObject);
        }
    }
}