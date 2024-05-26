using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	
	private void Awake()
	{
		this.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	
	private void Start()
	{
		this.stamina = this.staminaMax;
		Time.timeScale = 1f;
	}

	
	private void Update()
	{
		this.running = Input.GetButton("Run");
		this.MouseMove();
		this.PlayerMove();
		this.StaminaUpdate();
	}

	
	private void MouseMove()
	{
		Quaternion rotation = base.transform.rotation;
		rotation.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.deltaTime * Time.timeScale, 0f);
		base.transform.rotation = rotation;
	}

	
	private void PlayerMove()
	{
		float d = this.walkSpeed;
		if (this.stamina > 0f & this.running)
		{
			d = this.runSpeed;
		}
		Vector3 a = base.transform.right * Input.GetAxis("Strafe");
		Vector3 b = base.transform.forward * Input.GetAxis("Forward");
		this.sensitivity = Mathf.Clamp((a + b).magnitude, 0f, 1f);
		this.cc.Move((a + b).normalized * d * this.sensitivity * Time.deltaTime);
	}

	
	public void StaminaUpdate()
	{
		if (this.cc.velocity.magnitude > this.cc.minMoveDistance)
		{
			if (this.running)
			{
				this.stamina = Mathf.Max(this.stamina - this.staminaDrop * Time.deltaTime, 0f);
			}
		}
		else if (this.stamina < this.staminaMax)
		{
			this.stamina += this.staminaRise * Time.deltaTime;
		}
	}

	
	public CharacterController cc;

	
	public float walkSpeed;

	
	public float runSpeed;

	
	public float stamina;

	
	public float staminaDrop;

	
	public float staminaRise;

	
	public float staminaMax;

	
	private float sensitivity;

	
	private float mouseSensitivity;

	
	private bool running;
}
