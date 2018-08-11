using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class Trash : MonoBehaviour
	{
		private void OnTriggerStay(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;

			GameObject.Destroy(c.gameObject, 0.5f);
		}
	}
}