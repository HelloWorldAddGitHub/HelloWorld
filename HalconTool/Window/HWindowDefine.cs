using System.Drawing;

namespace Halcon.Window
{
    public enum WindowParam
    {
        save_depth_buffer, plot_quality, interactive_plot,
        axis_captions, caption_color, scale_plot, angle_of_view, display_grid,
        display_axes, window_title, background_color
    }


    public enum HColor
    {
        black, white, red, green, blue, cyan, magenta, yellow, dim_gray, gray,
        light_gray, medium_slate_blue, coral, slate_blue, spring_green,
        orange_red, orange, dark_olive_green, pink, cadet_blue
    }


    public enum GridCenterMode { ImaegCenter, ImageOrigin }
}
