using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeSpawner : MonoBehaviour
{
    private Cube _parentCube;
    private float _explosionCoeffsMultiplier;
    private float _cubeScaleDivider;
    private float _splitChanceDivider;

    private void Awake()
    {
        _parentCube = GetComponent<Cube>();
        _explosionCoeffsMultiplier = 1.5f;
        _cubeScaleDivider = 2f;
        _splitChanceDivider = 2f;
    }

    public List<Cube> Spawn(int count)
    {
        List<Cube> newCubes = new List<Cube>();

        for (int i = 0; i < count; i++)
        {
            float leftRandomOffset = -0.5f;
            float rightRandomOffset = 0.5f;
            float yAxisLift = 0.3f;
            Vector3 randomOffset = GetRandomVector3(leftRandomOffset, rightRandomOffset);
            Vector3 newCubePosition = transform.position + new Vector3(randomOffset.x, randomOffset.y + yAxisLift, randomOffset.z);

            float minRandomAngle = -45f;
            float maxRandomAngle = 45f;
            Quaternion newCubeRotation = Quaternion.Euler(GetRandomVector3(minRandomAngle, maxRandomAngle));

            Cube newCube = Instantiate(_parentCube, newCubePosition, newCubeRotation);
            newCube.transform.localScale /= _cubeScaleDivider;
            newCube.Init(_parentCube.SplitChance, _splitChanceDivider, _parentCube.ExplosionForceCoeff, _parentCube.ExplosionRadiusCoeff, _explosionCoeffsMultiplier);

            newCubes.Add(newCube);
        }

        return newCubes;
    }

    private Vector3 GetRandomVector3(float minValue, float maxValue)
    {
        float randomXValue = Random.Range(minValue, maxValue);
        float randomYValue = Random.Range(minValue, maxValue);
        float randomZValue = Random.Range(minValue, maxValue);

        return new Vector3(randomXValue, randomYValue, randomZValue);
    }
}
