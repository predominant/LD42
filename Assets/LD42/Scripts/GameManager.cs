﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LD42.ScriptableObjects;

namespace LD42
{
	public class GameManager : MonoBehaviour
	{
		public LevelSettings LevelSettings;
		public List<GameObject> PackagePrefabs;

		public Color PackageColor()
		{
			return this.LevelSettings.PackageColors[Random.Range(0, this.LevelSettings.PackageColors.Count)];
		}
	}
}