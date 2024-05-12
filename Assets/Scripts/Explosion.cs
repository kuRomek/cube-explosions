using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity) && hit.transform.TryGetComponent(out Cube cube))
                BlowCube(cube);
        }
    }

    private void BlowCube(Cube cube)
    {
        List<Cube> newCubes = cube.Split();

        foreach (Cube newCube in newCubes)
            newCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);

        if (newCubes.Count == 0)
        {
            float newExplosionForce = _explosionForce * cube.ExplosionForceCoeff;
            float newExplosionRadius = _explosionRadius * cube.ExplosionRadiusCoeff;

            Collider[] hitsBySphere = Physics.OverlapSphere(cube.transform.position, newExplosionRadius);

            foreach (Collider hitBySphere in hitsBySphere)
            {
                if (hitBySphere.TryGetComponent(out Cube hitCube))
                    hitCube.GetComponent<Rigidbody>().AddExplosionForce(newExplosionForce, cube.transform.position, newExplosionRadius);
            }
        }

        Instantiate(_explosionEffect, cube.transform.position, Quaternion.identity);

        Destroy(cube.gameObject);
    }
}
