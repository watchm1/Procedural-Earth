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
        [HideInInspector] public enum FaceRenderMask{All, Top, Bottom, Left, Right, Front, Back};
        [SerializeField] public FaceRenderMask faceRenderMask;
        [SerializeField][Range(2,256)] public int resolution = 10;
        [SerializeField, HideInInspector]private MeshFilter[] meshFilters;
        [SerializeField] public ShapeSettings shapeSettings;
        [SerializeField] public ColorSettings colorSettings;
        [SerializeField] public bool ShowEditorColorSettings;
        [SerializeField] public bool ShowEditorShapeSettings;
        private TerrainFace[] terrainFaces;
        private ShapeGenerator shapeGenerator = new ShapeGenerator();
        private ColorGenerator colorGenerator = new ColorGenerator();

        void Awake()
        {
            GeneratePlanet();
        }
        private  void Initialize() {

            shapeGenerator.UpdateSettings(shapeSettings);
            colorGenerator.UpdateSettings(colorSettings);
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

                    obj.AddComponent<MeshRenderer>();
                    meshFilters[i] = obj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }
                meshFilters[i].GetComponent<MeshRenderer>().material = colorSettings.planetMaterial;
                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
                bool renderFace = faceRenderMask == FaceRenderMask.All || (int) faceRenderMask -1 == i;
                meshFilters[i].gameObject.SetActive(renderFace);
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
            for(int i = 0; i < faceAmount; i++)
            {
                if(meshFilters[i].gameObject.activeSelf)
                {
                   terrainFaces[i].ConstructMesh(); 
                }
            }
            colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
        }
        void GenerateColors()
        {
            colorGenerator.UpdateColors();
            for(int i = 0; i < faceAmount; i++)
            {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
            
        }
    }
}
