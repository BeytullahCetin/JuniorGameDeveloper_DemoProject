using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform startTransform;

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject.GetComponentInParent<Obstacle>();
        if (hit)
        {
            foreach (Collider col in hit.GetColliders)
            {
                if (col.name == other.name)
                {
                    Die();
                    return;
                }
            }
        }

        var rotator = other.gameObject.GetComponentInParent<Rotator>();
        if (rotator)
        {
            Debug.Log(rotator.transform.right);
            Vector3 dir = rotator.transform.right; ;
            if (false == rotator.IsClokwise)
            {
                dir = -dir;
            }
            Debug.Log(rotator.IsClokwise + " original dir: " + rotator.transform.right + "\nafter calculation: " + dir);
            GetComponent<ImpactReceiver>().AddImpact(dir, rotator.PushForce);
        }
    }

    public void Die()
    {
        characterController.enabled = false;
        transform.position = startTransform.position;
        characterController.enabled = true;
    }
}