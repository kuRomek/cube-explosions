using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private CubeSpawner _spawner;
    public float ExplosionForceCoeff { get; private set; }
    public float ExplosionRadiusCoeff { get; private set; }
    public float SplitChance { get; private set; }

    private void Awake()
    {
        if (TryGetComponent(out CubeSpawner spawner))
            _spawner = spawner;

        ExplosionForceCoeff = 1f;
        ExplosionRadiusCoeff = 1f;
        SplitChance = 1f;
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    public List<Cube> Split()
    {
        if (Random.value < SplitChance && _spawner != null)
        {    
            int minNewCubesCount = 2;
            int maxNewCubesCount = 6;
            int newCubesCount = Random.Range(minNewCubesCount, maxNewCubesCount + 1);

            List<Cube> newCubes = _spawner.Spawn(newCubesCount);

            return newCubes;
        }
        else
        {
            return null;
        }
    }

    public void Init(float previousSplitChance, float splitChanceDivider, float previousExplotionForceCoeff, float previousExplotionRadiusCoeff, float explosionCoeffsMultiplier)
    {
        ExplosionForceCoeff = previousExplotionForceCoeff * explosionCoeffsMultiplier;
        ExplosionRadiusCoeff = previousExplotionRadiusCoeff * explosionCoeffsMultiplier;

        SplitChance = previousSplitChance / splitChanceDivider;
    }
}
