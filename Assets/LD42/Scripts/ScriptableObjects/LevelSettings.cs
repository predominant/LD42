﻿using System.Collections.Generic;
using UnityEngine;

namespace LD42.ScriptableObjects
{
	[CreateAssetMenu(menuName = "LD42/LevelSettings")]
	public class LevelSettings : ScriptableObject
	{
		public int LevelTime = 240;
		public float ConveyorSpeed = 1f;
		public float PackageSpawnInterval = 5f;
		public float PackageSpawnSplay = 1f;
		public List<GameObject> PackagePrefabs = new List<GameObject>();
		public List<Color> PackageColors = new List<Color>();
		public float BombProbability = 0.1f;
		public int MaxBombTimer = 60;
		public int MinBombTimer = 45;
		public float ScanFailProbability = 0.2f;
		public float ManualInspectionTime = 3f;
		public float CreatePackageRequestTimeMin = 4f;
		public float CreatePackageRequestTimeMax = 6f;
		public int MaxPackageRequests = 6;
		public int RequestWaitTime = 30;

		[Header("Bonuses")]
		public int BombDisposeBonus = 30;

		[Header("Penalities")]
		public int ExpiredPackagePenalty = -5;
		public int UnwantedPackagePenalty = -2;
		public int NotInspectedPackagePenalty = -10;
		public int BombDeliveryPenalty = -20;
		public int BombExplosionPenalty = -30;

		[Header("Explosions")]
		public GameObject ExplosionPrefab;
		public float ExplosionForce = 900f;
		public float ExplosionRadius = 200f;
		public float ExplosionUpModifier = 2f;

		[Header("Scan Colors")]
		public Color SuccessScanColor = Color.green;
		public Color FailScanColor = Color.red;

		[Header("Sounds")]
		public List<AudioClip> SuccessScanAudio;
		public List<AudioClip> FailScanAudio;
		public AudioClip DropAudio;
		public AudioClip PickupAudio;
	}
}