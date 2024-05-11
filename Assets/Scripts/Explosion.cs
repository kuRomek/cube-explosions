using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    private float _splitChanceDivider;
    private float _cubeScaleDivider;

    private void Awake()
    {
        _splitChanceDivider = 2f;
        _cubeScaleDivider = 2f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit, Mathf.Infinity) && hit.transform.TryGetComponent<Cube>(out Cube cube))
            {
                if (Random.Range(0f, 1f) < cube.SplitChance)
                    SplitCube(cube);
                else
                    Destroy(cube.gameObject);
            }
        }
    }

    private void SplitCube(Cube cube)
    {
        Vector3 previousCubePosition = cube.transform.position;
        float previousCubeScale = cube.transform.localScale.x;
        float previousChanceToSplit = cube.SplitChance;
        Destroy(cube.gameObject);

        int minNewCubesCount = 2;
        int maxNewCubesCount = 6;
        int newCubesCount = Random.Range(minNewCubesCount, maxNewCubesCount);

        for (int i = 0; i < newCubesCount; i++)
        {
            float leftRandomOffset = -0.5f;
            float rightRandomOffset = 0.5f;
            float randomXPosition = previousCubePosition.x + Random.Range(leftRandomOffset, rightRandomOffset);
            float randomYPosition = previousCubePosition.y + Random.Range(leftRandomOffset, rightRandomOffset) + 0.5f;
            float randomZPosition = previousCubePosition.z + Random.Range(leftRandomOffset, rightRandomOffset);
            Vector3 newCubePosition = new Vector3(randomXPosition, randomYPosition, randomZPosition);

            float minRandomAngle = -45f;
            float maxRandomAngle = 45f;
            float randomXAngle = Random.Range(minRandomAngle, maxRandomAngle);
            float randomYAngle = Random.Range(minRandomAngle, maxRandomAngle);
            float randomZAngle = Random.Range(minRandomAngle, maxRandomAngle);
            Quaternion newCubeRotation = Quaternion.Euler(randomXAngle, randomYAngle, randomZAngle);

            GameObject newCube = Instantiate(_cubePrefab, newCubePosition, newCubeRotation);
            newCube.transform.localScale = Vector3.one * previousCubeScale / _cubeScaleDivider;
            newCube.GetComponent<Cube>().SetSplitChance(previousChanceToSplit, _splitChanceDivider);

            newCube.GetComponent<Rigidbody>().AddExplosionForce(_explotionForce, previousCubePosition, _explotionRadius);
            Instantiate(_explosionEffect, previousCubePosition, Quaternion.identity);
        }
    }
}
