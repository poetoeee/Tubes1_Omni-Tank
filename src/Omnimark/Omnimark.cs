using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Omnimark : Bot
{
    bool movingForward;
    int turnDirection = 1;
    static void Main()
    {
        new Omnimark().Start();
    }
    
    Omnimark() : base(BotInfo.FromFile("Omnimark.json")) { }
    
    public override void Run()
    {
        BodyColor = Color.Yellow;
        TurretColor = Color.Blue;
        RadarColor = Color.Black;
        BulletColor = Color.Yellow;
        ScanColor = Color.Blue;
        
        movingForward = true;
        
        while (IsRunning)
        {
            // Cek apakah kita terlalu dekat dengan tembok
            if (IsNearWall(20))  // misal margin 80
            {
                // Jika dekat tembok, lakukan manuver khusus
                AvoidWall();
            }
            else
            {
                // Kalau tidak dekat tembok, lakukan gerakan default
                SetTurnRight(5000);
                MaxSpeed = 5;
                Forward(10000);
            }
        }
    }
    
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firePower = DetermineFirePower(distance);
        
        if (distance <= 50){
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Fire(firePower);
            Forward(distance+5);
        }
        else if (distance <= 100)
        {
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance*0.5);
        }
        else if (distance <= 800)
        {
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance*0.3);
        }
        else
        {
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Fire(firePower);
            Forward(distance*0.2);
        }
    }
    
    public override void OnHitWall(HitWallEvent e)
    {
        ReverseDirection();
    }
    public override void OnHitBot(HitBotEvent e)
    {
        var bearing = BearingTo(e.X, e.Y);
        if (bearing > -10 && bearing < 10)
        {
            Fire(3);
        }
        if (e.IsRammed)
        {
            TurnLeft(10);
            ReverseDirection();
        }
    }
    
    private void ReverseDirection()
    {
        if (movingForward)
        {
            SetBack(200);
            movingForward = false;
        }
        else
        {
            SetForward(200);
            movingForward = true;
        }
    }

    private void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        if (bearing >= 0)
            turnDirection = 1;
        else
            turnDirection = -1;

        TurnLeft(bearing);
    }
    private double DetermineFirePower(double distance)
    {
        // **Jika energi bot di atas 50, gunakan strategi normal**
        if (Energy > 60)
        {
            if (distance <= 500) return 3;
            return 2; // Jarak jauh, hemat energi
        }

        // **Jika energi bot mulai menipis, lebih hemat energi**
        if (Energy > 20)
        {
            if (distance <= 50) return 3;
            if (distance <= 100) return 2;
            return 1; // Jarak jauh, sangat hemat energi
        }

        // **Jika energi sangat rendah, hanya tembak jika perlu**
        if (Energy > 10)
        {
            if (distance <= 50) return 2;
            return 1; // Sangat hemat energi
        }

        // **Jika energi hampir habis, sangat defensif**
        return 0.5; // Tidak menembak untuk menghindari eliminasi cepat
    }
    private bool IsNearWall(double margin)
    {
        // Ambil ukuran arena dari base class
        double arenaWidth = ArenaWidth;
        double arenaHeight = ArenaHeight;
        
        // Jika X atau Y sudah mendekati tepi (kurang dari margin atau melebihi (arena - margin))
        if (X <= margin || X >= (arenaWidth - margin)) return true;
        if (Y <= margin || Y >= (arenaHeight - margin)) return true;
        return false;
    }

    // Manuver untuk menjauhi tembok
    private void AvoidWall()
    {
        // Pilih cara sederhana: putar 90 derajat & mundur sedikit
        TurnRight(90);
        Back(100);
    }
}
