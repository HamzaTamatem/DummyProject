using System.Collections;
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

    //IEnumerator ColorRainbow()
    //{

    //    while (true)
    //    {
    //        for (int i = 0; i < text.textInfo.characterCount; ++i)
    //        {
    //            // if the character is a space, the vertexIndex will be 0, which is the same as the first character. If you don't leave here, you'll keep modifying the first character's vertices, I believe this is a bug in the text mesh pro code
    //            if (!text.textInfo.characterInfo[i].isVisible)
    //            {
    //                continue;
    //            }
    //            string hexcolor = Rainbow(text.textInfo.characterCount * 5, i + count + (int)Time.deltaTime);
    //            Color32 myColor32 = hexToColor(hexcolor);
    //            int meshIndex = text.textInfo.characterInfo[i].materialReferenceIndex;
    //            int vertexIndex = text.textInfo.characterInfo[i].vertexIndex;
    //            Color32[] vertexColors = text.textInfo.meshInfo[meshIndex].colors32;
    //            vertexColors[vertexIndex + 0] = myColor32;
    //            vertexColors[vertexIndex + 1] = myColor32;
    //            vertexColors[vertexIndex + 2] = myColor32;
    //            vertexColors[vertexIndex + 3] = myColor32;
    //        }
    //        count++;
    //        txtMshComp.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    //        yield return new WaitForSeconds(refreshSpeed);
    //    }
    //}
}
