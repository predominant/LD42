using UnityEngine;
using UnityEngine.UI;

namespace LD42
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform CarryPoint;

        private bool _carrying = false;

        private GameObject _targetPackage;
        private GameObject TargetPackage
        {
            get { return this._targetPackage; }
            set
            {
                this._targetPackage = value;
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
                this.PackageInteract();
        }

        private void OnTriggerEnter(Collider c)
        {
            if (this._carrying)
                return;
            
            if (c.gameObject.layer == LayerMask.NameToLayer("Package"))
            {
                if (this.TargetPackage == null)
                    this.TargetPackage = c.gameObject;
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if (this._carrying)
                return;

            if (c.gameObject.layer == LayerMask.NameToLayer("Package"))
                this.TargetPackage = null;
        }

        private void PackageInteract()
        {
            if (this._carrying)
                this.Drop();
            else
                this.Pickup();
        }

        private void Drop()
        {
            if (!this._carrying || this.TargetPackage == null)
                return;
            
            this._carrying = false;

            this.TargetPackage.transform.parent = null;
            this.TargetPackage.GetComponent<Rigidbody>().isKinematic = false;
            this.TargetPackage.GetComponent<BoxCollider>().enabled = true;
        }

        private void Pickup()
        {
            if (this._carrying || this.TargetPackage == null)
                return; 

            this._carrying = true;

            this.TargetPackage.GetComponent<Rigidbody>().isKinematic = true;
            this.TargetPackage.GetComponent<BoxCollider>().enabled = false;
            this.TargetPackage.transform.parent = this.CarryPoint;
            this.TargetPackage.transform.localPosition = Vector3.zero;
        }
    }
}