using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private Animator popupAnimator;
    [SerializeField] private TextMeshProUGUI popupText;
    
    [SerializeField] private string customText;
    
    public void DisplayPopUp(string _popUpText)
    {
        if (popupText == null || popupText == null) return;
        
        popupText.text = customText + _popUpText;
        popup.SetActive(true);
        popupAnimator.SetBool("Slide", true);
    }

    public void ClosePopup()
    {
        popupAnimator.SetBool("Slide", false);
    }
    
    public void DeactivatePopup()
    {
        popup.SetActive(false);
    }
}
