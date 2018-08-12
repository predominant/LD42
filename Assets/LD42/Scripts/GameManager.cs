using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LD42.ScriptableObjects;

namespace LD42
{
	public class GameManager : MonoBehaviour
	{
		public float StartLevelRequestDelay = 5f;
		public LevelSettings LevelSettings;
		public GameObject PackageRequestListPanel;
		public GameObject PackageRequestPanelPrefab;

		public List<PackageRequest> PackageRequests = new List<PackageRequest>();

		private void Start()
		{
			this.StartCoroutine("CreatePackageRequests");
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
	}
}