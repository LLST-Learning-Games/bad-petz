using Godot;


public partial class ScreenEntity : Window
{
    [Export] protected AnimatedSprite2D _sprite;  
    [Export] protected AnimationPlayer _animationPlayer;
    [Export] protected float _scale = 3f;
    
    public override void _Ready()
    {
        _sprite.Scale = new Vector2(_scale, _scale);
        GetTree().GetRoot().SetTransparentBackground(true);
        this.SetTransparentBackground(true);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Transparent, true);
        SetPassthrough();
    }

    protected void SetPassthrough()
    {
        int currentFrame = _sprite.Frame;
        StringName currentAnimation = _sprite.GetAnimation();
        Vector2 textureCentre = _sprite.SpriteFrames.GetFrameTexture(currentAnimation, currentFrame).GetSize() / 2f * _scale;
        Vector2 position = _sprite.GlobalPosition;
        MousePassthroughPolygon =
        [
            position + (textureCentre * new Vector2(-1, -1)),
            position + (textureCentre * new Vector2(1, -1)),
            position + (textureCentre * new Vector2(1, 1)),
            position + (textureCentre * new Vector2(-1, 1))
        ];
    }
}