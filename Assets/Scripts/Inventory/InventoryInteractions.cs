using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryInteractions : MonoBehaviour
{
    private Inventory inventory;

    [Header("drag and drop")]
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private Transform dragingParent;
    private Transform dragingObjectTransform;
    private Transform dragingObjectParentTransform;
    private bool isDraging;
    private PointerEventData pointerEventData;

    [Header("Inputs"), HideInInspector]
    private InputSystem_Actions inputs;
    private InputAction mouseClick;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
        mouseClick = inputs.UI.Click;

        inventory = GetComponent<Inventory>();
        mouseClick.performed += OnClick;
    }
    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        if (!isDraging) { return; }

        Vector2 mousePos = Input.mousePosition;
        dragingObjectTransform.position = mousePos;
        if (!Input.GetMouseButtonUp(0)) { return; }

        isDraging = false;
        dragingObjectTransform.SetParent(dragingObjectParentTransform);
        dragingObjectTransform.position = dragingObjectParentTransform.position;

        List<RaycastResult> results = new List<RaycastResult>();
        RaycastUI(results);

        if (results.Count > 0)
        {
            ChangeItemBetweenSlots(results);
            return;
        }

        InteractWithItem();
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        if (isDraging) { return; }

        List<RaycastResult> results = new List<RaycastResult>();
        RaycastUI(results);

        if (!(results.Count > 0 && IsSlot(results[0].gameObject) && SoltHasItem(results[0].gameObject))) { return; }
        StartDraging(results[0].gameObject);
    }

    private void StartDraging(GameObject gameObject)
    {
        dragingObjectTransform = gameObject.transform;
        dragingObjectParentTransform = dragingObjectTransform.parent;
        dragingObjectTransform.SetParent(dragingParent);
        isDraging = true;
    }

    private bool IsSlot(GameObject gameObject)
    {
        return gameObject != null && gameObject.CompareTag("Slot");
    }

    private bool SoltHasItem(GameObject gameObject)
    {
        return gameObject.GetComponentInParent<ItemSlot>().HasItem();
    }

    private void RaycastUI(List<RaycastResult> results)
    {
        Vector2 mousePos = Input.mousePosition;
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = mousePos;
        raycaster.Raycast(pointerEventData, results);
    }

    private void ChangeItemBetweenSlots(List<RaycastResult> results)
    {

        if (!(results.Count > 0 && results[0].gameObject != null && results[0].gameObject.CompareTag("Slot"))) { return; }

        ItemSlot firstContainer = dragingObjectTransform.parent.GetComponent<ItemSlot>();
        ItemSlot secondContainer = results[0].gameObject.transform.parent.GetComponent<ItemSlot>();

        if (!(secondContainer.HasItem() && secondContainer.GetItem().itemName != firstContainer.GetItem().itemName))
        {
            secondContainer.AddItem(firstContainer.GetItem(), firstContainer.Getnumber());
            firstContainer.ResetItem();
            return;
        }

        Item savedItem = secondContainer.GetItem();
        int savedNumberItem = secondContainer.Getnumber();
        secondContainer.AddItem(firstContainer.GetItem(), firstContainer.Getnumber());
        firstContainer.AddItem(savedItem, savedNumberItem);
    }

    private Vector2 GetMouseWolrdPosition()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void InteractWithItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetMouseWolrdPosition(), Vector2.zero);

        if (!hit) { return; }

        ItemSlot itemSlot = dragingObjectTransform.parent.GetComponent<ItemSlot>();
        if (hit.transform.GetComponent<Interactions>().InteractWithItem(itemSlot.GetItem()))
        {
            itemSlot.ResetItem();
            return;
        }
        // faire un truck peut être ?
    }
}
