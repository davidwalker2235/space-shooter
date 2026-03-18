using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private float _topBound = 6.0f;
    [SerializeField]
    private float _bottomBound = -6.0f;
    [SerializeField]
    private float _powerupID;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _bottomBound)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            switch (_powerupID)
            {
                case 0:
                    player.TripleShotPowerupOn();
                    break;
                case 1:
                    player.SpeedUpPowerupOn();
                    break;
                default:
                    Debug.Log("Default Value");
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
