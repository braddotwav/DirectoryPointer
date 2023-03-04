using System.Collections.Generic;
using UnityEngine;

public class Settings : ScriptableObject
{
    public List<Directory> Directorys { get { return directorys; } }

    [SerializeField] private List<Directory> directorys;
}