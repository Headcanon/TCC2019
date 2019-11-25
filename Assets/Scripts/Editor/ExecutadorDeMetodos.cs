using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExecutadorDeMetodos : EditorWindow
{
    private string metodo;

    [MenuItem("Window/ExecutadorDeMetodos")]
    public static void ShowWindow()
    {
        GetWindow<ExecutadorDeMetodos>("ExecutadorDeMetodos");
    }

    public void OnGUI()
    {
        GUILayout.Label("Execute um metod definido!", EditorStyles.boldLabel);

        metodo = EditorGUILayout.TextField("Metodo", metodo);

        if (GUILayout.Button("Executar!"))
        {
            Executar();
        }
    }

    void Executar()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Pontuacao script = obj.GetComponent<Pontuacao>();
            if (script != null)
            {
                script.Invoke(metodo, 0);
            }
        }
    }
}
