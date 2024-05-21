








using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

[ExecuteInEditMode]
[RequireComponent (typeof (Spline))]
public class SplineControlledParticleSystem : MonoBehaviour
{
    
    public float startRadius;
    public float endRadius;

    
    [SerializeField] ParticleSystem _particleSystem = null;
    Spline _spline;
    ParticleSystem.Particle[] _particles;
    const float _previousDiff = .01f;

    
    void Awake ()
    {
        _spline = GetComponent<Spline> ();
    }

    
    void LateUpdate ()
    {
        if (_particleSystem == null) return;

        if (_particles == null) _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];

        int aliveParticlesCount = _particleSystem.GetParticles (_particles);

        for (int i = 0; i < aliveParticlesCount; i++)
        {
            
            float seedMax = Mathf.Pow(10, _particles[i].randomSeed.ToString().Length);
            float seedAsPercent = _particles[i].randomSeed / seedMax;
            float travelPercentage = 1 - (_particles[i].remainingLifetime / _particles[i].startLifetime);

            
            if (_spline.GetDirection(travelPercentage, false) == Vector3.zero) continue;

            
            Vector3 offshootDirection = Quaternion.AngleAxis(1080 * seedAsPercent, -_spline.GetDirection(travelPercentage, false)) * _spline.Up(travelPercentage);
            Vector3 previousOffshootDirection = Quaternion.AngleAxis(1080 * seedAsPercent, -_spline.GetDirection(travelPercentage - _previousDiff, false)) * _spline.Up(travelPercentage - _previousDiff, false);

            
            Vector3 position = _spline.GetPosition(travelPercentage, false);

            
            Vector3 lastPosition = position;
            if (travelPercentage - .01f >= 0) lastPosition = _spline.GetPosition(travelPercentage - _previousDiff, false);

            
            float offset = Mathf.Lerp(startRadius, endRadius, travelPercentage);
            float previousOffset = Mathf.Lerp(startRadius, endRadius, travelPercentage - _previousDiff);

            
            Vector3 currentPosition = Vector3.zero;
            Vector3 previousPosition = Vector3.zero;

            switch (_particleSystem.main.simulationSpace)
            {
                case ParticleSystemSimulationSpace.Local:

                    currentPosition = _particleSystem.transform.InverseTransformPoint(position + offshootDirection * offset);
                    previousPosition = _particleSystem.transform.InverseTransformPoint(lastPosition + previousOffshootDirection * previousOffset);
                    break;

                case ParticleSystemSimulationSpace.World:
                case ParticleSystemSimulationSpace.Custom:
                    currentPosition = position + offshootDirection * offset;
                    previousPosition = position + previousOffshootDirection * previousOffset;
                    break;
            }

            
            _particles[i].position = currentPosition;
            _particles[i].velocity = currentPosition - previousPosition;
        }

        
        _particleSystem.SetParticles (_particles, _particles.Length);
    }
}