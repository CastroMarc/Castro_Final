using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Shapes
    public const int    shapesCount = 4;
    [SerializeField]
    private GameObject  squareShape;
    [SerializeField]
    private GameObject  circleShape;
    [SerializeField]
    private GameObject  triangleShape;
    [SerializeField]
    private GameObject  starShape;

    // Spawn position
    [SerializeField]
    private float       xRange          = 5;
    [SerializeField]
    private float       yPosition       = 6;
    [SerializeField]
    private float       baseZPosition   = 0.02f;
    [SerializeField]
    private float       zPositionInc    = 0.02f;
    private int         zIndex          = 1;
    private int         maxZIndex       = 10;

    // Shape points
    private int         defaultShapeValue   = 1;
    private int         circleValue         = 3;
    private int         triangleValue       = 2;
    private int         starValue           = 2;

    // Spawn time
    private float       spawnInterval   = 1.5f;
    private float       spawnCooldown   = 1.5f;


    void Start()
    {
        xRange = Mathf.Abs(xRange);
        spawnCooldown = spawnInterval;
    }

    private void Update()
    {
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown <= 0) { 
            SpawnRandomShape();

            spawnCooldown += spawnInterval;
        }
    }

    public void SetSpawnInterval(float seconds) {
        spawnInterval = Mathf.Max(seconds, 0.01f);
    }

    /* ABSTRACTION
     * SpawnRandomShape() select a random shape and spawns it calling other methods that make all
     * necessary operations to spawn each different shape. There is no need to know how it works.
     */
    private void SpawnRandomShape() {
        if (GameManager.Instance.GameEnded) return;

        int shapeIndex = Random.Range(0, shapesCount);

        if      (shapeIndex == 0)   SpawnCircle();
        else if (shapeIndex == 1)   SpawnTriangle();
        else if (shapeIndex == 2)   SpawnStar();
        else                        SpawnSquare();
    }

    private void SpawnTriangle() {
        GameObject newShape = Instantiate(triangleShape, GetRandomSpawnPosition(), triangleShape.transform.rotation);

        SetShapeValue(newShape);
    }

    private void SpawnCircle() {
        GameObject newShape = Instantiate(circleShape, GetRandomSpawnPosition(), circleShape.transform.rotation);

        SetShapeValue(newShape);
    }

    private void SpawnSquare() {
        GameObject newShape = Instantiate(squareShape, GetRandomSpawnPosition(), squareShape.transform.rotation);

        SetShapeValue(newShape);
    }

    private void SpawnStar() {
        // Get a random movement direction
        bool right = Random.value > 0.5f;

        Vector3 spawnPosition = GetRandomSpawnPosition();
        spawnPosition.x = Mathf.Abs(spawnPosition.x);
        if (right) spawnPosition.x *= -1;

        GameObject newShape = Instantiate(starShape, spawnPosition, starShape.transform.rotation);

        SetShapeValue(newShape);

        Star star;
        if(newShape.TryGetComponent<Star>(out star)) {
            star.right = right;
        }
    }

    private Vector3 GetRandomSpawnPosition() { 
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(-xRange, xRange);
        spawnPosition.y = yPosition;
        spawnPosition.z = baseZPosition + zIndex * zPositionInc;

        // Update zIndex for next spawn position
        zIndex++;
        if(zIndex >= maxZIndex)
            zIndex = 0;

        return spawnPosition; 
    }

    private void SetShapeValue(GameObject shapeObject) { 
        Shape shape = shapeObject.GetComponent<Shape>();

        if(shape == null) return;

        if      (shape is Circle)   shape.Value = circleValue;
        else if (shape is Star)     shape.Value = starValue;
        else if (shape is Triangle) shape.Value = triangleValue;
        else                        shape.Value = defaultShapeValue;
    }
}
