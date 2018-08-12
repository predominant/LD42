using UnityEngine;

namespace LD42
{
    public class DeliveryArea : MonoBehaviour
    {
        private static GameManager GameManager;

        private void Start()
        {
            GameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        }

        private void OnTriggerStay(Collider c)
        {
            if (c.gameObject.layer != LayerMask.NameToLayer("Package"))
                return;
            
            var package = c.gameObject.GetComponent<Package>();

            if (package.Delivered)
                return;
            package.Delivered = true;
            
            // Is this for a valid request?
            var matchedRequest = false;
            PackageRequest request = null;
            foreach (var r in GameManager.PackageRequests)
            {
                Debug.LogFormat("Check Package [{0}=={1}]", r.Color, package.Color);
                if (r.Color == package.Color)
                {
                    matchedRequest = true;
                    request = r;
                    break;
                }
            }

            if (matchedRequest && request != null)
            {
                GameManager.CompletePackageRequest(request);
                Debug.Log("Score++");
            }
            else
            {
                Debug.Log("Score--");
            }

            GameObject.Destroy(package.gameObject);
        }
    }
}