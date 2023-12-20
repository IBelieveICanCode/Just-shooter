using System;

namespace ObjectPool
{

    public interface IFactory<T>
    {
        T Create();
    }

    public interface IFactoryType<T> : IFactory<T>
    {
        T Create(Type type);
    }
}