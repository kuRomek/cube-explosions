using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _splitChance;
    private float _splitChanceDivider;
    private float _cubeScaleDivider;

    private void Awake()
    {
        _splitChance = 1f;
        _splitChanceDivider = 2f;
        _cubeScaleDivider = 2f;
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    public List<Cube> Split()
    {
        if (Random.value < _splitChance)
        {    
            int minNewCubesCount = 2;
            int maxNewCubesCount = 6;
            int newCubesCount = Random.Range(minNewCubesCount, maxNewCubesCount + 1);

            List<Cube> newCubes = new List<Cube>();

            for (int i = 0; i < newCubesCount; i++)
            {
                float leftRandomOffset = -0.5f;
                float rightRandomOffset = 0.5f;
                float yAxisLift = 0.3f;
                Vector3 randomOffset = GetRandomVector3(leftRandomOffset, rightRandomOffset);
                Vector3 newCubePosition = transform.position + new Vector3(randomOffset.x, randomOffset.y + yAxisLift, randomOffset.z);

                float minRandomAngle = -45f;
                float maxRandomAngle = 45f;
                Quaternion newCubeRotation = Quaternion.Euler(GetRandomVector3(minRandomAngle, maxRandomAngle));

                Cube newCube = Instantiate(gameObject.GetComponent<Cube>(), newCubePosition, newCubeRotation);
                newCube.transform.localScale /= _cubeScaleDivider;
                newCube.SetSplitChance(_splitChance);

                newCubes.Add(newCube);
            }

            return newCubes;
        }
        else
        {
            return new List<Cube>();
        }
    }

    private Vector3 GetRandomVector3(float minValue, float maxValue)
    {
        float randomXValue = Random.Range(minValue, maxValue);
        float randomYValue = Random.Range(minValue, maxValue);
        float randomZValue = Random.Range(minValue, maxValue);
        
        return new Vector3(randomXValue, randomYValue, randomZValue);
    }

    public void SetSplitChance(float previousSplitChance)
    {
        _splitChance = previousSplitChance / _splitChanceDivider;
    }
}
