/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Console : MonoBehaviour
{
    public Text displayer;
    public List<string> lines;
    // Start is called before the first frame update
    void Start()
    {
        lines = new List<string>();
    }
    public void Flush()
    {
        lines.Clear();
    }
    public void addLine(string line)
    {
        lines.Add(line);
    }

    // Update is called once per frame
    void Update()
    {
        string content ="";
        for(int i =lines.Count-1; lines.Count-i<=20 && i >=0 ; i--)
            content += lines[i]+"\n";
        displayer.text = content;
        
    }
}
*/

// Script pour gérer une console d'affichage avec ajout de lignes et mise à jour
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    // Afficheur de texte pour les lignes de la console
    public Text displayer;
    // Liste des lignes affichées
    public List<string> lines;

    // Initialisation de la console
    void Start()
    {
        lines = new List<string>();
    }

    // Efface toutes les lignes de la console
    public void Flush()
    {
        lines.Clear();
    }

    // Ajoute une ligne à la console
    public void addLine(string line)
    {
        lines.Add(line);
    }

    // Met à jour l'affichage : affiche les 20 dernières lignes
    void Update()
    {
        string content = "";
        for (int i = lines.Count - 1; lines.Count - i <= 20 && i >= 0; i--)
        {
            content += lines[i] + "\n";
        }
        displayer.text = content;
    }
}
