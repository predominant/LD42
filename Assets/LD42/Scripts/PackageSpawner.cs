using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageSpawner : MonoBehaviour
	{
		private GameManager _gameManager;
		private void Start()
		{
			this._gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
			this.StartCoroutine("SpawnPackages");
		}

		private IEnumerator SpawnPackages()
		{
			var interval = this._gameManager.LevelSettings.PackageSpawnInterval;
			var splay = this._gameManager.LevelSettings.PackageSpawnSplay;

			while (true)
			{
				this.SpawnPackage();
				yield return new WaitForSecondsRealtime(interval + (Random.Range(-splay, splay)));
			}
		}

		private void SpawnPackage()
		{
			var index = Random.Range(0, this._gameManager.LevelSettings.PackagePrefabs.Count);
			var prefab = this._gameManager.LevelSettings.PackagePrefabs[index];
			var g = GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation);
			g.name = prefab.name;
		}
	}
}