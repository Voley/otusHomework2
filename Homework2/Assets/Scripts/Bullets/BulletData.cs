using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletData
{
    public Vector2 position;
    public Vector2 velocity;
    public int physicsLayer;
    public int damage;
    public bool isPlayer;
    public Color color;

    public BulletData(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage, bool isPlayer) {
        this.position = position;
        this.velocity = velocity;
        this.color = color;
        this.physicsLayer = physicsLayer;
        this.damage = damage;
        this.isPlayer = isPlayer;
    }

    public static BulletData EnemyBulletData(Vector2 position, Vector2 direction)
    {
        return new BulletData(position, direction * 2f, Color.red, (int)PhysicsLayer.ENEMY, 1, false);
    }

    public static BulletData PlayerBulletData(BulletConfig config, Vector2 position, Vector2 velocity)
    {
        return new BulletData(position, velocity, config.color, (int)config.physicsLayer, config.damage, true);
    }
}