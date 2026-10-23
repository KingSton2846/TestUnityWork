using NUnit.Framework;

public class HealthTests
{
    [TestCase(100, 20, 80)] // Обычный урон
    [TestCase(10, 50, 0)]   // Летальный урон (не должно уходить в минус)
    [TestCase(50, 0, 50)]   // Урон 0
    [TestCase(0, 10, 0)]    // Урон уже мертвому
    public void CalculateNewHealth_ReturnsCorrectValue(int current, int damage, int expected)
    {
        int result = Heath.CalculateNewHealth(current, damage);

        Assert.AreEqual(expected, result);
    }
}