using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator: MonoBehaviour
{
    public ServiceLocator Shared => _instance; 

    private readonly List<object> _services = new List<object>();

    private ServiceLocator _instance;

    public List<T> GetServices<T>()
    {
        var result = new List<T>();

        foreach (var service in _services)
        {
            if (service is T tService)
            {
                result.Add(tService);
            }
        }

        return result;
    }

    public object GetService(Type serviceType)
    {
        foreach (var service in _services)
        {
            if (serviceType.IsInstanceOfType(service))
            {
                return service;
            }
        }

        throw new Exception($"Service of type {serviceType.Name} is not found!");
    }

    public T GetService<T>()
    {
        foreach (var service in _services)
        {
            if (service is T result)
            {
                return result;
            }
        }

        throw new Exception($"Service of type {typeof(T).Name} is not found!");
    }

    public void AddService(object service)
    {
        _services.Add(service);
    }

    public void AddServices(IEnumerable<object> services)
    {
        _services.AddRange(services);
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
}
