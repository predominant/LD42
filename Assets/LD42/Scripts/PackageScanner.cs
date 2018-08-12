using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageScanner : MonoBehaviour
	{
		[SerializeField]
		private GameObject Indicator;
		private static GameManager GameManager;
		private List<GameObject> _scanning = new List<GameObject>();

		private void Start()
		{
			GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		}

		public void StartScan(GameObject g)
		{
			this._scanning.Add(g);
		}

		public void StopScan(GameObject g)
		{
			if (!this._scanning.Remove(g))
				return;
			
			// We removed a package that had started a scan.
			this.CompleteScan(g);
		}

		private void CompleteScan(GameObject g)
		{
			var p = g.GetComponent<Package>();
			var result = p.Scan();

			// Color
			var color = result ? GameManager.LevelSettings.SuccessScanColor : GameManager.LevelSettings.FailScanColor;
			this.Indicator.GetComponent<MeshRenderer>().material.color = color;
			this.StartCoroutine("ResetColor");

			// Sound
			var audioClips = result ? GameManager.LevelSettings.SuccessScanAudio : GameManager.LevelSettings.FailScanAudio;
			var audioClip = audioClips[Random.Range(0, audioClips.Count)];
			var source = this.GetComponent<AudioSource>();
			source.pitch = Random.Range(0.95f, 1.05f);
			source.PlayOneShot(audioClip);
		}

		private IEnumerator ResetColor()
		{
			yield return new WaitForSecondsRealtime(1.3f);
			this.Indicator.GetComponent<MeshRenderer>().material.color = Color.white;
		}
	}
}