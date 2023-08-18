using System.Collections.Generic;
using UnityEngine;

// Gerencia todos os patinhos de uma vez
public class DuckManager : MonoBehaviour
{
    //APENAS PARA DEBUG
    [SerializeField] bool debugSize = false;

    //referencias para instanciar fora do campo de visão e criar os pontos de patrulha
    [SerializeField] Transform center;
    [SerializeField] Vector2 spawnSize;
    [SerializeField] Vector2 patrolSize;
    [SerializeField] int minPatrolPoints, maxPatrolPoints;
    [SerializeField] float minDuckSpeed, maxDuckSpeed;

    //prefab do patinho
    [SerializeField] GameObject duckPrefab;
    [SerializeField] DuckMomController duckMom;

    int activeDuckAmount = 0;   //quantos patinhos estão participando da musica no total
    List<DuckController> duckControllers = new ();

    public void SetDucksAmount(int amount) {

        if (amount > activeDuckAmount) {

            // Cria novos patos
            if (amount > duckControllers.Count)
                Spawn(amount - duckControllers.Count);
            
            // Reativa todos desde o ultimo ativo
            for (int i = activeDuckAmount; i < duckControllers.Count; i++)
                duckControllers[i].CurrentDuckState = DuckState.Patrol;
            
        }
        else if (amount < activeDuckAmount) {

            // Desativa partindo da quantidade pedida
            for (int i = amount - 1; i < duckControllers.Count; i++)
                duckControllers[i].CurrentDuckState = DuckState.Playing;            
        }
        
        activeDuckAmount = amount;
    }

    public void SetDucksState(DuckState state, int amount) {
        for (int i = 0; i < amount; i++) {
            duckControllers[i].CurrentDuckState = state;
        }
    }
    
    public void CallDuckMom() {
        duckMom.StartMoving();
    }

    void Spawn(int amount) {
        for (int i = 0; i < amount; i++) {

            Vector2 spawnPosition = GetDuckPosition();
            GameObject go = Instantiate(duckPrefab, spawnPosition, Quaternion.identity);
            DuckController dc = go.AddComponent<DuckController>();
            dc.SetupDuck(
                GetDuckPatrolPoints(),
                spawnPosition,
                Random.Range(minDuckSpeed, maxDuckSpeed));

            duckControllers.Add(dc);
        }
    }

    Vector2 GetDuckPosition() {

        Vector2 halfSize = spawnSize * .5f;
        int randomEdge = Random.Range(0, 4);

        Vector2 spawnPos = randomEdge switch {
            0 => new Vector2(Random.Range(-halfSize.x, halfSize.x), halfSize.y),
            1 => new Vector2(halfSize.x, Random.Range(-halfSize.y, halfSize.y)),
            2 => new Vector2(Random.Range(-halfSize.x, halfSize.x), -halfSize.y),
            _ => new Vector2(-halfSize.x, Random.Range(-halfSize.y, halfSize.y)),
        };

        spawnPos += (Vector2)center.position;
        return spawnPos;
    }

    List<Vector2> GetDuckPatrolPoints() {

        int pointsAmount = Random.Range(minPatrolPoints, maxPatrolPoints);
        List<Vector2> points = new();

        for (int i = 0; i < pointsAmount; i++) {
            float x = patrolSize.x * .5f;
            float y = patrolSize.y * .5f;
            points.Add(new Vector2(Random.Range(-x, x), Random.Range(-y, y)));
        }

        return points;
    }

    private void OnDrawGizmos() {
        if (!debugSize)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center.position, spawnSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center.position, patrolSize);
    }
}
