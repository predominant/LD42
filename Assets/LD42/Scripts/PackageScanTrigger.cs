using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42
{
	public class PackageScanTrigger : MonoBehaviour
	{
		protected PackageScanner _scanner;
		
		protected void Start()
		{
			this._scanner = this.transform.parent.GetComponent<PackageScanner>();
		}

		protected void OnTriggerEnter(Collider c)
		{
			if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
				return;

			var p = c.gameObject.GetComponent<Package>();
			if (p == null)
				return;
			
			if (p.IsHeld)
				return;

            this.ProcessPackage(c.gameObject);
		}

        protected virtual void ProcessPackage(GameObject g)
        {
        }
	}
}