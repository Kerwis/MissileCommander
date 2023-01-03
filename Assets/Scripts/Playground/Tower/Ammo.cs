using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public bool CanShot => _availableAmmo > 0;

    private TowerData _data;
    private List<GameObject> _AmmoGameObjects = new();
    private int _availableAmmo;

    public void Setup(TowerData towerData)
    {
        _data = towerData;
        _availableAmmo = towerData.AmmoCount;
        SpawnAmmoUI();
    }

    public void Shot()
    {
        if (!CanShot)
        {
            Debug.LogError("Can't shot");
            return;
        }
        RemoveAmmoUI();
    }

    private void SpawnAmmoUI()
    {
        GameObject newAmmo;
        int row = 0;
        for (int i = 0; i < _data.AmmoCount;)
        {
            newAmmo = Instantiate(_data.AmmoUIPrefab, transform);
            newAmmo.transform.localPosition = new Vector3(i * 0.1f, row * -0.25f);
            _AmmoGameObjects.Add(newAmmo);
            if (++i >= _data.MissileRows.SumIntArray(row))
                row++;
        }
    }

    private void RemoveAmmoUI()
    {
        _availableAmmo--;
        var ammo = _AmmoGameObjects.Last();
        _AmmoGameObjects.Remove(ammo);
        Destroy(ammo);
    }
}

static class Extension
{
    public static int SumIntArray(this int[] array, int index)
    {
        if (index >= array.Length)
            return 0;
        if (index == 0)
            return array[index];
        return array[index] + array.SumIntArray(--index);
    }
}
