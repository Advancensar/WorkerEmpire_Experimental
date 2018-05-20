using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftRecipeWindow : EditorWindow
{
    [MenuItem("Worker Empire/Create Item Recipe")]
    private static void ShowWindow()
    {
        CraftRecipeWindow window = GetWindow<CraftRecipeWindow>();
        
    }
}
