using UnityEngine;

using LD42.ScriptableObjects;

namespace LD42
{
    public class PackageRequest
    {
        private LevelSettings LevelSettings;

        public string Type;
        public int TypeIndex;
        public Color Color;

        public float StartTime;

        public PackageRequest(LevelSettings settings)
        {
            this.LevelSettings = settings;

            // What package type?
            var typeIndex = Random.Range(0, this.LevelSettings.PackagePrefabs.Count);
            this.Type = this.LevelSettings.PackagePrefabs[typeIndex].name;

            // What package Color?
            var colorIndex = Random.Range(0, this.LevelSettings.PackageColors.Count);
            this.Color = this.LevelSettings.PackageColors[colorIndex];

            this.StartTime = Time.time;
        }
    }
}