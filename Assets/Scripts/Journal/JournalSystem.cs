using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JournalSystem : MonoBehaviour
{
    private List<string> texts = new List<string>();
    public TMP_InputField leftPage;
    public TMP_InputField rightPage;
    public Button previousPageButton;
    public Button nextPageButton;
    
    public Transform openedPosition;
    public Transform closedPosition;

    public float moveSpeed = 100;
    
    private Transform journal;
    private bool isMoving = false;
    
    private int currentPage = 0;
    [SerializeField] private int maxPage = 10;

    private void Awake()
    {
        journal = transform;
        
        //Verifies that there is at least 2 pages and the number of pages is pair
        if (maxPage < 2)
        {
            maxPage = 2;
        }
        else if (maxPage % 2 == 1)
        {
            maxPage++;
        }
        
        //Adds all the pages to the journal
        for (int i = 0; i < maxPage; i++)
        {
            texts.Add("");
        }

        //Init the buttons for next and previous page
        ChangePage(0);
    }

    /// <summary>
    /// Puts the text of the corresponding page in memory (pageIndex = 0 or 1 for left or right page)
    /// </summary>
    public void ChangeText(int pageIndex)
    {
        string text = (pageIndex == 0) ? leftPage.text : rightPage.text;
        texts[currentPage + pageIndex] = text;
    }

    /// <summary>
    /// Loads the pages at currentPage - pageIndex
    /// </summary>
    public void ChangePage(int pageIndex)
    {
        currentPage = Mathf.Clamp(currentPage + pageIndex * 2, 0, maxPage - 2);

        previousPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = (currentPage < maxPage - 2);
            
        leftPage.text = texts[currentPage];
        rightPage.text = texts[currentPage + 1];
    }

    public void MoveJournal()
    {
        if (isMoving) return;
        StartCoroutine(journal.position == openedPosition.position
            ? MoveJournalCoroutine(closedPosition.position)
            : MoveJournalCoroutine(openedPosition.position));
    }

    private IEnumerator MoveJournalCoroutine(Vector3 _position)
    {
        isMoving = true;
        float t = 0;
        while (journal.position != _position)
        {
            t = Mathf.Clamp(t + (moveSpeed * Time.fixedDeltaTime), 0, 1);
            journal.position = Vector3.Lerp(journal.position, _position, t);
            yield return new WaitForFixedUpdate();
        }
        isMoving = false;
        yield return null;
    }
}