using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{

	public GameObject CenterOfMassPrefab;
	private GameObject CenterOfMass;

	private void Awake()
	{
		this.CenterOfMass = GameObject.Instantiate(
			this.CenterOfMassPrefab,
			this.transform.position,
			Quaternion.identity);
		this.StartCoroutine("CalculateCenterOfMass");
	}

	private IEnumerator CalculateCenterOfMass()
	{
		while (this.CenterOfMass == null)
		{
			yield return new WaitForEndOfFrame();
		}

		while (true)
		{
			var rbs = this.GetComponentsInChildren<Rigidbody>();
			Vector3 com = Vector3.zero;
			float totalMass = 0f;
			foreach (var r in rbs)
			{
				com += r.worldCenterOfMass * r.mass;
				totalMass += r.mass;
			}
			com /= totalMass;
			this.CenterOfMass.transform.position = com;
			// Calculate center point
			// Move CenterOfMass object
			yield return null;
		}
	}
}
