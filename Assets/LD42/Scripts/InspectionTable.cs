using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

namespace LD42
{
	public class InspectionTable : MonoBehaviour
	{
		private static GameManager GameManager;
		private Package _package;

		private void Start()
		{
			GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		}

		private void OnTriggerStay(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;
			
			this._package = c.gameObject.GetComponent<Package>();

			// TODO: Make this a player action
			this.StartInspection();
		}

		private void OnTriggerExit(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;

			this._package = null;
		}

		public void StartInspection()
		{
			// 2. Start a countdown / progress bar
			this.StartCoroutine("RunInspection");
		}

		private IEnumerator RunInspection()
		{
			var objs = GameObject.FindObjectsOfType(typeof(ThirdPersonUserControl));
			foreach (ThirdPersonUserControl o in objs)
				o.enabled = false;

			var startTime = Time.time;

			// Enable Progress bar

			while (Time.time < startTime + GameManager.LevelSettings.ManualInspectionTime)
			{
				yield return new WaitForEndOfFrame();
				//this.UpdateProgressBar();
			}

			foreach (ThirdPersonUserControl o in objs)
				o.enabled = true;

			// Disable Progress bar
			this._package.Scan(true);
		}
	}
}
