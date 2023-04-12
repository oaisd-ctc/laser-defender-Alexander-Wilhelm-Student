using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //EnemySpawner enemyspawn;
    public WaveSO wave;
    List<Transform> waypoints;
    int wpIndex = 0;

    enum movemode
    {
        simple, magnet
    };

    [SerializeField] movemode movementMode;

    Rigidbody2D rb;

    [SerializeField] float magnetWaypointRadius;

    void Awake()
    {
        //enemyspawn = FindObjectOfType<EnemySpawner>();
    }
    // Start is called before the first frame update

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // wave = enemyspawn.GetCurrentWave();
        waypoints = wave.GetWaypoints();
        transform.position = waypoints[wpIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        Vector3 targetPos;
        Vector2 targetDir;
        float delta; 
        float waypointRadius;
        if (wpIndex < waypoints.Count)
        {

            switch (movementMode)
            {
                case movemode.simple:
                    targetPos = waypoints[wpIndex].position;
                    delta = wave.GetMoveSpeed() * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
                    if (transform.position == targetPos)
                    {
                        wpIndex++;
                    }
                    break;

                case movemode.magnet:
                    targetPos = waypoints[wpIndex].position;
                    targetDir = (transform.position - targetPos).normalized;
                    rb.AddForce(-targetDir * wave.GetMoveSpeed());
                    if (wave.GetUseEnemyRadius()) waypointRadius = magnetWaypointRadius; else waypointRadius = wave.GetWaypointRadius();
                    if (Vector2.Distance(transform.position, targetPos) <= waypointRadius) wpIndex++;
                    break;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}