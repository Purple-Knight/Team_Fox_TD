using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public List<Vector2> pathPoints;
    private List<float> pathCheckpoints;

    public float Distance { get => distance; private set => distance = value; }
    private float distance;

    public Vector2 StartPosition { get => pathPoints[0]; }

    private void Awake()
    {
        pathCheckpoints = new List<float>(1) { 0f };

        for (int i = 0; i < pathPoints.Count - 1; ++i)
        {
            Distance += Vector2.Distance(pathPoints[i], pathPoints[i + 1]);
            pathCheckpoints.Add(distance);
        }
    }

    /// <summary>
    /// Returns Lerped Position over the total Path from 0-1 value.
    /// </summary>
    /// <param name="currentTime">Current path progression</param>
    /// <param name="fullTime">Total path time</param>
    /// <returns></returns>
    public Vector2 GetPathPosition(float currentTime, float fullTime)
    {
        float t = currentTime / fullTime;
        
        int section = 0;
        while ( section < pathPoints.Count - 1 && pathCheckpoints[section + 1] < t * Distance )
        {
            section++;
        }
        section = Mathf.Clamp(section, 0, pathPoints.Count - 2);

        return Vector2.Lerp(
            pathPoints[section],
            pathPoints[section + 1],
            Mathf.InverseLerp(pathCheckpoints[section] / Distance, pathCheckpoints[section + 1] / Distance, t));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 pos;
        float count = pathPoints.Count;
        for (int i = 0; i < count; ++i)
        {
            pos = pathPoints[i];

            Gizmos.color = i == 0 ? Color.cyan : Color.Lerp(Color.green, Color.red, i / count);
            Gizmos.DrawWireSphere(pos, .2f);

            if (i == 0) continue;

            Gizmos.color = new Color(180f / 255f, 250f / 255f, 110f / 255f);
            Gizmos.DrawLine(pathPoints[i - 1], pos);
        }
    }
#endif
}
