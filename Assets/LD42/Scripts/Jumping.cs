using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class Jumping : MonoBehaviour
	{
		private float _offset;

		private void Start()
		{
			this._offset = Random.Range(0f, Mathf.PI * 2f);
			this.StartCoroutine("Jump");
		}

		private IEnumerator Jump()
		{
			var pos = this.transform.position;

			while (true)
			{
				yield return new WaitForEndOfFrame();
				var y = Mathf.Sin((Time.time + this._offset) * 5f);
				y = (y + 1f) / 2f;

				this.transform.position = new Vector3(
					pos.x,
					y,
					pos.z);
			}
		}
	}
}