using UnityEngine;
using System.Collections;

public class RippleEffect : MonoBehaviour {
    MeshFilter meshFilt;
    Mesh mesh;

    Vector3[] outsideCircle;
    Vector3[] insideCircle;

    Vector3[] archived;

    int[] tris;

    public int numberOfRipples = 5;
    public int vertsPerCircle = 20;

    int rippleCount;

    public float rippleTime = 10.0f;
    public float rippleMaxSize = 10.0f;
    public float rippleThickness = 2.0f;

    float curTime;

	void Start () {
        CreateRingMesh();
	}

    void CreateVertices ()
    {

        insideCircle = new Vector3[vertsPerCircle];
        outsideCircle = new Vector3[vertsPerCircle];

        archived = new Vector3[vertsPerCircle * 2];

        for (int index = 0; index < vertsPerCircle; ++index)
        {
            float curAngle = Mathf.PI * 2 * index / (vertsPerCircle - 1);
            float x = Mathf.Cos(curAngle);
            float y = Mathf.Sin(curAngle);

            insideCircle[index] = new Vector3(x, 0, y);
            outsideCircle[index] = new Vector3(x, 0, y) * 2;

            archived[index] = insideCircle[index];
            archived[index + vertsPerCircle] = outsideCircle[index];
        }
    }

    void CreateTris ()
    {
        tris = new int[vertsPerCircle * 6];

        for (int index = 0; index < vertsPerCircle - 1; ++index)
        {
            int curTris = index * 6;
            int innerTris = index;
            int outerTris = innerTris + outsideCircle.Length;

            tris[curTris++] = innerTris;
            tris[curTris++] = outerTris + 1;
            tris[curTris++] = outerTris;

            tris[curTris++] = innerTris + 1;
            tris[curTris++] = outerTris + 1;
            tris[curTris++] = innerTris;
        }
    }

    void CreateRingMesh ()
    {
        vertsPerCircle++;

        CreateVertices();

        CreateTris();

        mesh = new Mesh();

        mesh.vertices = archived;

        mesh.triangles = tris;

        meshFilt = GetComponent<MeshFilter>();

        meshFilt.mesh = mesh;

        meshFilt.mesh.RecalculateNormals();
    }

    void UpdateMesh (float radius)
    {
        float inner = Mathf.Clamp(radius - rippleThickness, 0, rippleMaxSize - rippleThickness);
        float outer = Mathf.Clamp(radius, 0, rippleMaxSize - rippleThickness);

        for (int index = 0; index < vertsPerCircle; ++index)
        {
            archived[index] = insideCircle[index] * inner;
            archived[index + vertsPerCircle] = outsideCircle[index] * outer / 2;
        }

        meshFilt.mesh.vertices = archived;
    }
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime * numberOfRipples;

        if(curTime > rippleTime)
        {
            curTime = 0;
            rippleCount++;
        }

        if (rippleCount > numberOfRipples)
            Destroy(this.gameObject);

        float magnitude = Mathf.Clamp(curTime, 0, rippleTime) / rippleTime;

        UpdateMesh(magnitude * rippleMaxSize);
	}
}
