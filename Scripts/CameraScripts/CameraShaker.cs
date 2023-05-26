using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Camera cam;
    private float shakeTime = 0.3f;
    private float cameraDeZoom = 0.15f; // between 0 and 1, 1 being the original size (0 is a division by zero !)
    private float cameraRotation = 2.5f;
    private bool isDamageShaking = false;

    void Start() {
        cam = GetComponent<Camera>();
    }

    public IEnumerator Shake() {
        isDamageShaking = true;
        float originalCameraSize = cam.orthographicSize;
        float elapsedTime = 0f;
        Vector3 tempRot;

        cam.orthographicSize += originalCameraSize * cameraDeZoom;
        tempRot = new(0f, 0f, cameraRotation);
        transform.Rotate(tempRot);
        while (elapsedTime < shakeTime) {
            elapsedTime += Time.deltaTime;
            cam.orthographicSize -= (originalCameraSize * cameraDeZoom) * (Time.deltaTime / shakeTime);
            tempRot = new(0f, 0f, ((Time.deltaTime / shakeTime) * -cameraRotation));
            transform.Rotate(tempRot);
            yield return null;
        }
        transform.rotation = Quaternion.identity;
        cam.orthographicSize = originalCameraSize;
        isDamageShaking = false;
    }

    void Update() {
        if (!isDamageShaking)
            cam.orthographicSize = 10f + (0.1f * SpectrumGeneratorWithBuffer.scaledBuffFreqBand[0]);
    }
}
