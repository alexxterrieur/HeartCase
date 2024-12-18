using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryOpenner : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    
    public void OpenCloseInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
        AudioManager.Instance.PlaySFX("Inventory");
    }
}
