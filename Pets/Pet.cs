using Godot;
using System;

public partial class Pet : Window
{
    [Export] private AnimatedSprite2D _sprite;
    [Export] private float _speed;
    public override void _Ready()
    {
        GetTree().GetRoot().SetTransparentBackground(true);
        this.SetTransparentBackground(true);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Transparent, true);
        SetPassthrough();
    }

    private void SetPassthrough()
    {
        int currentFrame = _sprite.Frame;
        StringName currentAnimation = _sprite.GetAnimation();
        Vector2 textureCentre = _sprite.SpriteFrames.GetFrameTexture(currentAnimation, currentFrame).GetSize() / 2;
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
        Vector2 direction = (mousePosition - _sprite.GlobalPosition).Normalized();
        _sprite.GlobalPosition += direction * _speed;
        SetPassthrough();
    }
}
