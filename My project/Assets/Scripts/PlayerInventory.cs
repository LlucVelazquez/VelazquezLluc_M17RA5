using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasObject = false; 
    public GameObject selecteableObject; 

    void Start()
    {
        if (selecteableObject != null)
        {
            selecteableObject.SetActive(false);
        }
    }

    public void PickUpObject()
    {
        hasObject = true;
        Debug.Log("¡Objeto recogido!");

        if (selecteableObject != null)
        {
            selecteableObject.SetActive(true);
        }
    }
}
