using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private float time = 5f*60f;
    [SerializeField] private TextMeshProUGUI timerText;
    
    public void Update()
    {
        time -= Time.deltaTime;
        timerText.text = "Time Left : " + Mathf.Floor(time).ToString();
        if (time <= 0)
        {
            Debug.Log("Time Over");
        }
    }

    public void RemoveTime(float _time)
    {
        time -= _time;
    }
}
