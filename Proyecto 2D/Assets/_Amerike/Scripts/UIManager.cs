using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Gameplay")]
    public Slider slHitPoints;

    private void Awake()
    {
        instance = this;
    }
}
