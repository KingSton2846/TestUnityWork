using NUnit.Framework;
using UnityEngine;

public class InputDataTests
{
    [Test]
    public void NetworkInputData_DefaultValues_AreZeroAndFalse()
    {
        var inputData = new NetworkInputData();

        Assert.AreEqual(Vector3.zero, inputData.Direction, "Вектор направления по умолчанию должен быть нулевым.");
        Assert.IsFalse(inputData.IsShootPressed, "Кнопка выстрела по умолчанию не должна быть нажата.");
    }
}