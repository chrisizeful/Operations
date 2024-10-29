/*******************************************************************************
 * Copyright 2011 See AUTHORS file.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 ******************************************************************************/

using System.Collections.Generic;
using System;

namespace Operations;

/// <summary>
/// Provides methods for pooling objects of any type.
/// 
/// All code in this file is modified code from <a href="https://github.com/libgdx/libgdx">libGDX</a>.
/// See the LICENSE file in this folder or visit http://www.apache.org/licenses/LICENSE-2.0 to read more.
/// These classes have simply been translated from Java to C#, with no major functionality changes.
/// </summary>
public static class Pools
{

    private static readonly Dictionary<Type, Pool> _typePools = new();

    public static void Set(Type type, Pool pool) => _typePools[type] = pool;
    public static void Set<T>(Pool pool) where T : class => _typePools[typeof(T)] = pool;

    public static Pool Get(Type type, int max = 32)
    {
        if (_typePools.TryGetValue(type, out var pool))
            return pool;
        pool = new Pool(type, 4, max);
        _typePools[type] = pool;
        return pool;
    }

    public static Pool Get<T>(int max = 32) => Get(typeof(T), max);
    
    public static object Obtain(Type type) => Get(type).Obtain();
    public static T Obtain<T>() => (T) Get<T>().Obtain();

    public static void Free(object obj)
    {
        if (obj == null)
            throw new Exception("object cannot be null.");
        if (_typePools.TryGetValue(obj.GetType(), out var pool)) // Only free an object that was retained
            pool.Free(obj);
    }
}

/// <summary>
/// A collection of freed objects of the same type.
/// </summary>
public class Pool
{

    public Type Type;
    public readonly int Max;
    public int Peak;
    private readonly List<object> _freeObjects = new();

    public Pool(Type type, int initialCapacity = 16, int max = int.MaxValue)
    {
        Type = type;
        _freeObjects = new(initialCapacity);
        Max = max;
    }

    protected object NewObject() => Activator.CreateInstance(Type);

    public object Obtain()
    {
        if (_freeObjects.Count == 0)
            return NewObject();
        object obtained = _freeObjects[0];
        _freeObjects.RemoveAt(0);
        return obtained;
    }

    public void Free(object obj)
    {
        if (obj == null)
            throw new Exception("object cannot be null.");
        if (_freeObjects.Count < Max)
        {
            _freeObjects.Add(obj);
            Peak = Math.Max(Peak, _freeObjects.Count);
            Reset(obj);
        }
        else
        {
            Reset(obj);
            Discard(obj);
        }
    }

    public void Fill(int size)
    {
        for (int i = 0; i < size; i++)
            if (_freeObjects.Count < Max)
                _freeObjects.Add(NewObject());
        Peak = Math.Max(Peak, _freeObjects.Count);
    }

    protected void Reset(object obj)
    {
        if (obj is IPoolable pl)
            pl.Reset();
    }

    protected void Discard(object obj) {}

    public void FreeAll(List<object> objects)
    {
        if (objects == null)
            throw new Exception("objects cannot be null.");
        for (int i = 0, n = objects.Count; i < n; i++)
        {
            object obj = objects[i];
            if (obj == null)
                continue;
            if (_freeObjects.Count < Max)
            {
                _freeObjects.Add(obj);
                Reset(obj);
            }
            else
            {
                Reset(obj);
                Discard(obj);
            }
        }
        Peak = Math.Max(Peak, _freeObjects.Count);
    }

    public void Clear()
    {
        for (int i = 0; i < _freeObjects.Count; i++)
        {
            object obj = _freeObjects[0];
            _freeObjects.RemoveAt(0);
            Discard(obj);
        }
    }

    public int GetFree() => _freeObjects.Count;

}

/// <summary>
/// Objects can inherit from this to implement a method that will be called when it is freed and enters a pool.
/// </summary>
public interface IPoolable
{

    void Reset();
}
