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
		public bool HasBeenInspected = false;

		public bool IsBeingInspected = false;

		public int BombTimer = 0;
		private float BombTimeStarted = 0f;

		public bool Delivered = false;

		public bool Trashed = false;

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

		public bool Scan(bool manualScan = false)
		{
			if (this.HasBeenScanned && !manualScan)
				return false;

			this.HasBeenScanned = true;

			if (!manualScan && this.DidFailScan())
				return false;

			this.HasBeenInspected = true;
			this.IsBeingInspected = false;

			// When a package is scanned, identify its color
			var renderer = this.GetComponentInChildren<MeshRenderer>();
			renderer.material.color = this.Color;

			// When a package is scanned, if its a bomb, enable the timer text
			this.TextMesh.gameObject.SetActive(true);
			return true;
		}

		public void SetupBomb()
		{
			if (!this.ShouldBeBomb())
				return;

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

			if (GameManager.LevelSettings.ExplosionPrefab != null)
			{
				var ex = GameObject.Instantiate(
					GameManager.LevelSettings.ExplosionPrefab,
					this.transform.position,
					Quaternion.identity);
				GameObject.Destroy(ex, 2f);
			}

			var packages = GameObject.FindObjectsOfType<Package>();
			foreach (var p in packages)
			{
				if (p == this)
					continue;
				
				p.GetComponent<Rigidbody>().AddExplosionForce(
					GameManager.LevelSettings.ExplosionForce,
					this.transform.position,
					GameManager.LevelSettings.ExplosionRadius,
					GameManager.LevelSettings.ExplosionUpModifier);
			}
			GameManager.AdjustScore(GameManager.LevelSettings.BombExplosionPenalty);
			GameManager.Stats["BombExploded"]++;
		}

		private bool DidFailScan()
		{
			return Random.Range(0f, 1f) < GameManager.LevelSettings.ScanFailProbability;
		}
	}
}