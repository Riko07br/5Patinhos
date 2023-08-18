using UnityEngine;

public class DuckMomController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] Transform[] targets;

    bool isMoving = false;
    int currentPoint = 0;

    SpriteRenderer spriteRenderer;

    public void StartMoving() {
        isMoving = true;
        currentPoint++;
        currentPoint = currentPoint >= targets.Length ? 0 : currentPoint;
        spriteRenderer.flipX = (targets[currentPoint].position - transform.position).x > 0;
    }

    private void Start() {        
        transform.position = targets[0].position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isMoving)
            return;

        Vector2 direction = (Vector2)(targets[currentPoint].position - transform.position).normalized;
        transform.position = (Vector2)transform.position + (direction * moveSpeed * Time.deltaTime);        

        if (Vector2.Distance(transform.position, targets[currentPoint].position) <= .5f)
            isMoving = false;        
    }
}
