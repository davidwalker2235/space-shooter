using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy (this.gameObject);
        }
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            switch (_powerupId)
            {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedPowerupActive();
                    break;
                case 2:
                    player.ShieldPowerupActive();
                    break;
                default:
                    break;
            }
        }
    }
}
