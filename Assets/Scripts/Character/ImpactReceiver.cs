using UnityEngine;

public class ImpactReceiver : MonoBehaviour
{
    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    [SerializeField] CharacterController character;
    [SerializeField] float characterPushForce = 2f;

    // Update is called once per frame
    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2F) character.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var impactReceiver = hit.gameObject.GetComponent<ImpactReceiver>();
        if (impactReceiver)
        {
            impactReceiver.AddImpact(transform.forward, characterPushForce);
        }
    }
}