using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour
{
    Checkpoints checkpointParent;

    // Start is called before the first frame update
    void Start()
    {
        checkpointParent = GetComponentInParent<Checkpoints>();
        if (checkpointParent == null)
        {
            Debug.LogWarning("Parent Checkpoint does not have script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        checkpointParent.ChangeCheckpoint(this.transform, other.gameObject.transform.root);
    }
}
