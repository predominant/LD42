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

		private ThirdPersonUserControl DisabledController;

		[SerializeField]
		private TextMesh CounterText;

		private bool _inspectionRunning = false;

		private void Start()
		{
			GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		}

		private void OnTriggerStay(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;

			if (this._inspectionRunning)
				return;
			
			var package = c.gameObject.GetComponent<Package>();
			if (package.HasBeenInspected)
				return;

			this._package = package;

			// TODO: Make this a player action
			this.StartInspection();
		}

		private void OnTriggerExit(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;

			this._package = null;

			this.StopCoroutine("RunInspection");
			this._inspectionRunning = false;
			this.CounterText.gameObject.SetActive(false);
		}

		public void StartInspection()
		{
			this._inspectionRunning = true;
			this.CounterText.gameObject.SetActive(true);
			this.StartCoroutine("RunInspection");
		}

		private IEnumerator RunInspection()
		{
			var startTime = Time.time;

			// Enable Progress bar

			while (Time.time < (startTime + GameManager.LevelSettings.ManualInspectionTime))
			{
				var floatTime = startTime + GameManager.LevelSettings.ManualInspectionTime - Time.time;
				var time = Mathf.CeilToInt(floatTime);
				this.CounterText.text = time.ToString();
				yield return new WaitForEndOfFrame();
			}

			// Disable Progress bar
			this._package.Scan(true);
			this.CounterText.text = "3";
			this.CounterText.gameObject.SetActive(false);
			this._inspectionRunning = false;
		}
	}
}
