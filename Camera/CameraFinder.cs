using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;
using HalconDotNet;

namespace HalconCamera
{
    


    public static class CameraFinder
    {
        public enum Finder { Halcon, Pylon }

        public static dynamic Find(Finder finder)
        {
            switch (finder)
            {
                case Finder.Halcon: return HalconFind();
                case Finder.Pylon: return PylonFind();
                default: return null;
            }
        }


        private static List<ICameraInfo> PylonFind()
        {
            return Basler.Pylon.CameraFinder.Enumerate();
        }


        private static List<HInterfaceInfo> HalconFind()
        {
            List<HInterfaceInfo> cameraInfo = new List<HInterfaceInfo>();
            string[] names = Enum.GetNames(typeof(HInterfaceName));
            //string[] names = HOperatorSet.GetParamInfo("open_framegrabber", "Name", "values").SArr;

            foreach (var name in names)
            {
                try
                {
                    HInterfaceInfo info = new HInterfaceInfo();

                    HTuple information;
                    HOperatorSet.InfoFramegrabber(name, "info_boards", out information, out info.InfoBoards);

                    if (info.InfoBoards.Length > 0)
                    {
                        info.Name = (HInterfaceName)Enum.Parse(typeof(HInterfaceName), name);

                        HOperatorSet.InfoFramegrabber(name, "horizontal_resolution", out information, out info.HorizontalResolution);
                        HOperatorSet.InfoFramegrabber(name, "vertical_resolution", out information, out info.VerticalResolution);
                        HOperatorSet.InfoFramegrabber(name, "image_width", out information, out info.ImageWidth);
                        HOperatorSet.InfoFramegrabber(name, "image_height", out information, out info.ImageHeight);
                        HOperatorSet.InfoFramegrabber(name, "start_row", out information, out info.StartRow);
                        HOperatorSet.InfoFramegrabber(name, "start_column", out information, out info.StartColumn);
                        HOperatorSet.InfoFramegrabber(name, "field", out information, out info.Field);
                        HOperatorSet.InfoFramegrabber(name, "bits_per_channel", out information, out info.BitsPerChannel);
                        HOperatorSet.InfoFramegrabber(name, "color_space", out information, out info.ColorSpace);
                        HOperatorSet.InfoFramegrabber(name, "generic", out information, out info.Generic);
                        HOperatorSet.InfoFramegrabber(name, "external_trigger", out information, out info.ExternalTrigger);
                        HOperatorSet.InfoFramegrabber(name, "camera_type", out information, out info.CameraType);
                        HOperatorSet.InfoFramegrabber(name, "device", out information, out info.Device);
                        HOperatorSet.InfoFramegrabber(name, "port", out information, out info.Port);
                        HOperatorSet.InfoFramegrabber(name, "line_in", out information, out info.LineIn);

                        HOperatorSet.InfoFramegrabber(name, "parameters", out information, out info.Parameters);
                        HOperatorSet.InfoFramegrabber(name, "parameters_readonly", out information, out info.ParametersReadonly);
                        HOperatorSet.InfoFramegrabber(name, "parameters_writeonly", out information, out info.ParametersWriteonly);

                        HOperatorSet.InfoFramegrabber(name, "defaults", out information, out info.Defaults);
                        HOperatorSet.InfoFramegrabber(name, "general", out information, out info.General);
                        HOperatorSet.InfoFramegrabber(name, "revision", out information, out info.Revision);

                        cameraInfo.Add(info);
                    }
                }
                catch (Exception)
                {
                    
                }
            }

            return cameraInfo;
        }
    }
}
