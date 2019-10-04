using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save_Pipilson : MonoBehaviour
{
    public string nome;
    public Vector3 posicao;
    string json;

    void Start()
    {
        //checkpoint
        //moedas
        //combustivel
        //hp
        //puzzles

        if (PlayerPrefs.HasKey(name))
        {
            json = PlayerPrefs.GetString(name);
            JsonUtility.FromJsonOverwrite(json, this);
            transform.position = posicao;
            name = nome;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            posicao = transform.position;
            nome = name;
            json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(nome, json);
        }

        if (Input.GetKey(KeyCode.C))
        {
            JsonUtility.FromJsonOverwrite(json, this);
            transform.position = posicao;
            name = nome;
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}