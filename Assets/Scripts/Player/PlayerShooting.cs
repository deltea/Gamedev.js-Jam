using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    private float shootInput;

    PlayerGun[] guns;

    void Start() {
        guns = transform.GetComponentsInChildren<PlayerGun>();
    }

    void Update() {
        
    }

    void OnShoot(InputValue value) {
        shootInput = value.Get<float>();
        foreach (PlayerGun gun in guns)
        {
            gun.shooting = shootInput > 0;
        }
    }

}
