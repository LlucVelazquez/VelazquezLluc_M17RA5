using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null) cc.enabled = false;

            other.transform.position = destination.position;

            other.transform.rotation = destination.rotation;

            if (cc != null) cc.enabled = true;

            Debug.Log("Teletransportado a: " + destination.name);
        }
    }
}
