using System;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
	
	private void Start()
	{
		this.offset = base.transform.position - this.player.transform.position; 
	}

	
	private void Update()
	{
		if (this.ps.jumpRope) 
		{
			this.velocity -= this.gravity * Time.deltaTime; 
			this.jumpHeight += this.velocity * Time.deltaTime; 
			if (this.jumpHeight <= 0f) 
			{
				this.jumpHeight = 0f;
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.velocity = this.initVelocity; 
				}
			}
			this.jumpHeightV3 = new Vector3(0f, this.jumpHeight, 0f); 
		}
		else if (Input.GetButton("Look Behind"))
		{
			this.lookBehind = 180; 
		}
		else
		{
			this.lookBehind = 0; 
		}
	}

	
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset; 
		if (!this.ps.gameOver & !this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset; 
			base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f); 
		}
		else if (this.ps.gameOver)
		{
			base.transform.position = this.baldi.transform.position + this.baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f); 
			base.transform.LookAt(new Vector3(this.baldi.position.x, this.baldi.position.y + 5f, this.baldi.position.z)); 
		}
		else if (this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset + this.jumpHeightV3; 
			base.transform.rotation = this.player.transform.rotation; 
		}
	}

	
	public GameObject player;

	
	public PlayerScript ps;

	
	public Transform baldi;

	
	public float initVelocity;

	
	public float velocity;

	
	public float gravity;

	
	private int lookBehind;

	
	public Vector3 offset;

	
	public float jumpHeight;

	
	public Vector3 jumpHeightV3;

}
