/*

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpeechToText : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetTextFromPython());
    }

    IEnumerator GetTextFromPython()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/transcribe");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.ConnectionError && www.result != UnityWebRequest.Result.ProtocolError)
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Texto da voz: " + responseText);

            // Processa o JSON e extrai o resultado da transcrição
            SpeechResult result = JsonUtility.FromJson<SpeechResult>(responseText);
            Debug.Log("Transcrição: " + result.text);
        }
        else
        {
            Debug.LogError("Erro na requisição: " + www.error);
        }
    }
}

// Classe para mapear o JSON retornado pela API Flask
[System.Serializable]
public class SpeechResult
{
    public string text;
}
*/
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpeechToText : MonoBehaviour
{
    void Start()
    {
        // Inicia a requisição contínua para transcrição
        StartCoroutine(ContinuousTranscription());
    }



    IEnumerator ContinuousTranscription()
{
    while (true)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/transcribe");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.ConnectionError && www.result != UnityWebRequest.Result.ProtocolError)
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Texto da voz: " + responseText);

            // Processa o JSON para pegar a transcrição
            SpeechResult result = JsonUtility.FromJson<SpeechResult>(responseText);
            Debug.Log("Transcrição (contínua): " + result.text);
        }
        else
        {
            Debug.LogError("Erro na requisição HTTP: " + www.error);
        }

        // Adicione um atraso maior entre as requisições
        yield return new WaitForSeconds(3f);  // Requisição a cada 1 segundo
    }
}

}

// Classe para mapear o JSON retornado pela API Flask
[System.Serializable]
public class SpeechResult
{
    public string text;
}

