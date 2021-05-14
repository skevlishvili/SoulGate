﻿using System.Collections.Generic;

public struct ParameterUpdates
{
    public List<LayerWeightUpdate> LayerWeights;
    public List<SpeedUpdate> Speeds;
    public List<LayerStateUpdate> LayerStates;
    public List<BooleanUpdate> Bools;
    public List<FloatUpdate> Floats;
    public List<IntUpdate> Ints;
    public List<TriggerUpdate> Triggers;

    public void MakeNewList()
    {
        LayerWeights = new List<LayerWeightUpdate>() { new LayerWeightUpdate() };
        Speeds = new List<SpeedUpdate>() { new SpeedUpdate() };
        LayerStates = new List<LayerStateUpdate>() { new LayerStateUpdate() };
        Bools = new List<BooleanUpdate>() { new BooleanUpdate() };
        Floats = new List<FloatUpdate>() { new FloatUpdate() };
        Ints = new List<IntUpdate>() { new IntUpdate() };
        Triggers = new List<TriggerUpdate>() { new TriggerUpdate() };
    }

}

public struct LayerWeightUpdate
{
    public byte Layer;
    public float Weight;

    public LayerWeightUpdate(byte layer, float weight)
    {
        Layer = layer;
        Weight = weight;
    }
}

public struct SpeedUpdate
{
    public float Speed;

    public SpeedUpdate(float speed)
    {
        Speed = speed;
    }
}

public struct LayerStateUpdate
{
    public byte Layer;
    public int Hash;
    public float Time;

    public LayerStateUpdate(byte layer, int hash, float time)
    {
        Layer = layer;
        Hash = hash;
        Time = time;
    }
}

public struct BooleanUpdate
{
    public byte ParameterIndex;
    public bool Value;

    public BooleanUpdate(byte parameterIndex, bool value)
    {
        ParameterIndex = parameterIndex;
        Value = value;
    }
}

public struct FloatUpdate
{
    public byte ParameterIndex;
    public float Value;

    public FloatUpdate(byte parameterIndex, float value)
    {
        ParameterIndex = parameterIndex;
        Value = value;
    }
}

public struct IntUpdate
{
    public byte ParameterIndex;
    public int Value;

    public IntUpdate(byte parameterIndex, int value)
    {
        ParameterIndex = parameterIndex;
        Value = value;
    }
}

public struct TriggerUpdate
{
    public byte ParameterIndex;
    public bool Set;

    public TriggerUpdate(byte parameterIndex, bool set)
    {
        ParameterIndex = parameterIndex;
        Set = set;
    }
}
