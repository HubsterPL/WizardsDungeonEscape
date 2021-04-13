using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    private static GameAudioManager _instance;
    public static GameAudioManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<GameAudioManager>();
            if (_instance == null)
                _instance = new GameObject("AudioManager").AddComponent<GameAudioManager>();
            return _instance;
        }
    }

    [SerializeField] AudioSource pickupSource;
    public void PlayPotionPickup()
    {
        pickupSource.pitch = Random.Range(.8f, 1.5f);
        if (pickupSource.isPlaying)
        {
            pickupSource.time = 0f;
        }
        pickupSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else if(_instance != this)
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
