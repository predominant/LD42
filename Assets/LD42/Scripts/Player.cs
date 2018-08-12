using UnityEngine;
using UnityEngine.UI;

namespace LD42
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform CarryPoint;

        private static GameManager GameManager;

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

        private void Start()
        {
            GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
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
            if (!this._carrying)
                return;
            
            this._carrying = false;

            if (this.TargetPackage == null)
                return;

            this.TargetPackage.transform.parent = null;
            this.TargetPackage.GetComponent<Rigidbody>().isKinematic = false;
            this.TargetPackage.GetComponent<Collider>().enabled = true;

            this.PlaySound(GameManager.LevelSettings.DropAudio);
        }

        private void Pickup()
        {
            if (this._carrying || this.TargetPackage == null || this.TargetPackage.GetComponent<Package>().IsBeingInspected)
                return; 

            this._carrying = true;

            this.TargetPackage.GetComponent<Rigidbody>().isKinematic = true;
            this.TargetPackage.GetComponent<Collider>().enabled = false;
            this.TargetPackage.transform.parent = this.CarryPoint;
            this.TargetPackage.transform.localPosition = Vector3.zero;

            this.PlaySound(GameManager.LevelSettings.PickupAudio);
        }

        private void PlaySound(AudioClip c)
        {
            var source = this.GetComponent<AudioSource>();
            source.PlayOneShot(c);
        }
    }
}