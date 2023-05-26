using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnBeat : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    void Update() {
        var emission = ps.emission;
        emission.rate = 500f + (1000f * SpectrumGeneratorWithBuffer.scaledBuffFreqBand[0]);
    }
}
