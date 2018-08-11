using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
	private Rigidbody _rigidbody;

	public float Thrust = 250f;

	public bool Activated;

	private bool HasFuel
	{
		get
		{
			return true;
		}
	}

	private void Start()
	{
		this._rigidbody = this.GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (this.Activated && this.HasFuel)
		{
			this._rigidbody.AddForce(
				this.transform.forward * this.Thrust// * 1000f
			);
		}
	}
}
