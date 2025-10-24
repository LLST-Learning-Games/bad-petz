using Godot;

public partial class Bed : ScreenEntity
{
    [Export] private Pet _bedOwner;
    [Export] private float _waitDistance = 20f;
    
    public override void _UnhandledInput(InputEvent @event)
    {
        // this is a bit of a hack while we get things working
        _bedOwner ??= _container.GetEntity("Pet") as Pet;
        
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
        {
            if(_bedOwner.GlobalPosition.DistanceTo(_sprite.GlobalPosition) > _waitDistance)
            {
                _bedOwner.TryCallPet(_sprite.GlobalPosition);
                _bedOwner.SetIsWaitingAtLocation(true);
            }
            else
            {
                _bedOwner.SetIsWaitingAtLocation(false);
            }
        }
    }
}