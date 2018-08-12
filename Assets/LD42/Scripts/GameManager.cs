using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LD42.ScriptableObjects;
using TMPro;

namespace LD42
{
	public class GameManager : MonoBehaviour
	{
		public float StartLevelRequestDelay = 5f;
		public LevelSettings LevelSettings;
		public GameObject PackageRequestListPanel;
		public GameObject PackageRequestPanelPrefab;

		public TextMeshProUGUI ScoreText;
		public TextMeshProUGUI TimerText;

		public List<PackageRequest> PackageRequests = new List<PackageRequest>();

		public Dictionary<string, int> Stats = new Dictionary<string, int>{
			{"Delivered", 0},
			{"Failed", 0},
			{"BombExploded", 0},
			{"BombDelivered", 0},
			{"Unwanted", 0},
			{"NotInspected", 0},
		};

		private int Score = 0;

		private float StartTime;

		private void Start()
		{
			this.StartTime = Time.time;
			this.StartCoroutine("CreatePackageRequests");
			this.StartCoroutine("LevelTimer");
		}

		public Color PackageColor()
		{
			return this.LevelSettings.PackageColors[Random.Range(0, this.LevelSettings.PackageColors.Count)];
		}

		private IEnumerator CreatePackageRequests()
		{
			yield return new WaitForSecondsRealtime(this.StartLevelRequestDelay);
			
			while (true)
			{
				var waitTime = Random.Range(
					this.LevelSettings.CreatePackageRequestTimeMin,
					this.LevelSettings.CreatePackageRequestTimeMax);

				if (this.PackageRequests.Count >= this.LevelSettings.MaxPackageRequests)
				{
					yield return new WaitForSecondsRealtime(waitTime);
					continue;
				}

				this.CreatePackageRequest();

				yield return new WaitForSecondsRealtime(waitTime);
			}
		}

		private void CreatePackageRequest()
		{
			this.PackageRequests.Add(new PackageRequest(this.LevelSettings));
			this.UpdatePackageRequestUI();
		}

		public void CompletePackageRequest(PackageRequest r)
		{
            this.PackageRequests.Remove(r);
			this.UpdatePackageRequestUI();
			this.AdjustScore(r.RemainingTime());
		}

		public void ExpirePackageRequest(PackageRequest r)
		{
			this.PackageRequests.Remove(r);
			this.UpdatePackageRequestUI();
			this.AdjustScore(this.LevelSettings.ExpiredPackagePenalty);
			this.Stats["Failed"]++;
		}

		public void UpdatePackageRequestUI()
		{
			foreach (Transform t in this.PackageRequestListPanel.transform)
				GameObject.Destroy(t.gameObject);
			
			foreach (var r in this.PackageRequests)
			{
				var g = GameObject.Instantiate(this.PackageRequestPanelPrefab);
				g.name = "Request - " + r.Type;
				var rui = g.GetComponent<PackageRequestUI>();
				rui.Setup(r);
				g.transform.SetParent(this.PackageRequestListPanel.transform, false);
			}
		}

		public void AdjustScore(int value)
		{
			this.Score += value;
			this.UpdateScoreUI();
		}

		private void UpdateScoreUI()
		{
			this.ScoreText.text = this.Score.ToString();
		}

		private IEnumerator LevelTimer()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(0.25f);
				var remaining = Mathf.Clamp(
					this.StartTime + (float)this.LevelSettings.LevelTime - Time.time,
					0f,
					(float)this.LevelSettings.LevelTime);
				
				this.TimerText.text = string.Format(
					"{0}:{1:D2}",
					(int)(remaining / 60f),
					(int)(remaining % 60f));
			}
		}
	}
}