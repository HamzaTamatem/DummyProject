using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "XpTable", menuName = "ScriptableObjects/XpTableObject")]
public class XpTable : ScriptableObject
{
    public List<int> XpList = new List<int>();
}
