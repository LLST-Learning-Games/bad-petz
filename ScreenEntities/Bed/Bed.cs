using Godot;

public partial class Bed : ScreenEntity
{
    [Export] private Pet _bedOwner;
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
        {
            //_animationPlayer.Play(_happyAnimationKey);
            _bedOwner.TryCallPet(_sprite.GlobalPosition);
        }
    }
}