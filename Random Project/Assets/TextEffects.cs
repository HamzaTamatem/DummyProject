using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    Mesh mesh;
    Vector3[] vertices;

    private void Start()
    {
        text.textInfo.characterInfo[1].color = new Color(0, 0, 0);

        print(text.textInfo.characterInfo[1].color);
        //print(text.textInfo.characterInfo[5].color);

        //text.canvasRenderer.SetMesh();
    }

    private void Update()
    {
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices = mesh.vertices;
        //Color[] newTextColor = new Color[text.textInfo.characterCount];

        //var meshInfo = text.textInfo.meshInfo[1];
        //meshInfo.colors32[1] = Color.red;

        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Vector3 offset = Wobble(Time.time + i);

        //    vertices[i] = vertices[i] + offset;


        //    //print(text.textInfo.characterInfo[5].vertexIndex);
        //}

        //newTextColor[1] = new Color(0, 0, 0);

        //mesh.colors = newTextColor;
        //mesh.vertices = vertices;
        //text.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 1.1f), Mathf.Cos(time * 2.5f));
    }
}
