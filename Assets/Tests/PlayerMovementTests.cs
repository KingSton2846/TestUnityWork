using NUnit.Framework;
using UnityEngine;

public class PlayerMovementTests
{
    [Test]
    public void InputDirection_Normalization_MakesLengthEqualOne()
    {
        Vector3 rawInput = new Vector3(1, 0, 1); // Нажали W и D одновременно

        Vector3 normalizedInput = rawInput.normalized;

        Assert.AreEqual(1f, normalizedInput.magnitude, 0.001f, "Длина вектора должна быть равна 1.");
        Assert.AreEqual(0.707f, normalizedInput.x, 0.01f, "Значение X должно быть нормализовано.");
        Assert.AreEqual(0.707f, normalizedInput.z, 0.01f, "Значение Z должно быть нормализовано.");
    }
}