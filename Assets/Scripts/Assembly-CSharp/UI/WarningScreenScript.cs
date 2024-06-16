using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreenScript : MonoBehaviour
{
	[SerializeField] private GameObject Cover;
	private bool debounce; 

	private void Update()
	{
		if (Input.anyKeyDown && !debounce)
		{
			debounce = true;
			PersistentCamera.Instance?.Transition(TransitionType.Dither, 2);
            Cover.SetActive(true);
			Debug.Log("showed cover");
            //SceneManager.LoadScene("MainMenu");
		}
	}
}
