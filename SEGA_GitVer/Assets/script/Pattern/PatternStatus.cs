using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternStatus : MonoBehaviour
{
    /// <summary>
    /// 道順
    /// </summary>
    [SerializeField] List<string> routeNumber;
    [SerializeField] NumberPattern p;
    public List<string> Get_routeNumber()
    {
        return routeNumber;
    }
}