using Godot;

public partial class Bed : ScreenEntity
{
    [Export] private Pet _bedOwner;
    [Export] private float _waitDistance = 20f;
    [Export] private Vector2 _bedHomingOffset;

    private Vector2 _destination => _sprite.GlobalPosition + _bedHomingOffset;
    private bool _isBedBeingMoved = false;
    
    public override void _UnhandledInput(InputEvent @event)
    {
        // this is a bit of a hack while we get things working
        _bedOwner ??= _container.GetEntity("Pet") as Pet;
        
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed )
        {
            
            if(_isBedBeingMoved)
            {
                StopMoveBed();
                return;
            }
            
            if (eventMouseButton.ButtonIndex == MouseButton.Left)
            {
                BringCatToBed();
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.Right)
            {
                if(!_isBedBeingMoved)
                {
                    StartMoveBed();
                }
            }
        }
    }

    private void StartMoveBed()
    {
        _bedOwner.SetIsWaitingAtLocation(false);
        _isBedBeingMoved = true;
    }

    private void StopMoveBed()
    {
        _isBedBeingMoved = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isBedBeingMoved)
        {
            _sprite.GlobalPosition = GetMousePosition();
            SetPassthrough();
        }
    }

    private void BringCatToBed()
    {
        if (_bedOwner.GlobalPosition.DistanceTo(_destination) > _waitDistance)
        {
            _bedOwner.TryCallPet(_destination);
            _bedOwner.SetIsWaitingAtLocation(true);
        }
        else
        {
            _bedOwner.SetIsWaitingAtLocation(false);
        }
    }
}