using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject frame;
    
    void Start()
    {
        // Hide other buttons at the start
        transform.Find("StopwatchButton").gameObject.SetActive(true);
        transform.Find("TimerButton").gameObject.SetActive(true);
    
        transform.Find("PauseButton").gameObject.SetActive(false);
        transform.Find("DoneButton").gameObject.SetActive(false);
        transform.Find("TagsButton").gameObject.SetActive(false);
        
    }


    public void StartWorkingUI()
    {

        // Hide current stopwatch and timer buttons 
        transform.Find("StopwatchButton").gameObject.SetActive(false);
        transform.Find("TimerButton").gameObject.SetActive(false);

        // Show pause, done, tags button
        transform.Find("PauseButton").gameObject.SetActive(true);
        transform.Find("DoneButton").gameObject.SetActive(true);
        transform.Find("TagsButton").gameObject.SetActive(true);
        frame.SetActive(true);

    }

    public void BackToOriginUI()
    {
        transform.Find("StopwatchButton").gameObject.SetActive(true);
        transform.Find("TimerButton").gameObject.SetActive(true);

        transform.Find("PauseButton").gameObject.SetActive(false);
        transform.Find("DoneButton").gameObject.SetActive(false);
        transform.Find("TagsButton").gameObject.SetActive(false);
        frame.SetActive(false);
    }
}
