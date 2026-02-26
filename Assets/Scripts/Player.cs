using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float leftBoundary = -9;
    [SerializeField]
    private float rightBoundary = 9;
    [SerializeField]
    private float topBoundary = 5;
    [SerializeField]
    private float bottomBoundary = -5;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        shot();
    }

    void calculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, leftBoundary, rightBoundary);
        position.y = Mathf.Clamp(position.y, bottomBoundary, topBoundary);
        transform.position = position;

    }

    void shot() { 
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
        }
    
    
    }
}
