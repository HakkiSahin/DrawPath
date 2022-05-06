using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private GameObject _brush;
    [SerializeField] private float _brushSize = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Plane")
                {
                    GameObject obj = Instantiate(_brush, hit.point + Vector3.up * .1f, Quaternion.identity, transform);
                    gameObject.transform.localScale = Vector3.one * _brushSize;
                }
            }
        }
    }
}