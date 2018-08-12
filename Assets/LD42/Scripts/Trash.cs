using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class Trash : MonoBehaviour
	{
		private static GameManager GameManager;

		private void Start()
		{
			GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		}

		private void OnTriggerStay(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;

			var package = c.gameObject.GetComponent<Package>();

			if (package.Trashed)
				return;

			package.Trashed = true;

			if (package.IsBomb && package.HasBeenInspected)
			{
				GameManager.AdjustScore(GameManager.LevelSettings.BombDisposeBonus);
				GameManager.Stats["BombDisposed"]++;
			}

			GameObject.Destroy(c.gameObject, 0.5f);
		}
	}
}