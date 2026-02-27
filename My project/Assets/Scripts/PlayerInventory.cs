using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasObject; 
    public GameObject selecteableObject;
    public GameObject inventoryObject;
    public GameObject botaHead;

    void Start()
    {
        if (selecteableObject != null)
        {
            selecteableObject.SetActive(false);
        }
    }
    void Update()
    {
        if (hasObject)
        {
            //inventoryObject.SetActive(true);
            selecteableObject.SetActive(false);
            botaHead.SetActive(false);
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
