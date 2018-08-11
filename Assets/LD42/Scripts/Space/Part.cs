using UnityEngine;

public class Part : MonoBehaviour
{
    private void OnJointBreak(float force)
    {
        Debug.Log("Joint Broken: Force = " + force);
    }
}