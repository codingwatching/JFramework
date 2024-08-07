// *********************************************************************************
// # Project: Forest
// # Unity: 2022.3.5f1c1
// # Author: jinyijie
// # Version: 1.0.0
// # History: 2024-06-03  23:06
// # Copyright: 2024, jinyijie
// # Description: This is an automatically generated comment.
// *********************************************************************************

using System;
using JFramework.Core;
using Random = UnityEngine.Random;

namespace JFramework
{
    [Serializable]
    public struct Variable<T>
    {
        public T origin;
        public byte[] buffer;
        public int offset;

        public T Value
        {
            get
            {
                if (offset == 0)
                {
                    Value = default;
                }
                
                var target = new byte[buffer.Length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    target[i] = (byte)(buffer[i] - offset);
                }

                if (!origin.Equals(target.Read<T>()))
                {
                    GlobalManager.Cheat();
                }

                return origin;
            }
            set
            {
                buffer = value.Write();
                origin = buffer.Read<T>();
                offset = Random.Range(1, byte.MaxValue);
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = (byte)(buffer[i] + offset);
                }
            }
        }

        public Variable(T value = default)
        {
            offset = 0;
            buffer = null;
            origin = default;
            Value = value;
        }

        public static implicit operator T(Variable<T> secret)
        {
            return secret.Value;
        }

        public static implicit operator Variable<T>(T value)
        {
            return new Variable<T>(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    [Serializable]
    public struct SecretInt
    {
        public int origin;
        public int buffer;
        public int offset;

        public int Value
        {
            get
            {
                if (offset == 0)
                {
                    Value = 0;
                }

                var target = buffer - offset;
                if (!origin.Equals(target))
                {
                    GlobalManager.Cheat();
                }

                return target;
            }
            set
            {
                origin = value;
                unchecked
                {
                    offset = Random.Range(1, int.MaxValue - value);
                    buffer = value + offset;
                }
            }
        }

        public SecretInt(int value)
        {
            origin = 0;
            buffer = 0;
            offset = 0;
            Value = value;
        }

        public static implicit operator int(SecretInt secret)
        {
            return secret.Value;
        }

        public static implicit operator SecretInt(int value)
        {
            return new SecretInt(value);
        }

        public static implicit operator bool(SecretInt secret)
        {
            return secret.Value != 0;
        }

        public static implicit operator SecretInt(bool secret)
        {
            return new SecretInt(secret ? 1 : 0);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}