using System;
using System.Collections.Generic;
using System.Reflection;
using LabApi.Features.Console;
using NetworkManagerUtils.Dummies;

namespace RaCustomMenuLabApi.API;

public abstract class Provider
{
    public static readonly List<Provider> providersLoaded = new List<Provider>();
    
    public abstract string CategoryName { get; }
    
    public abstract bool IsDirty { get; }
    
    public abstract List<DummyAction> AddAction(ReferenceHub hub);
    
    
    public static void RegisterDynamicProvider(string categoryName, bool isDirty, Func<ReferenceHub, List<DummyAction>> actionGenerator)
    {
        var dynamicProvider = new DynamicProvider(categoryName, isDirty, actionGenerator);
        RegisterProviders(dynamicProvider);
    }

    public static void RegisterAllProviders() => RegisterProviders(Assembly.GetCallingAssembly());

    private static void RegisterProviders(Assembly assembly)
    {
        List<Provider> providers = new();

        foreach (Type type in assembly.GetTypes())
        {
            if (!type.IsAbstract && typeof(Provider).IsAssignableFrom(type) && type != typeof(DynamicProvider))
            {
                try
                {
                    Provider provider = Activator.CreateInstance(type) as Provider;
                    if (provider != null)
                        providers.Add(provider);
                }
                catch (Exception ex)
                {
                    Logger.Error($"Erreur lors de l'instanciation de {type.Name} : {ex}");
                }
            }
        }

        foreach (Provider provider in providers)
        {
            try
            {
                RegisterProviders(provider);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }

    private static void RegisterProviders(Provider provider)
    {
        if(provider == null)
            return;
        providersLoaded.Add(provider);
        provider.OnRegistered();
    }
    
    protected virtual void OnRegistered(){}
}

public class DynamicProvider : Provider
{
    private readonly string _categoryName;
    private readonly bool _isDirty;
    private readonly Func<ReferenceHub, List<DummyAction>> _actionGenerator;

    public DynamicProvider(string categoryName, bool isDirty, Func<ReferenceHub, List<DummyAction>> actionGenerator)
    {
        _categoryName = categoryName;
        _isDirty = isDirty;
        _actionGenerator = actionGenerator;
    }

    public override string CategoryName => _categoryName;

    public override bool IsDirty => _isDirty;

    public override List<DummyAction> AddAction(ReferenceHub hub)
        => _actionGenerator.Invoke(hub);
}