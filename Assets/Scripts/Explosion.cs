using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity) && hit.transform.TryGetComponent(out Cube cube))
            {
                Vector3 previousCubePosition = cube.transform.position;
                List<Cube> newCubes = cube.Split();
                Destroy(cube.gameObject);

                foreach (Cube newCube in newCubes)
                    newCube.GetComponent<Rigidbody>().AddExplosionForce(_explotionForce, previousCubePosition, _explotionRadius);

                if (newCubes.Count != 0)
                    Instantiate(_explosionEffect, previousCubePosition, Quaternion.identity);
            }
        }
    }
}
