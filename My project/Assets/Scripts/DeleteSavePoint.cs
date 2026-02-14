using UnityEngine;

public class DeleteSavePoint : MonoBehaviour
{
    private SaveSystem saveSystem;
    private void Start()
    {
        saveSystem = FindFirstObjectByType<SaveSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (saveSystem != null)
            {
                saveSystem.DeleteSaveData();
                Debug.Log("Has tocado el punto de borrado via Shader/Partículas.");
            }
        }
    }
}
