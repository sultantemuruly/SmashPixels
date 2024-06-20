using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance { get; private set; }

    // connected to game logic
    public static Action onStarted;
    public static Action onGameEnded;
    public static Action onLevelCompleted;

    // game elements 
    public static Action onPixelEffect;
    public static Action < int > onTextEffect;
    public static Action < Transform, Rigidbody > setNewBall;
    public static Action < GameObject, float, int> onItemDestroyed;
    public static Action < int > onElementActivated;
    public static Func < FloatingJoystick > getJoystick;

    // money management
    public static Action < int > onGetMoney;
    public static Action < int > onReduceMoney;

    // shop management
    public static Action < int > onSetSkinIndex;
    public static Action onResetSkin;

    // base management
    public static Action < int > onFillWithPixels;
    public static Action < int > onOreGetsHit;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
