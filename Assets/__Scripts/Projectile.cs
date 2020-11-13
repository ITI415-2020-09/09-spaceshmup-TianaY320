﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {

    private BoundsCheck bndCheck;
    private Renderer rend;

    [Header("Set Dynamically")]
    public Rigidbody rigid;
    [SerializeField]
    private WeaponType _type;
    public Text scoreGT;

    // This public property masks the field _type and takes action when it is set
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }
    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "0";
    }

    private void Update()
    {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);

        }
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Enemy")
        {
            int score = int.Parse(scoreGT.text);
            score += 10;
            scoreGT.text = score.ToString();

            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }
    }

    ///<summary>
    /// Sets the _type private field and colors this projectile to match the
    /// WeaponDefinition.
    /// </summary>
    /// <param name="eType">The WeaponType to use.</param>
    public void SetType(WeaponType eType)
    {
        // Set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
}
