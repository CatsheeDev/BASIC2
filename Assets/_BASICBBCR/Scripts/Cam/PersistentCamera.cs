using Pixelplacement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum TransitionType
{
    Dither
}

[RequireComponent(typeof(Camera))]
public class PersistentCamera : Singleton<PersistentCamera>
{
    //[SerializeField] private RenderTexture globalTexture;
    //private RenderTexture transitionToTexture, transitionFromTexture;
    //[SerializeField] private RawImage transitionToObject, transitionFromObject;

    private Rect screenSize;
    private IEnumerator transitionManager;
    private bool isInTransition;

    private Camera worldCam;
    [SerializeField] private Camera toCam, fromCam;

    [SerializeField] private Animator ditherAnimator;

    private void Awake()
    {
        worldCam = GetComponent<Camera>();
        InitializeTextures();
        UpdateResolution();
    }

    private void InitializeTextures()
    {
/*        transitionToTexture = new RenderTexture(globalTexture);
        transitionFromTexture = new RenderTexture(globalTexture);*/
    }

    public void UpdateResolution()
    {
/*        screenSize = new Rect(0, 0, 640, 360);
        globalTexture.width = (int)screenSize.width;
        globalTexture.height = (int)screenSize.height;

        transitionToTexture.width = (int)screenSize.width;
        transitionToTexture.height = (int)screenSize.height;
        transitionToObject.SetNativeSize();

        transitionFromTexture.width = (int)screenSize.width;
        transitionFromTexture.height = (int)screenSize.height;
        transitionFromObject.SetNativeSize();*/
    }

    public void Transition(TransitionType type, float duration, bool reverse)
    {
        StartCoroutine(BeginTransition(type, duration, reverse));
    }

    private IEnumerator BeginTransition(TransitionType type, float duration, bool reverse)
    {
        CopyGlobalTextureToTransitionTextures();
        CursorControllerScript.Instance?.LockCursor();
        fromCam.enabled = false;
        Debug.Log("off with thy cams");
        yield return new WaitForEndOfFrame();
        Debug.Log("end fo frame");

        toCam.enabled = false; 
        if (isInTransition)
        {
            StopCoroutine(transitionManager);
        }

        switch (type)
        {
            case TransitionType.Dither:
                transitionManager = Dither(duration, reverse    );
                break;
        }

        isInTransition = true;
        yield return StartCoroutine(transitionManager);
    }

    private void CopyGlobalTextureToTransitionTextures()
    {
        //Graphics.CopyTexture(globalTexture, transitionFromTexture);
       //Graphics.CopyTexture(globalTexture, transitionToTexture);
    }

    private void ToggleCams(bool type)
    {
        Debug.LogWarning("----IGNORE-----");
        fromCam.enabled = type;
        worldCam.enabled = type;
        toCam.enabled = type;
    }

    private IEnumerator Dither(float duration, bool reverse)
    {
        string trigger = "Dither";
        if (reverse)
        {
            trigger = "DitherR";
        }
        ditherAnimator.gameObject.SetActive(true);
        ditherAnimator.SetTrigger(trigger);
        yield return new WaitForSecondsRealtime(duration);
        EndTransition(ditherAnimator.gameObject);
        yield return null;
    }

    private void EndTransition(GameObject target)
    {
        isInTransition = false;
        target.SetActive(true);
        worldCam.targetTexture = null;
        CursorControllerScript.Instance?.UnlockCursor();
        ToggleCams(true); 
    }
}
