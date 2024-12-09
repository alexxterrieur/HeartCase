using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionChanger : MonoBehaviour
{
    [SerializeField] private List<Transform> camPositions = new List<Transform>();
    private Transform selfTransform;
    private int currentPoint = 0;
    private Vector3 nextPos;

    [SerializeField] private GameObject changePositionButtons;

    private void Start()
    {
        selfTransform = GetComponent<Transform>();
        nextPos = selfTransform.position;
    }

    /// <summary>
    /// Move Camera to the next position
    /// </summary>
    /// <param name="nextPoint"> 1 for next, -1 for previous </param>
    public void GoToNextPos(int nextPoint)
    {
        if (nextPoint != 1 && nextPoint != -1)
        {
            Debug.LogWarning("Warning : param nextPoint can only be 1 or -1 ! Please assign a valide param.");
            return;
        }

        changePositionButtons.SetActive(false);

        currentPoint += nextPoint;
        if ((currentPoint) >= camPositions.Count)
        {
            currentPoint = 0;
        }
        else if (currentPoint < 0)
        {
            currentPoint = (camPositions.Count - 1);
        }

        StartCoroutine(GoToPoint(camPositions[currentPoint]));
    }

    private IEnumerator GoToPoint(Transform targetTransform)
    {
        float time = 0;
        while (nextPos != targetTransform.position)
        {
            selfTransform.rotation = Quaternion.Lerp(selfTransform.rotation, targetTransform.rotation, time);
            nextPos = Vector3.Lerp(nextPos, targetTransform.position, time);
            selfTransform.position = nextPos;
            yield return new WaitForSeconds(0.01f);
            time += 0.01f;
        }
        changePositionButtons.SetActive(true);
    }

}
