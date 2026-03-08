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
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
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

    void FireLaser() {

        _canFire = Time.time + _fireRate;
        GameObject laser = Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
    }

    public void Damage() {
        _lives--;
        if (_lives < 1) {
            Destroy(this.gameObject);
        }
    }
}
