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
    private void Awake()
    {
        if (hasObject)
        {
            //inventoryObject.SetActive(true);
            selecteableObject.SetActive(false);
        }
    }

    public void PickUpObject()
    {
        hasObject = true;
        inventoryObject.SetActive(true);
        Debug.Log("¡Objeto recogido!");
        /*
        if (selecteableObject != null)
        {
            selecteableObject.SetActive(true);
        }*/
    }
}
