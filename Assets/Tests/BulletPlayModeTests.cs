using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BulletPlayModeTests
{
    [UnityTest]
    public IEnumerator Bullet_Spawned_AppliesForwardForce()
    {
        var bulletGO = new GameObject("TestBullet");
        bulletGO.AddComponent<BoxCollider>();
        var rb = bulletGO.AddComponent<Rigidbody>();
        rb.useGravity = false; // Отключаем гравитацию, чтобы не мешала

        var bullet = bulletGO.AddComponent<Bullet>();

        // Принудительно задаем скорость через рефлексию, так как поле private
        var speedField = typeof(Bullet).GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        speedField.SetValue(bullet, 20f);

        // Вызываем Spawned вручную (в реальной игре это делает Fusion)
        bullet.SendMessage("Spawned", SendMessageOptions.DontRequireReceiver);

        // Ждем физический кадр
        yield return new WaitForFixedUpdate();

        // Проверяем, что пуля полетела вперед (по оси Z) со скоростью 20
        Assert.AreEqual(20f, rb.velocity.z, 0.1f, "Пуля должна приобрести скорость 20 по оси Z");

        Object.Destroy(bulletGO);
    }
}