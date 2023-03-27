using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WaveSO wave;
    List<Transform> waypoints;
    int wpIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = wave.GetWaypoints();
        transform.position = waypoints[wpIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    void FollowPath() {
        if (wpIndex < waypoints.Count) {
            Vector3 targetPos = waypoints[wpIndex].position;
            float delta = wave.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if (transform.position == targetPos) {
                wpIndex++;
            }
        } else {
            Destroy(gameObject);
        }
    }
}
