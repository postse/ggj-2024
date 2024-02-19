using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClownNames
{
    private static List<string> _clownNames = new List<string>
    {
        "Bozo",
        "Ronald McDonald",
        "Krusty",
        "Joker",
        "Pogo",
        "Puddles",
        "Penny",
        "Patches",
        "Punchy",
        "Pennywise",
        "Penny",
        "Bingo",
        "Bongo",
        "Steve",
        "Scungo",
        "Abraham",
        "Denver",
        "Logan",
        "Simon",
        "Zane",
        "Bobby",
        "Bubbles",
        "ClownyMcClownface",
        "Oobabadooba"
    };

    private static List<string> _currentClownNames = new List<string>();

    public static int Count { get { return _clownNames.Count; } }

    public static string PickClownName()
    {
        int index = Random.Range(0, _clownNames.Count);
        string clownName = _clownNames[index];
        _currentClownNames.Add(clownName);
        _clownNames.RemoveAt(index);
        return clownName;
    }

    public static void RestoreClownName(string name)
    {
        // should do some security here to make sure the name is actually in the list or whatever
        _clownNames.Add(name);
        _currentClownNames.Remove(name);
    }

    public static void RestoreAllClownNames()
    {
        for (int i = 0; i < _currentClownNames.Count; i++)
        {
            string name = _currentClownNames[i];
            RestoreClownName(name);
        }
    }

    
}
