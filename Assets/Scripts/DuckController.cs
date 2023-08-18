using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    float moveSpeed = 5f;
    int currentPatrolIndex = 0;
    List<Vector2> patrolPoints;
    Vector2 spawnPoint;
    SpriteRenderer spriteRenderer;

    public DuckState CurrentDuckState { get; set; } = DuckState.Patrol;
 
    public void SetupDuck(List<Vector2> patrolPoints, Vector2 spawnPoint, float moveSpeed) {
        
        this.moveSpeed = moveSpeed;
        this.spawnPoint = spawnPoint;
        this.patrolPoints = new List<Vector2>(patrolPoints);
        currentPatrolIndex = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }    

    void MoveTo(Vector2 target) {

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        spriteRenderer.flipX = direction.x > 0;
        transform.position = (Vector2)transform.position + (direction * moveSpeed * Time.deltaTime);
    }

    private void Update() {

        switch (CurrentDuckState) {
            case DuckState.Stop:
                break;
            case DuckState.Patrol:

                Vector2 target = patrolPoints[currentPatrolIndex];

                MoveTo(target);

                float targetDistance = Vector2.Distance(transform.position, target);

                if (targetDistance <= .5f) {
                    currentPatrolIndex++;
                    currentPatrolIndex = currentPatrolIndex >= patrolPoints.Count ? 0 : currentPatrolIndex;
                }

                break;
            case DuckState.Playing:                

                MoveTo(spawnPoint);

                targetDistance = Vector2.Distance(transform.position, spawnPoint);

                if (targetDistance <= .5f)
                    CurrentDuckState = DuckState.Stop;                

                break;
            case DuckState.FollowMom:
                break;
            default:
                break;
        }
    }
}
