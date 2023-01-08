using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    public class TerrainFace
    {
        private ShapeGenerator generator;
        private Mesh mesh;
        private int resolution;
        private Vector3 localUp;
        private Vector3 axisA;
        private Vector3 axisB;


        public TerrainFace(ShapeGenerator generator, Mesh mesh, int resolution, Vector3 localUp)
        {
            this.mesh = mesh;
            this.resolution = resolution;
            this.localUp = localUp;
            this.generator = generator;

            axisA = new Vector3(localUp.y, localUp.z, localUp.x);
            axisB = Vector3.Cross(localUp, axisA);   
        }
        public void ConstructMesh()
        {
            Vector3[] vertices = new Vector3[resolution * resolution];
            int[] triangles = new int[(resolution -1) * (resolution -1) * 6];
            int triIndex = 0;
            Vector2[] uv = (mesh.uv.Length == vertices.Length) ? mesh.uv : new Vector2[vertices.Length];
            for(int y = 0; y < resolution; y++)
            {
                for(int x = 0; x < resolution; x++)
                {
                    int i= x+y*resolution;
                    Vector2 percent = new Vector2(x,y)/(resolution -1);
                    Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 *axisA + (percent.y - .5f) *2 * axisB;
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                    float unscaledElevation = generator.CalculateUnscaledElevation(pointOnUnitSphere);
                    vertices[i] = pointOnUnitSphere * generator.GetScaledElevation(unscaledElevation);
                    uv[i].y = unscaledElevation;

                    if(x != resolution-1  && y != resolution -1)
                    {
                        triangles[triIndex] = i;
                        triangles[triIndex + 1] = i+resolution+1;
                        triangles[triIndex + 2] = i+resolution;

                        triangles[triIndex + 3] = i;
                        triangles[triIndex + 4] = i + 1;
                        triangles[triIndex + 5] = i + resolution + 1;
                        triIndex += 6;
                    }
                }
            }
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.uv = uv;
        }
        public void UpdateUVs(ColorGenerator generator)
        {
            Vector2[] uv = mesh.uv;
             for(int y = 0; y < resolution; y++)
            {
                for(int x = 0; x < resolution; x++)
                {
                    int i= x+y*resolution;
                    Vector2 percent = new Vector2(x,y)/(resolution -1);
                    Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 *axisA + (percent.y - .5f) *2 * axisB;
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                    uv[i].x = generator.BiomePercentFromPoint(pointOnUnitSphere);
                }
            }
            mesh.uv = uv;
        }
    }
}

