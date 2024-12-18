using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalSystem : MonoBehaviour
{
    private JournalSaver journalSaver;
    [SerializeField] private TMP_InputField leftPage;
    [SerializeField] private TMP_InputField rightPage;
    [SerializeField] private Button previousPageButton;
    [SerializeField] private Button nextPageButton;
    
    [SerializeField] private Transform openedPosition;
    [SerializeField] private Transform closedPosition;

    [SerializeField] private float moveSpeed = 100;
    
    private Transform journal;
    private bool isMoving = false;
    
    private bool isOpen = false;
    
    private int maxPage;
    private int currentPage;
    
    private void Start()
    {
        journalSaver = JournalSaver.Instance;
        journal = transform;
        
        //Verifies that there is at least 2 pages and the number of pages is pair
        if (journalSaver.maxPage < 2)
        {
            journalSaver.maxPage = 2;
        }
        else if (journalSaver.maxPage % 2 == 1)
        {
            journalSaver.maxPage++;
        }
        
        maxPage = journalSaver.maxPage;
        currentPage = journalSaver.currentPage;

        if (journalSaver.texts.Count > 1)
        {
            //Init the buttons for next and previous page
            ChangePage(currentPage);
            return;
        }
        
        //Adds all the pages to the journal
        for (int i = 0; i < maxPage; i++)
        {
            journalSaver.texts.Add("");
        }
        
        //Init the buttons for next and previous page
        ChangePage(0);
        isOpen = true;
    }

    /// <summary>
    /// Puts the text of the corresponding page in memory (pageIndex = 0 or 1 for left or right page)
    /// </summary>
    public void ChangeText(int pageIndex)
    {
        string text = (pageIndex == 0) ? leftPage.text : rightPage.text;
        journalSaver.texts[currentPage + pageIndex] = text;
    }

    /// <summary>
    /// Loads the pages at currentPage - pageIndex
    /// </summary>
    public void ChangePage(int _pageIndex)
    {
        if (isOpen)
        {
            AudioManager.Instance.PlaySFX("Book");
        }
        journalSaver.currentPage = Mathf.Clamp(journalSaver.currentPage + _pageIndex * 2, 0, maxPage - 2);
        currentPage = journalSaver.currentPage;
        
        previousPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = (currentPage < maxPage - 2);
            
        leftPage.text = journalSaver.texts[currentPage];
        rightPage.text = journalSaver.texts[currentPage + 1];
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