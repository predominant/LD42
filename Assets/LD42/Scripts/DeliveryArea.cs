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
                if (r.Color == package.Color)
                {
                    matchedRequest = true;
                    request = r;
                    break;
                }
            }

            if (package.IsBomb)
            {
                GameManager.AdjustScore(GameManager.LevelSettings.BombDeliveryPenalty);
                GameManager.Stats["BombDelivered"]++;
            }
            else
            {
                if (!package.HasBeenInspected)
                {
                    GameManager.AdjustScore(GameManager.LevelSettings.NotInspectedPackagePenalty);
                    GameManager.Stats["NotInspected"]++;
                }
                else
                {
                    if (matchedRequest && request != null)
                    {
                        GameManager.CompletePackageRequest(request);
                        GameManager.Stats["Delivered"]++;
                    }
                    else
                    {
                        GameManager.AdjustScore(GameManager.LevelSettings.UnwantedPackagePenalty);
                        GameManager.Stats["Unwanted"]++;
                    }
                }
            }

            GameObject.Destroy(package.gameObject);
        }
    }
}