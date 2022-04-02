using System.Collections.Generic;
using UnityEngine;

public class InitialiseRoom : MonoBehaviour
{
    public RoomObject ro;

    // ------------------------------------------------------------------------
    void Start()
    {

        // If there's no savefile, unblock code.

        for (int i = 0; i < ro.gameObjectsRN.Count; i++)
        {
            ro.gameObjectsRN[i].name.Replace("(clone)", "").Trim();
            Instantiate(ro.gameObjectsRN[i], ro.room.transform);
        }

    }

    public List<TagsDict> DictionaryToDict(Dictionary<string, int> _tagsFreq)
    {
        List<TagsDict> _dict = new List<TagsDict>();
        foreach (KeyValuePair<string, int> entry in _tagsFreq)
        {
            TagsDict _entry = new TagsDict();
            _entry.key = entry.Key;
            _entry.value = entry.Value;
            _dict.Add(_entry);
        }

        return _dict;
    }

    public Dictionary<string, int> DictToDictionary(List<TagsDict> _tagsDict)
    {
        Dictionary<string, int> _dict = new Dictionary<string, int>();
        Debug.Log(_tagsDict);
        foreach (TagsDict stuff in _tagsDict)
        {

            _dict.Add(stuff.key, stuff.value);
        }

        return _dict;

    }

    // ------------------------------------------------------------------------
    void Update()
    {
        // Save only when you press down the letter ","
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            // change the locations
            int numChild = transform.childCount;
            for (int i=0; i < numChild; i++)
            {
                ro.gameObjectsRN[i].transform.position = transform.GetChild(i).localPosition;
            }


            ro.timedSessions = Stopwatch.GetTimedSessions();
            ro.tagsDictionary = DictionaryToDict(TagFiller.GetTagsDictionary());

            Debug.Log("fdkflj " + ro.tagsDictionary.Count);

            SaveManager.Save(ro);
            Debug.Log("Saved clicked");
        }

        // Load from savefile only when you press down the letter "/"
        if (Input.GetKeyDown(KeyCode.Slash))
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
            TagFiller.tagsFrequency = DictToDictionary(ro.tagsDictionary);

            Debug.Log(TagFiller.GetTagsDictionary().Count);

        }
    }
}
