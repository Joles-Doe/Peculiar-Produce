using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public BridgeScript bridge;

    private void OnTriggerEnter(Collider other)
    {
        bridge.DropBridge();
    }
}
