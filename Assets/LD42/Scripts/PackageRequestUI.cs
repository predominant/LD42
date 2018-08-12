using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LD42
{
	public class PackageRequestUI : MonoBehaviour
	{
		private static GameManager GameManager;
		private int _waitTime;

		public TextMeshProUGUI CountdownText;
		public PackageRequest Request;
		public Image RequestColorImage;

		private void Start()
		{
			GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
			this._waitTime = GameManager.LevelSettings.RequestWaitTime;
		}

		public void Setup(PackageRequest r)
		{
			this.Request = r;
			var c = r.Color;
			c.a = 255;
			this.RequestColorImage.color = c;
			this.StartCoroutine("Countdown");
		}

		private IEnumerator Countdown()
		{
			while (true)
			{
				var remaining = Mathf.Clamp(
					Mathf.FloorToInt(this._waitTime + this.Request.StartTime - Time.time),
					0,
					this._waitTime);
				this.CountdownText.text = remaining.ToString();
				yield return new WaitForSecondsRealtime(0.25f);
			}
		}
	}
}