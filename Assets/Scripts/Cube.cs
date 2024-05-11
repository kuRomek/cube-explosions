using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _splitChance;

    public float SplitChance => _splitChance;

    private void Awake()
    {
        _splitChance = 1f;
        float minColorIntensity = 0f;
        float maxColorIntensity = 1f;
        GetComponent<Renderer>().material.color = new Color(Random.Range(minColorIntensity, maxColorIntensity),
                                                            Random.Range(minColorIntensity, maxColorIntensity),
                                                            Random.Range(minColorIntensity, maxColorIntensity));
    }

    public void SetSplitChance(float previousCubeChance, float chanceToSplitDivider)
    {
        _splitChance = previousCubeChance / chanceToSplitDivider;
    }
}
