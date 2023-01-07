using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Global.EarthBase;

namespace Global.EarthBase.Editor {
    
[CustomEditor(typeof(Planet))]
    public class PlanetEditor : UnityEditor.Editor
    {
        Planet planet;
        UnityEditor.Editor shapeEditor;
        UnityEditor.Editor colorEditor; 
        public override void OnInspectorGUI()
        {
            using(var check = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if(check.changed)
                    planet.GeneratePlanet();
            }
            if(GUILayout.Button("GeneratePlanet"))
            {
                planet.GeneratePlanet();
            }
            DrawSettingsEditor(planet.shapeSettings, () => {planet.OnShapeSettingsUpdated();}, ref planet.ShowEditorShapeSettings, ref shapeEditor);
            DrawSettingsEditor(planet.colorSettings, () => {planet.OnColorSettingsUpdated();}, ref planet.ShowEditorColorSettings, ref colorEditor);
        }
        void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated,ref bool foldOut, ref UnityEditor.Editor editor)
        {
            if(settings != null)
            {
                using(var check = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUILayout.InspectorTitlebar(foldOut, settings);
                    if(foldOut)
                    {
                        CreateCachedEditor(settings, null, ref editor);
                        editor.OnInspectorGUI();

                        if(check.changed)
                        {
                            if(onSettingsUpdated != null)
                                onSettingsUpdated();
                        }
                    }
                }
            }
            
        }
        private void OnEnable() 
        {
            planet = (Planet)target;
        }
    }

}