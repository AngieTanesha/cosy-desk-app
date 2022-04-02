using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomObject 
{
    public List<GameObject> gameObjectsRN;
    public GameObject room;
    public List<TimedSessions> timedSessions;
    public List<TagsDict> tagsDictionary;

}
