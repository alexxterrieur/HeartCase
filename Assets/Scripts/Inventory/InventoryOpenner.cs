using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryOpenner : MonoBehaviour
{
    [SerializeField] private GameObject inventory;

    InputSystem_Actions inputActions;
    InputAction openInventory;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        openInventory = inputActions.Player.OpenInventory;
        openInventory.started += OpenCloseInvetoryByKeyboard;
    }

    public void OpenCloseInventory()
    {
        inventory.SetActive(inventory.activeInHierarchy);
    }

    public void OpenCloseInvetoryByKeyboard(InputAction.CallbackContext ctx)
    {
        OpenCloseInventory();
    }

}
