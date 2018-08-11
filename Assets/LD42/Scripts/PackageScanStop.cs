using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageScanStop : PackageScanTrigger
	{
		protected override void ProcessPackage(GameObject g)
		{
			this._scanner.StopScan(g);
		}
	}
}