using UnityEngine;

public class SavePoint : MonoBehaviour
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
                saveSystem.SaveGame();
                Debug.Log("Has tocado el punto de guardado via Shader/Partículas.");
            }
        }
    }
}
