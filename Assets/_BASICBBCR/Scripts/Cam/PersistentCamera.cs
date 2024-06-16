using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.PostLateUpdate;

public enum TransitionType
{
    Dither
}

[RequireComponent(typeof(Camera))]
public class PersistentCamera : Singleton<PersistentCamera>
{
    [SerializeField] private RenderTexture GlobalTexture;
    private RenderTexture TransToTex; 
    private RenderTexture TransFromTex;
    [SerializeField] private RawImage TransToObject;
    [SerializeField] private RawImage TransFromObject;

    private Rect screenSize; 
    private IEnumerator transitionManager;
    private bool inTransition;

    private Camera WorldCam;
    [SerializeField] private Animator ditherAnimation; 
    private void Start()
    {
        WorldCam = GetComponent<Camera>();
        TransToTex = new RenderTexture(GlobalTexture);
        TransFromTex = new(GlobalTexture); 

        UpdateResolution(); 
    }

    public void UpdateResolution()
    {
        screenSize = new Rect(0, 0, 640, 360);
        GlobalTexture.width = (int)screenSize.width;
        GlobalTexture.height = (int)screenSize.height;

        TransToTex.width = (int)screenSize.width;
        TransToTex.height = (int)screenSize.height;
        TransToObject.SetNativeSize();

        TransFromTex.width = (int)screenSize.width; 
        TransFromTex.height = (int)screenSize.height;
        TransFromObject.SetNativeSize(); 
    }

    public void Transition(TransitionType type, float duration)
    {
        StartCoroutine(BeginTransition(type, duration));
    }

    private IEnumerator BeginTransition(TransitionType type, float duration)
    {
        WorldCam.Render();
        Debug.Log("rendered camera");
        ScreenCapture.CaptureScreenshotIntoRenderTexture(TransFromTex);
        RenderTexture.active = TransToTex;
        if (inTransition)
        {
            StopCoroutine(transitionManager);
        }
        switch (type)
        {
            case TransitionType.Dither:
                transitionManager = Dither(duration);
                break;
        }

        inTransition = true; 
        StartCoroutine(transitionManager);

        yield return null; 
    }

    private void EndTransition(GameObject transer)
    {
        inTransition = false;
        transer.SetActive(true);
        WorldCam.enabled = true;
    }

    
    private IEnumerator Dither(float timeBetweenAdvance)
    {
        ditherAnimation.gameObject.SetActive(true); 
        WorldCam.enabled = false; 
        ditherAnimation.Play("DitherWindow", 1, timeBetweenAdvance);
        yield return new WaitForSecondsRealtime(timeBetweenAdvance);
        EndTransition(ditherAnimation.gameObject); 
        yield return null;
    }
}
