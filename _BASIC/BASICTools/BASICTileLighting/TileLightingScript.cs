using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[ExecuteAlways]
#else
[ExecuteInEditMode]
#endif
public class TileLighting : MonoBehaviour
{
    [System.Serializable]
    public class LightSourceSettings
    {
        public GameObject lightSource;
        public Color lightColor = Color.white;
        public float tileFalloffRange = 45f;
        public float spriteDetectionRange = 8f;
    }

    private struct LightRenderers
    {
        public Renderer materialRenderer;
        public MaterialPropertyBlock renderBlock;
        public Color originalColor; 
    }

    [SerializeField] private LightSourceSettings[] lightSources;
    [SerializeField] private List<GameObject> tiles = new List<GameObject>();
    [SerializeField] private bool reverseLighting = true;
    private Dictionary<Renderer, LightRenderers> lightRenderers = new Dictionary<Renderer, LightRenderers>();
    private void Update()
    {
        foreach (var tile in tiles)
        {
            LightSourceSettings closestLightSource = FindClosestLightSource(tile.transform.position);

            if (closestLightSource == null || closestLightSource.lightSource == null)
                continue;

            Vector3 lightPosition = closestLightSource.lightSource.transform.position;


            Renderer[] renderers = tile.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (!lightRenderers.ContainsKey(renderer))
                {
                    MaterialPropertyBlock newMaterialBlock = new MaterialPropertyBlock();
                    renderer.GetPropertyBlock(newMaterialBlock);
                    Color originalColor = renderer.sharedMaterial.color;

                    LightRenderers newLightRenderer = new LightRenderers
                    {
                        materialRenderer = renderer,
                        renderBlock = newMaterialBlock,
                        originalColor = originalColor
                    };

                    lightRenderers[renderer] = newLightRenderer;
                }

                LightRenderers existingLightRenderer = lightRenderers[renderer];
                UpdateRendererColor(existingLightRenderer.materialRenderer, lightPosition, closestLightSource.tileFalloffRange, closestLightSource.lightColor);
            }

            UpdateSpritesColor(lightPosition, closestLightSource.spriteDetectionRange, closestLightSource.lightColor);
        }
    }

    private LightSourceSettings FindClosestLightSource(Vector3 position)
    {
        LightSourceSettings closestSource = null;
        float closestDistance = Mathf.Infinity;

        foreach (var source in lightSources)
        {
            if (source.lightSource == null)
                continue;

            float distance = Vector3.Distance(position, source.lightSource.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSource = source;
            }
        }

        return closestSource;
    }

    private void UpdateRendererColor(Renderer renderer, Vector3 lightPosition, float falloffRange, Color lightColor)
    {
        MaterialPropertyBlock originalBlock = lightRenderers[renderer].renderBlock;
        MaterialPropertyBlock currentBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(currentBlock);

        float distanceToLight = Vector3.Distance(renderer.transform.position, lightPosition);
        float intensity = reverseLighting ? Mathf.Clamp01(distanceToLight / falloffRange) : Mathf.Clamp01(1 - distanceToLight / falloffRange);

        Color originalColor = renderer.sharedMaterial.color;

        Color[] colours = new Color[2];
        colours[0] = originalColor;
        colours[1] = lightColor; 
        Color newColor = AverageColour(colours) * (intensity); 

        newColor.a = originalBlock.GetColor("_Color").a;
        currentBlock.SetColor("_Color", newColor);

        renderer.SetPropertyBlock(currentBlock);
    }
    private Color AverageColour(Color[] colours)
    {
        float totalRed = 0f;
        float totalGreen = 0f;
        float totalBlue = 0f;

        foreach (Color colour in colours)
        {
            totalRed += colour.r;
            totalGreen += colour.g;
            totalBlue += colour.b;
        }

        float numColours = colours.Length;
        return new Color(totalRed / numColours, totalGreen / numColours, totalBlue / numColours);

    }

    private void UpdateSpritesColor(Vector3 lightPosition, float detectionRange, Color lightColor)
    {
        SpriteRenderer[] spriteRenderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float distanceToLight = Vector3.Distance(spriteRenderer.transform.position, lightPosition);
            float intensity = reverseLighting ? Mathf.Clamp01(distanceToLight / detectionRange) : Mathf.Clamp01(1 - distanceToLight / detectionRange);

            Color originalColor = spriteRenderer.color;
            Color[] colours = new Color[2];
            colours[0] = originalColor;
            colours[1] = lightColor;
            Color newColor = AverageColour(colours) * (intensity);
            newColor.a = spriteRenderer.color.a;
            spriteRenderer.color = newColor;
        }
    }


    public void AddTile(GameObject tile)
    {
        if (!tiles.Contains(tile))
            tiles.Add(tile);
    }

    public void RemoveTile(GameObject tile)
    {
        tiles.Remove(tile);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        foreach (var source in lightSources)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(source.lightSource.transform.position, source.tileFalloffRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(source.lightSource.transform.position, source.spriteDetectionRange);
        }
    }
#endif
}
