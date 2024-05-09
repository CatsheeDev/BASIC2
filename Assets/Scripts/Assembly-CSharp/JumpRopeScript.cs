using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class JumpRopeScript : MonoBehaviour
{
	
	private void OnEnable()
	{
		this.jumpDelay = 1f;
		this.ropeHit = true;
		this.jumpStarted = false;
		this.jumps = 0;
		this.jumpCount.text = 0 + "/5";
		this.cs.jumpHeight = 0f;
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_ReadyGo);
	}

	
	private void Update()
	{
		if (this.jumpDelay > 0f) 
		{
			this.jumpDelay -= Time.deltaTime;
		}
		else if (!this.jumpStarted) 
		{
			this.jumpStarted = true; 
			this.ropePosition = 1f; 
			this.rope.SetTrigger("ActivateJumpRope"); 
			this.ropeHit = false;
		}
		if (this.ropePosition > 0f)
		{
			this.ropePosition -= Time.deltaTime;
		}
		else if (!this.ropeHit) 
		{
			this.RopeHit();
		}
	}

	
	private void RopeHit()
	{
		this.ropeHit = true; 
		if (this.cs.jumpHeight <= 0.2f)
		{
			this.Fail(); 
		}
		else
		{
			this.Success(); 
		}
		this.jumpStarted = false;
	}

	
	private void Success()
	{
		this.playtime.audioDevice.Stop(); 
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Numbers[this.jumps]);
		this.jumps++;
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 0.5f;
		if (this.jumps >= 5) 
		{
			this.playtime.audioDevice.Stop(); 
			this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Congrats);
			this.ps.DeactivateJumpRope(); 
		}
	}

	
	private void Fail()
	{
		this.jumps = 0; 
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 2f; 
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Oops);
	}

	
	public TMP_Text jumpCount;

	
	public Animator rope;

	
	public CameraScript cs;

	
	public PlayerScript ps;

	
	public PlaytimeScript playtime;

	
	public GameObject mobileIns;

	
	public int jumps;

	
	public float jumpDelay;

	
	public float ropePosition;

	
	public bool ropeHit;

	
	public bool jumpStarted;
}
