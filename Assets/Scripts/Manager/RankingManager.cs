using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RankingEntry
{   
    public int Score;
}
public class RankingManager : MonoBehaviour
{
    public List<RankingEntry> rankingEntries = new List<RankingEntry>();

    public void AddRankingEntry(int score)
    {
        RankingEntry entry = new RankingEntry();
        
        entry.Score = score;
        rankingEntries.Add(entry);
        rankingEntries.Sort((x,y) => y.Score.CompareTo(x.Score));
        rankingEntries = rankingEntries.Take(5).ToList();
    }
}

