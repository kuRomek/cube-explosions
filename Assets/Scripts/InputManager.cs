using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    
    private Explosion _explosion;
    private int _mouseButtonForExplosion;

    private void Awake()
    {
        if (TryGetComponent(out Explosion explosion))
            _explosion = explosion;

        _mouseButtonForExplosion = 0;
    }

    private void Update()
    {
        if (_explosion != null)
            if (Input.GetMouseButtonDown(_mouseButtonForExplosion))
                Explode();
    }

    private void Explode()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default")) && hit.transform.TryGetComponent(out Cube cube))
        {
            List<Cube> newCubes = cube.Split();

            if (newCubes != null)
                _explosion.Blow(cube, newCubes);
            else
                _explosion.Blow(cube);
        }
    }
}
