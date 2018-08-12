using UnityEngine;

using LD42.ScriptableObjects;

namespace LD42
{
    public class PackageRequest
    {
        private LevelSettings LevelSettings;
        private GameManager GameManager;

        public string Type;
        public int TypeIndex;
        public Color Color;

        public float StartTime;

        public PackageRequest(LevelSettings settings)
        {
            this.LevelSettings = settings;
            this.GameManager = GameObject.Find("Manager").GetComponent<GameManager>();

            // What package type?
            var typeIndex = Random.Range(0, this.LevelSettings.PackagePrefabs.Count);
            this.Type = this.LevelSettings.PackagePrefabs[typeIndex].name;

            // What package Color?
            var colorIndex = Random.Range(0, this.LevelSettings.PackageColors.Count);
            this.Color = this.LevelSettings.PackageColors[colorIndex];

            this.StartTime = Time.time;
        }

        public int RemainingTime()
        {
            var time = (int)Mathf.Clamp(
                Mathf.CeilToInt(this.StartTime + LevelSettings.RequestWaitTime - Time.time),
                0,
                LevelSettings.RequestWaitTime
            );
            if (time <= 0f)
            {
                GameManager.ExpirePackageRequest(this);
            }
            return time;
        }
    }
}