using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageScanStart : PackageScanTrigger
	{
		protected override void ProcessPackage(GameObject g)
		{
			this._scanner.StartScan(g);
		}
	}
}