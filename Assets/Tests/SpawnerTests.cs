using NUnit.Framework;

public class SpawnerTests
{
    [TestCase(0, 10, 0f)]  // Игрок 0, макс 10 -> 0 * 3 = 0
    [TestCase(1, 10, 3f)]  // Игрок 1, макс 10 -> 1 * 3 = 3
    [TestCase(5, 10, 15f)] // Игрок 5, макс 10 -> 5 * 3 = 15
    public void CalculateSpawnOffset_ReturnsCorrectX(int playerId, int maxPlayers, float expectedX)
    {
        float calculatedX = (playerId % maxPlayers) * 3;

        Assert.AreEqual(expectedX, calculatedX);
    }
}