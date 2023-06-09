using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletData
{
    public Vector2 position;
    public Vector2 velocity;
    public Color color;
    public int physicsLayer;
    public int damage;
    public bool isPlayer;

    public BulletData(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage, bool isPlayer) {
        this.position = position;
        this.velocity = velocity;
        this.color = color;
        this.physicsLayer = physicsLayer;
        this.damage = damage;
        this.isPlayer = isPlayer;
       }

    public static BulletData BulletWithConfig(BulletConfig config, Vector2 position, Vector2 velocity, bool isPlayer)
    {
        return new BulletData(position, velocity, config.color, (int)config.physicsLayer, config.damage, isPlayer);
    }
}