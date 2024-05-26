using System;
using UnityEngine;


public class FirstPrizeSpriteScript : MonoBehaviour
{
	
	private void Start()
	{
		this.sprite = base.GetComponent<SpriteRenderer>();
	}

	
	private void Update()
	{
		this.angleF = Mathf.Atan2(this.cam.position.z - this.body.position.z, this.cam.position.x - this.body.position.x) * 57.29578f;
		if (this.angleF < 0f)
		{
			this.angleF += 360f;
		}
		this.debug = this.body.eulerAngles.y;
		this.angleF += this.body.eulerAngles.y;
		this.angle = Mathf.RoundToInt(this.angleF / 22.5f);
		while (this.angle < 0 || this.angle >= 16)
		{
			this.angle += (int)(-16f * Mathf.Sign((float)this.angle));
		}
		this.sprite.sprite = this.sprites[this.angle];
	}

	
	public float debug;

	
	public int angle;

	
	public float angleF;

	
	private SpriteRenderer sprite;

	
	public Transform cam;

	
	public Transform body;

	
	public Sprite[] sprites = new Sprite[16];
}
