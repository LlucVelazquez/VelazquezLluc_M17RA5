using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasObject; 
    public GameObject selecteableObject;
    public GameObject inventoryObject;

    void Start()
    {
        if (selecteableObject != null)
        {
            selecteableObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (hasObject)
        {
            inventoryObject.SetActive(true);
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
