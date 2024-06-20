using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource soundSource;

    [SerializeField] private AudioClip pixelSound;
    [SerializeField] private AudioClip cashSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip rocketSound;
    [SerializeField] private AudioClip laserSound;

    private static AudioClip CashSound;
    private static AudioClip BombSound;
    private static AudioClip RocketSound;
    private static AudioClip LaserSound;

    private void OnEnable()
    {
        EventsManager.onPixelEffect += OnPixelSound;
    }

    private void OnDisable()
    {
        EventsManager.onPixelEffect -= OnPixelSound;
    }

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();

        CashSound = cashSound;
        BombSound = bombSound;
        RocketSound = rocketSound;
        LaserSound = laserSound;
    }

    private void OnPixelSound()
    {
        soundSource.PlayOneShot(pixelSound);
    }

    public static void OnCashSound()
    {
        soundSource.PlayOneShot(CashSound);
    }
    
    public static void OnBombSound()
    {
        soundSource.PlayOneShot(BombSound);
    }

    public static void OnRocketSound()
    {
        soundSource.PlayOneShot(RocketSound);
    }

    public static void OnLaserSound()
    {
        soundSource.PlayOneShot(LaserSound);
    }
}
