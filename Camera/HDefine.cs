namespace Camera
{
    public enum HPixelType { @byte, int1, int2, uint2, int4, real, direction, cyclic, }

    public enum HColorFormat
    {
        rgb, bgr, rgbx, bgrx, rgb48, bgr48, rgbx64, bgrx64,
        rgb555, bgr555, rgb565, bgr565, rgb555le, bgr555le,
        rgb565le, bgr565le, rgb555be, bgr555be, rgb565be, bgr565be,
        rgb5551, bgr5551, rgb5551le, bgr5551le, rgb5551be, bgr5551be
    }
}
