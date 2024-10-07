﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstrap
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}