using System.Collections.Generic;
using UnityEngine;

public class InitialiseRoom : MonoBehaviour
{
    public RoomObject ro;

    // ------------------------------------------------------------------------
    void Start()
    {
        
        // If there's no savefile, unblock code.

        //for (int i = 0; i < ro.gameObjectsRN.Count; i++)
        //{
        //    ro.gameObjectsRN[i].name.Replace("(clone)", "").Trim();
        //    Instantiate(ro.gameObjectsRN[i], ro.room.transform);
        //}

    }

    // ------------------------------------------------------------------------
    void Update()
    {
        // Save only when you press down the letter "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            // change the locations
            int numChild = transform.childCount;
            for (int i=0; i < numChild; i++)
            {
                ro.gameObjectsRN[i].transform.position = transform.GetChild(i).localPosition;
            }


            ro.timedSessions = Stopwatch.GetTimedSessions();

            SaveManager.Save(ro);
            Debug.Log("Saved clicked");
            Debug.Log(ro.timedSessions);
        }

        // Load from savefile only when you press down the letter "O"
        if (Input.GetKeyDown(KeyCode.O))
        {

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            ro = SaveManager.Load();
            Debug.Log("Loaded click");


            for (int i = 0; i < ro.gameObjectsRN.Count; i++)
            {
                Instantiate(ro.gameObjectsRN[i], ro.room.transform);

            }

            Stopwatch.SetTimedSessions(ro.timedSessions);
            Debug.Log("current:");
            Debug.Log(ro.timedSessions.Count);

        }
    }
}
