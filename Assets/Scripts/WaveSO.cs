using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Congig", fileName = "New Wave Config")]
public class WaveSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed;


    public Transform GetStartWaypoint(){
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints(){
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab) {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetMoveSpeed() {
        return moveSpeed;
    }
}
