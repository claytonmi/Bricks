using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text;

public class JSONUploader : MonoBehaviour
{
    // URL do servidor para onde você enviará o JSON
    public string serverURL = "leilamd.com.br/backend/main.php";

    // Método chamado quando o botão é pressionado
    public void UploadJSON()
    {
        StartCoroutine(UploadJSONCoroutine());
    }

    IEnumerator UploadJSONCoroutine()
    {
        // Caminho completo do arquivo JSON
        string jsonFilePath = Path.Combine(Application.dataPath, "ranking.json");

        // Verifica se o arquivo JSON existe
        if (File.Exists(jsonFilePath))
        {
            // Lê o conteúdo do arquivo JSON
            byte[] jsonBytes = File.ReadAllBytes(jsonFilePath);

            // Convertendo de Hexadecimal para JSON
            string hexContent = Encoding.UTF8.GetString(jsonBytes);
            string jsonContent = ConverterHexadecimalParaJSON(hexContent);
            byte[] finalJsonBytes = Encoding.UTF8.GetBytes(jsonContent);

            // Cria uma requisição HTTP POST
            UnityWebRequest request = new UnityWebRequest(serverURL, "POST");

            // Define o corpo da requisição com os parâmetros e o arquivo JSON
            string boundary = "boundary";
            byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            string body = "--" + boundary + "\r\n" +
                          "Content-Disposition: form-data; name=\"REQUEST_METHOD\"\r\n\r\n" +
                          "POST\r\n" +
                          "--" + boundary + "\r\n" +
                          "Content-Disposition: form-data; name=\"action\"\r\n\r\n" +
                          "upload-ranking\r\n" +
                          "--" + boundary + "\r\n" +
                          "Content-Disposition: form-data; name=\"file\"; filename=\"ranking.json\"\r\n" +
                          "Content-Type: application/json\r\n\r\n";
            byte[] bodyStartBytes = System.Text.Encoding.UTF8.GetBytes(body);
            byte[] bodyEndBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            // Combina os bytes dos dados do arquivo e os bytes do corpo da requisição
            byte[] postData = new byte[bodyStartBytes.Length + finalJsonBytes.Length + bodyEndBytes.Length];
            System.Array.Copy(bodyStartBytes, postData, bodyStartBytes.Length);
            System.Array.Copy(finalJsonBytes, 0, postData, bodyStartBytes.Length, finalJsonBytes.Length);
            System.Array.Copy(bodyEndBytes, 0, postData, bodyStartBytes.Length + finalJsonBytes.Length, bodyEndBytes.Length);

            // Configura a requisição
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.SetRequestHeader("Content-Type", "multipart/form-data; boundary=" + boundary);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Defina o tempo limite em segundos(por exemplo, 30 segundos)
            request.timeout = 30;

            Debug.Log("Cabeçalhos da solicitação: " + request.GetRequestHeader("Content-Type"));

            // Envia a requisição para o servidor
            yield return request.SendWebRequest();

            Debug.Log("isNetWorkErro:" + request.isNetworkError + " | isHttpErro: " + request.isHttpError);

            // Verifica se houve algum erro durante o envio
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Erro ao enviar o JSON para o servidor: " + request.error);
                // Log do retorno do servidor em caso de erro
                Debug.LogError("Resposta do servidor: " + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("JSON enviado com sucesso para o servidor.");
            }
        }
        else
        {
            Debug.LogError("Arquivo JSON não encontrado em: " + jsonFilePath);
        }
    }

    static string ConverterHexadecimalParaJSON(string hex)
    {
        try
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int byteValue = int.Parse(hex.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                bytes[i] = (byte)byteValue;
            }
            return Encoding.UTF8.GetString(bytes);
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro ao converter texto hexadecimal para JSON: " + ex.Message);
            return null;
        }
    }

}
