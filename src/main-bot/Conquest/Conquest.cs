﻿﻿using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Conquest : Bot
{
    double lastEnemyX, lastEnemyY;
    double lastEnemyVelocity;
    double lastEnemyDistance = 0; 

    static void Main()
    {
        new Conquest().Start();
    }

    Conquest() : base(BotInfo.FromFile("Conquest.json")) { }

    public override void Run()
    {
        BodyColor = Color.White;
        TurretColor = Color.WhiteSmoke;
        RadarColor = Color.DarkGray;
        BulletColor = Color.Gold;
        ScanColor = Color.Red;

        AdjustGunForBodyTurn = true;
        AdjustRadarForBodyTurn = true;
        AdjustRadarForGunTurn = true;

        double moveAmount = Math.Max(ArenaWidth, ArenaHeight);
        TurnRight(Direction % 90);
        Forward(moveAmount);
        TurnRight(90);

        while (IsRunning)
        {
            SetTurnRadarRight(360);
            SetForward(40);
            SetTurnRight(40);
            SetForward(40);
            SetTurnLeft(40);


            Go();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // double bearingToEnemy = BearingTo(e.X, e.Y);

        // double radarTurn = Direction + bearingToEnemy - RadarDirection;
        // SetTurnRadarRight(radarTurn);

        double enemyX = e.X;
        double enemyY = e.Y;
        double enemyVelocity = e.Speed; 
        double enemyHeading = e.Direction;

        double predictedX = enemyX + enemyVelocity * Math.Sin(enemyHeading * Math.PI / 180);
        double predictedY = enemyY + enemyVelocity * Math.Cos(enemyHeading * Math.PI / 180);

        double bearing = GunBearingTo(predictedX, predictedY);
        TurnGunLeft(bearing);

        double distance = DistanceTo(predictedX, predictedY);
        if (distance < 500){
            Fire(3);
        }else{
            Fire(2);
        }

        lastEnemyX = enemyX;
        lastEnemyY = enemyY;
        lastEnemyVelocity = enemyVelocity;
        lastEnemyDistance = distance;
    }

    public override void OnHitBot(HitBotEvent e)
    {
        SetBack(50);
        TurnRight(90);
        Go();
    }
}