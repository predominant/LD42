using UnityEngine;

namespace LD42
{
    public static class GameObjectExtensions
    {
        public static bool IsPackage(this GameObject g)
        {
            return g.layer == LayerMask.NameToLayer("Package");
        }
    }
}