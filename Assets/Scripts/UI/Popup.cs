using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private PopupManager manager;
    
    public void ActivateButton()
    {
        closeButton.interactable = true;
    }

    public void DeactivateButton()
    {
        closeButton.interactable = false;
    }

    public void ClosePopup()
    {
        manager.DeactivatePopup();
    }
}
