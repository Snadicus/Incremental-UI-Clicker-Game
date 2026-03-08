using UnityEngine;
using System.Collections.Generic;
using System;

public class TeamManager : MonoBehaviour
{
    public List<string> teammates;

    // Function called by other functions that adds teammate
    public void AddTeammate(string teammateType)
    {
        teammates.Add(teammateType);
    }
}
