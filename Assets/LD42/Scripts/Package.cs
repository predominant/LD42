using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class Package : MonoBehaviour
	{
		public static GameManager GameManager;

		public bool IsHeld = false;

		public bool IsBomb = false;

		public bool HasBeenScanned = false;

		public int BombTimer = 0;
		private float BombTimeStarted = 0f;

		public Color Color;

		[SerializeField]
		private TextMesh TextMesh;

		private void Start()
		{
			if (GameManager == null)
				GameManager = GameObject.Find("Manager").GetComponent<GameManager>();

			this.SetupBomb();
			this.Color = GameManager.PackageColor();
		}

		public void Scan()
		{
			if (this.HasBeenScanned)
				return;

			this.HasBeenScanned = true;

			if (this.DidFailScan())
				return;

			// When a package is scanned, identify its color
			var renderer = this.GetComponentInChildren<MeshRenderer>();
			renderer.material.color = this.Color;

			// When a package is scanned, if its a bomb, enable the timer text
			this.TextMesh.gameObject.SetActive(true);
		}

		public void SetupBomb()
		{
			if (!this.ShouldBeBomb())
				return;

			Debug.Log("IM A BOMB");
			this.IsBomb = true;
			this.BombTimeStarted = Time.time;
			this.BombTimer = Random.Range(
				GameManager.LevelSettings.MinBombTimer,
				GameManager.LevelSettings.MaxBombTimer);
			this.StartCoroutine("BombCountdown");
		}

		private bool ShouldBeBomb()
		{
			return Random.Range(0f, 1f) < GameManager.LevelSettings.BombProbability;
		}

		private IEnumerator BombCountdown()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(0.1f);

				this.UpdateTimerText();

				if (Time.time > this.BombTimeStarted + this.BombTimer)
					break;
			}
			this.Explode();
		}

		private void UpdateTimerText()
		{
			var time = Mathf.Clamp(
				this.BombTimeStarted + this.BombTimer - Time.time,
				0f,
				this.BombTimer);
			
			this.TextMesh.text = time.ToString("0.00");
		}

		private void Explode()
		{
			GameObject.Destroy(this.gameObject, 0.5f);
			// var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			// g.transform.position = this.transform.position;
			// g.transform.localScale = Vector3.one * 5f;

			var packages = GameObject.FindObjectsOfType<Package>();
			foreach (var p in packages)
			{
				if (p == this)
					continue;
				
				Debug.Log("Applying explosion");
				p.GetComponent<Rigidbody>().AddExplosionForce(800f, this.transform.position, 200f);
			}

		}

		private bool DidFailScan()
		{
			return Random.Range(0f, 1f) < GameManager.LevelSettings.ScanFailProbability;
		}
	}
}