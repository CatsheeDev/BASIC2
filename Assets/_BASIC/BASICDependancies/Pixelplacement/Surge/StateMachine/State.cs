








using UnityEngine;
using System.Collections;

namespace Pixelplacement
{
    public class State : MonoBehaviour 
    {
        
        
        
        
        public bool IsFirst
        {
            get
            {
                return transform.GetSiblingIndex () == 0;
            }
        }

        
        
        
        public bool IsLast
        {
            get
            {
                return transform.GetSiblingIndex () == transform.parent.childCount - 1;
            }
        }

        
        
        
        public StateMachine StateMachine
        {
            get
            {
                if (_stateMachine == null)
                {
                    _stateMachine = transform.parent.GetComponent<StateMachine>();
                    if (_stateMachine == null)
                    {
                        Debug.LogError("States must be the child of a StateMachine to operate.");
                        return null;
                    }
                }

                return _stateMachine;
            }
        }

        
        StateMachine _stateMachine;

        
        
        
        
        public void ChangeState(int childIndex)
        {
            StateMachine.ChangeState(childIndex);
        }

        
        
        
        public void ChangeState (GameObject state)
        {
            StateMachine.ChangeState (state.name);
        }

        
        
        
        public void ChangeState (string state)
        {
            StateMachine.ChangeState (state);
        }

        
        
        
        public GameObject Next (bool exitIfLast = false)
        {
            return StateMachine.Next (exitIfLast);
        }

        
        
        
        public GameObject Previous (bool exitIfFirst = false)
        {
            return StateMachine.Previous (exitIfFirst);
        }

        
        
        
        public void Exit ()
        {
            StateMachine.Exit ();
        }
        
        protected Coroutine StartCoroutineIfActive(IEnumerator coroutine)
        {
            if (gameObject.activeInHierarchy)
            {
                return StartCoroutine(coroutine);
            }
            else
            {
                return null;
            }
        }
    }
}