using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem explosion;
    public ParticleSystem enemyExplosion;

    #region Singleton
    
    static public ParticleManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void Play(ParticleSystem effect, Vector2 position, Quaternion rotation) {
        if (effect != null)
        {
            Destroy(Instantiate(effect, position, rotation).gameObject, effect.main.startLifetime.constantMax);
        }
    }

}
