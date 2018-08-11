using System.Collections.Generic;
using UnityEngine;

namespace LD42.ScriptableObjects
{
	[CreateAssetMenu(menuName = "LD42/LevelSettings")]
	public class LevelSettings : ScriptableObject
	{
		public float ConveyorSpeed = 1f;
		public float PackageSpawnInterval = 5f;
		public float PackageSpawnSplay = 1f;
		public List<GameObject> PackagePrefabs = new List<GameObject>();
	}
}