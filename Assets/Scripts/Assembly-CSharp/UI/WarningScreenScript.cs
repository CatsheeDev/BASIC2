using System;
using System.Collections;
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
			PersistentCamera.Instance?.Transition(TransitionType.Dither, 10);
            Cover.SetActive(true);
            GetComponent<AudioSource>().Stop(); 
            StartCoroutine(WaitForTransition());
		}
	}

    private IEnumerator WaitForTransition()
    {
        yield return null;
        float WaitTime = 2f;
        while (WaitTime > 0f)
        {
            WaitTime -= Time.unscaledDeltaTime;
            yield return null;
        }

        yield return null;
        SceneManager.LoadScene("MainMenu");
        yield break;
    }
}
