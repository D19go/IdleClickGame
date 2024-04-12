using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
///     Gerencia todos os registros do jogo, como a quantidade de dinheiro, clicadores e multiplicadores
/// </summary>
public class GameManager : MonoBehaviour
{
    // Dinheiro
    public static int dinheiro = 0;
    static TextMeshProUGUI textoDinheiro;

    // Clicadores
    public static int clicadores = 0;
    public static int custoClicador = 25;

    // Multiplicadores
    public static int multiplicadores = 1;
    public static int custoMultiplicador = 90;

    // Está sendo usado Awake invés do Start porque a função AddDinheiro é estática e depende que o
    // texto do dinheiro exista para funcionar. Então deve ser iniciado com certeza antes de todos
    private void Awake()
    {
        autoSave();
        // Pega o texto de dinheiro que está na tela
        textoDinheiro = GameObject.Find("Canvas").transform.Find("Dinheiro").GetComponent<TextMeshProUGUI>();
    }

    // A variável de dinheiro poderia ser alterada diretamente, porém ao usar esta função,
    // o texto do Canvas já é atualizado ao mesmo tempo
    public static void AddDinheiro(int valor)
    {
        GameManager.dinheiro += valor;
        textoDinheiro.text = "$ " + GameManager.dinheiro.ToString();
    }
    //----------------------------------------------------------------------------

    public void SalvarGmae()
    {
        DadosJSON dj = new DadosJSON();
        dj.dinheiro = dinheiro;
        dj.clicadores = clicadores;
        dj.multiplicadores = multiplicadores;

        string json = JsonUtility.ToJson(dj);
        string nomeArquivo = "save_Data.JSON";
        string pasta = Application.persistentDataPath;
        string caminho = pasta + Path.AltDirectorySeparatorChar + nomeArquivo;

        File.WriteAllText(caminho, json);
    }

    void autoSave()
    {
        SalvarGmae();   
        Invoke("autoSave", 1);
    }

    void carregaSave()
    {
        string nomeArquivo = "save_Data.JSON";
        string pasta = Application.persistentDataPath;
        string caminho = pasta + Path.AltDirectorySeparatorChar + nomeArquivo;

        string dados = null;

        if (File.Exists(caminho))
        {
            dados = File.ReadAllText(caminho);
        }

        DadosJSON dj = JsonUtility.FromJson<DadosJSON>(dados);

        dinheiro = dj.dinheiro;
        clicadores = dj.clicadores;
        multiplicadores = dj.multiplicadores;

        // PlayerPrefs.DeleteAll();
        //File.Delete(caminho);
    }
}

class DadosJSON
{
    public int dinheiro;
    public int clicadores;
    public int multiplicadores;
}

