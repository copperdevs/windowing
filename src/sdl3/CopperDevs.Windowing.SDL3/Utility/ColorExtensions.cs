namespace CopperDevs.Windowing.SDL3.Utility;

public static class ColorExtensions
{
    extension(SDL_FColor)
    {
        public static SDL_FColor LightGray => Create(200, 200, 200, 255);
        public static SDL_FColor Gray => Create(130, 130, 130, 255);
        public static SDL_FColor DarkGray => Create(80, 80, 80, 255);
        public static SDL_FColor Yellow => Create(253, 249, 0, 255);
        public static SDL_FColor Gold => Create(255, 203, 0, 255);
        public static SDL_FColor Orange => Create(255, 161, 0, 255);
        public static SDL_FColor Pink => Create(255, 109, 194, 255);
        public static SDL_FColor LightRed => Create(240, 60, 72, 255);
        public static SDL_FColor Red => Create(230, 41, 55, 255);
        public static SDL_FColor Maroon => Create(190, 33, 55, 255);
        public static SDL_FColor Green => Create(0, 228, 48, 255);
        public static SDL_FColor Lime => Create(0, 158, 47, 255);
        public static SDL_FColor DarkGreen => Create(0, 117, 44, 255);
        public static SDL_FColor SkyBlue => Create(102, 191, 255, 255);
        public static SDL_FColor Blue => Create(0, 121, 241, 255);
        public static SDL_FColor DarkBlue => Create(0, 82, 172, 255);
        public static SDL_FColor Purple => Create(200, 122, 255, 255);
        public static SDL_FColor Violet => Create(135, 60, 190, 255);
        public static SDL_FColor DarkPurple => Create(112, 31, 126, 255);
        public static SDL_FColor Beige => Create(211, 176, 131, 255);
        public static SDL_FColor Brown => Create(127, 106, 79, 255);
        public static SDL_FColor DarkBrown => Create(76, 63, 47, 255);
        public static SDL_FColor White => Create(255, 255, 255, 255);
        public static SDL_FColor Black => Create(0, 0, 0, 255);
        public static SDL_FColor Blank => Create(0, 0, 0, 0);
        public static SDL_FColor Magenta => Create(255, 0, 255, 255);
    }

    private static SDL_FColor Create(int r, int g, int b, int a)
    {
        return new SDL_FColor()
        {
            r = r / 255f,
            g = g / 255f,
            b = b / 255f,
            a = a / 255f,
        };
    }
}