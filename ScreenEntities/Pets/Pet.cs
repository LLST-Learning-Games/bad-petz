using Godot;
using System;

public partial class Pet : ScreenEntity
{
    [Export] private string _happyAnimationKey = "Happy_React";
    [Export] private float _speed = 1f;
    [Export] private float _stopCursorDistance = 200f;
    [Export] private float _stopDestinationDistance = 10f;
    [Export] private Vector2 _destination = Vector2.Zero;

    private bool _isWaitingAtLocation = false;

    public void SetIsWaitingAtLocation(bool isWaitingAtLocation) => _isWaitingAtLocation = isWaitingAtLocation;
    public bool IsWaitingAtLocation => _isWaitingAtLocation;
    public Vector2 GlobalPosition => _sprite.GlobalPosition;

    
    public override void _PhysicsProcess(double delta)
    {
        if (_destination != Vector2.Zero
            && _destination.DistanceTo(_sprite.GlobalPosition) > _stopDestinationDistance 
            && !_animationPlayer.IsPlaying())
        {
            MovePetToLocation(_destination);
            return;
        }
        
        _destination = Vector2.Zero;
        
        Vector2 mousePosition = GetMousePosition();
        if(!_isWaitingAtLocation 
           && mousePosition.DistanceTo(_sprite.GlobalPosition) > _stopCursorDistance 
           && !_animationPlayer.IsPlaying())
        {
            MovePetToLocation(mousePosition);
        }
        else if (_sprite.GetAnimation() != _happyAnimationKey)
        {
            _sprite.SetAnimation("default");
        }
    }

    private void MovePetToLocation(Vector2 mousePosition)
    {
        Vector2 direction = (mousePosition - _sprite.GlobalPosition).Normalized();
        _sprite.GlobalPosition += direction * _speed;

        _sprite.FlipH = direction.X < 0;
        _sprite.SetAnimation("walk");
        _sprite.Play();
            
        SetPassthrough();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
        {
            //_animationPlayer.Play(_happyAnimationKey);
            
            _sprite.SetAnimation(_happyAnimationKey);
            _sprite.Play();
            SetIsWaitingAtLocation(false);
        }
    }

    public bool TryCallPet(Vector2 destination)
    {
        GD.Print($"[{GetType().Name}] Pet called to destination {destination}");
        _destination = destination;
        return true;
    }
}

