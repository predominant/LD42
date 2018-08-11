using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class FaceMainCamera : MonoBehaviour
	{
		private void Update()
		{
			this.transform.LookAt(Camera.main.transform, Vector3.up);
		}
	}
}