using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeCurveManager : MonoBehaviour {    
    
    public static int positionCount = 10;
    static Vector3 curveHeight = new Vector3(0, 50, 0);

    [MenuItem("Worker Empire/Draw Curve %h")]
    private static void DrawCurve()
    {        
        var gos = Selection.gameObjects;
        if (gos.Length == 2)
        {
            Vector3 p0 = gos[0].transform.position;
            Vector3 p2 = gos[1].transform.position;

            //var go = Instantiate<Resources.Load("Prefabs/World/NodeCurve", typeof(GameObject)), GameObject.FindGameObjectWithTag("NodeCurveManager").transform);
            var go = Instantiate(Resources.Load("Prefabs/World/NodeCurve", typeof(GameObject)), GameObject.FindGameObjectWithTag("NodeCurveManager").transform) as GameObject;

            go.name = gos[0].name + "<->" + gos[1].name;

            var lineRenderer = go.GetComponent<LineRenderer>();
            lineRenderer.positionCount = positionCount;
            //p1 is mid point
            var p1 = ((p0 + p2) / 2) + curveHeight;

            for (int i = 0; i < positionCount; i++)
            {
                float t = i / ((float)positionCount - 1);
                var p = CalculateQuadraticBezierPoint(t, p0, p1, p2);
                lineRenderer.SetPosition(i, p);
            }
        }
    }

    static Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        //B(t) = (1-t)^2P0 + 2(1-t)tP1 + t^2P2 , 0 < t < 1
        return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
    }
}
