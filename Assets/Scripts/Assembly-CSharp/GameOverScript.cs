using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverScript : MonoBehaviour
{
	
	private void Start()
	{
		this.image = base.GetComponent<Image>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.delay = 5f;
		this.chance = UnityEngine.Random.Range(1f, 99f);
		if (this.chance < 98f)
		{
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
			this.image.sprite = this.images[num];
		}
		else
		{
			this.image.sprite = this.rare;
		}
	}

	
	private void Update()
	{
		this.delay -= 1f * Time.deltaTime;
		if (this.delay <= 0f)
		{
			if (this.chance < 98f)
			{
				SceneManager.LoadScene("MainMenu");
			}
			else
			{
				this.image.transform.localScale = new Vector3(5f, 5f, 1f);
				this.image.color = Color.red;
				if (!this.audioDevice.isPlaying)
				{
					this.audioDevice.Play();
				}
				if (this.delay <= -5f)
				{
					Application.Quit();
				}
			}
		}
	}

	
	private Image image;

	
	private float delay;

	
	public Sprite[] images = new Sprite[5];

	
	public Sprite rare;

	
	private float chance;

	
	private AudioSource audioDevice;
}
