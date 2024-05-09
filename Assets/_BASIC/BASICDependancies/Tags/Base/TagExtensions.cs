
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TagExtensions
{
	
    private static readonly Dictionary<Tag, HashSet<GameObject>> allObjectsWithTag = new Dictionary<Tag, HashSet<GameObject>>();
    
    private static readonly Dictionary<GameObject, Tags> cachedTags = new Dictionary<GameObject, Tags>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        
        

        
        cachedTags.Clear();
    }

	#region FindGameObjectWithTag
    public static GameObject FindWithTag(this GameObject gameObject, Tag tag)
    {
	    CheckFindWithTagArguments(tag);

	    if (!allObjectsWithTag.TryGetValue(tag, out var objectsLookup) || objectsLookup.Count == 0)
	    {
		    throw new NullReferenceException($"No Game Objects found with tag {tag.name}.");
	    }

	    GameObject firstObject = null;
	    foreach (var obj in objectsLookup)
	    {
		    firstObject = obj;
		    break;
	    }

	    return firstObject;
    }
    public static HashSet<GameObject> FindAllWithTag(Tag tag)
    {
	    CheckFindWithTagArguments(tag);

	    if (!allObjectsWithTag.TryGetValue(tag, out var objectsLookup) || objectsLookup.Count == 0)
	    {
	    }

	    return objectsLookup;
    }

    public static bool TryFindWithTag(this GameObject gameObject, Tag tag, out GameObject objectWithTag)
    {
	    CheckFindWithTagArguments(tag);

	    objectWithTag = null;
	    if (!allObjectsWithTag.TryGetValue(tag, out var objectsLookup) || objectsLookup.Count == 0)
	    {
		    return false;
	    }

	    foreach (var obj in objectsLookup)
	    {
		    objectWithTag = obj;
		    break;
	    }
	    return true;
    }
    public static bool TryFindAllWithTag(this GameObject gameObject, Tag tag, out HashSet<GameObject> objectsLookup)
    {
	    CheckFindWithTagArguments(tag);
	    return allObjectsWithTag.TryGetValue(tag, out objectsLookup) && objectsLookup.Count != 0;
    }

    private static void CheckFindWithTagArguments(Tag tag)
    {
	    if (!tag)
		    throw new NullReferenceException("Trying to find with none tag.");
    }
	#endregion

	#region HasTag
    public static bool HasTag(this Component instance, Tag tag)
    {
        return HasTag(instance.gameObject, tag);
    }
    public static bool HasTag(this GameObject gameObject, Tag tag)
    {
        var passed = false;

        if (!TryGetTagComponent(gameObject, out var component))
            return false;

        if (component.GetTags().Contains(tag))
            passed = true;

        return passed;
    }

    public static bool HasOnlyTag(this Component instance, Tag tag)
    {
        return HasOnlyTag(instance.gameObject, tag);
    }
    public static bool HasOnlyTag(this GameObject gameObject, Tag tag)
    {
        var passed = false;

        if (!TryGetTagComponent(gameObject, out var component))
            return false;

        var compareTags = component.GetTags();
        if (compareTags.Count > 1)
            return false;

        if (compareTags[0] == tag)
            passed = true;

        return passed;
    }

    public static bool HasAnyTag(this Component instance, params Tag[] tags)
    {
        return HasAnyTag(instance.gameObject, tags);
    }
    public static bool HasAnyTag(this GameObject gameObject, params Tag[] tags)
    {
        var passed = false;

        if (!TryGetTagComponent(gameObject, out var component))
            return false;

        var compareTags = component.GetTags();

        foreach (var tag in tags)
        {
            if (compareTags.Contains(tag))
                passed = true;
        }

        return passed;
    }

    public static bool HasAllTags(this Component instance, params Tag[] tags)
    {
        return HasAllTags(instance.gameObject, tags);
    }
    public static bool HasAllTags(this GameObject gameObject, params Tag[] tags)
    {
        var passed = true;

        if (!TryGetTagComponent(gameObject, out var component))
            return false;

        var compareTags = component.GetTags();

        foreach (var tag in tags)
        {
            if (!compareTags.Contains(tag))
                passed = false;
        }

        return passed;
    }
    #endregion

    
    
    
    
    public static bool TryGetRootChildWithTag(this Transform transform, Tag tag, out Transform child)
    {
	    for (int i = 0; i < transform.childCount; i++)
	    {
		    child = transform.GetChild(i);
		    if (TryGetTagComponent(child.gameObject, out var component))
			    return true;
	    }

	    child = null;
	    return false;
    }


    internal static void RegisterGameObjectWithTag(this GameObject gameObject, Tag tag)
    {
	    if (!allObjectsWithTag.TryGetValue(tag, out var objectsList))
	    {
		    objectsList = new HashSet<GameObject>();
		    allObjectsWithTag.Add(tag, objectsList);
	    }
		objectsList.Add(gameObject);
    }

    private static bool TryGetTagComponent(GameObject gameObject, out Tags component)
    {
	    if (!cachedTags.TryGetValue(gameObject, out component))
	    {
		    if (!gameObject.TryGetComponent(out component))
			    return false;

		    cachedTags.Add(gameObject, component);
	    }
	    return true;
    }
}
