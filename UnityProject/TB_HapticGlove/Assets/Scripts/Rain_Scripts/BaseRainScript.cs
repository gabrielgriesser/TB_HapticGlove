using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

public class BaseRainScript : MonoBehaviour
{
    [Tooltip("Camera the rain should hover over, defaults to main camera")]
    public Camera Camera;

    [Tooltip("Whether rain should follow the camera. If false, rain must be moved manually and will not follow the camera.")]
    public bool FollowCamera = true;

    [Tooltip("Intensity of rain (0-1)")]
    [Range(0.0f, 1.0f)]
    public float RainIntensity;

    [Tooltip("Rain particle system")]
    public ParticleSystem RainFallParticleSystem;

    protected Material rainMaterial;

    private float lastRainIntensityValue = -1.0f;

    private void CheckForRainChange()
    {
        if (lastRainIntensityValue != RainIntensity)
        {
            lastRainIntensityValue = RainIntensity;
            if (RainIntensity <= 0.01f)
            {
                
                if (RainFallParticleSystem != null)
                {
                    ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                    e.enabled = false;
                    RainFallParticleSystem.Stop();
                }
            }
            else
            {
                
                if (RainFallParticleSystem != null)
                {
                    ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                    e.enabled = RainFallParticleSystem.GetComponent<Renderer>().enabled = true;
                    if (!RainFallParticleSystem.isPlaying)
                    {
                        RainFallParticleSystem.Play();
                    }
                    ParticleSystem.MinMaxCurve rate = e.rateOverTime;
                    rate.mode = ParticleSystemCurveMode.Constant;
                    rate.constantMin = rate.constantMax = RainFallEmissionRate();
                    e.rateOverTime = rate;
                }
            }
        }
    }

    protected virtual void Start()
    {

#if DEBUG

        if (RainFallParticleSystem == null)
        {
            Debug.LogError("Rain fall particle system must be set to a particle system");
            return;
        }

#endif

        if (Camera == null)
        {
            Camera = Camera.main;
        }

        if (RainFallParticleSystem != null)
        {
            ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
            e.enabled = false;
            Renderer rainRenderer = RainFallParticleSystem.GetComponent<Renderer>();
            rainRenderer.enabled = false;
            rainMaterial = new Material(rainRenderer.material);
            rainMaterial.EnableKeyword("SOFTPARTICLES_OFF");
            rainRenderer.material = rainMaterial;
        }
    }

    protected virtual void Update()
    {

#if DEBUG

        if (RainFallParticleSystem == null)
        {
            Debug.LogError("Rain fall particle system must be set to a particle system");
            return;
        }

#endif

        CheckForRainChange();
    }

    protected virtual float RainFallEmissionRate()
    {
        return (RainFallParticleSystem.main.maxParticles / RainFallParticleSystem.main.startLifetime.constant) * RainIntensity;
    }

    protected virtual bool UseRainMistSoftParticles
    {
        get
        {
            return true;
        }
    }
}