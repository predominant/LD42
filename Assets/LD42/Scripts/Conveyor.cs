using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LD42
{
	public class Conveyor : MonoBehaviour
	{
		private GameManager _gameManager;
		private float _speed;

		public void Start()
		{
			this._gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
			this._speed = this._gameManager.LevelSettings.ConveyorSpeed;
		}

		private void OnTriggerStay(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;
				
			var g = c.gameObject;
			g.transform.rotation = Quaternion.identity;
			var pos = g.transform.position;
			// g.transform.position = new Vector3(
			// 	pos.x,
			// 	this.transform.position.y + 1.5f,
			// 	pos.z
			// );

			var rb = g.GetComponent<Rigidbody>();
			if (rb == null)
				return;

			// g.transform.position = Vector3.MoveTowards(
			// 	g.transform.position,
			// 	g.transform.position + this.transform.forward * this.Speed * Time.deltaTime,
			// 	1f
			// );
			rb.MovePosition(rb.position + this.transform.forward * this._speed * Time.fixedDeltaTime);
		}
	}
}