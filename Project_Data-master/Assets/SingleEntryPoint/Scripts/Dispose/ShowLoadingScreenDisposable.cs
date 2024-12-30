using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLoadingScreenDisposable : IDisposable
{
    private readonly T_Loading _loading;

    public ShowLoadingScreenDisposable(T_Loading loading)
    {
        _loading = loading;
        _loading.Show();
    }

    public void SetLoadingPercent(float percent)
    {
        _loading.SetLoadingPercent(percent);
    }

    public void Dispose()
    {
        _loading.Hide();
    }
}
