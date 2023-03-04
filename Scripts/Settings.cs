using NightshiftGames.DirectoryPointer;
using System.Collections.Generic;
using UnityEngine;

public class Settings : ScriptableObject
{
    public List<DirectoryContainer> Directorys { get { return directorys; } }

    [SerializeField] private List<DirectoryContainer> directorys;
}