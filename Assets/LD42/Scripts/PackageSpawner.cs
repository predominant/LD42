using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageSpawner : MonoBehaviour
	{
		private GameManager _gameManager;
		private float _interval;
		private float _splay;

		private void Start()
		{
			this._gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
			this._interval = this._gameManager.LevelSettings.PackageSpawnInterval;
			this._splay = this._gameManager.LevelSettings.PackageSpawnSplay;
			this.StartCoroutine("SpawnPackages");
		}

		private IEnumerator SpawnPackages()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(this._interval + (Random.Range(-this._splay, this._splay)));
				this.SpawnPackage();
			}
		}

		private void SpawnPackage()
		{
			var prefab = this._gameManager.PackagePrefabs[Random.Range(0, this._gameManager.PackagePrefabs.Count)];
			GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation);
		}
	}
}