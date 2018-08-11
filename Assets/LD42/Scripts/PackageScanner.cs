using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageScanner : MonoBehaviour
	{
		private List<GameObject> _scanning = new List<GameObject>();

		public void StartScan(GameObject g)
		{
			this._scanning.Add(g);
		}

		public void StopScan(GameObject g)
		{
			if (!this._scanning.Remove(g))
				return;
			
			// We removed a package that had started a scan.
			this.CompleteScan(g);
		}

		private void CompleteScan(GameObject g)
		{
			var p = g.GetComponent<Package>();
			p.Scan();
		}
	}
}