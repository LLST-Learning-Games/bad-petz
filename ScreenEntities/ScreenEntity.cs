using Godot;


public partial class ScreenEntity : Window
{
    [Export] protected AnimatedSprite2D _sprite;  
    [Export] protected AnimationPlayer _animationPlayer;
    [Export] protected float _scale = 3f;
    [Export] protected bool _startFocused = false;
    
    public override void _Ready()
    {
        if (_sprite is null)
        {
            DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.MousePassthrough, true);
            return;
        }
        _sprite.Scale = new Vector2(_scale, _scale);
        GetTree().GetRoot().SetTransparentBackground(true);
        this.SetTransparentBackground(true);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Transparent, true);
        SetPassthrough();
        
        if(_startFocused)
        {
            SetFocused();
        }
    }

    private void SetFocused()
    {
        // might have to look into this: https://www.reddit.com/r/godot/comments/1i4s2x2/does_window_set_mouse_passthrough_allow_mouse/
        DisplayServer.WindowMoveToForeground(this.GetWindowId());
    }

    protected void SetPassthrough()
    {
        if (_sprite is null)
        {
            return;
        }
        
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