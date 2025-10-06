using Godot;
using System;

public partial class Pet : Window
{
    [Export] private AnimatedSprite2D _sprite;
    [Export] private AnimationPlayer _animationPlayer;
    [Export] private string _happyAnimationKey = "Happy_React";
    [Export] private float _speed = 1f;
    [Export] private float _stopDistance = 1f;
    
    
    public override void _Ready()
    {
        _sprite.Scale = new Vector2(2, 2);
        GetTree().GetRoot().SetTransparentBackground(true);
        this.SetTransparentBackground(true);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Transparent, true);
        SetPassthrough();
    }

    private void SetPassthrough()
    {
        int currentFrame = _sprite.Frame;
        StringName currentAnimation = _sprite.GetAnimation();
        Vector2 textureCentre = _sprite.SpriteFrames.GetFrameTexture(currentAnimation, currentFrame).GetSize();
        Vector2 position = _sprite.GlobalPosition;
        MousePassthroughPolygon =
        [
            position + (textureCentre * new Vector2(-1, -1)),
            position + (textureCentre * new Vector2(1, -1)),
            position + (textureCentre * new Vector2(1, 1)),
            position + (textureCentre * new Vector2(-1, 1))
        ];
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 mousePosition = GetMousePosition();
        if(mousePosition.DistanceTo(_sprite.GlobalPosition) > _stopDistance 
           && !_animationPlayer.IsPlaying())
        {
            Vector2 direction = (mousePosition - _sprite.GlobalPosition).Normalized();
            _sprite.GlobalPosition += direction * _speed;

            _sprite.FlipH = direction.X < 0;
            _sprite.SetAnimation("walk");
            _sprite.Play();
            
            SetPassthrough();
        }
        else if (_sprite.GetAnimation() != _happyAnimationKey)
        {
            _sprite.SetAnimation("default");
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
        {
            //_animationPlayer.Play(_happyAnimationKey);
            
            _sprite.SetAnimation(_happyAnimationKey);
            _sprite.Play();
        }
    }
}
