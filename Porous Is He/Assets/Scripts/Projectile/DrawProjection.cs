using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    public GameObject ProjectileSpawn;
    LineRenderer lineRenderer;

    //number of points on the line
    public int numPoints = 50;
    //distance between points
    public float timeBetweenPoints = 0.1f;
    public LayerMask CollidableLayers;

    private ShootingScript shootingScript;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shootingScript = ProjectileSpawn.GetComponent<ShootingScript>();
    }

    void Update()
    {
        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startPosition = ProjectileSpawn.transform.position;
        Vector3 startVelocity = shootingScript.GetVelocity();

        for (float t=0; t<numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startPosition + t * startVelocity;
            newPoint.y = startPosition.y + startVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if(Physics.OverlapSphere(newPoint, 0.01f, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        lineRenderer.SetPositions(points.ToArray());
    }
}
