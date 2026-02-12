using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject player;              
    public PlayerInventory playerInventory; 

    private void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PosZ", player.transform.position.z);

        PlayerPrefs.SetFloat("RotY", player.transform.eulerAngles.y);

        int hasObjectState = playerInventory.hasObject ? 1 : 0;
        PlayerPrefs.SetInt("HasObject", hasObjectState);

        PlayerPrefs.Save();
        Debug.Log("Partida Guardada Correctamente");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PosX"))
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false; 

            float x = PlayerPrefs.GetFloat("PosX");
            float y = PlayerPrefs.GetFloat("PosY");
            float z = PlayerPrefs.GetFloat("PosZ");
            player.transform.position = new Vector3(x, y, z);

            float rotY = PlayerPrefs.GetFloat("RotY");
            Vector3 currentRot = player.transform.rotation.eulerAngles;
            player.transform.rotation = Quaternion.Euler(currentRot.x, rotY, currentRot.z);

            if (controller != null) controller.enabled = true; 

            int hasObjectState = PlayerPrefs.GetInt("HasObject");
            if (hasObjectState == 1)
            {
                playerInventory.PickUpObject();
            }

            Debug.Log("Partida Cargada");
        }
    }

    [ContextMenu("Borrar Datos Guardados")]
    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Datos borrados");
    }
}
