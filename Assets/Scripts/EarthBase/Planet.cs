using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] public bool autoUpdate = true;
        [SerializeField] public int faceAmount;
        [SerializeField][Range(2,256)] public int resolution = 10;
        [SerializeField, HideInInspector]private MeshFilter[] meshFilters;
        [SerializeField] public ShapeSettings shapeSettings;
        [SerializeField] public ColorSettings colorSettings;
        [SerializeField] public bool ShowEditorColorSettings;
        [SerializeField] public bool ShowEditorShapeSettings;
        private TerrainFace[] terrainFaces;
        private ShapeGenerator shapeGenerator;

        private  void Initialize() {

            shapeGenerator = new ShapeGenerator(shapeSettings);
            if(meshFilters == null ||meshFilters.Length == 0)
            {
                meshFilters = new MeshFilter[faceAmount];
            }
            
            terrainFaces = new TerrainFace[faceAmount];
            Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
            for(int i = 0; i < faceAmount; i++)
            {
                if(meshFilters[i] == null)
                {
                    GameObject obj = new GameObject("mesh");
                    obj.transform.parent = transform;

                    obj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    meshFilters[i] = obj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }
                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            }

            
        } 
        public void GeneratePlanet()
        {
            Initialize();
            GenerateMesh();
            GenerateColors();
        }
        public void OnColorSettingsUpdated()
        {
            if(autoUpdate)
            {
                Initialize();
                GenerateColors();
            }
            
        }
        public void OnShapeSettingsUpdated()
        {
            if(autoUpdate)
            {
                Initialize();
                GenerateMesh();
            }
        }
        private void GenerateMesh()
        {
            foreach(var item in terrainFaces)
            {
                item.ConstructMesh();
            }
        }
        void GenerateColors()
        {
            foreach(var item in meshFilters)
            {
                item.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
            }
        }
    }
}
