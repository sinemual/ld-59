using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PathProvider
{
    public int Id;
    public List<Transform> Value;
    public bool IsLoop;
    public bool IsPoolingInTheEnd;
}