using Godot;
using System;
using Godot.Collections;

public partial class Container : Window
{
    [Export] private Array<PackedScene> _screenEntityPrefabs;

    private Dictionary<string, ScreenEntity> _screenEntities = new();

    public override void _Ready()
    {
        foreach (var entity in _screenEntityPrefabs)
        {
            var newEntity = entity.Instantiate<ScreenEntity>();
            newEntity.SetContainer(this);
            _screenEntities.Add(newEntity.Id, newEntity);
            AddChild(newEntity);
        }
    }

    public ScreenEntity GetEntity(string name)
    {
        return _screenEntities.ContainsKey(name) ? _screenEntities[name] : null;
    }
}
