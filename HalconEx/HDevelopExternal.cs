using System.Windows.Forms;
using HalconDotNet;

namespace HalconEx
{
    public class HDevelopExternal
    {
        public HTuple hv_ExpDefaultWinHandle;

        public void HDevelopStop()
        {
            MessageBox.Show("Press button to continue", "Program stop");
        }

        HTuple gIsSinglePose;
        HTuple gInfoDecor;
        HTuple gInfoPos;
        HTuple gTitlePos;
        HTuple gTitleDecor;
        HTuple gAlphaDeselected;
        HTuple gTerminationButtonLabel;
        HTuple gDispObjOffset;
        HTuple gLabelsDecor;
        HTuple gUsesOpenGL;

        HTuple ExpGetGlobalVar_gIsSinglePose()
        {
            return gIsSinglePose;
        }
        void ExpSetGlobalVar_gIsSinglePose(HTuple val)
        {
            gIsSinglePose = val;
        }

        HTuple ExpGetGlobalVar_gInfoDecor()
        {
            return gInfoDecor;
        }
        void ExpSetGlobalVar_gInfoDecor(HTuple val)
        {
            gInfoDecor = val;
        }

        HTuple ExpGetGlobalVar_gInfoPos()
        {
            return gInfoPos;
        }
        void ExpSetGlobalVar_gInfoPos(HTuple val)
        {
            gInfoPos = val;
        }

        HTuple ExpGetGlobalVar_gTitlePos()
        {
            return gTitlePos;
        }
        void ExpSetGlobalVar_gTitlePos(HTuple val)
        {
            gTitlePos = val;
        }

        HTuple ExpGetGlobalVar_gTitleDecor()
        {
            return gTitleDecor;
        }
        void ExpSetGlobalVar_gTitleDecor(HTuple val)
        {
            gTitleDecor = val;
        }

        HTuple ExpGetGlobalVar_gAlphaDeselected()
        {
            return gAlphaDeselected;
        }
        void ExpSetGlobalVar_gAlphaDeselected(HTuple val)
        {
            gAlphaDeselected = val;
        }

        HTuple ExpGetGlobalVar_gTerminationButtonLabel()
        {
            return gTerminationButtonLabel;
        }
        void ExpSetGlobalVar_gTerminationButtonLabel(HTuple val)
        {
            gTerminationButtonLabel = val;
        }

        HTuple ExpGetGlobalVar_gDispObjOffset()
        {
            return gDispObjOffset;
        }
        void ExpSetGlobalVar_gDispObjOffset(HTuple val)
        {
            gDispObjOffset = val;
        }

        HTuple ExpGetGlobalVar_gLabelsDecor()
        {
            return gLabelsDecor;
        }
        void ExpSetGlobalVar_gLabelsDecor(HTuple val)
        {
            gLabelsDecor = val;
        }

        HTuple ExpGetGlobalVar_gUsesOpenGL()
        {
            return gUsesOpenGL;
        }
        void ExpSetGlobalVar_gUsesOpenGL(HTuple val)
        {
            gUsesOpenGL = val;
        }

        // Procedures 
        // Chapter: Filters / Lines
        // Short Description: Calculates the parameters Sigma, Low, and High for lines_gauss from the maximum width and the contrast of the lines to be extracted. 
        public void calculate_lines_gauss_parameters(HTuple hv_MaxLineWidth, HTuple hv_Contrast,
            out HTuple hv_Sigma, out HTuple hv_Low, out HTuple hv_High)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ContrastHigh = null, hv_ContrastLow = new HTuple();
            HTuple hv_HalfWidth = null, hv_Help = null;
            HTuple hv_MaxLineWidth_COPY_INP_TMP = hv_MaxLineWidth.Clone();

            // Initialize local and output iconic variables 
            //Check control parameters
            if ((int)(new HTuple((new HTuple(hv_MaxLineWidth_COPY_INP_TMP.TupleLength())).TupleNotEqual(
                1))) != 0)
            {
                throw new HalconException("Wrong number of values of control parameter: 1");
            }
            if ((int)(((hv_MaxLineWidth_COPY_INP_TMP.TupleIsNumber())).TupleNot()) != 0)
            {
                throw new HalconException("Wrong type of control parameter: 1");
            }
            if ((int)(new HTuple(hv_MaxLineWidth_COPY_INP_TMP.TupleLessEqual(0))) != 0)
            {
                throw new HalconException("Wrong value of control parameter: 1");
            }
            if ((int)((new HTuple((new HTuple(hv_Contrast.TupleLength())).TupleNotEqual(1))).TupleAnd(
                new HTuple((new HTuple(hv_Contrast.TupleLength())).TupleNotEqual(2)))) != 0)
            {
                throw new HalconException("Wrong number of values of control parameter: 2");
            }
            if ((int)(new HTuple(((((hv_Contrast.TupleIsNumber())).TupleMin())).TupleEqual(
                0))) != 0)
            {
                throw new HalconException("Wrong type of control parameter: 2");
            }
            //Set and check ContrastHigh
            hv_ContrastHigh = hv_Contrast[0];
            if ((int)(new HTuple(hv_ContrastHigh.TupleLess(0))) != 0)
            {
                throw new HalconException("Wrong value of control parameter: 2");
            }
            //Set or derive ContrastLow
            if ((int)(new HTuple((new HTuple(hv_Contrast.TupleLength())).TupleEqual(2))) != 0)
            {
                hv_ContrastLow = hv_Contrast[1];
            }
            else
            {
                hv_ContrastLow = hv_ContrastHigh / 3.0;
            }
            //Check ContrastLow
            if ((int)(new HTuple(hv_ContrastLow.TupleLess(0))) != 0)
            {
                throw new HalconException("Wrong value of control parameter: 2");
            }
            if ((int)(new HTuple(hv_ContrastLow.TupleGreater(hv_ContrastHigh))) != 0)
            {
                throw new HalconException("Wrong value of control parameter: 2");
            }
            //
            //Calculate the parameters Sigma, Low, and High for lines_gauss
            if ((int)(new HTuple(hv_MaxLineWidth_COPY_INP_TMP.TupleLess((new HTuple(3.0)).TupleSqrt()
                ))) != 0)
            {
                //Note that LineWidthMax < sqrt(3.0) would result in a Sigma < 0.5,
                //which does not make any sense, because the corresponding smoothing
                //filter mask would be of size 1x1.
                //To avoid this, LineWidthMax is restricted to values greater or equal
                //to sqrt(3.0) and the contrast values are adapted to reflect the fact
                //that lines that are thinner than sqrt(3.0) pixels have a lower contrast
                //in the smoothed image (compared to lines that are sqrt(3.0) pixels wide).
                hv_ContrastLow = (hv_ContrastLow * hv_MaxLineWidth_COPY_INP_TMP) / ((new HTuple(3.0)).TupleSqrt()
                    );
                hv_ContrastHigh = (hv_ContrastHigh * hv_MaxLineWidth_COPY_INP_TMP) / ((new HTuple(3.0)).TupleSqrt()
                    );
                hv_MaxLineWidth_COPY_INP_TMP = (new HTuple(3.0)).TupleSqrt();
            }
            //Convert LineWidthMax and the given contrast values into the input parameters
            //Sigma, Low, and High required by lines_gauss
            hv_HalfWidth = hv_MaxLineWidth_COPY_INP_TMP / 2.0;
            hv_Sigma = hv_HalfWidth / ((new HTuple(3.0)).TupleSqrt());
            hv_Help = ((-2.0 * hv_HalfWidth) / (((new HTuple(6.283185307178)).TupleSqrt()) * (hv_Sigma.TuplePow(
                3.0)))) * (((-0.5 * (((hv_HalfWidth / hv_Sigma)).TuplePow(2.0)))).TupleExp());
            hv_High = ((hv_ContrastHigh * hv_Help)).TupleFabs();
            hv_Low = ((hv_ContrastLow * hv_Help)).TupleFabs();

            return;
        }

        // Chapter: Identification / Bar Code
        // Short Description: Convert a decoded string of a bar code of type 'Code 39' to the type 'Code 32'. 
        public void convert_decoded_string_code39_to_code32(HTuple hv_DecodedDataStringCode39,
            out HTuple hv_ConvertedDataStringCode32)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Symbols = null, hv_Digit = null;
            HTuple hv_CheckDigit = null, hv_CheckSum = null, hv_Value = new HTuple();
            // Initialize local and output iconic variables 
            //This procedure converts a decoded string of a 'Code 32'
            //barcode that was read with the bar code reader for 'Code 39'
            //to the 'Code 32' decoding.
            //
            //Basically a 'Code 32' bar code corresponds to a 'Code 39' with
            //8 digits and a checksum digit % 10 whereas even positions are
            //weighted twice.
            //The 9-digit number is represented to the base 32 and written
            //with chars (via the symbol table) analogous to a hexadecimal number.
            //
            //Initialize symbol table
            hv_Symbols = new HTuple();
            hv_Symbols[0] = "0";
            hv_Symbols[1] = "1";
            hv_Symbols[2] = "2";
            hv_Symbols[3] = "3";
            hv_Symbols[4] = "4";
            hv_Symbols[5] = "5";
            hv_Symbols[6] = "6";
            hv_Symbols[7] = "7";
            hv_Symbols[8] = "8";
            hv_Symbols[9] = "9";
            hv_Symbols[10] = "B";
            hv_Symbols[11] = "C";
            hv_Symbols[12] = "D";
            hv_Symbols[13] = "F";
            hv_Symbols[14] = "G";
            hv_Symbols[15] = "H";
            hv_Symbols[16] = "J";
            hv_Symbols[17] = "K";
            hv_Symbols[18] = "L";
            hv_Symbols[19] = "M";
            hv_Symbols[20] = "N";
            hv_Symbols[21] = "P";
            hv_Symbols[22] = "Q";
            hv_Symbols[23] = "R";
            hv_Symbols[24] = "S";
            hv_Symbols[25] = "T";
            hv_Symbols[26] = "U";
            hv_Symbols[27] = "V";
            hv_Symbols[28] = "W";
            hv_Symbols[29] = "X";
            hv_Symbols[30] = "Y";
            hv_Symbols[31] = "Z";
            //
            //Convert the value of each digit in the decoded 'Code 39' string
            hv_ConvertedDataStringCode32 = 0;
            for (hv_Digit = 0; (int)hv_Digit <= 5; hv_Digit = (int)hv_Digit + 1)
            {
                hv_ConvertedDataStringCode32 = hv_ConvertedDataStringCode32 + (hv_Symbols.TupleFind(
                    hv_DecodedDataStringCode39.TupleStrBitSelect(hv_Digit)));
                if ((int)(new HTuple(hv_Digit.TupleLess(5))) != 0)
                {
                    hv_ConvertedDataStringCode32 = hv_ConvertedDataStringCode32 * 32;
                }
            }
            //Write the converted string as 9 digit string with leading zeros
            hv_ConvertedDataStringCode32 = hv_ConvertedDataStringCode32.TupleString("9.9d");
            //
            //Verify the checksum (last digit)
            hv_CheckDigit = ((hv_ConvertedDataStringCode32.TupleStrBitSelect(8))).TupleNumber()
                ;
            hv_CheckSum = 0;
            for (hv_Digit = 0; (int)hv_Digit <= 7; hv_Digit = (int)hv_Digit + 1)
            {
                //Sum first 8 digits, but even digits have weight 2
                hv_Value = (1 + (hv_Digit % 2)) * (((hv_ConvertedDataStringCode32.TupleStrBitSelect(
                    hv_Digit))).TupleNumber());
                //But actually we only want the cross digit sum,
                //This 'formula' works for 0-19
                if ((int)(new HTuple(hv_Value.TupleGreaterEqual(10))) != 0)
                {
                    hv_Value = hv_Value - 9;
                }
                hv_CheckSum = hv_CheckSum + hv_Value;
            }
            hv_CheckSum = hv_CheckSum % 10;
            //
            //If the checksum fits, return the converted 'Code 32' string,
            //else return an empty string
            if ((int)(new HTuple(hv_CheckDigit.TupleNotEqual(hv_CheckSum))) != 0)
            {
                //Bad checksum
                hv_ConvertedDataStringCode32 = "";
            }
            else
            {
                //Always printed with leading A
                hv_ConvertedDataStringCode32 = "A" + hv_ConvertedDataStringCode32;
            }

            return;
            //
        }

        // Chapter: Matching / Shape-Based
        // Short Description: Display the results of Shape-Based Matching. 
        public void dev_display_shape_matching_results(HTuple hv_ModelID, HTuple hv_Color,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_ScaleR, HTuple hv_ScaleC,
            HTuple hv_Model)
        {



            // Local iconic variables 

            HObject ho_ModelContours = null, ho_ContoursAffinTrans = null;

            // Local control variables 

            HTuple hv_NumMatches = null, hv_Index = new HTuple();
            HTuple hv_Match = new HTuple(), hv_HomMat2DIdentity = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_HomMat2DTranslate = new HTuple();
            HTuple hv_Model_COPY_INP_TMP = hv_Model.Clone();
            HTuple hv_ScaleC_COPY_INP_TMP = hv_ScaleC.Clone();
            HTuple hv_ScaleR_COPY_INP_TMP = hv_ScaleR.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
            //This procedure displays the results of Shape-Based Matching.
            //
            hv_NumMatches = new HTuple(hv_Row.TupleLength());
            if ((int)(new HTuple(hv_NumMatches.TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple((new HTuple(hv_ScaleR_COPY_INP_TMP.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleR_COPY_INP_TMP, out hv_ScaleR_COPY_INP_TMP);
                }
                if ((int)(new HTuple((new HTuple(hv_ScaleC_COPY_INP_TMP.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleC_COPY_INP_TMP, out hv_ScaleC_COPY_INP_TMP);
                }
                if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                }
                else if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                    )).TupleEqual(1))) != 0)
                {
                    HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out hv_Model_COPY_INP_TMP);
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    ho_ModelContours.Dispose();
                    HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID.TupleSelect(
                        hv_Index), 1);
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Color.TupleSelect(hv_Index % (new HTuple(hv_Color.TupleLength()
                        ))));
                    HTuple end_val18 = hv_NumMatches - 1;
                    HTuple step_val18 = 1;
                    for (hv_Match = 0; hv_Match.Continue(end_val18, step_val18); hv_Match = hv_Match.TupleAdd(step_val18))
                    {
                        if ((int)(new HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                            hv_Match)))) != 0)
                        {
                            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                            HOperatorSet.HomMat2dScale(hv_HomMat2DIdentity, hv_ScaleR_COPY_INP_TMP.TupleSelect(
                                hv_Match), hv_ScaleC_COPY_INP_TMP.TupleSelect(hv_Match), 0, 0, out hv_HomMat2DScale);
                            HOperatorSet.HomMat2dRotate(hv_HomMat2DScale, hv_Angle.TupleSelect(hv_Match),
                                0, 0, out hv_HomMat2DRotate);
                            HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row.TupleSelect(
                                hv_Match), hv_Column.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                            ho_ContoursAffinTrans.Dispose();
                            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans,
                                hv_HomMat2DTranslate);
                            HOperatorSet.DispObj(ho_ContoursAffinTrans, hv_ExpDefaultWinHandle);
                        }
                    }
                }
            }
            ho_ModelContours.Dispose();
            ho_ContoursAffinTrans.Dispose();

            return;
        }

        // Chapter: Develop
        // Short Description: Open a new graphics window that preserves the aspect ratio of the given image. 
        public void dev_open_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column,
            HTuple hv_WidthLimit, HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = null, hv_ImageWidth = null, hv_ImageHeight = null;
            HTuple hv_TempWidth = null, hv_TempHeight = null, hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = null;
            // Initialize local and output iconic variables 
            hv_WindowHandle = new HTuple();
            //This procedure opens a new graphics window and adjusts the size
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct image aspect ratio.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority,
            //
            //Parse input tuple WidthLimit
            if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
            {
                hv_MinWidth = 500;
                hv_MaxWidth = 800;
            }
            else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinWidth = 0;
                hv_MaxWidth = hv_WidthLimit.Clone();
            }
            else
            {
                hv_MinWidth = hv_WidthLimit[0];
                hv_MaxWidth = hv_WidthLimit[1];
            }
            //Parse input tuple HeightLimit
            if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
            {
                hv_MinHeight = 400;
                hv_MaxHeight = 600;
            }
            else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinHeight = 0;
                hv_MaxHeight = hv_HeightLimit.Clone();
            }
            else
            {
                hv_MinHeight = hv_HeightLimit[0];
                hv_MaxHeight = hv_HeightLimit[1];
            }
            //
            //Test, if window size has to be changed.
            hv_ResizeFactor = 1;
            HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
            //First, expand window to the minimum extents (if necessary).
            if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                hv_ImageHeight)))) != 0)
            {
                hv_ResizeFactor = (((((hv_MinWidth.TupleReal()) / hv_ImageWidth)).TupleConcat(
                    (hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax();
            }
            hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
            hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;
            //Then, shrink window to maximum extents (if necessary).
            if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                hv_TempHeight)))) != 0)
            {
                hv_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()) / hv_TempWidth)).TupleConcat(
                    (hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin());
            }
            hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
            hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
            //Resize window
            //dev_open_window(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_ImageHeight - 1, hv_ImageWidth - 1);

            return;
        }

        // Chapter: Develop
        // Short Description: Open a new graphics window that preserves the aspect ratio of the given image size. 
        public void dev_open_window_fit_size(HTuple hv_Row, HTuple hv_Column, HTuple hv_Width,
            HTuple hv_Height, HTuple hv_WidthLimit, HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = null, hv_TempWidth = null, hv_TempHeight = null;
            HTuple hv_WindowWidth = new HTuple(), hv_WindowHeight = null;
            // Initialize local and output iconic variables 
            hv_WindowHandle = new HTuple();
            //This procedure open a new graphic window
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct aspect ratio
            //given by Width and Height.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority.
            //
            //Parse input tuple WidthLimit
            if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
            {
                hv_MinWidth = 500;
                hv_MaxWidth = 800;
            }
            else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinWidth = 0;
                hv_MaxWidth = hv_WidthLimit.Clone();
            }
            else
            {
                hv_MinWidth = hv_WidthLimit[0];
                hv_MaxWidth = hv_WidthLimit[1];
            }
            //Parse input tuple HeightLimit
            if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
            {
                hv_MinHeight = 400;
                hv_MaxHeight = 600;
            }
            else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinHeight = 0;
                hv_MaxHeight = hv_HeightLimit.Clone();
            }
            else
            {
                hv_MinHeight = hv_HeightLimit[0];
                hv_MaxHeight = hv_HeightLimit[1];
            }
            //
            //Test, if window size has to be changed.
            hv_ResizeFactor = 1;
            //First, expand window to the minimum extents (if necessary).
            if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_Width))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                hv_Height)))) != 0)
            {
                hv_ResizeFactor = (((((hv_MinWidth.TupleReal()) / hv_Width)).TupleConcat((hv_MinHeight.TupleReal()
                    ) / hv_Height))).TupleMax();
            }
            hv_TempWidth = hv_Width * hv_ResizeFactor;
            hv_TempHeight = hv_Height * hv_ResizeFactor;
            //Then, shrink window to maximum extents (if necessary).
            if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                hv_TempHeight)))) != 0)
            {
                hv_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()) / hv_TempWidth)).TupleConcat(
                    (hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin());
            }
            hv_WindowWidth = hv_Width * hv_ResizeFactor;
            hv_WindowHeight = hv_Height * hv_ResizeFactor;
            //Resize window
            //dev_open_window(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_Height - 1, hv_Width - 1);

            return;
        }

        // Chapter: Develop
        // Short Description: Changes the size of a graphics window with a given maximum and minimum extent such that it preserves the aspect ratio of the given image 
        public void dev_resize_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column,
            HTuple hv_WidthLimit, HTuple hv_HeightLimit)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = null, hv_Pointer = null, hv_Type = null;
            HTuple hv_ImageWidth = null, hv_ImageHeight = null, hv_TempWidth = null;
            HTuple hv_TempHeight = null, hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = null;
            // Initialize local and output iconic variables 
            //This procedure adjusts the size of the current window
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct image aspect ratio.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority,
            //
            //Parse input tuple WidthLimit
            if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
            {
                hv_MinWidth = 500;
                hv_MaxWidth = 800;
            }
            else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinWidth = 0;
                hv_MaxWidth = hv_WidthLimit.Clone();
            }
            else
            {
                hv_MinWidth = hv_WidthLimit[0];
                hv_MaxWidth = hv_WidthLimit[1];
            }
            //Parse input tuple HeightLimit
            if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
            {
                hv_MinHeight = 400;
                hv_MaxHeight = 600;
            }
            else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinHeight = 0;
                hv_MaxHeight = hv_HeightLimit.Clone();
            }
            else
            {
                hv_MinHeight = hv_HeightLimit[0];
                hv_MaxHeight = hv_HeightLimit[1];
            }
            //
            //Test, if window size has to be changed.
            hv_ResizeFactor = 1;
            HOperatorSet.GetImagePointer1(ho_Image, out hv_Pointer, out hv_Type, out hv_ImageWidth,
                out hv_ImageHeight);
            //First, expand window to the minimum extents (if necessary).
            if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                hv_ImageHeight)))) != 0)
            {
                hv_ResizeFactor = (((((hv_MinWidth.TupleReal()) / hv_ImageWidth)).TupleConcat(
                    (hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax();
            }
            hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
            hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;
            //Then, shrink window to maximum extents (if necessary).
            if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                hv_TempHeight)))) != 0)
            {
                hv_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()) / hv_TempWidth)).TupleConcat(
                    (hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin());
            }
            hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
            hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
            //Resize window
            //dev_set_window_extents(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_ImageHeight - 1, hv_ImageWidth - 1);

            return;
        }

        // Chapter: Develop
        // Short Description: Resizes a graphics window with a given maximum extent such that it preserves the aspect ratio of a given width and height 
        public void dev_resize_window_fit_size(HTuple hv_Row, HTuple hv_Column, HTuple hv_Width,
            HTuple hv_Height, HTuple hv_WidthLimit, HTuple hv_HeightLimit)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = null, hv_TempWidth = null, hv_TempHeight = null;
            HTuple hv_WindowWidth = new HTuple(), hv_WindowHeight = null;
            // Initialize local and output iconic variables 
            //This procedure adjusts the size of the current window
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct aspect ratio
            //given by Width and Height.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority.
            //
            //Parse input tuple WidthLimit
            if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
            {
                hv_MinWidth = 500;
                hv_MaxWidth = 800;
            }
            else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinWidth = 0;
                hv_MaxWidth = hv_WidthLimit.Clone();
            }
            else
            {
                hv_MinWidth = hv_WidthLimit[0];
                hv_MaxWidth = hv_WidthLimit[1];
            }
            //Parse input tuple HeightLimit
            if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
            {
                hv_MinHeight = 400;
                hv_MaxHeight = 600;
            }
            else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinHeight = 0;
                hv_MaxHeight = hv_HeightLimit.Clone();
            }
            else
            {
                hv_MinHeight = hv_HeightLimit[0];
                hv_MaxHeight = hv_HeightLimit[1];
            }
            //
            //Test, if window size has to be changed.
            hv_ResizeFactor = 1;
            //First, expand window to the minimum extents (if necessary).
            if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_Width))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                hv_Height)))) != 0)
            {
                hv_ResizeFactor = (((((hv_MinWidth.TupleReal()) / hv_Width)).TupleConcat((hv_MinHeight.TupleReal()
                    ) / hv_Height))).TupleMax();
            }
            hv_TempWidth = hv_Width * hv_ResizeFactor;
            hv_TempHeight = hv_Height * hv_ResizeFactor;
            //Then, shrink window to maximum extents (if necessary).
            if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                hv_TempHeight)))) != 0)
            {
                hv_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()) / hv_TempWidth)).TupleConcat(
                    (hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin());
            }
            hv_WindowWidth = hv_Width * hv_ResizeFactor;
            hv_WindowHeight = hv_Height * hv_ResizeFactor;
            //Resize window
            //dev_set_window_extents(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_Height - 1, hv_Width - 1);

            return;
        }

        // Chapter: Develop
        // Short Description: Switch dev_update_pc, dev_update_var and dev_update_window to 'off'. 
        public void dev_update_off()
        {

            // Initialize local and output iconic variables 
            //This procedure sets different update settings to 'off'.
            //This is useful to get the best performance and reduce overhead.
            //
            // dev_update_pc(...); only in hdevelop
            // dev_update_var(...); only in hdevelop
            // dev_update_window(...); only in hdevelop

            return;
        }

        // Chapter: Develop
        // Short Description: Switch dev_update_pc, dev_update_var and dev_update_window to 'on'. 
        public void dev_update_on()
        {

            // Initialize local and output iconic variables 
            //This procedure sets different update settings to 'on'.
            //
            // dev_update_pc(...); only in hdevelop
            // dev_update_var(...); only in hdevelop
            // dev_update_window(...); only in hdevelop

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Display the axes of a 3d coordinate system 
        public void disp_3d_coord_system(HTuple hv_WindowHandle, HTuple hv_CamParam, HTuple hv_Pose,
            HTuple hv_CoordAxesLength)
        {



            // Local iconic variables 

            HObject ho_Arrows;

            // Local control variables 

            HTuple hv_TransWorld2Cam = null, hv_OrigCamX = null;
            HTuple hv_OrigCamY = null, hv_OrigCamZ = null, hv_Row0 = null;
            HTuple hv_Column0 = null, hv_X = null, hv_Y = null, hv_Z = null;
            HTuple hv_RowAxX = null, hv_ColumnAxX = null, hv_RowAxY = null;
            HTuple hv_ColumnAxY = null, hv_RowAxZ = null, hv_ColumnAxZ = null;
            HTuple hv_Distance = null, hv_HeadLength = null, hv_Red = null;
            HTuple hv_Green = null, hv_Blue = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrows);
            //This procedure displays a 3D coordinate system.
            //It needs the procedure gen_arrow_contour_xld.
            //
            //Input parameters:
            //WindowHandle: The window where the coordinate system shall be displayed
            //CamParam: The camera paramters
            //Pose: The pose to be displayed
            //CoordAxesLength: The length of the coordinate axes in world coordinates
            //
            //Check, if Pose is a correct pose tuple.
            if ((int)(new HTuple((new HTuple(hv_Pose.TupleLength())).TupleNotEqual(7))) != 0)
            {
                ho_Arrows.Dispose();

                return;
            }
            if ((int)((new HTuple(((hv_Pose.TupleSelect(2))).TupleEqual(0.0))).TupleAnd(new HTuple(((hv_CamParam.TupleSelect(
                0))).TupleNotEqual(0)))) != 0)
            {
                //For projective cameras:
                //Poses with Z position zero cannot be projected
                //(that would lead to a division by zero error).
                ho_Arrows.Dispose();

                return;
            }
            //Convert to pose to a transformation matrix
            HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_TransWorld2Cam);
            //Project the world origin into the image
            HOperatorSet.AffineTransPoint3d(hv_TransWorld2Cam, 0, 0, 0, out hv_OrigCamX,
                out hv_OrigCamY, out hv_OrigCamZ);
            HOperatorSet.Project3dPoint(hv_OrigCamX, hv_OrigCamY, hv_OrigCamZ, hv_CamParam,
                out hv_Row0, out hv_Column0);
            //Project the coordinate axes into the image
            HOperatorSet.AffineTransPoint3d(hv_TransWorld2Cam, hv_CoordAxesLength, 0, 0,
                out hv_X, out hv_Y, out hv_Z);
            HOperatorSet.Project3dPoint(hv_X, hv_Y, hv_Z, hv_CamParam, out hv_RowAxX, out hv_ColumnAxX);
            HOperatorSet.AffineTransPoint3d(hv_TransWorld2Cam, 0, hv_CoordAxesLength, 0,
                out hv_X, out hv_Y, out hv_Z);
            HOperatorSet.Project3dPoint(hv_X, hv_Y, hv_Z, hv_CamParam, out hv_RowAxY, out hv_ColumnAxY);
            HOperatorSet.AffineTransPoint3d(hv_TransWorld2Cam, 0, 0, hv_CoordAxesLength,
                out hv_X, out hv_Y, out hv_Z);
            HOperatorSet.Project3dPoint(hv_X, hv_Y, hv_Z, hv_CamParam, out hv_RowAxZ, out hv_ColumnAxZ);
            //
            //Generate an XLD contour for each axis
            HOperatorSet.DistancePp(((hv_Row0.TupleConcat(hv_Row0))).TupleConcat(hv_Row0),
                ((hv_Column0.TupleConcat(hv_Column0))).TupleConcat(hv_Column0), ((hv_RowAxX.TupleConcat(
                hv_RowAxY))).TupleConcat(hv_RowAxZ), ((hv_ColumnAxX.TupleConcat(hv_ColumnAxY))).TupleConcat(
                hv_ColumnAxZ), out hv_Distance);
            hv_HeadLength = (((((((hv_Distance.TupleMax()) / 12.0)).TupleConcat(5.0))).TupleMax()
                )).TupleInt();
            ho_Arrows.Dispose();
            gen_arrow_contour_xld(out ho_Arrows, ((hv_Row0.TupleConcat(hv_Row0))).TupleConcat(
                hv_Row0), ((hv_Column0.TupleConcat(hv_Column0))).TupleConcat(hv_Column0),
                ((hv_RowAxX.TupleConcat(hv_RowAxY))).TupleConcat(hv_RowAxZ), ((hv_ColumnAxX.TupleConcat(
                hv_ColumnAxY))).TupleConcat(hv_ColumnAxZ), hv_HeadLength, hv_HeadLength);
            //
            //Display coordinate system
            HOperatorSet.DispXld(ho_Arrows, hv_ExpDefaultWinHandle);
            //
            HOperatorSet.GetRgb(hv_ExpDefaultWinHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red.TupleSelect(0), hv_Green.TupleSelect(
                0), hv_Blue.TupleSelect(0));
            HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_RowAxX + 3, hv_ColumnAxX + 3);
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, "X");
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red.TupleSelect(1 % (new HTuple(hv_Red.TupleLength()
                ))), hv_Green.TupleSelect(1 % (new HTuple(hv_Green.TupleLength()))), hv_Blue.TupleSelect(
                1 % (new HTuple(hv_Blue.TupleLength()))));
            HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_RowAxY + 3, hv_ColumnAxY + 3);
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, "Y");
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red.TupleSelect(2 % (new HTuple(hv_Red.TupleLength()
                ))), hv_Green.TupleSelect(2 % (new HTuple(hv_Green.TupleLength()))), hv_Blue.TupleSelect(
                2 % (new HTuple(hv_Blue.TupleLength()))));
            HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_RowAxZ + 3, hv_ColumnAxZ + 3);
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, "Z");
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
            ho_Arrows.Dispose();

            return;
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure displays 'Click 'Run' to continue' in the lower right corner of the screen. 
        public void disp_continue_message(HTuple hv_WindowHandle, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ContinueMessage = null, hv_Row = null;
            HTuple hv_Column = null, hv_Width = null, hv_Height = null;
            HTuple hv_Ascent = null, hv_Descent = null, hv_TextWidth = null;
            HTuple hv_TextHeight = null;
            // Initialize local and output iconic variables 
            //This procedure displays 'Press Run (F5) to continue' in the
            //lower right corner of the screen.
            //It uses the procedure disp_message.
            //
            //Input parameters:
            //WindowHandle: The window, where the text shall be displayed
            //Color: defines the text color.
            //   If set to '' or 'auto', the currently set color is used.
            //Box: If set to 'true', the text is displayed in a box.
            //
            hv_ContinueMessage = "Press Run (F5) to continue";
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_Row, out hv_Column,
                out hv_Width, out hv_Height);
            HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, (" " + hv_ContinueMessage) + " ",
                out hv_Ascent, out hv_Descent, out hv_TextWidth, out hv_TextHeight);
            disp_message(hv_ExpDefaultWinHandle, hv_ContinueMessage, "window", (hv_Height - hv_TextHeight) - 12,
                (hv_Width - hv_TextWidth) - 12, hv_Color, hv_Box);

            return;
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure displays 'End of program' in the lower right corner of the screen. 
        public void disp_end_of_program_message(HTuple hv_WindowHandle, HTuple hv_Color,
            HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_EndMessage = null, hv_Row = null;
            HTuple hv_Column = null, hv_Width = null, hv_Height = null;
            HTuple hv_Ascent = null, hv_Descent = null, hv_TextWidth = null;
            HTuple hv_TextHeight = null;
            // Initialize local and output iconic variables 
            //This procedure displays 'End of program' in the
            //lower right corner of the screen.
            //It uses the procedure disp_message.
            //
            //Input parameters:
            //WindowHandle: The window, where the text shall be displayed
            //Color: defines the text color.
            //   If set to '' or 'auto', the currently set color is used.
            //Box: If set to 'true', the text is displayed in a box.
            //
            hv_EndMessage = "      End of program      ";
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_Row, out hv_Column,
                out hv_Width, out hv_Height);
            HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, (" " + hv_EndMessage) + " ",
                out hv_Ascent, out hv_Descent, out hv_TextWidth, out hv_TextHeight);
            disp_message(hv_ExpDefaultWinHandle, hv_EndMessage, "window", (hv_Height - hv_TextHeight) - 12,
                (hv_Width - hv_TextWidth) - 12, hv_Color, hv_Box);

            return;
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = new HTuple(), hv_HeightWin = null;
            HTuple hv_MaxAscent = null, hv_MaxDescent = null, hv_MaxWidth = null;
            HTuple hv_MaxHeight = null, hv_R1 = new HTuple(), hv_C1 = new HTuple();
            HTuple hv_FactorRow = new HTuple(), hv_FactorColumn = new HTuple();
            HTuple hv_UseShadow = null, hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow (same as if no second value is given)
            //       otherwise -> use given string as color string for the shadow color
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_ExpDefaultWinHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_ExpDefaultWinHandle, out hv_Row1Part, out hv_Column1Part,
                out hv_Row2Part, out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_ExpDefaultWinHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                        0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_ExpDefaultWinHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_ExpDefaultWinHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1,
                        hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                    0));
                HOperatorSet.DispRectangle1(hv_ExpDefaultWinHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        // Chapter: XLD / Creation
        // Short Description: Creates an arrow shaped XLD contour. 
        public void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
            HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_TempArrow = null;

            // Local control variables 

            HTuple hv_Length = null, hv_ZeroLengthIndices = null;
            HTuple hv_DR = null, hv_DC = null, hv_HalfHeadWidth = null;
            HTuple hv_RowP1 = null, hv_ColP1 = null, hv_RowP2 = null;
            HTuple hv_ColP2 = null, hv_Index = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);
            //This procedure generates arrow shaped XLD contours,
            //pointing from (Row1, Column1) to (Row2, Column2).
            //If starting and end point are identical, a contour consisting
            //of a single point is returned.
            //
            //input parameteres:
            //Row1, Column1: Coordinates of the arrows' starting points
            //Row2, Column2: Coordinates of the arrows' end points
            //HeadLength, HeadWidth: Size of the arrow heads in pixels
            //
            //output parameter:
            //Arrow: The resulting XLD contour
            //
            //The input tuples Row1, Column1, Row2, and Column2 have to be of
            //the same length.
            //HeadLength and HeadWidth either have to be of the same length as
            //Row1, Column1, Row2, and Column2 or have to be a single element.
            //If one of the above restrictions is violated, an error will occur.
            //
            //
            //Init
            ho_Arrow.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            //
            //Calculate the arrow length
            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
            //
            //Mark arrows with identical start and end point
            //(set Length to -1 to avoid division-by-zero exception)
            hv_ZeroLengthIndices = hv_Length.TupleFind(0);
            if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
            {
                if (hv_Length == null)
                    hv_Length = new HTuple();
                hv_Length[hv_ZeroLengthIndices] = -1;
            }
            //
            //Calculate auxiliary variables.
            hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
            hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
            hv_HalfHeadWidth = hv_HeadWidth / 2.0;
            //
            //Calculate end points of the arrow head.
            hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
            hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
            hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
            hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
            //
            //Finally create output XLD contour for each input point pair
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                {
                    //Create_ single points for arrows with identical start and end point
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(hv_Index),
                        hv_Column1.TupleSelect(hv_Index));
                }
                else
                {
                    //Create arrow contour
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                        hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                        ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                        hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                    ho_Arrow.Dispose();
                    ho_Arrow = ExpTmpOutVar_0;
                }
            }
            ho_TempArrow.Dispose();

            return;
        }

        // Chapter: File
        // Short Description: Get all image files under the given path 
        public void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options,
            out HTuple hv_ImageFiles)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_HalconImages = null, hv_OS = null;
            HTuple hv_Directories = null, hv_Index = null, hv_Length = null;
            HTuple hv_network_drive = null, hv_Substring = new HTuple();
            HTuple hv_FileExists = new HTuple(), hv_AllFiles = new HTuple();
            HTuple hv_i = new HTuple(), hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = hv_Extensions.Clone();
            HTuple hv_ImageDirectory_COPY_INP_TMP = hv_ImageDirectory.Clone();

            // Initialize local and output iconic variables 
            //This procedure returns all files in a given directory
            //with one of the suffixes specified in Extensions.
            //
            //input parameters:
            //ImageDirectory: as the name says
            //   If a tuple of directories is given, only the images in the first
            //   existing directory are returned.
            //   If a local directory is not found, the directory is searched
            //   under %HALCONIMAGES%/ImageDirectory. If %HALCONIMAGES% is not set,
            //   %HALCONROOT%/images is used instead.
            //Extensions: A string tuple containing the extensions to be found
            //   e.g. ['png','tif',jpg'] or others
            //If Extensions is set to 'default' or the empty string '',
            //   all image suffixes supported by HALCON are used.
            //Options: as in the operator list_files, except that the 'files'
            //   option is always used. Note that the 'directories' option
            //   has no effect but increases runtime, because only files are
            //   returned.
            //
            //output parameter:
            //ImageFiles: A tuple of all found image file names
            //
            if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                "default")))) != 0)
            {
                hv_Extensions_COPY_INP_TMP = new HTuple();
                hv_Extensions_COPY_INP_TMP[0] = "ima";
                hv_Extensions_COPY_INP_TMP[1] = "tif";
                hv_Extensions_COPY_INP_TMP[2] = "tiff";
                hv_Extensions_COPY_INP_TMP[3] = "gif";
                hv_Extensions_COPY_INP_TMP[4] = "bmp";
                hv_Extensions_COPY_INP_TMP[5] = "jpg";
                hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                hv_Extensions_COPY_INP_TMP[7] = "jp2";
                hv_Extensions_COPY_INP_TMP[8] = "jxr";
                hv_Extensions_COPY_INP_TMP[9] = "png";
                hv_Extensions_COPY_INP_TMP[10] = "pcx";
                hv_Extensions_COPY_INP_TMP[11] = "ras";
                hv_Extensions_COPY_INP_TMP[12] = "xwd";
                hv_Extensions_COPY_INP_TMP[13] = "pbm";
                hv_Extensions_COPY_INP_TMP[14] = "pnm";
                hv_Extensions_COPY_INP_TMP[15] = "pgm";
                hv_Extensions_COPY_INP_TMP[16] = "ppm";
                //
            }
            if ((int)(new HTuple(hv_ImageDirectory_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                hv_ImageDirectory_COPY_INP_TMP = ".";
            }
            HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                hv_HalconImages = hv_HalconImages.TupleSplit(";");
            }
            else
            {
                hv_HalconImages = hv_HalconImages.TupleSplit(":");
            }
            hv_Directories = hv_ImageDirectory_COPY_INP_TMP.Clone();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_Directories = hv_Directories.TupleConcat(((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_ImageDirectory_COPY_INP_TMP);
            }
            HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
            HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_network_drive);
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                        )).TupleGreater(1))) != 0)
                    {
                        HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1, out hv_Substring);
                        if ((int)(new HTuple(hv_Substring.TupleEqual("//"))) != 0)
                        {
                            if (hv_network_drive == null)
                                hv_network_drive = new HTuple();
                            hv_network_drive[hv_Index] = 1;
                        }
                    }
                }
            }
            hv_ImageFiles = new HTuple();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                if ((int)(hv_FileExists) != 0)
                {
                    HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                        hv_Options), out hv_AllFiles);
                    hv_ImageFiles = new HTuple();
                    for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                        )) - 1); hv_i = (int)hv_i + 1)
                    {
                        HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                            hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                        hv_ImageFiles = hv_ImageFiles.TupleConcat(hv_Selection);
                    }
                    HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("\\\\")).TupleConcat(
                        "replace_all"), "/", out hv_ImageFiles);
                    if ((int)(hv_network_drive.TupleSelect(hv_Index)) != 0)
                    {
                        HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat(
                            "replace_all"), "/", out hv_ImageFiles);
                        hv_ImageFiles = "/" + hv_ImageFiles;
                    }
                    else
                    {
                        HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat(
                            "replace_all"), "/", out hv_ImageFiles);
                    }

                    return;
                }
            }

            return;
        }

        // Chapter: File
        // Short Description: Parse a filename into directory, base filename, and extension 
        public void parse_filename(HTuple hv_FileName, out HTuple hv_BaseName, out HTuple hv_Extension,
            out HTuple hv_Directory)
        {



            // Local control variables 

            HTuple hv_DirectoryTmp = null, hv_Substring = null;
            // Initialize local and output iconic variables 
            //This procedure gets a filename (with full path) as input
            //and returns the directory path, the base filename and the extension
            //in three different strings.
            //
            //In the output path the path separators will be replaced
            //by '/' in all cases.
            //
            //The procedure shows the possibilities of regular expressions in HALCON.
            //
            //Input parameters:
            //FileName: The input filename
            //
            //Output parameters:
            //BaseName: The filename without directory description and file extension
            //Extension: The file extension
            //Directory: The directory path
            //
            //Example:
            //basename('C:/images/part_01.png',...) returns
            //BaseName = 'part_01'
            //Extension = 'png'
            //Directory = 'C:\\images\\' (on Windows systems)
            //
            //Explanation of the regular expressions:
            //
            //'([^\\\\/]*?)(?:\\.[^.]*)?$':
            //To start at the end, the '$' matches the end of the string,
            //so it is best to read the expression from right to left.
            //The part in brackets (?:\\.[^.}*) denotes a non-capturing group.
            //That means, that this part is matched, but not captured
            //in contrast to the first bracketed group ([^\\\\/], see below.)
            //\\.[^.]* matches a dot '.' followed by as many non-dots as possible.
            //So (?:\\.[^.]*)? matches the file extension, if any.
            //The '?' at the end assures, that even if no extension exists,
            //a correct match is returned.
            //The first part in brackets ([^\\\\/]*?) is a capture group,
            //which means, that if a match is found, only the part in
            //brackets is returned as a result.
            //Because both HDevelop strings and regular expressions need a '\\'
            //to describe a backslash, inside regular expressions within HDevelop
            //a backslash has to be written as '\\\\'.
            //[^\\\\/] matches any character but a slash or backslash ('\\' in HDevelop)
            //[^\\\\/]*? matches a string od 0..n characters (except '/' or '\\')
            //where the '?' after the '*' switches the greediness off,
            //that means, that the shortest possible match is returned.
            //This option is necessary to cut off the extension
            //but only if (?:\\.[^.]*)? is able to match one.
            //To summarize, the regular expression matches that part of
            //the input string, that follows after the last '/' or '\\' and
            //cuts off the extension (if any) after the last '.'.
            //
            //'\\.([^.]*)$':
            //This matches everything after the last '.' of the input string.
            //Because ([^.]) is a capturing group,
            //only the part after the dot is returned.
            //
            //'.*[\\\\/]':
            //This matches the longest substring with a '/' or a '\\' at the end.
            //
            HOperatorSet.TupleRegexpMatch(hv_FileName, ".*[\\\\/]", out hv_DirectoryTmp);
            HOperatorSet.TupleSubstr(hv_FileName, hv_DirectoryTmp.TupleStrlen(), (hv_FileName.TupleStrlen()
                ) - 1, out hv_Substring);
            HOperatorSet.TupleRegexpMatch(hv_Substring, "([^\\\\/]*?)(?:\\.[^.]*)?$", out hv_BaseName);
            HOperatorSet.TupleRegexpMatch(hv_Substring, "\\.([^.]*)$", out hv_Extension);
            //
            //
            //Finally all found backslashes ('\\') are converted
            //to a slash to get consistent paths
            HOperatorSet.TupleRegexpReplace(hv_DirectoryTmp, (new HTuple("\\\\")).TupleConcat(
                "replace_all"), "/", out hv_Directory);

            return;
        }

        // Chapter: Graphics / Output
        // Short Description:  This procedure plots tuples representing functions or curves in a coordinate system. 
        public void plot_funct_1d(HTuple hv_WindowHandle, HTuple hv_Function, HTuple hv_XLabel,
            HTuple hv_YLabel, HTuple hv_Color, HTuple hv_GenParamNames, HTuple hv_GenParamValues)
        {



            // Local control variables 

            HTuple hv_XValues = null, hv_YValues = null;
            // Initialize local and output iconic variables 
            //This procedure plots a function in a coordinate system.
            //
            //Input parameters:
            //
            //Function: 1d function
            //
            //XLabel: X axis label
            //
            //XLabel: Y axis label
            //
            //Color: Color of the plotted function
            //       If [] is given, the currently set display color is used.
            //       If 'none is given, the function is not plotted, but only
            //       the coordinate axes as specified.
            //
            //GenParamNames: Generic parameters to control the presentation
            //               The parameters are evaluated from left to right.
            //
            //               Possible Values:
            //   'axes_color': coordinate system color
            //                 Default: 'white'
            //                 If 'none' is given, no coordinate system is shown.
            //   'style': Graph style
            //            Possible values: 'line' (default), 'cross', 'filled'
            //   'clip': Clip graph to coordinate system area
            //           Possibile values: 'yes' (default), 'no'
            //   'ticks': Control display of ticks on the axes
            //            If 'min_max_origin' is given (default), ticks are shown
            //            at the minimum and maximum values of the axes and at the
            //            intercept point of x- and y-axis.
            //            If 'none' is given, no ticks are shown.
            //            If any number != 0 is given, it is interpreted as distance
            //            between the ticks.
            //   'ticks_x': Control display of ticks on x-axis only
            //   'ticks_y': Control display of ticks on y-axis only
            //   'grid': Control display of grid lines within the coordinate system
            //           If 'min_max_origin' is given (default), grid lines are shown
            //           at the minimum and maximum values of the axes.
            //           If 'none' is given, no grid lines are shown.
            //           If any number != 0 is given, it is interpreted as distance
            //           between the grid lines.
            //   'grid_x': Control display of grid lines for the x-axis only
            //   'grid_y': Control display of grid lines for the y-axis only
            //   'grid_color': Color of the grid (default: 'dim gray')
            //   'margin': The distance in pixels of the coordinate system area
            //             to all four window borders.
            //   'margin_left': The distance in pixels of the coordinate system area
            //                  to the left window border.
            //   'margin_right': The distance in pixels of the coordinate system area
            //                   to the right window border.
            //   'margin_top': The distance in pixels of the coordinate system area
            //                 to the upper window border.
            //   'margin_bottom': The distance in pixels of the coordinate system area
            //                    to the lower window border.
            //   'start_x': Lowest x value of the x axis
            //              Default: min(XValues)
            //   'end_x': Highest x value of the x axis
            //            Default: max(XValues)
            //   'start_y': Lowest y value of the x axis
            //              Default: min(YValues)
            //   'end_y': Highest y value of the x axis
            //            Default: max(YValues)
            //   'origin_x': X coordinate of the intercept point of x- and y-axis.
            //               Default: same as start_x
            //   'origin_y': Y coordinate of the intercept point of x- and y-axis.
            //               Default: same as start_y
            //
            //GenParamValues: Values of the generic parameters of GenericParamNames
            //
            //
            HOperatorSet.Funct1dToPairs(hv_Function, out hv_XValues, out hv_YValues);
            plot_tuple(hv_ExpDefaultWinHandle, hv_XValues, hv_YValues, hv_XLabel, hv_YLabel,
                hv_Color, hv_GenParamNames, hv_GenParamValues);

            return;
        }

        // Chapter: Graphics / Output
        // Short Description:  This procedure plots tuples representing functions or curves in a coordinate system. 
        public void plot_tuple(HTuple hv_WindowHandle, HTuple hv_XValues, HTuple hv_YValues,
            HTuple hv_XLabel, HTuple hv_YLabel, HTuple hv_Color, HTuple hv_GenParamNames,
            HTuple hv_GenParamValues)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ContourXGrid = null, ho_ContourYGrid = null;
            HObject ho_XArrow = null, ho_YArrow = null, ho_ContourXTick = null;
            HObject ho_ContourYTick = null, ho_Contour = null, ho_Cross = null;
            HObject ho_Filled = null;

            // Local control variables 

            HTuple hv_PreviousWindowHandle = new HTuple();
            HTuple hv_ClipRegion = null, hv_Row = null, hv_Column = null;
            HTuple hv_Width = null, hv_Height = null, hv_PartRow1 = null;
            HTuple hv_PartColumn1 = null, hv_PartRow2 = null, hv_PartColumn2 = null;
            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_DrawMode = null, hv_OriginStyle = null, hv_XAxisEndValue = new HTuple();
            HTuple hv_YAxisEndValue = new HTuple(), hv_XAxisStartValue = new HTuple();
            HTuple hv_YAxisStartValue = new HTuple(), hv_XValuesAreStrings = new HTuple();
            HTuple hv_XTickValues = new HTuple(), hv_XTicks = null;
            HTuple hv_OriginX = null, hv_OriginY = null, hv_LeftBorder = null;
            HTuple hv_RightBorder = null, hv_UpperBorder = null, hv_LowerBorder = null;
            HTuple hv_AxesColor = null, hv_Style = null, hv_Clip = null;
            HTuple hv_YTicks = null, hv_XGrid = null, hv_YGrid = null;
            HTuple hv_GridColor = null, hv_NumGenParamNames = null;
            HTuple hv_NumGenParamValues = null, hv_SetOriginXToDefault = null;
            HTuple hv_SetOriginYToDefault = null, hv_GenParamIndex = null;
            HTuple hv_XGridTicks = new HTuple(), hv_XAxisWidthPx = null;
            HTuple hv_XAxisWidth = null, hv_XScaleFactor = null, hv_YAxisHeightPx = null;
            HTuple hv_YAxisHeight = null, hv_YScaleFactor = null, hv_YAxisOffsetPx = null;
            HTuple hv_XAxisOffsetPx = null, hv_DotStyle = new HTuple();
            HTuple hv_XGridValues = new HTuple(), hv_XGridStart = new HTuple();
            HTuple hv_XPosition = new HTuple(), hv_IndexGrid = new HTuple();
            HTuple hv_YGridValues = new HTuple(), hv_YGridStart = new HTuple();
            HTuple hv_YPosition = new HTuple(), hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv_TextWidthXLabel = new HTuple();
            HTuple hv_TextHeightXLabel = new HTuple(), hv_XTickStart = new HTuple();
            HTuple hv_TypeTicks = new HTuple(), hv_IndexTicks = new HTuple();
            HTuple hv_YTickValues = new HTuple(), hv_YTickStart = new HTuple();
            HTuple hv_Ascent1 = new HTuple(), hv_Descent1 = new HTuple();
            HTuple hv_TextWidthYTicks = new HTuple(), hv_TextHeightYTicks = new HTuple();
            HTuple hv_Num = new HTuple(), hv_I = new HTuple(), hv_YSelected = new HTuple();
            HTuple hv_Y1Selected = new HTuple(), hv_X1Selected = new HTuple();
            HTuple hv_XValues_COPY_INP_TMP = hv_XValues.Clone();
            HTuple hv_YValues_COPY_INP_TMP = hv_YValues.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ContourXGrid);
            HOperatorSet.GenEmptyObj(out ho_ContourYGrid);
            HOperatorSet.GenEmptyObj(out ho_XArrow);
            HOperatorSet.GenEmptyObj(out ho_YArrow);
            HOperatorSet.GenEmptyObj(out ho_ContourXTick);
            HOperatorSet.GenEmptyObj(out ho_ContourYTick);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_Filled);
            //This procedure plots tuples representing functions
            //or curves in a coordinate system.
            //
            //Input parameters:
            //
            //XValues: X values of the function to be plotted
            //         If XValues is set to [], it is interally set to 0,1,2,...,|YValues|-1.
            //         If XValues is a tuple of strings, the values are taken as categories.
            //
            //YValues: Y values of the function(s) to be plotted
            //         If YValues is set to [], it is interally set to 0,1,2,...,|XValues|-1.
            //         The number of y values must be equal to the number of x values
            //         or an integral multiple. In the latter case,
            //         multiple functions are plotted, that share the same x values.
            //
            //XLabel: X axis label
            //
            //XLabel: Y axis label
            //
            //Color: Color of the plotted function
            //       If [] is given, the currently set display color is used.
            //       If 'none is given, the function is not plotted, but only
            //       the coordinate axes as specified.
            //       If more than one color is given, multiple functions
            //       can be displayed in different colors.
            //
            //GenParamNames: Generic parameters to control the presentation
            //               Possible Values:
            //   'axes_color': coordinate system color
            //                 Default: 'white'
            //                 If 'none' is given, no coordinate system is shown.
            //   'style': Graph style
            //            Possible values: 'line' (default), 'cross', 'filled'
            //   'clip': Clip graph to coordinate system area
            //           Possibile values: 'yes', 'no' (default)
            //   'ticks': Control display of ticks on the axes
            //            If 'min_max_origin' is given (default), ticks are shown
            //            at the minimum and maximum values of the axes and at the
            //            intercept point of x- and y-axis.
            //            If 'none' is given, no ticks are shown.
            //            If any number != 0 is given, it is interpreted as distance
            //            between the ticks.
            //   'ticks_x': Control display of ticks on x-axis only
            //   'ticks_y': Control display of ticks on y-axis only
            //   'grid': Control display of grid lines within the coordinate system
            //           If 'min_max_origin' is given (default), grid lines are shown
            //           at the minimum and maximum values of the axes.
            //           If 'none' is given, no grid lines are shown.
            //           If any number != 0 is given, it is interpreted as distance
            //           between the grid lines.
            //   'grid_x': Control display of grid lines for the x-axis only
            //   'grid_y': Control display of grid lines for the y-axis only
            //   'grid_color': Color of the grid (default: 'dim gray')
            //   'margin': The distance in pixels of the coordinate system area
            //             to all four window borders.
            //   'margin_left': The distance in pixels of the coordinate system area
            //                  to the left window border.
            //   'margin_right': The distance in pixels of the coordinate system area
            //                   to the right window border.
            //   'margin_top': The distance in pixels of the coordinate system area
            //                 to the upper window border.
            //   'margin_bottom': The distance in pixels of the coordinate system area
            //                    to the lower window border.
            //   'start_x': Lowest x value of the x axis
            //              Default: min(XValues)
            //   'end_x': Highest x value of the x axis
            //            Default: max(XValues)
            //   'start_y': Lowest y value of the x axis
            //              Default: min(YValues)
            //   'end_y': Highest y value of the x axis
            //            Default: max(YValues)
            //   'origin_x': X coordinate of the intercept point of x- and y-axis.
            //               Default: same as start_x
            //   'origin_y': Y coordinate of the intercept point of x- and y-axis.
            //               Default: same as start_y
            //
            //GenParamValues: Values of the generic parameters of GenericParamNames
            //
            //
            //Store current display settings
            //dev_get_window(...);
            //dev_set_window(...);
            HOperatorSet.GetSystem("clip_region", out hv_ClipRegion);
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_Row, out hv_Column,
                out hv_Width, out hv_Height);
            HOperatorSet.GetPart(hv_ExpDefaultWinHandle, out hv_PartRow1, out hv_PartColumn1,
                out hv_PartRow2, out hv_PartColumn2);
            HOperatorSet.GetRgb(hv_ExpDefaultWinHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetDraw(hv_ExpDefaultWinHandle, out hv_DrawMode);
            HOperatorSet.GetLineStyle(hv_ExpDefaultWinHandle, out hv_OriginStyle);
            //
            //Set display parameters
            HOperatorSet.SetLineStyle(hv_ExpDefaultWinHandle, new HTuple());
            HOperatorSet.SetSystem("clip_region", "false");
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_Height - 1, hv_Width - 1);
            //
            //Check input coordinates
            //
            if ((int)((new HTuple(hv_XValues_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleAnd(
                new HTuple(hv_YValues_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {
                //Neither XValues nor YValues are given:
                //Set axes to interval [0,1]
                hv_XAxisEndValue = 1;
                hv_YAxisEndValue = 1;
                hv_XAxisStartValue = 0;
                hv_YAxisStartValue = 0;
                hv_XValuesAreStrings = 0;
            }
            else
            {
                if ((int)(new HTuple(hv_XValues_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    //XValues are omitted:
                    //Set equidistant XValues
                    hv_XValues_COPY_INP_TMP = HTuple.TupleGenSequence(0, (new HTuple(hv_YValues_COPY_INP_TMP.TupleLength()
                        )) - 1, 1);
                    hv_XValuesAreStrings = 0;
                }
                else if ((int)(new HTuple(hv_YValues_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    //YValues are omitted:
                    //Set equidistant YValues
                    hv_YValues_COPY_INP_TMP = HTuple.TupleGenSequence(0, (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                        )) - 1, 1);
                }
                if ((int)(new HTuple((new HTuple((new HTuple(hv_YValues_COPY_INP_TMP.TupleLength()
                    )) % (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())))).TupleNotEqual(
                    0))) != 0)
                {
                    //Number of YValues does not match number of XValues
                    throw new HalconException("Number of YValues is no multiple of the number of XValues!");
                    ho_ContourXGrid.Dispose();
                    ho_ContourYGrid.Dispose();
                    ho_XArrow.Dispose();
                    ho_YArrow.Dispose();
                    ho_ContourXTick.Dispose();
                    ho_ContourYTick.Dispose();
                    ho_Contour.Dispose();
                    ho_Cross.Dispose();
                    ho_Filled.Dispose();

                    return;
                }
                hv_XValuesAreStrings = hv_XValues_COPY_INP_TMP.TupleIsStringElem();
                hv_XValuesAreStrings = new HTuple(((hv_XValuesAreStrings.TupleSum())).TupleEqual(
                    new HTuple(hv_XValuesAreStrings.TupleLength())));
                if ((int)(hv_XValuesAreStrings) != 0)
                {
                    //XValues are given as strings:
                    //Show XValues as ticks
                    hv_XTickValues = hv_XValues_COPY_INP_TMP.Clone();
                    hv_XTicks = 1;
                    //Set x-axis dimensions
                    hv_XValues_COPY_INP_TMP = HTuple.TupleGenSequence(1, new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                        ), 1);
                }
                //Set default x-axis dimensions
                if ((int)(new HTuple((new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    hv_XAxisStartValue = hv_XValues_COPY_INP_TMP.TupleMin();
                    hv_XAxisEndValue = hv_XValues_COPY_INP_TMP.TupleMax();
                }
                else
                {
                    hv_XAxisEndValue = (hv_XValues_COPY_INP_TMP.TupleSelect(0)) + 0.5;
                    hv_XAxisStartValue = (hv_XValues_COPY_INP_TMP.TupleSelect(0)) - 0.5;
                }
            }
            //Set default y-axis dimensions
            if ((int)(new HTuple((new HTuple(hv_YValues_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                hv_YAxisStartValue = hv_YValues_COPY_INP_TMP.TupleMin();
                hv_YAxisEndValue = hv_YValues_COPY_INP_TMP.TupleMax();
            }
            else if ((int)(new HTuple((new HTuple(hv_YValues_COPY_INP_TMP.TupleLength()
                )).TupleEqual(1))) != 0)
            {
                hv_YAxisStartValue = (hv_YValues_COPY_INP_TMP.TupleSelect(0)) - 0.5;
                hv_YAxisEndValue = (hv_YValues_COPY_INP_TMP.TupleSelect(0)) + 0.5;
            }
            else
            {
                hv_YAxisStartValue = 0;
                hv_YAxisEndValue = 1;
            }
            //Set default interception point of x- and y- axis
            hv_OriginX = hv_XAxisStartValue.Clone();
            hv_OriginY = hv_YAxisStartValue.Clone();
            //
            //Set more defaults
            hv_LeftBorder = hv_Width * 0.1;
            hv_RightBorder = hv_Width * 0.1;
            hv_UpperBorder = hv_Height * 0.1;
            hv_LowerBorder = hv_Height * 0.1;
            hv_AxesColor = "white";
            hv_Style = "line";
            hv_Clip = "no";
            hv_XTicks = "min_max_origin";
            hv_YTicks = "min_max_origin";
            hv_XGrid = "none";
            hv_YGrid = "none";
            hv_GridColor = "dim gray";
            //
            //Parse generic parameters
            //
            hv_NumGenParamNames = new HTuple(hv_GenParamNames.TupleLength());
            hv_NumGenParamValues = new HTuple(hv_GenParamValues.TupleLength());
            if ((int)(new HTuple(hv_NumGenParamNames.TupleNotEqual(hv_NumGenParamValues))) != 0)
            {
                throw new HalconException("Number of generic parameter names does not match generic parameter values!");
                ho_ContourXGrid.Dispose();
                ho_ContourYGrid.Dispose();
                ho_XArrow.Dispose();
                ho_YArrow.Dispose();
                ho_ContourXTick.Dispose();
                ho_ContourYTick.Dispose();
                ho_Contour.Dispose();
                ho_Cross.Dispose();
                ho_Filled.Dispose();

                return;
            }
            //
            hv_SetOriginXToDefault = 1;
            hv_SetOriginYToDefault = 1;
            for (hv_GenParamIndex = 0; (int)hv_GenParamIndex <= (int)((new HTuple(hv_GenParamNames.TupleLength()
                )) - 1); hv_GenParamIndex = (int)hv_GenParamIndex + 1)
            {
                //
                //Set 'axes_color'
                if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "axes_color"))) != 0)
                {
                    hv_AxesColor = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'style'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "style"))) != 0)
                {
                    hv_Style = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'clip'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "clip"))) != 0)
                {
                    hv_Clip = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    if ((int)((new HTuple(hv_Clip.TupleNotEqual("yes"))).TupleAnd(new HTuple(hv_Clip.TupleNotEqual(
                        "no")))) != 0)
                    {
                        throw new HalconException(("Unsupported clipping option: '" + hv_Clip) + "'");
                    }
                    //
                    //Set 'ticks'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "ticks"))) != 0)
                {
                    hv_XTicks = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_YTicks = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'ticks_x'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "ticks_x"))) != 0)
                {
                    hv_XTicks = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'ticks_y'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "ticks_y"))) != 0)
                {
                    hv_YTicks = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'grid'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "grid"))) != 0)
                {
                    hv_XGrid = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_YGrid = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_XGridTicks = hv_XTicks.Clone();
                    //
                    //Set 'grid_x'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "grid_x"))) != 0)
                {
                    hv_XGrid = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'grid_y'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "grid_y"))) != 0)
                {
                    hv_YGrid = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'grid_color'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "grid_color"))) != 0)
                {
                    hv_GridColor = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'start_x'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "start_x"))) != 0)
                {
                    hv_XAxisStartValue = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'end_x'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "end_x"))) != 0)
                {
                    hv_XAxisEndValue = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'start_y'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "start_y"))) != 0)
                {
                    hv_YAxisStartValue = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'end_y'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "end_y"))) != 0)
                {
                    hv_YAxisEndValue = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'origin_x'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "origin_x"))) != 0)
                {
                    hv_OriginX = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_SetOriginXToDefault = 0;
                    //
                    //Set 'origin_y'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "origin_y"))) != 0)
                {
                    hv_OriginY = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_SetOriginYToDefault = 0;
                    //
                    //Set 'margin'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "margin"))) != 0)
                {
                    hv_LeftBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_RightBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_UpperBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    hv_LowerBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'margin_left'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "margin_left"))) != 0)
                {
                    hv_LeftBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'margin_right'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "margin_right"))) != 0)
                {
                    hv_RightBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'margin_top'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "margin_top"))) != 0)
                {
                    hv_UpperBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                    //
                    //Set 'margin_bottom'
                }
                else if ((int)(new HTuple(((hv_GenParamNames.TupleSelect(hv_GenParamIndex))).TupleEqual(
                    "margin_bottom"))) != 0)
                {
                    hv_LowerBorder = hv_GenParamValues.TupleSelect(hv_GenParamIndex);
                }
                else
                {
                    throw new HalconException(("Unknown generic parameter: '" + (hv_GenParamNames.TupleSelect(
                        hv_GenParamIndex))) + "'");
                }
            }
            //
            //
            //Check consistency of start and end values
            //of the axes.
            if ((int)(new HTuple(hv_XAxisStartValue.TupleGreater(hv_XAxisEndValue))) != 0)
            {
                throw new HalconException("Value for 'start_x' is greater than value for 'end_x'");
            }
            if ((int)(new HTuple(hv_YAxisStartValue.TupleGreater(hv_YAxisEndValue))) != 0)
            {
                throw new HalconException("Value for 'start_y' is greater than value for 'end_y'");
            }
            //
            //Set default origin to lower left corner
            if ((int)(hv_SetOriginXToDefault) != 0)
            {
                hv_OriginX = hv_XAxisStartValue.Clone();
            }
            if ((int)(hv_SetOriginYToDefault) != 0)
            {
                hv_OriginY = hv_YAxisStartValue.Clone();
            }
            //
            //
            //Calculate basic pixel coordinates and scale factors
            //
            hv_XAxisWidthPx = (hv_Width - hv_LeftBorder) - hv_RightBorder;
            hv_XAxisWidth = hv_XAxisEndValue - hv_XAxisStartValue;
            if ((int)(new HTuple(hv_XAxisWidth.TupleEqual(0))) != 0)
            {
                hv_XAxisStartValue = hv_XAxisStartValue - 0.5;
                hv_XAxisEndValue = hv_XAxisEndValue + 0.5;
                hv_XAxisWidth = 1;
            }
            hv_XScaleFactor = hv_XAxisWidthPx / (hv_XAxisWidth.TupleReal());
            hv_YAxisHeightPx = (hv_Height - hv_LowerBorder) - hv_UpperBorder;
            hv_YAxisHeight = hv_YAxisEndValue - hv_YAxisStartValue;
            if ((int)(new HTuple(hv_YAxisHeight.TupleEqual(0))) != 0)
            {
                hv_YAxisStartValue = hv_YAxisStartValue - 0.5;
                hv_YAxisEndValue = hv_YAxisEndValue + 0.5;
                hv_YAxisHeight = 1;
            }
            hv_YScaleFactor = hv_YAxisHeightPx / (hv_YAxisHeight.TupleReal());
            hv_YAxisOffsetPx = (hv_OriginX - hv_XAxisStartValue) * hv_XScaleFactor;
            hv_XAxisOffsetPx = (hv_OriginY - hv_YAxisStartValue) * hv_YScaleFactor;
            //
            //Display grid lines
            //
            if ((int)(new HTuple(hv_GridColor.TupleNotEqual("none"))) != 0)
            {
                hv_DotStyle = new HTuple();
                hv_DotStyle[0] = 5;
                hv_DotStyle[1] = 7;
                HOperatorSet.SetLineStyle(hv_ExpDefaultWinHandle, hv_DotStyle);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_GridColor);
                //
                //Display x grid lines
                if ((int)(new HTuple(hv_XGrid.TupleNotEqual("none"))) != 0)
                {
                    if ((int)(new HTuple(hv_XGrid.TupleEqual("min_max_origin"))) != 0)
                    {
                        //Calculate 'min_max_origin' grid line coordinates
                        if ((int)(new HTuple(hv_OriginX.TupleEqual(hv_XAxisStartValue))) != 0)
                        {
                            hv_XGridValues = new HTuple();
                            hv_XGridValues = hv_XGridValues.TupleConcat(hv_XAxisStartValue);
                            hv_XGridValues = hv_XGridValues.TupleConcat(hv_XAxisEndValue);
                        }
                        else
                        {
                            hv_XGridValues = new HTuple();
                            hv_XGridValues = hv_XGridValues.TupleConcat(hv_XAxisStartValue);
                            hv_XGridValues = hv_XGridValues.TupleConcat(hv_OriginX);
                            hv_XGridValues = hv_XGridValues.TupleConcat(hv_XAxisEndValue);
                        }
                    }
                    else
                    {
                        //Calculate equidistant grid line coordinates
                        hv_XGridStart = (((hv_XAxisStartValue / hv_XGrid)).TupleCeil()) * hv_XGrid;
                        hv_XGridValues = HTuple.TupleGenSequence(hv_XGridStart, hv_XAxisEndValue,
                            hv_XGrid);
                    }
                    hv_XPosition = (hv_XGridValues - hv_XAxisStartValue) * hv_XScaleFactor;
                    //Generate and display grid lines
                    for (hv_IndexGrid = 0; (int)hv_IndexGrid <= (int)((new HTuple(hv_XGridValues.TupleLength()
                        )) - 1); hv_IndexGrid = (int)hv_IndexGrid + 1)
                    {
                        ho_ContourXGrid.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_ContourXGrid, ((hv_Height - hv_LowerBorder)).TupleConcat(
                            hv_UpperBorder), ((hv_LeftBorder + (hv_XPosition.TupleSelect(hv_IndexGrid)))).TupleConcat(
                            hv_LeftBorder + (hv_XPosition.TupleSelect(hv_IndexGrid))));
                        HOperatorSet.DispObj(ho_ContourXGrid, hv_ExpDefaultWinHandle);
                    }
                }
                //
                //Display y grid lines
                if ((int)(new HTuple(hv_YGrid.TupleNotEqual("none"))) != 0)
                {
                    if ((int)(new HTuple(hv_YGrid.TupleEqual("min_max_origin"))) != 0)
                    {
                        //Calculate 'min_max_origin' grid line coordinates
                        if ((int)(new HTuple(hv_OriginY.TupleEqual(hv_YAxisStartValue))) != 0)
                        {
                            hv_YGridValues = new HTuple();
                            hv_YGridValues = hv_YGridValues.TupleConcat(hv_YAxisStartValue);
                            hv_YGridValues = hv_YGridValues.TupleConcat(hv_YAxisEndValue);
                        }
                        else
                        {
                            hv_YGridValues = new HTuple();
                            hv_YGridValues = hv_YGridValues.TupleConcat(hv_YAxisStartValue);
                            hv_YGridValues = hv_YGridValues.TupleConcat(hv_OriginY);
                            hv_YGridValues = hv_YGridValues.TupleConcat(hv_YAxisEndValue);
                        }
                    }
                    else
                    {
                        //Calculate equidistant grid line coordinates
                        hv_YGridStart = (((hv_YAxisStartValue / hv_YGrid)).TupleCeil()) * hv_YGrid;
                        hv_YGridValues = HTuple.TupleGenSequence(hv_YGridStart, hv_YAxisEndValue,
                            hv_YGrid);
                    }
                    hv_YPosition = (hv_YGridValues - hv_YAxisStartValue) * hv_YScaleFactor;
                    //Generate and display grid lines
                    for (hv_IndexGrid = 0; (int)hv_IndexGrid <= (int)((new HTuple(hv_YGridValues.TupleLength()
                        )) - 1); hv_IndexGrid = (int)hv_IndexGrid + 1)
                    {
                        ho_ContourYGrid.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_ContourYGrid, (((hv_Height - hv_LowerBorder) - (hv_YPosition.TupleSelect(
                            hv_IndexGrid)))).TupleConcat((hv_Height - hv_LowerBorder) - (hv_YPosition.TupleSelect(
                            hv_IndexGrid))), hv_LeftBorder.TupleConcat(hv_Width - hv_RightBorder));
                        HOperatorSet.DispObj(ho_ContourYGrid, hv_ExpDefaultWinHandle);
                    }
                }
            }
            HOperatorSet.SetLineStyle(hv_ExpDefaultWinHandle, new HTuple());
            //
            //
            //Display the coordinate sytem axes
            if ((int)(new HTuple(hv_AxesColor.TupleNotEqual("none"))) != 0)
            {
                //Display axes
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_AxesColor);
                ho_XArrow.Dispose();
                gen_arrow_contour_xld(out ho_XArrow, (hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx,
                    hv_LeftBorder, (hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx, hv_Width - hv_RightBorder,
                    0, 0);
                HOperatorSet.DispObj(ho_XArrow, hv_ExpDefaultWinHandle);
                ho_YArrow.Dispose();
                gen_arrow_contour_xld(out ho_YArrow, hv_Height - hv_LowerBorder, hv_LeftBorder + hv_YAxisOffsetPx,
                    hv_UpperBorder, hv_LeftBorder + hv_YAxisOffsetPx, 0, 0);
                HOperatorSet.DispObj(ho_YArrow, hv_ExpDefaultWinHandle);
                //Display labels
                HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_XLabel, out hv_Ascent,
                    out hv_Descent, out hv_TextWidthXLabel, out hv_TextHeightXLabel);
                disp_message(hv_ExpDefaultWinHandle, hv_XLabel, "window", ((hv_Height - hv_LowerBorder) - hv_TextHeightXLabel) - hv_XAxisOffsetPx,
                    ((hv_Width - hv_RightBorder) - hv_TextWidthXLabel) - 3, hv_AxesColor, "false");
                disp_message(hv_ExpDefaultWinHandle, " " + hv_YLabel, "window", hv_UpperBorder,
                    (hv_LeftBorder + 3) + hv_YAxisOffsetPx, hv_AxesColor, "false");
            }
            //
            //Display ticks
            //
            if ((int)(new HTuple(hv_AxesColor.TupleNotEqual("none"))) != 0)
            {
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_AxesColor);
                if ((int)(new HTuple(hv_XTicks.TupleNotEqual("none"))) != 0)
                {
                    //
                    //Display x ticks
                    if ((int)(hv_XValuesAreStrings) != 0)
                    {
                        //Display string XValues as categories
                        hv_XTicks = (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())) / (new HTuple(hv_XTickValues.TupleLength()
                            ));
                        hv_XPosition = (hv_XValues_COPY_INP_TMP - hv_XAxisStartValue) * hv_XScaleFactor;
                    }
                    else
                    {
                        //Display tick values
                        if ((int)(new HTuple(hv_XTicks.TupleEqual("min_max_origin"))) != 0)
                        {
                            //Calculate 'min_max_origin' tick coordinates
                            if ((int)(new HTuple(hv_OriginX.TupleEqual(hv_XAxisStartValue))) != 0)
                            {
                                hv_XTickValues = new HTuple();
                                hv_XTickValues = hv_XTickValues.TupleConcat(hv_XAxisStartValue);
                                hv_XTickValues = hv_XTickValues.TupleConcat(hv_XAxisEndValue);
                            }
                            else
                            {
                                hv_XTickValues = new HTuple();
                                hv_XTickValues = hv_XTickValues.TupleConcat(hv_XAxisStartValue);
                                hv_XTickValues = hv_XTickValues.TupleConcat(hv_OriginX);
                                hv_XTickValues = hv_XTickValues.TupleConcat(hv_XAxisEndValue);
                            }
                        }
                        else
                        {
                            //Calculate equidistant tick coordinates
                            hv_XTickStart = (((hv_XAxisStartValue / hv_XTicks)).TupleCeil()) * hv_XTicks;
                            hv_XTickValues = HTuple.TupleGenSequence(hv_XTickStart, hv_XAxisEndValue,
                                hv_XTicks);
                        }
                        hv_XPosition = (hv_XTickValues - hv_XAxisStartValue) * hv_XScaleFactor;
                        hv_TypeTicks = hv_XTicks.TupleType();
                        if ((int)(new HTuple(hv_TypeTicks.TupleEqual(4))) != 0)
                        {
                            //String ('min_max_origin')
                            //Format depends on actual values
                            hv_TypeTicks = hv_XTickValues.TupleType();
                        }
                        if ((int)(new HTuple(hv_TypeTicks.TupleEqual(1))) != 0)
                        {
                            //Round to integer
                            hv_XTickValues = hv_XTickValues.TupleInt();
                        }
                        else
                        {
                            //Use floating point numbers
                            hv_XTickValues = hv_XTickValues.TupleString(".2f");
                        }
                    }
                    //Generate and display ticks
                    for (hv_IndexTicks = 0; (int)hv_IndexTicks <= (int)((new HTuple(hv_XTickValues.TupleLength()
                        )) - 1); hv_IndexTicks = (int)hv_IndexTicks + 1)
                    {
                        ho_ContourXTick.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_ContourXTick, (((hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx)).TupleConcat(
                            ((hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx) - 5), ((hv_LeftBorder + (hv_XPosition.TupleSelect(
                            hv_IndexTicks)))).TupleConcat(hv_LeftBorder + (hv_XPosition.TupleSelect(
                            hv_IndexTicks))));
                        HOperatorSet.DispObj(ho_ContourXTick, hv_ExpDefaultWinHandle);
                        disp_message(hv_ExpDefaultWinHandle, hv_XTickValues.TupleSelect(hv_IndexTicks),
                            "window", ((hv_Height - hv_LowerBorder) + 2) - hv_XAxisOffsetPx, hv_LeftBorder + (hv_XPosition.TupleSelect(
                            hv_IndexTicks)), hv_AxesColor, "false");
                    }
                }
                //
                if ((int)(new HTuple(hv_YTicks.TupleNotEqual("none"))) != 0)
                {
                    //
                    //Display y ticks
                    if ((int)(new HTuple(hv_YTicks.TupleEqual("min_max_origin"))) != 0)
                    {
                        //Calculate 'min_max_origin' tick coordinates
                        if ((int)(new HTuple(hv_OriginY.TupleEqual(hv_YAxisStartValue))) != 0)
                        {
                            hv_YTickValues = new HTuple();
                            hv_YTickValues = hv_YTickValues.TupleConcat(hv_YAxisStartValue);
                            hv_YTickValues = hv_YTickValues.TupleConcat(hv_YAxisEndValue);
                        }
                        else
                        {
                            hv_YTickValues = new HTuple();
                            hv_YTickValues = hv_YTickValues.TupleConcat(hv_YAxisStartValue);
                            hv_YTickValues = hv_YTickValues.TupleConcat(hv_OriginY);
                            hv_YTickValues = hv_YTickValues.TupleConcat(hv_YAxisEndValue);
                        }
                    }
                    else
                    {
                        //Calculate equidistant tick coordinates
                        hv_YTickStart = (((hv_YAxisStartValue / hv_YTicks)).TupleCeil()) * hv_YTicks;
                        hv_YTickValues = HTuple.TupleGenSequence(hv_YTickStart, hv_YAxisEndValue,
                            hv_YTicks);
                    }
                    hv_YPosition = (hv_YTickValues - hv_YAxisStartValue) * hv_YScaleFactor;
                    hv_TypeTicks = hv_YTicks.TupleType();
                    if ((int)(new HTuple(hv_TypeTicks.TupleEqual(4))) != 0)
                    {
                        //String ('min_max_origin')
                        //Format depends on actual values
                        hv_TypeTicks = hv_YTickValues.TupleType();
                    }
                    if ((int)(new HTuple(hv_TypeTicks.TupleEqual(1))) != 0)
                    {
                        //Round to integer
                        hv_YTickValues = hv_YTickValues.TupleInt();
                    }
                    else
                    {
                        //Use floating point numbers
                        hv_YTickValues = hv_YTickValues.TupleString(".2f");
                    }
                    //Generate and display ticks
                    for (hv_IndexTicks = 0; (int)hv_IndexTicks <= (int)((new HTuple(hv_YTickValues.TupleLength()
                        )) - 1); hv_IndexTicks = (int)hv_IndexTicks + 1)
                    {
                        ho_ContourYTick.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_ContourYTick, (((hv_Height - hv_LowerBorder) - (hv_YPosition.TupleSelect(
                            hv_IndexTicks)))).TupleConcat((hv_Height - hv_LowerBorder) - (hv_YPosition.TupleSelect(
                            hv_IndexTicks))), ((hv_LeftBorder + hv_YAxisOffsetPx)).TupleConcat((hv_LeftBorder + hv_YAxisOffsetPx) + 5));
                        HOperatorSet.DispObj(ho_ContourYTick, hv_ExpDefaultWinHandle);
                        HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_YTickValues.TupleSelect(
                            hv_IndexTicks), out hv_Ascent1, out hv_Descent1, out hv_TextWidthYTicks,
                            out hv_TextHeightYTicks);
                        disp_message(hv_ExpDefaultWinHandle, hv_YTickValues.TupleSelect(hv_IndexTicks),
                            "window", (((hv_Height - hv_LowerBorder) - hv_TextHeightYTicks) + 3) - (hv_YPosition.TupleSelect(
                            hv_IndexTicks)), ((hv_LeftBorder - hv_TextWidthYTicks) - 2) + hv_YAxisOffsetPx,
                            hv_AxesColor, "false");
                    }
                }
            }
            //
            //Display funtion plot
            //
            if ((int)(new HTuple(hv_Color.TupleNotEqual("none"))) != 0)
            {
                if ((int)((new HTuple(hv_XValues_COPY_INP_TMP.TupleNotEqual(new HTuple()))).TupleAnd(
                    new HTuple(hv_YValues_COPY_INP_TMP.TupleNotEqual(new HTuple())))) != 0)
                {
                    hv_Num = (new HTuple(hv_YValues_COPY_INP_TMP.TupleLength())) / (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                        ));
                    //
                    //Iterate over all functions to be displayed
                    HTuple end_val482 = hv_Num - 1;
                    HTuple step_val482 = 1;
                    for (hv_I = 0; hv_I.Continue(end_val482, step_val482); hv_I = hv_I.TupleAdd(step_val482))
                    {
                        //Select y values for current function
                        hv_YSelected = hv_YValues_COPY_INP_TMP.TupleSelectRange(hv_I * (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                            )), ((hv_I + 1) * (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()))) - 1);
                        //Set color
                        if ((int)(new HTuple(hv_Color.TupleEqual(new HTuple()))) != 0)
                        {
                            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
                        }
                        else
                        {
                            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Color.TupleSelect(hv_I % (new HTuple(hv_Color.TupleLength()
                                ))));
                        }
                        //
                        //Display in different styles
                        //
                        if ((int)((new HTuple(hv_Style.TupleEqual("line"))).TupleOr(new HTuple(hv_Style.TupleEqual(
                            new HTuple())))) != 0)
                        {
                            //Line
                            ho_Contour.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_Contour, ((hv_Height - hv_LowerBorder) - (hv_YSelected * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor),
                                ((hv_XValues_COPY_INP_TMP * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor));
                            //Clip, if necessary
                            if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                            {
                                {
                                    HObject ExpTmpOutVar_0;
                                    HOperatorSet.ClipContoursXld(ho_Contour, out ExpTmpOutVar_0, hv_UpperBorder,
                                        hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                    ho_Contour.Dispose();
                                    ho_Contour = ExpTmpOutVar_0;
                                }
                            }
                            HOperatorSet.DispObj(ho_Contour, hv_ExpDefaultWinHandle);
                        }
                        else if ((int)(new HTuple(hv_Style.TupleEqual("cross"))) != 0)
                        {
                            //Cross
                            ho_Cross.Dispose();
                            HOperatorSet.GenCrossContourXld(out ho_Cross, ((hv_Height - hv_LowerBorder) - (hv_YSelected * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor),
                                ((hv_XValues_COPY_INP_TMP * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor),
                                6, 0.785398);
                            //Clip, if necessary
                            if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                            {
                                {
                                    HObject ExpTmpOutVar_0;
                                    HOperatorSet.ClipContoursXld(ho_Cross, out ExpTmpOutVar_0, hv_UpperBorder,
                                        hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                    ho_Cross.Dispose();
                                    ho_Cross = ExpTmpOutVar_0;
                                }
                            }
                            HOperatorSet.DispObj(ho_Cross, hv_ExpDefaultWinHandle);
                        }
                        else if ((int)(new HTuple(hv_Style.TupleEqual("filled"))) != 0)
                        {
                            //Filled
                            hv_Y1Selected = new HTuple();
                            hv_Y1Selected = hv_Y1Selected.TupleConcat(0 + hv_OriginY);
                            hv_Y1Selected = hv_Y1Selected.TupleConcat(hv_YSelected);
                            hv_Y1Selected = hv_Y1Selected.TupleConcat(0 + hv_OriginY);
                            hv_X1Selected = new HTuple();
                            hv_X1Selected = hv_X1Selected.TupleConcat(hv_XValues_COPY_INP_TMP.TupleMin()
                                );
                            hv_X1Selected = hv_X1Selected.TupleConcat(hv_XValues_COPY_INP_TMP);
                            hv_X1Selected = hv_X1Selected.TupleConcat(hv_XValues_COPY_INP_TMP.TupleMax()
                                );
                            HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, "fill");
                            ho_Filled.Dispose();
                            HOperatorSet.GenRegionPolygonFilled(out ho_Filled, ((hv_Height - hv_LowerBorder) - (hv_Y1Selected * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor),
                                ((hv_X1Selected * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor));
                            //Clip, if necessary
                            if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                            {
                                {
                                    HObject ExpTmpOutVar_0;
                                    HOperatorSet.ClipRegion(ho_Filled, out ExpTmpOutVar_0, hv_UpperBorder,
                                        hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                    ho_Filled.Dispose();
                                    ho_Filled = ExpTmpOutVar_0;
                                }
                            }
                            HOperatorSet.DispObj(ho_Filled, hv_ExpDefaultWinHandle);
                        }
                        else
                        {
                            throw new HalconException("Unsupported style: " + hv_Style);
                        }
                    }
                }
            }
            //
            //
            //Reset original display settings
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, hv_PartRow1, hv_PartColumn1, hv_PartRow2,
                hv_PartColumn2);
            //dev_set_window(...);
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, hv_DrawMode);
            HOperatorSet.SetLineStyle(hv_ExpDefaultWinHandle, hv_OriginStyle);
            HOperatorSet.SetSystem("clip_region", hv_ClipRegion);
            ho_ContourXGrid.Dispose();
            ho_ContourYGrid.Dispose();
            ho_XArrow.Dispose();
            ho_YArrow.Dispose();
            ho_ContourXTick.Dispose();
            ho_ContourYTick.Dispose();
            ho_Contour.Dispose();
            ho_Cross.Dispose();
            ho_Filled.Dispose();

            return;
        }

        // Chapter: Filters / Arithmetic
        // Short Description: Scale the gray values of an image from the interval [Min,Max] to [0,255] 
        public void scale_image_range(HObject ho_Image, out HObject ho_ImageScaled, HTuple hv_Min,
            HTuple hv_Max)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_SelectedChannel = null, ho_LowerRegion = null;
            HObject ho_UpperRegion = null;

            // Local copy input parameter variables 
            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = ho_Image.CopyObj(1, -1);



            // Local control variables 

            HTuple hv_LowerLimit = new HTuple(), hv_UpperLimit = new HTuple();
            HTuple hv_Mult = null, hv_Add = null, hv_Channels = null;
            HTuple hv_Index = null, hv_MinGray = new HTuple(), hv_MaxGray = new HTuple();
            HTuple hv_Range = new HTuple();
            HTuple hv_Max_COPY_INP_TMP = hv_Max.Clone();
            HTuple hv_Min_COPY_INP_TMP = hv_Min.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_SelectedChannel);
            HOperatorSet.GenEmptyObj(out ho_LowerRegion);
            HOperatorSet.GenEmptyObj(out ho_UpperRegion);
            //Convenience procedure to scale the gray values of the
            //input image Image from the interval [Min,Max]
            //to the interval [0,255] (default).
            //Gray values < 0 or > 255 (after scaling) are clipped.
            //
            //If the image shall be scaled to an interval different from [0,255],
            //this can be achieved by passing tuples with 2 values [From, To]
            //as Min and Max.
            //Example:
            //scale_image_range(Image:ImageScaled:[100,50],[200,250])
            //maps the gray values of Image from the interval [100,200] to [50,250].
            //All other gray values will be clipped.
            //
            //input parameters:
            //Image: the input image
            //Min: the minimum gray value which will be mapped to 0
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //Max: The maximum gray value which will be mapped to 255
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //
            //output parameter:
            //ImageScale: the resulting scaled image
            //
            if ((int)(new HTuple((new HTuple(hv_Min_COPY_INP_TMP.TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_LowerLimit = hv_Min_COPY_INP_TMP[1];
                hv_Min_COPY_INP_TMP = hv_Min_COPY_INP_TMP[0];
            }
            else
            {
                hv_LowerLimit = 0.0;
            }
            if ((int)(new HTuple((new HTuple(hv_Max_COPY_INP_TMP.TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_UpperLimit = hv_Max_COPY_INP_TMP[1];
                hv_Max_COPY_INP_TMP = hv_Max_COPY_INP_TMP[0];
            }
            else
            {
                hv_UpperLimit = 255.0;
            }
            //
            //Calculate scaling parameters
            hv_Mult = (((hv_UpperLimit - hv_LowerLimit)).TupleReal()) / (hv_Max_COPY_INP_TMP - hv_Min_COPY_INP_TMP);
            hv_Add = ((-hv_Mult) * hv_Min_COPY_INP_TMP) + hv_LowerLimit;
            //
            //Scale image
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ScaleImage(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0, hv_Mult, hv_Add);
                ho_Image_COPY_INP_TMP.Dispose();
                ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
            }
            //
            //Clip gray values if necessary
            //This must be done for each channel separately
            HOperatorSet.CountChannels(ho_Image_COPY_INP_TMP, out hv_Channels);
            HTuple end_val48 = hv_Channels;
            HTuple step_val48 = 1;
            for (hv_Index = 1; hv_Index.Continue(end_val48, step_val48); hv_Index = hv_Index.TupleAdd(step_val48))
            {
                ho_SelectedChannel.Dispose();
                HOperatorSet.AccessChannel(ho_Image_COPY_INP_TMP, out ho_SelectedChannel, hv_Index);
                HOperatorSet.MinMaxGray(ho_SelectedChannel, ho_SelectedChannel, 0, out hv_MinGray,
                    out hv_MaxGray, out hv_Range);
                ho_LowerRegion.Dispose();
                HOperatorSet.Threshold(ho_SelectedChannel, out ho_LowerRegion, ((hv_MinGray.TupleConcat(
                    hv_LowerLimit))).TupleMin(), hv_LowerLimit);
                ho_UpperRegion.Dispose();
                HOperatorSet.Threshold(ho_SelectedChannel, out ho_UpperRegion, hv_UpperLimit,
                    ((hv_UpperLimit.TupleConcat(hv_MaxGray))).TupleMax());
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.PaintRegion(ho_LowerRegion, ho_SelectedChannel, out ExpTmpOutVar_0,
                        hv_LowerLimit, "fill");
                    ho_SelectedChannel.Dispose();
                    ho_SelectedChannel = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.PaintRegion(ho_UpperRegion, ho_SelectedChannel, out ExpTmpOutVar_0,
                        hv_UpperLimit, "fill");
                    ho_SelectedChannel.Dispose();
                    ho_SelectedChannel = ExpTmpOutVar_0;
                }
                if ((int)(new HTuple(hv_Index.TupleEqual(1))) != 0)
                {
                    ho_ImageScaled.Dispose();
                    HOperatorSet.CopyObj(ho_SelectedChannel, out ho_ImageScaled, 1, 1);
                }
                else
                {
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.AppendChannel(ho_ImageScaled, ho_SelectedChannel, out ExpTmpOutVar_0
                            );
                        ho_ImageScaled.Dispose();
                        ho_ImageScaled = ExpTmpOutVar_0;
                    }
                }
            }
            ho_Image_COPY_INP_TMP.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();

            return;
        }

        // Chapter: Graphics / Text
        // Short Description: Set font independent of OS 
        public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
            HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = null, hv_BufferWindowHandle = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Scale = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_SubFamily = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_SystemFonts = new HTuple(), hv_Guess = new HTuple();
            HTuple hv_I = new HTuple(), hv_Index = new HTuple(), hv_AllowedFontSizes = new HTuple();
            HTuple hv_Distances = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_FontSelRegexp = new HTuple(), hv_FontsCourier = new HTuple();
            HTuple hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
            HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
            HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
            HTuple hv_Slant_COPY_INP_TMP = hv_Slant.Clone();

            // Initialize local and output iconic variables 
            //This procedure sets the text font of the current window with
            //the specified attributes.
            //It is assumed that following fonts are installed on the system:
            //Windows: Courier New, Arial Times New Roman
            //Mac OS X: CourierNewPS, Arial, TimesNewRomanPS
            //Linux: courier, helvetica, times
            //Because fonts are displayed smaller on Linux than on Windows,
            //a scaling factor of 1.25 is used the get comparable results.
            //For Linux, only a limited number of font sizes is supported,
            //to get comparable results, it is recommended to use one of the
            //following sizes: 9, 11, 14, 16, 20, 27
            //(which will be mapped internally on Linux systems to 11, 14, 17, 20, 25, 34)
            //
            //Input parameters:
            //WindowHandle: The graphics window for which the font will be set
            //Size: The font size. If Size=-1, the default of 16 is used.
            //Bold: If set to 'true', a bold font is used
            //Slant: If set to 'true', a slanted font is used
            //
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            // dev_get_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
            {
                hv_Size_COPY_INP_TMP = 16;
            }
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                //Set font on Windows systems
                try
                {
                    //Check, if font scaling is switched on
                    //open_window(...);
                    HOperatorSet.SetFont(hv_ExpDefaultWinHandle, "-Consolas-16-*-0-*-*-1-");
                    HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, "test_string", out hv_Ascent,
                        out hv_Descent, out hv_Width, out hv_Height);
                    //Expected width is 110
                    hv_Scale = 110.0 / hv_Width;
                    hv_Size_COPY_INP_TMP = ((hv_Size_COPY_INP_TMP * hv_Scale)).TupleInt();
                    //close_window(...);
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //throw (Exception)
                }
                if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                    new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Courier New";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Consolas";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Arial";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Times New Roman";
                }
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = 1;
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = 0;
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Slant_COPY_INP_TMP = 1;
                }
                else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Slant_COPY_INP_TMP = 0;
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                try
                {
                    HOperatorSet.SetFont(hv_ExpDefaultWinHandle, ((((((("-" + hv_Font_COPY_INP_TMP) + "-") + hv_Size_COPY_INP_TMP) + "-*-") + hv_Slant_COPY_INP_TMP) + "-*-*-") + hv_Bold_COPY_INP_TMP) + "-");
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //throw (Exception)
                }
            }
            else if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Dar"))) != 0)
            {
                //Set font on Mac OS X systems. Since OS X does not have a strict naming
                //scheme for font attributes, we use tables to determine the correct font
                //name.
                hv_SubFamily = 0;
                if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_SubFamily = hv_SubFamily.TupleBor(1);
                }
                else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_SubFamily = hv_SubFamily.TupleBor(2);
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Menlo-Regular";
                    hv_Fonts[1] = "Menlo-Italic";
                    hv_Fonts[2] = "Menlo-Bold";
                    hv_Fonts[3] = "Menlo-BoldItalic";
                }
                else if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                    new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "CourierNewPSMT";
                    hv_Fonts[1] = "CourierNewPS-ItalicMT";
                    hv_Fonts[2] = "CourierNewPS-BoldMT";
                    hv_Fonts[3] = "CourierNewPS-BoldItalicMT";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "ArialMT";
                    hv_Fonts[1] = "Arial-ItalicMT";
                    hv_Fonts[2] = "Arial-BoldMT";
                    hv_Fonts[3] = "Arial-BoldItalicMT";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "TimesNewRomanPSMT";
                    hv_Fonts[1] = "TimesNewRomanPS-ItalicMT";
                    hv_Fonts[2] = "TimesNewRomanPS-BoldMT";
                    hv_Fonts[3] = "TimesNewRomanPS-BoldItalicMT";
                }
                else
                {
                    //Attempt to figure out which of the fonts installed on the system
                    //the user could have meant.
                    HOperatorSet.QueryFont(hv_ExpDefaultWinHandle, out hv_SystemFonts);
                    hv_Fonts = new HTuple();
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Regular");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "MT");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[0] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                    //Guess name of slanted font
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Italic");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-ItalicMT");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Oblique");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[1] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                    //Guess name of bold font
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Bold");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldMT");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[2] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                    //Guess name of bold slanted font
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalic");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalicMT");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldOblique");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[3] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                }
                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_SubFamily);
                try
                {
                    HOperatorSet.SetFont(hv_ExpDefaultWinHandle, (hv_Font_COPY_INP_TMP + "-") + hv_Size_COPY_INP_TMP);
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //throw (Exception)
                }
            }
            else
            {
                //Set font for UNIX systems
                hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP * 1.25;
                hv_AllowedFontSizes = new HTuple();
                hv_AllowedFontSizes[0] = 11;
                hv_AllowedFontSizes[1] = 14;
                hv_AllowedFontSizes[2] = 17;
                hv_AllowedFontSizes[3] = 20;
                hv_AllowedFontSizes[4] = 25;
                hv_AllowedFontSizes[5] = 34;
                if ((int)(new HTuple(((hv_AllowedFontSizes.TupleFind(hv_Size_COPY_INP_TMP))).TupleEqual(
                    -1))) != 0)
                {
                    hv_Distances = ((hv_AllowedFontSizes - hv_Size_COPY_INP_TMP)).TupleAbs();
                    HOperatorSet.TupleSortIndex(hv_Distances, out hv_Indices);
                    hv_Size_COPY_INP_TMP = hv_AllowedFontSizes.TupleSelect(hv_Indices.TupleSelect(
                        0));
                }
                if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
                    "Courier")))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "courier";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "helvetica";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "times";
                }
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = "bold";
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = "medium";
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("times"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = "i";
                    }
                    else
                    {
                        hv_Slant_COPY_INP_TMP = "o";
                    }
                }
                else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Slant_COPY_INP_TMP = "r";
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                try
                {
                    HOperatorSet.SetFont(hv_ExpDefaultWinHandle, ((((((("-adobe-" + hv_Font_COPY_INP_TMP) + "-") + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    if ((int)((new HTuple(((hv_OS.TupleSubstr(0, 4))).TupleEqual("Linux"))).TupleAnd(
                        new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        HOperatorSet.QueryFont(hv_ExpDefaultWinHandle, out hv_Fonts);
                        hv_FontSelRegexp = (("^-[^-]*-[^-]*[Cc]ourier[^-]*-" + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP;
                        hv_FontsCourier = ((hv_Fonts.TupleRegexpSelect(hv_FontSelRegexp))).TupleRegexpMatch(
                            hv_FontSelRegexp);
                        if ((int)(new HTuple((new HTuple(hv_FontsCourier.TupleLength())).TupleEqual(
                            0))) != 0)
                        {
                            hv_Exception = "Wrong font name";
                            //throw (Exception)
                        }
                        else
                        {
                            try
                            {
                                HOperatorSet.SetFont(hv_ExpDefaultWinHandle, (((hv_FontsCourier.TupleSelect(
                                    0)) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                            }
                            // catch (Exception) 
                            catch (HalconException HDevExpDefaultException2)
                            {
                                HDevExpDefaultException2.ToHTuple(out hv_Exception);
                                //throw (Exception)
                            }
                        }
                    }
                    //throw (Exception)
                }
            }
            // dev_set_preferences(...); only in hdevelop

            return;
        }

        // Chapter: Tools / Geometry
        // Short Description: Sort tuple pairs. 
        public void sort_pairs(HTuple hv_T1, HTuple hv_T2, HTuple hv_SortMode, out HTuple hv_Sorted1,
            out HTuple hv_Sorted2)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Indices1 = new HTuple(), hv_Indices2 = new HTuple();
            // Initialize local and output iconic variables 
            hv_Sorted1 = new HTuple();
            hv_Sorted2 = new HTuple();
            //Sort tuple pairs.
            //
            //input parameters:
            //T1: first tuple
            //T2: second tuple
            //SortMode: if set to '1', sort by the first tuple,
            //   if set to '2', sort by the second tuple
            //
            if ((int)((new HTuple(hv_SortMode.TupleEqual("1"))).TupleOr(new HTuple(hv_SortMode.TupleEqual(
                1)))) != 0)
            {
                HOperatorSet.TupleSortIndex(hv_T1, out hv_Indices1);
                hv_Sorted1 = hv_T1.TupleSelect(hv_Indices1);
                hv_Sorted2 = hv_T2.TupleSelect(hv_Indices1);
            }
            else if ((int)((new HTuple((new HTuple(hv_SortMode.TupleEqual("column"))).TupleOr(
                new HTuple(hv_SortMode.TupleEqual("2"))))).TupleOr(new HTuple(hv_SortMode.TupleEqual(
                2)))) != 0)
            {
                HOperatorSet.TupleSortIndex(hv_T2, out hv_Indices2);
                hv_Sorted1 = hv_T1.TupleSelect(hv_Indices2);
                hv_Sorted2 = hv_T2.TupleSelect(hv_Indices2);
            }

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate one or more features of a given image and/or region. 
        public void calculate_features(HObject ho_Region, HObject ho_Image, HTuple hv_FeatureNames,
            out HTuple hv_Features)
        {



            // Initialize local and output iconic variables 
            //
            //Calculate features given in FeatureNames
            //for the input regions in Region
            //(if needed supported by the underlying
            //gray-value or color image Image).
            //
            get_features(ho_Region, ho_Image, hv_FeatureNames, "calculate", out hv_Features);

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Generate a dummy image and region that are, e.g., used to determine the lengths of the feature vectors in get_feature_lengths. 
        public void gen_dummy_objects(out HObject ho_Region, out HObject ho_Image)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Image);
            //
            //Create dummy objects for the feature calculation
            //(may be used to determine the lengths of the
            //vectors etc.).
            //
            ho_Image.Dispose();
            HOperatorSet.GenImageConst(out ho_Image, "byte", 3, 3);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Compose3(ho_Image, ho_Image, ho_Image, out ExpTmpOutVar_0);
                ho_Image.Dispose();
                ho_Image = ExpTmpOutVar_0;
            }
            ho_Region.Dispose();
            HOperatorSet.GetDomain(ho_Image, out ho_Region);

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: List all available feature group names. 
        public void query_feature_group_names(out HTuple hv_GroupNames)
        {


            // Local iconic variables 

            HObject ho_Region, ho_Image;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Image);
            //
            //Return all available feature groups
            //
            ho_Region.Dispose(); ho_Image.Dispose();
            gen_dummy_objects(out ho_Region, out ho_Image);
            get_features(ho_Region, ho_Image, "", "get_groups", out hv_GroupNames);
            hv_GroupNames = ((hv_GroupNames.TupleSort())).TupleUniq();
            hv_GroupNames = hv_GroupNames.TupleConcat("all");
            ho_Region.Dispose();
            ho_Image.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Returns the length of the feature vector for each feature name. 
        public void get_feature_lengths(HTuple hv_FeatureNames, out HTuple hv_Lengths)
        {



            // Local iconic variables 

            HObject ho_Region, ho_Image;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Image);
            //
            //Calculate the lengths of the feature vectors of
            //the features in FeatureNames.
            //
            ho_Region.Dispose(); ho_Image.Dispose();
            gen_dummy_objects(out ho_Region, out ho_Image);
            get_features(ho_Region, ho_Image, hv_FeatureNames, "get_lengths", out hv_Lengths);
            ho_Region.Dispose();
            ho_Image.Dispose();

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Reflect the pose change that was introduced by the user by moving the mouse 
        public void analyze_graph_event(HObject ho_BackgroundImage, HTuple hv_MouseMapping,
            HTuple hv_Button, HTuple hv_Row, HTuple hv_Column, HTuple hv_WindowHandle, HTuple hv_WindowHandleBuffer,
            HTuple hv_VirtualTrackball, HTuple hv_TrackballSize, HTuple hv_SelectedObjectIn,
            HTuple hv_Scene3D, HTuple hv_AlphaOrig, HTuple hv_ObjectModel3DID, HTuple hv_CamParam,
            HTuple hv_Labels, HTuple hv_Title, HTuple hv_Information, HTuple hv_GenParamName,
            HTuple hv_GenParamValue, HTuple hv_PosesIn, HTuple hv_ButtonHoldIn, HTuple hv_TBCenter,
            HTuple hv_TBSize, HTuple hv_WindowCenteredRotationlIn, HTuple hv_MaxNumModels,
            out HTuple hv_PosesOut, out HTuple hv_SelectedObjectOut, out HTuple hv_ButtonHoldOut,
            out HTuple hv_WindowCenteredRotationOut)
        {




            // Local iconic variables 

            HObject ho_ImageDump = null;

            // Local control variables 

            HTuple ExpTmpLocalVar_gIsSinglePose = new HTuple();
            HTuple hv_VisualizeTB = null, hv_InvLog2 = null, hv_Seconds = new HTuple();
            HTuple hv_ModelIndex = new HTuple(), hv_Exception1 = new HTuple();
            HTuple hv_HomMat3DIdentity = new HTuple(), hv_NumModels = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_MinImageSize = new HTuple(), hv_TrackballRadiusPixel = new HTuple();
            HTuple hv_TrackballCenterRow = new HTuple(), hv_TrackballCenterCol = new HTuple();
            HTuple hv_NumChannels = new HTuple(), hv_ColorImage = new HTuple();
            HTuple hv_BAnd = new HTuple(), hv_SensFactor = new HTuple();
            HTuple hv_IsButtonTrans = new HTuple(), hv_IsButtonRot = new HTuple();
            HTuple hv_IsButtonDist = new HTuple(), hv_MRow1 = new HTuple();
            HTuple hv_MCol1 = new HTuple(), hv_ButtonLoop = new HTuple();
            HTuple hv_MRow2 = new HTuple(), hv_MCol2 = new HTuple();
            HTuple hv_PX = new HTuple(), hv_PY = new HTuple(), hv_PZ = new HTuple();
            HTuple hv_QX1 = new HTuple(), hv_QY1 = new HTuple(), hv_QZ1 = new HTuple();
            HTuple hv_QX2 = new HTuple(), hv_QY2 = new HTuple(), hv_QZ2 = new HTuple();
            HTuple hv_Len = new HTuple(), hv_Dist = new HTuple(), hv_Translate = new HTuple();
            HTuple hv_Index = new HTuple(), hv_PoseIn = new HTuple();
            HTuple hv_HomMat3DIn = new HTuple(), hv_HomMat3DOut = new HTuple();
            HTuple hv_PoseOut = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_Sequence = new HTuple(), hv_Mod = new HTuple();
            HTuple hv_SequenceReal = new HTuple(), hv_Sequence2Int = new HTuple();
            HTuple hv_Selected = new HTuple(), hv_InvSelected = new HTuple();
            HTuple hv_Exception = new HTuple(), hv_DRow = new HTuple();
            HTuple hv_TranslateZ = new HTuple(), hv_MX1 = new HTuple();
            HTuple hv_MY1 = new HTuple(), hv_MX2 = new HTuple(), hv_MY2 = new HTuple();
            HTuple hv_RelQuaternion = new HTuple(), hv_HomMat3DRotRel = new HTuple();
            HTuple hv_HomMat3DInTmp1 = new HTuple(), hv_HomMat3DInTmp = new HTuple();
            HTuple hv_PosesOut2 = new HTuple();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_PosesIn_COPY_INP_TMP = hv_PosesIn.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_TBCenter_COPY_INP_TMP = hv_TBCenter.Clone();
            HTuple hv_TBSize_COPY_INP_TMP = hv_TBSize.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageDump);
            //This procedure reflects
            //- the pose change that was introduced by the user by
            //  moving the mouse
            //- the selection of a single object
            //
            //global tuple gIsSinglePose
            //
            hv_ButtonHoldOut = hv_ButtonHoldIn.Clone();
            hv_PosesOut = hv_PosesIn_COPY_INP_TMP.Clone();
            hv_SelectedObjectOut = hv_SelectedObjectIn.Clone();
            hv_WindowCenteredRotationOut = hv_WindowCenteredRotationlIn.Clone();
            hv_VisualizeTB = new HTuple(((hv_SelectedObjectOut.TupleMax())).TupleNotEqual(
                0));
            hv_InvLog2 = 1.0 / ((new HTuple(2)).TupleLog());
            //
            if ((int)(new HTuple(hv_Button.TupleEqual(hv_MouseMapping.TupleSelect(6)))) != 0)
            {
                if ((int)(hv_ButtonHoldOut) != 0)
                {
                    ho_ImageDump.Dispose();

                    return;
                }
                //Ctrl (16) + Alt (32) + left mouse button (1) => Toggle rotation center position
                //If WindowCenteredRotation is not 1, set it to 1, otherwise, set it to 2
                HOperatorSet.CountSeconds(out hv_Seconds);
                if ((int)(new HTuple(hv_WindowCenteredRotationOut.TupleEqual(1))) != 0)
                {
                    hv_WindowCenteredRotationOut = 2;
                }
                else
                {
                    hv_WindowCenteredRotationOut = 1;
                }
                hv_ButtonHoldOut = 1;
                ho_ImageDump.Dispose();

                return;
            }
            if ((int)((new HTuple(hv_Button.TupleEqual(hv_MouseMapping.TupleSelect(5)))).TupleAnd(
                new HTuple((new HTuple(hv_ObjectModel3DID.TupleLength())).TupleLessEqual(
                hv_MaxNumModels)))) != 0)
            {
                if ((int)(hv_ButtonHoldOut) != 0)
                {
                    ho_ImageDump.Dispose();

                    return;
                }
                //Ctrl (16) + left mouse button (1) => Select an object
                try
                {
                    HOperatorSet.SetScene3dParam(hv_Scene3D, "object_index_persistence", "true");
                    HOperatorSet.DisplayScene3d(hv_ExpDefaultWinHandle, hv_Scene3D, 0);
                    HOperatorSet.GetDisplayScene3dInfo(hv_ExpDefaultWinHandle, hv_Scene3D, hv_Row_COPY_INP_TMP,
                        hv_Column_COPY_INP_TMP, "object_index", out hv_ModelIndex);
                    HOperatorSet.SetScene3dParam(hv_Scene3D, "object_index_persistence", "false");
                }
                // catch (Exception1) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception1);
                    //* NO OpenGL, no selection possible
                    ho_ImageDump.Dispose();

                    return;
                }
                if ((int)(new HTuple(hv_ModelIndex.TupleEqual(-1))) != 0)
                {
                    //Background click:
                    if ((int)(new HTuple(((hv_SelectedObjectOut.TupleSum())).TupleEqual(new HTuple(hv_SelectedObjectOut.TupleLength()
                        )))) != 0)
                    {
                        //If all objects are already selected, deselect all
                        hv_SelectedObjectOut = HTuple.TupleGenConst(new HTuple(hv_ObjectModel3DID.TupleLength()
                            ), 0);
                    }
                    else
                    {
                        //Otherwise select all
                        hv_SelectedObjectOut = HTuple.TupleGenConst(new HTuple(hv_ObjectModel3DID.TupleLength()
                            ), 1);
                    }
                }
                else
                {
                    //Object click:
                    if (hv_SelectedObjectOut == null)
                        hv_SelectedObjectOut = new HTuple();
                    hv_SelectedObjectOut[hv_ModelIndex] = ((hv_SelectedObjectOut.TupleSelect(
                        hv_ModelIndex))).TupleNot();
                }
                hv_ButtonHoldOut = 1;
            }
            else
            {
                //Change the pose
                HOperatorSet.HomMat3dIdentity(out hv_HomMat3DIdentity);
                hv_NumModels = new HTuple(hv_ObjectModel3DID.TupleLength());
                hv_Width = hv_CamParam[(new HTuple(hv_CamParam.TupleLength())) - 2];
                hv_Height = hv_CamParam[(new HTuple(hv_CamParam.TupleLength())) - 1];
                hv_MinImageSize = ((hv_Width.TupleConcat(hv_Height))).TupleMin();
                hv_TrackballRadiusPixel = (hv_TrackballSize * hv_MinImageSize) / 2.0;
                //Set trackball fixed in the center of the window
                hv_TrackballCenterRow = hv_Height / 2;
                hv_TrackballCenterCol = hv_Width / 2;
                if ((int)(new HTuple((new HTuple(hv_ObjectModel3DID.TupleLength())).TupleLess(
                    hv_MaxNumModels))) != 0)
                {
                    if ((int)(new HTuple(hv_WindowCenteredRotationOut.TupleEqual(1))) != 0)
                    {
                        get_trackball_center_fixed(hv_SelectedObjectIn, hv_TrackballCenterRow,
                            hv_TrackballCenterCol, hv_TrackballRadiusPixel, hv_Scene3D, hv_ObjectModel3DID,
                            hv_PosesIn_COPY_INP_TMP, hv_WindowHandleBuffer, hv_CamParam, hv_GenParamName,
                            hv_GenParamValue, out hv_TBCenter_COPY_INP_TMP, out hv_TBSize_COPY_INP_TMP);
                    }
                    else
                    {
                        get_trackball_center(hv_SelectedObjectIn, hv_TrackballRadiusPixel, hv_ObjectModel3DID,
                            hv_PosesIn_COPY_INP_TMP, out hv_TBCenter_COPY_INP_TMP, out hv_TBSize_COPY_INP_TMP);
                    }
                }
                if ((int)((new HTuple(((hv_SelectedObjectOut.TupleMin())).TupleEqual(0))).TupleAnd(
                    new HTuple(((hv_SelectedObjectOut.TupleMax())).TupleEqual(1)))) != 0)
                {
                    //At this point, multiple objects do not necessary have the same
                    //pose any more. Consequently, we have to return a tuple of poses
                    //as output of visualize_object_model_3d
                    ExpTmpLocalVar_gIsSinglePose = 0;
                    ExpSetGlobalVar_gIsSinglePose(ExpTmpLocalVar_gIsSinglePose);
                }
                HOperatorSet.CountChannels(ho_BackgroundImage, out hv_NumChannels);
                hv_ColorImage = new HTuple(hv_NumChannels.TupleEqual(3));
                //Alt (32) => lower sensitivity
                HOperatorSet.TupleRsh(hv_Button, 5, out hv_BAnd);
                if ((int)(hv_BAnd % 2) != 0)
                {
                    hv_SensFactor = 0.1;
                }
                else
                {
                    hv_SensFactor = 1.0;
                }
                hv_IsButtonTrans = (new HTuple(((hv_MouseMapping.TupleSelect(0))).TupleEqual(
                    hv_Button))).TupleOr(new HTuple(((32 + (hv_MouseMapping.TupleSelect(0)))).TupleEqual(
                    hv_Button)));
                hv_IsButtonRot = (new HTuple(((hv_MouseMapping.TupleSelect(1))).TupleEqual(
                    hv_Button))).TupleOr(new HTuple(((32 + (hv_MouseMapping.TupleSelect(1)))).TupleEqual(
                    hv_Button)));
                hv_IsButtonDist = (new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(((hv_MouseMapping.TupleSelect(
                    2))).TupleEqual(hv_Button))).TupleOr(new HTuple(((32 + (hv_MouseMapping.TupleSelect(
                    2)))).TupleEqual(hv_Button))))).TupleOr(new HTuple(((hv_MouseMapping.TupleSelect(
                    3))).TupleEqual(hv_Button))))).TupleOr(new HTuple(((32 + (hv_MouseMapping.TupleSelect(
                    3)))).TupleEqual(hv_Button))))).TupleOr(new HTuple(((hv_MouseMapping.TupleSelect(
                    4))).TupleEqual(hv_Button))))).TupleOr(new HTuple(((32 + (hv_MouseMapping.TupleSelect(
                    4)))).TupleEqual(hv_Button)));
                if ((int)(hv_IsButtonTrans) != 0)
                {
                    //Translate in XY-direction
                    hv_MRow1 = hv_Row_COPY_INP_TMP.Clone();
                    hv_MCol1 = hv_Column_COPY_INP_TMP.Clone();
                    while ((int)(hv_IsButtonTrans) != 0)
                    {
                        try
                        {
                            HOperatorSet.GetMpositionSubPix(hv_ExpDefaultWinHandle, out hv_Row_COPY_INP_TMP,
                                out hv_Column_COPY_INP_TMP, out hv_ButtonLoop);
                            hv_IsButtonTrans = new HTuple(hv_ButtonLoop.TupleEqual(hv_Button));
                            hv_MRow2 = hv_MRow1 + ((hv_Row_COPY_INP_TMP - hv_MRow1) * hv_SensFactor);
                            hv_MCol2 = hv_MCol1 + ((hv_Column_COPY_INP_TMP - hv_MCol1) * hv_SensFactor);
                            HOperatorSet.GetLineOfSight(hv_MRow1, hv_MCol1, hv_CamParam, out hv_PX,
                                out hv_PY, out hv_PZ, out hv_QX1, out hv_QY1, out hv_QZ1);
                            HOperatorSet.GetLineOfSight(hv_MRow2, hv_MCol2, hv_CamParam, out hv_PX,
                                out hv_PY, out hv_PZ, out hv_QX2, out hv_QY2, out hv_QZ2);
                            hv_Len = ((((hv_QX1 * hv_QX1) + (hv_QY1 * hv_QY1)) + (hv_QZ1 * hv_QZ1))).TupleSqrt()
                                ;
                            hv_Dist = (((((hv_TBCenter_COPY_INP_TMP.TupleSelect(0)) * (hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                0))) + ((hv_TBCenter_COPY_INP_TMP.TupleSelect(1)) * (hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                1)))) + ((hv_TBCenter_COPY_INP_TMP.TupleSelect(2)) * (hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                2))))).TupleSqrt();
                            hv_Translate = ((((((hv_QX2 - hv_QX1)).TupleConcat(hv_QY2 - hv_QY1))).TupleConcat(
                                hv_QZ2 - hv_QZ1)) * hv_Dist) / hv_Len;
                            hv_PosesOut = new HTuple();
                            if ((int)(new HTuple(hv_NumModels.TupleLessEqual(hv_MaxNumModels))) != 0)
                            {
                                HTuple end_val110 = hv_NumModels - 1;
                                HTuple step_val110 = 1;
                                for (hv_Index = 0; hv_Index.Continue(end_val110, step_val110); hv_Index = hv_Index.TupleAdd(step_val110))
                                {
                                    hv_PoseIn = hv_PosesIn_COPY_INP_TMP.TupleSelectRange(hv_Index * 7, (hv_Index * 7) + 6);
                                    if ((int)(hv_SelectedObjectOut.TupleSelect(hv_Index)) != 0)
                                    {
                                        HOperatorSet.PoseToHomMat3d(hv_PoseIn, out hv_HomMat3DIn);
                                        HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, hv_Translate.TupleSelect(
                                            0), hv_Translate.TupleSelect(1), hv_Translate.TupleSelect(2),
                                            out hv_HomMat3DOut);
                                        HOperatorSet.HomMat3dToPose(hv_HomMat3DOut, out hv_PoseOut);
                                        HOperatorSet.SetScene3dInstancePose(hv_Scene3D, hv_Index, hv_PoseOut);
                                    }
                                    else
                                    {
                                        hv_PoseOut = hv_PoseIn.Clone();
                                    }
                                    hv_PosesOut = hv_PosesOut.TupleConcat(hv_PoseOut);
                                }
                            }
                            else
                            {
                                HOperatorSet.TupleFind(hv_SelectedObjectOut, 1, out hv_Indices);
                                hv_PoseIn = hv_PosesIn_COPY_INP_TMP.TupleSelectRange((hv_Indices.TupleSelect(
                                    0)) * 7, ((hv_Indices.TupleSelect(0)) * 7) + 6);
                                HOperatorSet.PoseToHomMat3d(hv_PoseIn, out hv_HomMat3DIn);
                                HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, hv_Translate.TupleSelect(
                                    0), hv_Translate.TupleSelect(1), hv_Translate.TupleSelect(2), out hv_HomMat3DOut);
                                HOperatorSet.HomMat3dToPose(hv_HomMat3DOut, out hv_PoseOut);
                                hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                                HOperatorSet.TupleMod(hv_Sequence, 7, out hv_Mod);
                                hv_SequenceReal = HTuple.TupleGenSequence(0, hv_NumModels - (1.0 / 7.0),
                                    1.0 / 7.0);
                                hv_Sequence2Int = hv_SequenceReal.TupleInt();
                                HOperatorSet.TupleSelect(hv_SelectedObjectOut, hv_Sequence2Int, out hv_Selected);
                                hv_InvSelected = 1 - hv_Selected;
                                HOperatorSet.TupleSelect(hv_PoseOut, hv_Mod, out hv_PosesOut);
                                hv_PosesOut = (hv_PosesOut * hv_Selected) + (hv_PosesIn_COPY_INP_TMP * hv_InvSelected);
                                HOperatorSet.SetScene3dInstancePose(hv_Scene3D, HTuple.TupleGenSequence(
                                    0, hv_NumModels - 1, 1), hv_PosesOut);
                            }
                            dump_image_output(ho_BackgroundImage, hv_ExpDefaultWinHandle, hv_Scene3D,
                                hv_AlphaOrig, hv_ObjectModel3DID, hv_GenParamName, hv_GenParamValue,
                                hv_CamParam, hv_PosesOut, hv_ColorImage, hv_Title, hv_Information,
                                hv_Labels, hv_VisualizeTB, "true", hv_TrackballCenterRow, hv_TrackballCenterCol,
                                hv_TBSize_COPY_INP_TMP, hv_SelectedObjectOut, new HTuple(hv_WindowCenteredRotationOut.TupleEqual(
                                1)), hv_TBCenter_COPY_INP_TMP);
                            ho_ImageDump.Dispose();
                            HOperatorSet.DumpWindowImage(out ho_ImageDump, hv_ExpDefaultWinHandle);
                            //dev_set_window(...);
                            HOperatorSet.DispObj(ho_ImageDump, hv_ExpDefaultWinHandle);
                            //
                            hv_MRow1 = hv_Row_COPY_INP_TMP.Clone();
                            hv_MCol1 = hv_Column_COPY_INP_TMP.Clone();
                            hv_PosesIn_COPY_INP_TMP = hv_PosesOut.Clone();
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            //Keep waiting
                        }
                    }
                }
                else if ((int)(hv_IsButtonDist) != 0)
                {
                    //Change the Z distance
                    hv_MRow1 = hv_Row_COPY_INP_TMP.Clone();
                    while ((int)(hv_IsButtonDist) != 0)
                    {
                        try
                        {
                            HOperatorSet.GetMpositionSubPix(hv_ExpDefaultWinHandle, out hv_Row_COPY_INP_TMP,
                                out hv_Column_COPY_INP_TMP, out hv_ButtonLoop);
                            hv_IsButtonDist = new HTuple(hv_ButtonLoop.TupleEqual(hv_Button));
                            hv_MRow2 = hv_Row_COPY_INP_TMP.Clone();
                            hv_DRow = hv_MRow2 - hv_MRow1;
                            hv_Dist = (((((hv_TBCenter_COPY_INP_TMP.TupleSelect(0)) * (hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                0))) + ((hv_TBCenter_COPY_INP_TMP.TupleSelect(1)) * (hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                1)))) + ((hv_TBCenter_COPY_INP_TMP.TupleSelect(2)) * (hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                2))))).TupleSqrt();
                            hv_TranslateZ = (((-hv_Dist) * hv_DRow) * 0.003) * hv_SensFactor;
                            if (hv_TBCenter_COPY_INP_TMP == null)
                                hv_TBCenter_COPY_INP_TMP = new HTuple();
                            hv_TBCenter_COPY_INP_TMP[2] = (hv_TBCenter_COPY_INP_TMP.TupleSelect(2)) + hv_TranslateZ;
                            hv_PosesOut = new HTuple();
                            if ((int)(new HTuple(hv_NumModels.TupleLessEqual(hv_MaxNumModels))) != 0)
                            {
                                HTuple end_val164 = hv_NumModels - 1;
                                HTuple step_val164 = 1;
                                for (hv_Index = 0; hv_Index.Continue(end_val164, step_val164); hv_Index = hv_Index.TupleAdd(step_val164))
                                {
                                    hv_PoseIn = hv_PosesIn_COPY_INP_TMP.TupleSelectRange(hv_Index * 7, (hv_Index * 7) + 6);
                                    if ((int)(hv_SelectedObjectOut.TupleSelect(hv_Index)) != 0)
                                    {
                                        //Transform the whole scene or selected object only
                                        HOperatorSet.PoseToHomMat3d(hv_PoseIn, out hv_HomMat3DIn);
                                        HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, 0, 0, hv_TranslateZ,
                                            out hv_HomMat3DOut);
                                        HOperatorSet.HomMat3dToPose(hv_HomMat3DOut, out hv_PoseOut);
                                        HOperatorSet.SetScene3dInstancePose(hv_Scene3D, hv_Index, hv_PoseOut);
                                    }
                                    else
                                    {
                                        hv_PoseOut = hv_PoseIn.Clone();
                                    }
                                    hv_PosesOut = hv_PosesOut.TupleConcat(hv_PoseOut);
                                }
                            }
                            else
                            {
                                HOperatorSet.TupleFind(hv_SelectedObjectOut, 1, out hv_Indices);
                                hv_PoseIn = hv_PosesIn_COPY_INP_TMP.TupleSelectRange((hv_Indices.TupleSelect(
                                    0)) * 7, ((hv_Indices.TupleSelect(0)) * 7) + 6);
                                HOperatorSet.PoseToHomMat3d(hv_PoseIn, out hv_HomMat3DIn);
                                HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, 0, 0, hv_TranslateZ,
                                    out hv_HomMat3DOut);
                                HOperatorSet.HomMat3dToPose(hv_HomMat3DOut, out hv_PoseOut);
                                hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                                HOperatorSet.TupleMod(hv_Sequence, 7, out hv_Mod);
                                hv_SequenceReal = HTuple.TupleGenSequence(0, hv_NumModels - (1.0 / 7.0),
                                    1.0 / 7.0);
                                hv_Sequence2Int = hv_SequenceReal.TupleInt();
                                HOperatorSet.TupleSelect(hv_SelectedObjectOut, hv_Sequence2Int, out hv_Selected);
                                hv_InvSelected = 1 - hv_Selected;
                                HOperatorSet.TupleSelect(hv_PoseOut, hv_Mod, out hv_PosesOut);
                                hv_PosesOut = (hv_PosesOut * hv_Selected) + (hv_PosesIn_COPY_INP_TMP * hv_InvSelected);
                                HOperatorSet.SetScene3dInstancePose(hv_Scene3D, HTuple.TupleGenSequence(
                                    0, hv_NumModels - 1, 1), hv_PosesOut);
                            }
                            dump_image_output(ho_BackgroundImage, hv_ExpDefaultWinHandle, hv_Scene3D,
                                hv_AlphaOrig, hv_ObjectModel3DID, hv_GenParamName, hv_GenParamValue,
                                hv_CamParam, hv_PosesOut, hv_ColorImage, hv_Title, hv_Information,
                                hv_Labels, hv_VisualizeTB, "true", hv_TrackballCenterRow, hv_TrackballCenterCol,
                                hv_TBSize_COPY_INP_TMP, hv_SelectedObjectOut, hv_WindowCenteredRotationOut,
                                hv_TBCenter_COPY_INP_TMP);
                            ho_ImageDump.Dispose();
                            HOperatorSet.DumpWindowImage(out ho_ImageDump, hv_ExpDefaultWinHandle);
                            //dev_set_window(...);
                            HOperatorSet.DispObj(ho_ImageDump, hv_ExpDefaultWinHandle);
                            //
                            hv_MRow1 = hv_Row_COPY_INP_TMP.Clone();
                            hv_PosesIn_COPY_INP_TMP = hv_PosesOut.Clone();
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            //Keep waiting
                        }
                    }
                }
                else if ((int)(hv_IsButtonRot) != 0)
                {
                    //Rotate the object
                    hv_MRow1 = hv_Row_COPY_INP_TMP.Clone();
                    hv_MCol1 = hv_Column_COPY_INP_TMP.Clone();
                    while ((int)(hv_IsButtonRot) != 0)
                    {
                        try
                        {
                            HOperatorSet.GetMpositionSubPix(hv_ExpDefaultWinHandle, out hv_Row_COPY_INP_TMP,
                                out hv_Column_COPY_INP_TMP, out hv_ButtonLoop);
                            hv_IsButtonRot = new HTuple(hv_ButtonLoop.TupleEqual(hv_Button));
                            hv_MRow2 = hv_Row_COPY_INP_TMP.Clone();
                            hv_MCol2 = hv_Column_COPY_INP_TMP.Clone();
                            //Transform the pixel coordinates to relative image coordinates
                            hv_MX1 = (hv_TrackballCenterCol - hv_MCol1) / (0.5 * hv_MinImageSize);
                            hv_MY1 = (hv_TrackballCenterRow - hv_MRow1) / (0.5 * hv_MinImageSize);
                            hv_MX2 = (hv_TrackballCenterCol - hv_MCol2) / (0.5 * hv_MinImageSize);
                            hv_MY2 = (hv_TrackballCenterRow - hv_MRow2) / (0.5 * hv_MinImageSize);
                            //Compute the quaternion rotation that corresponds to the mouse
                            //movement
                            trackball(hv_MX1, hv_MY1, hv_MX2, hv_MY2, hv_VirtualTrackball, hv_TrackballSize,
                                hv_SensFactor, out hv_RelQuaternion);
                            //Transform the quaternion to a rotation matrix
                            HOperatorSet.QuatToHomMat3d(hv_RelQuaternion, out hv_HomMat3DRotRel);
                            hv_PosesOut = new HTuple();
                            if ((int)(new HTuple(hv_NumModels.TupleLessEqual(hv_MaxNumModels))) != 0)
                            {
                                HTuple end_val226 = hv_NumModels - 1;
                                HTuple step_val226 = 1;
                                for (hv_Index = 0; hv_Index.Continue(end_val226, step_val226); hv_Index = hv_Index.TupleAdd(step_val226))
                                {
                                    hv_PoseIn = hv_PosesIn_COPY_INP_TMP.TupleSelectRange(hv_Index * 7, (hv_Index * 7) + 6);
                                    if ((int)(hv_SelectedObjectOut.TupleSelect(hv_Index)) != 0)
                                    {
                                        //Transform the whole scene or selected object only
                                        HOperatorSet.PoseToHomMat3d(hv_PoseIn, out hv_HomMat3DIn);
                                        HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, -(hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                            0)), -(hv_TBCenter_COPY_INP_TMP.TupleSelect(1)), -(hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                            2)), out hv_HomMat3DIn);
                                        HOperatorSet.HomMat3dCompose(hv_HomMat3DRotRel, hv_HomMat3DIn,
                                            out hv_HomMat3DIn);
                                        HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                            0), hv_TBCenter_COPY_INP_TMP.TupleSelect(1), hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                            2), out hv_HomMat3DOut);
                                        HOperatorSet.HomMat3dToPose(hv_HomMat3DOut, out hv_PoseOut);
                                        HOperatorSet.SetScene3dInstancePose(hv_Scene3D, hv_Index, hv_PoseOut);
                                    }
                                    else
                                    {
                                        hv_PoseOut = hv_PoseIn.Clone();
                                    }
                                    hv_PosesOut = hv_PosesOut.TupleConcat(hv_PoseOut);
                                }
                            }
                            else
                            {
                                HOperatorSet.TupleFind(hv_SelectedObjectOut, 1, out hv_Indices);
                                hv_PoseIn = hv_PosesIn_COPY_INP_TMP.TupleSelectRange((hv_Indices.TupleSelect(
                                    0)) * 7, ((hv_Indices.TupleSelect(0)) * 7) + 6);
                                HOperatorSet.PoseToHomMat3d(hv_PoseIn, out hv_HomMat3DIn);
                                HOperatorSet.HomMat3dTranslate(hv_HomMat3DIn, -(hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                    0)), -(hv_TBCenter_COPY_INP_TMP.TupleSelect(1)), -(hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                    2)), out hv_HomMat3DInTmp1);
                                HOperatorSet.HomMat3dCompose(hv_HomMat3DRotRel, hv_HomMat3DInTmp1,
                                    out hv_HomMat3DInTmp);
                                HOperatorSet.HomMat3dTranslate(hv_HomMat3DInTmp, hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                    0), hv_TBCenter_COPY_INP_TMP.TupleSelect(1), hv_TBCenter_COPY_INP_TMP.TupleSelect(
                                    2), out hv_HomMat3DOut);
                                HOperatorSet.HomMat3dToPose(hv_HomMat3DOut, out hv_PoseOut);
                                hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                                HOperatorSet.TupleMod(hv_Sequence, 7, out hv_Mod);
                                hv_SequenceReal = HTuple.TupleGenSequence(0, hv_NumModels - (1.0 / 7.0),
                                    1.0 / 7.0);
                                hv_Sequence2Int = hv_SequenceReal.TupleInt();
                                HOperatorSet.TupleSelect(hv_SelectedObjectOut, hv_Sequence2Int, out hv_Selected);
                                hv_InvSelected = 1 - hv_Selected;
                                HOperatorSet.TupleSelect(hv_PoseOut, hv_Mod, out hv_PosesOut);
                                hv_PosesOut2 = (hv_PosesOut * hv_Selected) + (hv_PosesIn_COPY_INP_TMP * hv_InvSelected);
                                hv_PosesOut = hv_PosesOut2.Clone();
                                HOperatorSet.SetScene3dInstancePose(hv_Scene3D, HTuple.TupleGenSequence(
                                    0, hv_NumModels - 1, 1), hv_PosesOut);
                            }
                            dump_image_output(ho_BackgroundImage, hv_ExpDefaultWinHandle, hv_Scene3D,
                                hv_AlphaOrig, hv_ObjectModel3DID, hv_GenParamName, hv_GenParamValue,
                                hv_CamParam, hv_PosesOut, hv_ColorImage, hv_Title, hv_Information,
                                hv_Labels, hv_VisualizeTB, "true", hv_TrackballCenterRow, hv_TrackballCenterCol,
                                hv_TBSize_COPY_INP_TMP, hv_SelectedObjectOut, hv_WindowCenteredRotationOut,
                                hv_TBCenter_COPY_INP_TMP);
                            ho_ImageDump.Dispose();
                            HOperatorSet.DumpWindowImage(out ho_ImageDump, hv_ExpDefaultWinHandle);
                            //dev_set_window(...);
                            HOperatorSet.DispObj(ho_ImageDump, hv_ExpDefaultWinHandle);
                            //
                            hv_MRow1 = hv_Row_COPY_INP_TMP.Clone();
                            hv_MCol1 = hv_Column_COPY_INP_TMP.Clone();
                            hv_PosesIn_COPY_INP_TMP = hv_PosesOut.Clone();
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            //Keep waiting
                        }
                    }
                }
                hv_PosesOut = hv_PosesIn_COPY_INP_TMP.Clone();
            }
            ho_ImageDump.Dispose();

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Determine the optimum distance of the object to obtain a reasonable visualization 
        public void determine_optimum_pose_distance(HTuple hv_ObjectModel3DID, HTuple hv_CamParam,
            HTuple hv_ImageCoverage, HTuple hv_PoseIn, out HTuple hv_PoseOut)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_NumModels = null, hv_Rows = null;
            HTuple hv_Cols = null, hv_MinMinZ = null, hv_BB = null;
            HTuple hv_Seq = null, hv_DXMax = null, hv_DYMax = null;
            HTuple hv_DZMax = null, hv_Diameter = null, hv_ZAdd = null;
            HTuple hv_IBB = null, hv_BB0 = null, hv_BB1 = null, hv_BB2 = null;
            HTuple hv_BB3 = null, hv_BB4 = null, hv_BB5 = null, hv_X = null;
            HTuple hv_Y = null, hv_Z = null, hv_PoseInter = null, hv_HomMat3D = null;
            HTuple hv_CX = null, hv_CY = null, hv_CZ = null, hv_DR = null;
            HTuple hv_DC = null, hv_MaxDist = null, hv_HomMat3DRotate = new HTuple();
            HTuple hv_MinImageSize = null, hv_Zs = null, hv_ZDiff = null;
            HTuple hv_ScaleZ = null, hv_ZNew = null;
            // Initialize local and output iconic variables 
            //Determine the optimum distance of the object to obtain
            //a reasonable visualization
            //
            hv_NumModels = new HTuple(hv_ObjectModel3DID.TupleLength());
            hv_Rows = new HTuple();
            hv_Cols = new HTuple();
            hv_MinMinZ = 1e30;
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "bounding_box1", out hv_BB);
            //Calculate diameter over all objects to be visualized
            hv_Seq = HTuple.TupleGenSequence(0, (new HTuple(hv_BB.TupleLength())) - 1, 6);
            hv_DXMax = (((hv_BB.TupleSelect(hv_Seq + 3))).TupleMax()) - (((hv_BB.TupleSelect(
                hv_Seq))).TupleMin());
            hv_DYMax = (((hv_BB.TupleSelect(hv_Seq + 4))).TupleMax()) - (((hv_BB.TupleSelect(
                hv_Seq + 1))).TupleMin());
            hv_DZMax = (((hv_BB.TupleSelect(hv_Seq + 5))).TupleMax()) - (((hv_BB.TupleSelect(
                hv_Seq + 2))).TupleMin());
            hv_Diameter = ((((hv_DXMax * hv_DXMax) + (hv_DYMax * hv_DYMax)) + (hv_DZMax * hv_DZMax))).TupleSqrt()
                ;
            if ((int)(new HTuple(((((hv_BB.TupleAbs())).TupleSum())).TupleEqual(0.0))) != 0)
            {
                hv_BB = new HTuple();
                hv_BB = hv_BB.TupleConcat(-((new HTuple(HTuple.TupleRand(
                    3) * 1e-20)).TupleAbs()));
                hv_BB = hv_BB.TupleConcat((new HTuple(HTuple.TupleRand(
                    3) * 1e-20)).TupleAbs());
            }
            //Allow the visualization of single points or extremely small objects
            hv_ZAdd = 0.0;
            if ((int)(new HTuple(((hv_Diameter.TupleMax())).TupleLess(1e-10))) != 0)
            {
                hv_ZAdd = 0.01;
            }
            //Set extremely small diameters to 1e-10 to avoid CZ == 0.0, which would lead
            //to projection errors
            if ((int)(new HTuple(((hv_Diameter.TupleMin())).TupleLess(1e-10))) != 0)
            {
                hv_Diameter = hv_Diameter - (((((((hv_Diameter - 1e-10)).TupleSgn()) - 1)).TupleSgn()
                    ) * 1e-10);
            }
            hv_IBB = HTuple.TupleGenSequence(0, (new HTuple(hv_BB.TupleLength())) - 1, 6);
            hv_BB0 = hv_BB.TupleSelect(hv_IBB);
            hv_BB1 = hv_BB.TupleSelect(hv_IBB + 1);
            hv_BB2 = hv_BB.TupleSelect(hv_IBB + 2);
            hv_BB3 = hv_BB.TupleSelect(hv_IBB + 3);
            hv_BB4 = hv_BB.TupleSelect(hv_IBB + 4);
            hv_BB5 = hv_BB.TupleSelect(hv_IBB + 5);
            hv_X = new HTuple();
            hv_X = hv_X.TupleConcat(hv_BB0);
            hv_X = hv_X.TupleConcat(hv_BB3);
            hv_X = hv_X.TupleConcat(hv_BB0);
            hv_X = hv_X.TupleConcat(hv_BB0);
            hv_X = hv_X.TupleConcat(hv_BB3);
            hv_X = hv_X.TupleConcat(hv_BB3);
            hv_X = hv_X.TupleConcat(hv_BB0);
            hv_X = hv_X.TupleConcat(hv_BB3);
            hv_Y = new HTuple();
            hv_Y = hv_Y.TupleConcat(hv_BB1);
            hv_Y = hv_Y.TupleConcat(hv_BB1);
            hv_Y = hv_Y.TupleConcat(hv_BB4);
            hv_Y = hv_Y.TupleConcat(hv_BB1);
            hv_Y = hv_Y.TupleConcat(hv_BB4);
            hv_Y = hv_Y.TupleConcat(hv_BB1);
            hv_Y = hv_Y.TupleConcat(hv_BB4);
            hv_Y = hv_Y.TupleConcat(hv_BB4);
            hv_Z = new HTuple();
            hv_Z = hv_Z.TupleConcat(hv_BB2);
            hv_Z = hv_Z.TupleConcat(hv_BB2);
            hv_Z = hv_Z.TupleConcat(hv_BB2);
            hv_Z = hv_Z.TupleConcat(hv_BB5);
            hv_Z = hv_Z.TupleConcat(hv_BB2);
            hv_Z = hv_Z.TupleConcat(hv_BB5);
            hv_Z = hv_Z.TupleConcat(hv_BB5);
            hv_Z = hv_Z.TupleConcat(hv_BB5);
            hv_PoseInter = hv_PoseIn.TupleReplace(2, (-(hv_Z.TupleMin())) + (2 * (hv_Diameter.TupleMax()
                )));
            HOperatorSet.PoseToHomMat3d(hv_PoseInter, out hv_HomMat3D);
            //Determine the maximum extention of the projection
            HOperatorSet.AffineTransPoint3d(hv_HomMat3D, hv_X, hv_Y, hv_Z, out hv_CX, out hv_CY,
                out hv_CZ);
            HOperatorSet.Project3dPoint(hv_CX, hv_CY, hv_CZ, hv_CamParam, out hv_Rows, out hv_Cols);
            hv_MinMinZ = hv_CZ.TupleMin();
            hv_DR = hv_Rows - (hv_CamParam.TupleSelect((new HTuple(hv_CamParam.TupleLength()
                )) - 3));
            hv_DC = hv_Cols - (hv_CamParam.TupleSelect((new HTuple(hv_CamParam.TupleLength()
                )) - 4));
            hv_DR = (hv_DR.TupleMax()) - (hv_DR.TupleMin());
            hv_DC = (hv_DC.TupleMax()) - (hv_DC.TupleMin());
            hv_MaxDist = (((hv_DR * hv_DR) + (hv_DC * hv_DC))).TupleSqrt();
            //
            if ((int)(new HTuple(hv_MaxDist.TupleLess(1e-10))) != 0)
            {
                //If the object has no extension in the above projection (looking along
                //a line), we determine the extension of the object in a rotated view
                HOperatorSet.HomMat3dRotateLocal(hv_HomMat3D, (new HTuple(90)).TupleRad(),
                    "x", out hv_HomMat3DRotate);
                HOperatorSet.AffineTransPoint3d(hv_HomMat3DRotate, hv_X, hv_Y, hv_Z, out hv_CX,
                    out hv_CY, out hv_CZ);
                HOperatorSet.Project3dPoint(hv_CX, hv_CY, hv_CZ, hv_CamParam, out hv_Rows,
                    out hv_Cols);
                hv_DR = hv_Rows - (hv_CamParam.TupleSelect((new HTuple(hv_CamParam.TupleLength()
                    )) - 3));
                hv_DC = hv_Cols - (hv_CamParam.TupleSelect((new HTuple(hv_CamParam.TupleLength()
                    )) - 4));
                hv_DR = (hv_DR.TupleMax()) - (hv_DR.TupleMin());
                hv_DC = (hv_DC.TupleMax()) - (hv_DC.TupleMin());
                hv_MaxDist = ((hv_MaxDist.TupleConcat((((hv_DR * hv_DR) + (hv_DC * hv_DC))).TupleSqrt()
                    ))).TupleMax();
            }
            //
            hv_MinImageSize = ((((hv_CamParam.TupleSelect((new HTuple(hv_CamParam.TupleLength()
                )) - 2))).TupleConcat(hv_CamParam.TupleSelect((new HTuple(hv_CamParam.TupleLength()
                )) - 1)))).TupleMin();
            //
            hv_Z = hv_PoseInter[2];
            hv_Zs = hv_MinMinZ.Clone();
            hv_ZDiff = hv_Z - hv_Zs;
            hv_ScaleZ = hv_MaxDist / (((0.5 * hv_MinImageSize) * hv_ImageCoverage) * 2.0);
            hv_ZNew = ((hv_ScaleZ * hv_Zs) + hv_ZDiff) + hv_ZAdd;
            hv_PoseOut = hv_PoseInter.TupleReplace(2, hv_ZNew);
            //

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Can replace disp_object_model_3d if there is no OpenGL available. 
        public void disp_object_model_no_opengl(out HObject ho_ModelContours, HTuple hv_ObjectModel3DID,
            HTuple hv_GenParamName, HTuple hv_GenParamValue, HTuple hv_WindowHandleBuffer,
            HTuple hv_CamParam, HTuple hv_PosesOut)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Idx = null, hv_CustomParamName = new HTuple();
            HTuple hv_CustomParamValue = new HTuple(), hv_Font = null;
            HTuple hv_IndicesDispBackGround = null, hv_Indices = new HTuple();
            HTuple hv_HasPolygons = null, hv_HasTri = null, hv_HasPoints = null;
            HTuple hv_HasLines = null, hv_NumPoints = null, hv_IsPrimitive = null;
            HTuple hv_Center = null, hv_Diameter = null, hv_OpenGlHiddenSurface = null;
            HTuple hv_CenterX = null, hv_CenterY = null, hv_CenterZ = null;
            HTuple hv_PosObjectsZ = null, hv_I = new HTuple(), hv_Pose = new HTuple();
            HTuple hv_HomMat3DObj = new HTuple(), hv_PosObjCenterX = new HTuple();
            HTuple hv_PosObjCenterY = new HTuple(), hv_PosObjCenterZ = new HTuple();
            HTuple hv_PosObjectsX = new HTuple(), hv_PosObjectsY = new HTuple();
            HTuple hv_Color = null, hv_Indices1 = new HTuple(), hv_IndicesIntensities = new HTuple();
            HTuple hv_Indices2 = new HTuple(), hv_J = null, hv_Indices3 = new HTuple();
            HTuple hv_HomMat3D = new HTuple(), hv_SampledObjectModel3D = new HTuple();
            HTuple hv_X = new HTuple(), hv_Y = new HTuple(), hv_Z = new HTuple();
            HTuple hv_HomMat3D1 = new HTuple(), hv_Qx = new HTuple();
            HTuple hv_Qy = new HTuple(), hv_Qz = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_ObjectModel3DConvexHull = new HTuple();
            HTuple hv_Exception = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            //This procedure allows to use project_object_model_3d to simulate a disp_object_model_3d
            //call for small objects. Large objects are sampled down to display.
            hv_Idx = hv_GenParamName.TupleFind("point_size");
            if ((int)((new HTuple(hv_Idx.TupleLength())).TupleAnd(new HTuple(hv_Idx.TupleNotEqual(
                -1)))) != 0)
            {
                hv_CustomParamName = "point_size";
                hv_CustomParamValue = hv_GenParamValue.TupleSelect(hv_Idx);
                if ((int)(new HTuple(hv_CustomParamValue.TupleEqual(1))) != 0)
                {
                    hv_CustomParamValue = 0;
                }
            }
            else
            {
                hv_CustomParamName = new HTuple();
                hv_CustomParamValue = new HTuple();
            }
            HOperatorSet.GetFont(hv_ExpDefaultWinHandle, out hv_Font);
            HOperatorSet.TupleFind(hv_GenParamName, "disp_background", out hv_IndicesDispBackGround);
            if ((int)(new HTuple(hv_IndicesDispBackGround.TupleNotEqual(-1))) != 0)
            {
                HOperatorSet.TupleFind(hv_GenParamName.TupleSelect(hv_IndicesDispBackGround),
                    "false", out hv_Indices);
                if ((int)(new HTuple(hv_Indices.TupleNotEqual(-1))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
                }
            }
            set_display_font(hv_ExpDefaultWinHandle, 11, "mono", "false", "false");
            disp_message(hv_ExpDefaultWinHandle, "OpenGL missing!", "image", 5, (hv_CamParam.TupleSelect(
                6)) - 130, "red", "false");
            HOperatorSet.SetFont(hv_ExpDefaultWinHandle, hv_Font);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "has_polygons", out hv_HasPolygons);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "has_triangles", out hv_HasTri);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "has_points", out hv_HasPoints);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "has_lines", out hv_HasLines);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "num_points", out hv_NumPoints);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "has_primitive_data",
                out hv_IsPrimitive);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "center", out hv_Center);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "diameter", out hv_Diameter);
            HOperatorSet.GetSystem("opengl_hidden_surface_removal_enable", out hv_OpenGlHiddenSurface);
            HOperatorSet.SetSystem("opengl_hidden_surface_removal_enable", "false");
            //Sort the objects by inverse z
            hv_CenterX = hv_Center[HTuple.TupleGenSequence(0, (new HTuple(hv_Center.TupleLength()
                )) - 1, 3)];
            hv_CenterY = hv_Center[HTuple.TupleGenSequence(0, (new HTuple(hv_Center.TupleLength()
                )) - 1, 3) + 1];
            hv_CenterZ = hv_Center[HTuple.TupleGenSequence(0, (new HTuple(hv_Center.TupleLength()
                )) - 1, 3) + 2];
            hv_PosObjectsZ = new HTuple();
            if ((int)(new HTuple((new HTuple(hv_PosesOut.TupleLength())).TupleGreater(7))) != 0)
            {
                for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_ObjectModel3DID.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                {
                    hv_Pose = hv_PosesOut.TupleSelectRange(hv_I * 7, (hv_I * 7) + 6);
                    HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3DObj);
                    HOperatorSet.AffineTransPoint3d(hv_HomMat3DObj, hv_CenterX.TupleSelect(hv_I),
                        hv_CenterY.TupleSelect(hv_I), hv_CenterZ.TupleSelect(hv_I), out hv_PosObjCenterX,
                        out hv_PosObjCenterY, out hv_PosObjCenterZ);
                    hv_PosObjectsZ = hv_PosObjectsZ.TupleConcat(hv_PosObjCenterZ);
                }
            }
            else
            {
                hv_Pose = hv_PosesOut.TupleSelectRange(0, 6);
                HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3DObj);
                HOperatorSet.AffineTransPoint3d(hv_HomMat3DObj, hv_CenterX, hv_CenterY, hv_CenterZ,
                    out hv_PosObjectsX, out hv_PosObjectsY, out hv_PosObjectsZ);
            }
            hv_Idx = ((hv_PosObjectsZ.TupleSortIndex())).TupleInverse();
            hv_Color = "white";
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Color);
            if ((int)(new HTuple((new HTuple(hv_GenParamName.TupleLength())).TupleGreater(
                0))) != 0)
            {
                HOperatorSet.TupleFind(hv_GenParamName, "colored", out hv_Indices1);
                HOperatorSet.TupleFind(hv_GenParamName, "intensity", out hv_IndicesIntensities);
                HOperatorSet.TupleFind(hv_GenParamName, "color", out hv_Indices2);
                if ((int)(new HTuple(((hv_Indices1.TupleSelect(0))).TupleNotEqual(-1))) != 0)
                {
                    if ((int)(new HTuple(((hv_GenParamValue.TupleSelect(hv_Indices1.TupleSelect(
                        0)))).TupleEqual(3))) != 0)
                    {
                        hv_Color = new HTuple();
                        hv_Color[0] = "red";
                        hv_Color[1] = "green";
                        hv_Color[2] = "blue";
                    }
                    else if ((int)(new HTuple(((hv_GenParamValue.TupleSelect(hv_Indices1.TupleSelect(
                        0)))).TupleEqual(6))) != 0)
                    {
                        hv_Color = new HTuple();
                        hv_Color[0] = "red";
                        hv_Color[1] = "green";
                        hv_Color[2] = "blue";
                        hv_Color[3] = "cyan";
                        hv_Color[4] = "magenta";
                        hv_Color[5] = "yellow";
                    }
                    else if ((int)(new HTuple(((hv_GenParamValue.TupleSelect(hv_Indices1.TupleSelect(
                        0)))).TupleEqual(12))) != 0)
                    {
                        hv_Color = new HTuple();
                        hv_Color[0] = "red";
                        hv_Color[1] = "green";
                        hv_Color[2] = "blue";
                        hv_Color[3] = "cyan";
                        hv_Color[4] = "magenta";
                        hv_Color[5] = "yellow";
                        hv_Color[6] = "coral";
                        hv_Color[7] = "slate blue";
                        hv_Color[8] = "spring green";
                        hv_Color[9] = "orange red";
                        hv_Color[10] = "pink";
                        hv_Color[11] = "gold";
                    }
                }
                else if ((int)(new HTuple(((hv_Indices2.TupleSelect(0))).TupleNotEqual(
                    -1))) != 0)
                {
                    hv_Color = hv_GenParamValue.TupleSelect(hv_Indices2.TupleSelect(0));
                }
                else if ((int)(new HTuple(((hv_IndicesIntensities.TupleSelect(0))).TupleNotEqual(
                    -1))) != 0)
                {
                }
            }
            for (hv_J = 0; (int)hv_J <= (int)((new HTuple(hv_ObjectModel3DID.TupleLength())) - 1); hv_J = (int)hv_J + 1)
            {
                hv_I = hv_Idx.TupleSelect(hv_J);
                if ((int)((new HTuple((new HTuple((new HTuple(((hv_HasPolygons.TupleSelect(
                    hv_I))).TupleEqual("true"))).TupleOr(new HTuple(((hv_HasTri.TupleSelect(
                    hv_I))).TupleEqual("true"))))).TupleOr(new HTuple(((hv_HasPoints.TupleSelect(
                    hv_I))).TupleEqual("true"))))).TupleOr(new HTuple(((hv_HasLines.TupleSelect(
                    hv_I))).TupleEqual("true")))) != 0)
                {
                    if ((int)(new HTuple((new HTuple(hv_GenParamName.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        HOperatorSet.TupleFind(hv_GenParamName, "color_" + hv_I, out hv_Indices3);
                        if ((int)(new HTuple(((hv_Indices3.TupleSelect(0))).TupleNotEqual(-1))) != 0)
                        {
                            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_GenParamValue.TupleSelect(
                                hv_Indices3.TupleSelect(0)));
                        }
                        else
                        {
                            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Color.TupleSelect(hv_I % (new HTuple(hv_Color.TupleLength()
                                ))));
                        }
                    }
                    if ((int)(new HTuple((new HTuple(hv_PosesOut.TupleLength())).TupleGreaterEqual(
                        (hv_I * 7) + 6))) != 0)
                    {
                        hv_Pose = hv_PosesOut.TupleSelectRange(hv_I * 7, (hv_I * 7) + 6);
                    }
                    else
                    {
                        hv_Pose = hv_PosesOut.TupleSelectRange(0, 6);
                    }
                    if ((int)(new HTuple(((hv_NumPoints.TupleSelect(hv_I))).TupleLess(10000))) != 0)
                    {
                        ho_ModelContours.Dispose();
                        HOperatorSet.ProjectObjectModel3d(out ho_ModelContours, hv_ObjectModel3DID.TupleSelect(
                            hv_I), hv_CamParam, hv_Pose, hv_CustomParamName, hv_CustomParamValue);
                        HOperatorSet.DispObj(ho_ModelContours, hv_ExpDefaultWinHandle);
                    }
                    else
                    {
                        HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3D);
                        HOperatorSet.SampleObjectModel3d(hv_ObjectModel3DID.TupleSelect(hv_I),
                            "fast", 0.01 * (hv_Diameter.TupleSelect(hv_I)), new HTuple(), new HTuple(),
                            out hv_SampledObjectModel3D);
                        ho_ModelContours.Dispose();
                        HOperatorSet.ProjectObjectModel3d(out ho_ModelContours, hv_SampledObjectModel3D,
                            hv_CamParam, hv_Pose, "point_size", 1);
                        HOperatorSet.GetObjectModel3dParams(hv_SampledObjectModel3D, "point_coord_x",
                            out hv_X);
                        HOperatorSet.GetObjectModel3dParams(hv_SampledObjectModel3D, "point_coord_y",
                            out hv_Y);
                        HOperatorSet.GetObjectModel3dParams(hv_SampledObjectModel3D, "point_coord_z",
                            out hv_Z);
                        HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3D1);
                        HOperatorSet.AffineTransPoint3d(hv_HomMat3D1, hv_X, hv_Y, hv_Z, out hv_Qx,
                            out hv_Qy, out hv_Qz);
                        HOperatorSet.Project3dPoint(hv_Qx, hv_Qy, hv_Qz, hv_CamParam, out hv_Row,
                            out hv_Column);
                        HOperatorSet.DispObj(ho_ModelContours, hv_ExpDefaultWinHandle);
                        HOperatorSet.ClearObjectModel3d(hv_SampledObjectModel3D);
                    }
                }
                else
                {
                    if ((int)(new HTuple((new HTuple(hv_GenParamName.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        HOperatorSet.TupleFind(hv_GenParamName, "color_" + hv_I, out hv_Indices3);
                        if ((int)(new HTuple(((hv_Indices3.TupleSelect(0))).TupleNotEqual(-1))) != 0)
                        {
                            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_GenParamValue.TupleSelect(
                                hv_Indices3.TupleSelect(0)));
                        }
                        else
                        {
                            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Color.TupleSelect(hv_I % (new HTuple(hv_Color.TupleLength()
                                ))));
                        }
                    }
                    if ((int)(new HTuple((new HTuple(hv_PosesOut.TupleLength())).TupleGreaterEqual(
                        (hv_I * 7) + 6))) != 0)
                    {
                        hv_Pose = hv_PosesOut.TupleSelectRange(hv_I * 7, (hv_I * 7) + 6);
                    }
                    else
                    {
                        hv_Pose = hv_PosesOut.TupleSelectRange(0, 6);
                    }
                    if ((int)(new HTuple(((hv_IsPrimitive.TupleSelect(hv_I))).TupleEqual("true"))) != 0)
                    {
                        try
                        {
                            HOperatorSet.ConvexHullObjectModel3d(hv_ObjectModel3DID.TupleSelect(hv_I),
                                out hv_ObjectModel3DConvexHull);
                            if ((int)(new HTuple(((hv_NumPoints.TupleSelect(hv_I))).TupleLess(10000))) != 0)
                            {
                                ho_ModelContours.Dispose();
                                HOperatorSet.ProjectObjectModel3d(out ho_ModelContours, hv_ObjectModel3DConvexHull,
                                    hv_CamParam, hv_Pose, hv_CustomParamName, hv_CustomParamValue);
                                HOperatorSet.DispObj(ho_ModelContours, hv_ExpDefaultWinHandle);
                            }
                            else
                            {
                                HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3D);
                                HOperatorSet.SampleObjectModel3d(hv_ObjectModel3DConvexHull, "fast",
                                    0.01 * (hv_Diameter.TupleSelect(hv_I)), new HTuple(), new HTuple(),
                                    out hv_SampledObjectModel3D);
                                ho_ModelContours.Dispose();
                                HOperatorSet.ProjectObjectModel3d(out ho_ModelContours, hv_SampledObjectModel3D,
                                    hv_CamParam, hv_Pose, "point_size", 1);
                                HOperatorSet.DispObj(ho_ModelContours, hv_ExpDefaultWinHandle);
                                HOperatorSet.ClearObjectModel3d(hv_SampledObjectModel3D);
                            }
                            HOperatorSet.ClearObjectModel3d(hv_ObjectModel3DConvexHull);
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        }
                    }
                }
            }
            HOperatorSet.SetSystem("opengl_hidden_surface_removal_enable", hv_OpenGlHiddenSurface);

            return;
        }

        // Chapter: Graphics / Output
        public void disp_title_and_information(HTuple hv_WindowHandle, HTuple hv_Title,
            HTuple hv_Information)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_WinRow = null, hv_WinColumn = null;
            HTuple hv_WinWidth = new HTuple(), hv_WinHeight = null;
            HTuple hv_NumTitleLines = null, hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_TextWidth = new HTuple();
            HTuple hv_NumInfoLines = null, hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple();
            HTuple hv_Information_COPY_INP_TMP = hv_Information.Clone();
            HTuple hv_Title_COPY_INP_TMP = hv_Title.Clone();

            // Initialize local and output iconic variables 
            //global tuple gInfoDecor
            //global tuple gInfoPos
            //global tuple gTitlePos
            //global tuple gTitleDecor
            //
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_WinRow, out hv_WinColumn,
                out hv_WinWidth, out hv_WinHeight);
            hv_Title_COPY_INP_TMP = ((("" + hv_Title_COPY_INP_TMP) + "")).TupleSplit("\n");
            hv_NumTitleLines = new HTuple(hv_Title_COPY_INP_TMP.TupleLength());
            if ((int)(new HTuple(hv_NumTitleLines.TupleGreater(0))) != 0)
            {
                hv_Row = 12;
                if ((int)(new HTuple(ExpGetGlobalVar_gTitlePos().TupleEqual("UpperLeft"))) != 0)
                {
                    hv_Column = 12;
                }
                else if ((int)(new HTuple(ExpGetGlobalVar_gTitlePos().TupleEqual("UpperCenter"))) != 0)
                {
                    max_line_width(hv_WindowHandle, hv_Title_COPY_INP_TMP, out hv_TextWidth);
                    hv_Column = (hv_WinWidth / 2) - (hv_TextWidth / 2);
                }
                else if ((int)(new HTuple(ExpGetGlobalVar_gTitlePos().TupleEqual("UpperRight"))) != 0)
                {
                    if ((int)(new HTuple(((ExpGetGlobalVar_gTitleDecor().TupleSelect(1))).TupleEqual(
                        "true"))) != 0)
                    {
                        max_line_width(hv_WindowHandle, hv_Title_COPY_INP_TMP + "  ", out hv_TextWidth);
                    }
                    else
                    {
                        max_line_width(hv_WindowHandle, hv_Title_COPY_INP_TMP, out hv_TextWidth);
                    }
                    hv_Column = (hv_WinWidth - hv_TextWidth) - 10;
                }
                else
                {
                    //Unknown position!
                    HDevelopStop();
                }
                disp_message(hv_ExpDefaultWinHandle, hv_Title_COPY_INP_TMP, "window", hv_Row,
                    hv_Column, ExpGetGlobalVar_gTitleDecor().TupleSelect(0), ExpGetGlobalVar_gTitleDecor().TupleSelect(
                    1));
            }
            hv_Information_COPY_INP_TMP = ((("" + hv_Information_COPY_INP_TMP) + "")).TupleSplit(
                "\n");
            hv_NumInfoLines = new HTuple(hv_Information_COPY_INP_TMP.TupleLength());
            if ((int)(new HTuple(hv_NumInfoLines.TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple(ExpGetGlobalVar_gInfoPos().TupleEqual("UpperLeft"))) != 0)
                {
                    hv_Row = 12;
                    hv_Column = 12;
                }
                else if ((int)(new HTuple(ExpGetGlobalVar_gInfoPos().TupleEqual("UpperRight"))) != 0)
                {
                    if ((int)(new HTuple(((ExpGetGlobalVar_gInfoDecor().TupleSelect(1))).TupleEqual(
                        "true"))) != 0)
                    {
                        max_line_width(hv_WindowHandle, hv_Information_COPY_INP_TMP + "  ", out hv_TextWidth);
                    }
                    else
                    {
                        max_line_width(hv_WindowHandle, hv_Information_COPY_INP_TMP, out hv_TextWidth);
                    }
                    hv_Row = 12;
                    hv_Column = (hv_WinWidth - hv_TextWidth) - 12;
                }
                else if ((int)(new HTuple(ExpGetGlobalVar_gInfoPos().TupleEqual("LowerLeft"))) != 0)
                {
                    HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_Information_COPY_INP_TMP,
                        out hv_Ascent, out hv_Descent, out hv_Width, out hv_Height);
                    hv_Row = (hv_WinHeight - (hv_NumInfoLines * hv_Height)) - 12;
                    hv_Column = 12;
                }
                else
                {
                    //Unknown position!
                    HDevelopStop();
                }
                disp_message(hv_ExpDefaultWinHandle, hv_Information_COPY_INP_TMP, "window",
                    hv_Row, hv_Column, ExpGetGlobalVar_gInfoDecor().TupleSelect(0), ExpGetGlobalVar_gInfoDecor().TupleSelect(
                    1));
            }
            //

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Renders 3d object models in a buffer window. 
        public void dump_image_output(HObject ho_BackgroundImage, HTuple hv_WindowHandleBuffer,
            HTuple hv_Scene3D, HTuple hv_AlphaOrig, HTuple hv_ObjectModel3DID, HTuple hv_GenParamName,
            HTuple hv_GenParamValue, HTuple hv_CamParam, HTuple hv_Poses, HTuple hv_ColorImage,
            HTuple hv_Title, HTuple hv_Information, HTuple hv_Labels, HTuple hv_VisualizeTrackball,
            HTuple hv_DisplayContinueButton, HTuple hv_TrackballCenterRow, HTuple hv_TrackballCenterCol,
            HTuple hv_TrackballRadiusPixel, HTuple hv_SelectedObject, HTuple hv_VisualizeRotationCenter,
            HTuple hv_RotationCenter)
        {




            // Local iconic variables 

            HObject ho_ModelContours = null, ho_Image;
            HObject ho_TrackballContour = null, ho_CrossRotCenter = null;

            // Local control variables 

            HTuple ExpTmpLocalVar_gUsesOpenGL = new HTuple();
            HTuple hv_Exception = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Position = new HTuple(), hv_PosIdx = new HTuple();
            HTuple hv_Substrings = new HTuple(), hv_I = new HTuple();
            HTuple hv_HasExtended = new HTuple(), hv_ExtendedAttributeNames = new HTuple();
            HTuple hv_Matches = new HTuple(), hv_Exception1 = new HTuple();
            HTuple hv_DeselectedIdx = new HTuple(), hv_DeselectedName = new HTuple();
            HTuple hv_DeselectedValue = new HTuple(), hv_Pose = new HTuple();
            HTuple hv_HomMat3D = new HTuple(), hv_Center = new HTuple();
            HTuple hv_CenterCamX = new HTuple(), hv_CenterCamY = new HTuple();
            HTuple hv_CenterCamZ = new HTuple(), hv_CenterRow = new HTuple();
            HTuple hv_CenterCol = new HTuple(), hv_Label = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_TextWidth = new HTuple(), hv_TextHeight = new HTuple();
            HTuple hv_RotCenterRow = new HTuple(), hv_RotCenterCol = new HTuple();
            HTuple hv_Orientation = new HTuple(), hv_Colors = new HTuple();
            HTuple hv_RotationCenter_COPY_INP_TMP = hv_RotationCenter.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_TrackballContour);
            HOperatorSet.GenEmptyObj(out ho_CrossRotCenter);
            //global tuple gAlphaDeselected
            //global tuple gTerminationButtonLabel
            //global tuple gDispObjOffset
            //global tuple gLabelsDecor
            //global tuple gUsesOpenGL
            //
            //Display background image
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            if ((int)(hv_ColorImage) != 0)
            {
                HOperatorSet.DispColor(ho_BackgroundImage, hv_ExpDefaultWinHandle);
            }
            else
            {
                HOperatorSet.DispImage(ho_BackgroundImage, hv_ExpDefaultWinHandle);
            }
            //
            //Display objects
            if ((int)(new HTuple(((hv_SelectedObject.TupleSum())).TupleEqual(new HTuple(hv_SelectedObject.TupleLength()
                )))) != 0)
            {
                if ((int)(new HTuple(ExpGetGlobalVar_gUsesOpenGL().TupleEqual("true"))) != 0)
                {
                    try
                    {
                        HOperatorSet.DisplayScene3d(hv_ExpDefaultWinHandle, hv_Scene3D, 0);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)((new HTuple((new HTuple((new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(1306))).TupleOr(new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(1305))))).TupleOr(new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(1406))))).TupleOr(new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(1405)))) != 0)
                        {
                            if ((int)(new HTuple((new HTuple(hv_GenParamName.TupleLength())).TupleEqual(
                                new HTuple(hv_GenParamValue.TupleLength())))) != 0)
                            {
                                //This case means we have a Parameter with structure parameter_x with x > |ObjectModel3DID|-1
                                for (hv_Index = new HTuple(hv_ObjectModel3DID.TupleLength()); (int)hv_Index <= (int)((2 * (new HTuple(hv_ObjectModel3DID.TupleLength()
                                    ))) + 1); hv_Index = (int)hv_Index + 1)
                                {
                                    HOperatorSet.TupleStrstr(hv_GenParamName, "" + hv_Index, out hv_Position);
                                    for (hv_PosIdx = 0; (int)hv_PosIdx <= (int)((new HTuple(hv_Position.TupleLength()
                                        )) - 1); hv_PosIdx = (int)hv_PosIdx + 1)
                                    {
                                        if ((int)(new HTuple(((hv_Position.TupleSelect(hv_PosIdx))).TupleNotEqual(
                                            -1))) != 0)
                                        {
                                            throw new HalconException((("One of the parameters is refferring to a non-existing object model 3D:\n" + (hv_GenParamName.TupleSelect(
                                                hv_PosIdx))) + " -> ") + (hv_GenParamValue.TupleSelect(hv_PosIdx)));
                                        }
                                    }
                                }
                                //Test for non-existing extended attributes:
                                HOperatorSet.TupleStrstr(hv_GenParamName, "intensity", out hv_Position);
                                for (hv_PosIdx = 0; (int)hv_PosIdx <= (int)((new HTuple(hv_Position.TupleLength()
                                    )) - 1); hv_PosIdx = (int)hv_PosIdx + 1)
                                {
                                    if ((int)(new HTuple(((hv_Position.TupleSelect(hv_PosIdx))).TupleNotEqual(
                                        -1))) != 0)
                                    {
                                        HOperatorSet.TupleSplit(hv_GenParamName.TupleSelect(hv_PosIdx),
                                            "_", out hv_Substrings);
                                        if ((int)((new HTuple((new HTuple(hv_Substrings.TupleLength())).TupleGreater(
                                            1))).TupleAnd(((hv_Substrings.TupleSelect(1))).TupleIsNumber()
                                            )) != 0)
                                        {
                                            hv_I = ((hv_Substrings.TupleSelect(1))).TupleNumber();
                                            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID.TupleSelect(
                                                hv_I), "has_extended_attribute", out hv_HasExtended);
                                            if ((int)(hv_HasExtended) != 0)
                                            {
                                                HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID.TupleSelect(
                                                    hv_I), "extended_attribute_names", out hv_ExtendedAttributeNames);
                                                HOperatorSet.TupleFind(hv_ExtendedAttributeNames, hv_GenParamValue.TupleSelect(
                                                    hv_PosIdx), out hv_Matches);
                                            }
                                            if ((int)((new HTuple(hv_HasExtended.TupleNot())).TupleOr((new HTuple(hv_Matches.TupleEqual(
                                                -1))).TupleOr(new HTuple((new HTuple(hv_Matches.TupleLength()
                                                )).TupleEqual(0))))) != 0)
                                            {
                                                throw new HalconException((((("One of the parameters is refferring to an extended attribute that is not contained in the object model 3d with the handle " + (hv_ObjectModel3DID.TupleSelect(
                                                    hv_I))) + ":\n") + (hv_GenParamName.TupleSelect(hv_PosIdx))) + " -> ") + (hv_GenParamValue.TupleSelect(
                                                    hv_PosIdx)));
                                            }
                                        }
                                        else
                                        {
                                            for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_ObjectModel3DID.TupleLength()
                                                )) - 1); hv_I = (int)hv_I + 1)
                                            {
                                                HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID.TupleSelect(
                                                    hv_I), "extended_attribute_names", out hv_ExtendedAttributeNames);
                                                HOperatorSet.TupleFind(hv_ExtendedAttributeNames, hv_GenParamValue.TupleSelect(
                                                    hv_PosIdx), out hv_Matches);
                                                if ((int)((new HTuple(hv_Matches.TupleEqual(-1))).TupleOr(new HTuple((new HTuple(hv_Matches.TupleLength()
                                                    )).TupleEqual(0)))) != 0)
                                                {
                                                    throw new HalconException((("One of the parameters is refferring to an extended attribute that is not contained in all object models:\n" + (hv_GenParamName.TupleSelect(
                                                        hv_PosIdx))) + " -> ") + (hv_GenParamValue.TupleSelect(hv_PosIdx)));
                                                }
                                            }
                                        }
                                    }
                                }
                                //
                                throw new HalconException((new HTuple("Wrong generic parameters for display\n") + "Wrong Values are:\n") + (((((("    " + ((hv_GenParamName + " -> ") + hv_GenParamValue)) + "\n")).TupleSum()
                                    ) + "Exeption was:\n    ") + (hv_Exception.TupleSelect(2))));
                            }
                            else
                            {
                                throw new HalconException(hv_Exception);
                            }
                        }
                        else if ((int)((new HTuple((new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(5185))).TupleOr(new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(5188))))).TupleOr(new HTuple(((hv_Exception.TupleSelect(
                            0))).TupleEqual(5187)))) != 0)
                        {
                            ExpTmpLocalVar_gUsesOpenGL = "false";
                            ExpSetGlobalVar_gUsesOpenGL(ExpTmpLocalVar_gUsesOpenGL);
                        }
                        else
                        {
                            throw new HalconException(hv_Exception);
                        }
                    }
                }
                if ((int)(new HTuple(ExpGetGlobalVar_gUsesOpenGL().TupleEqual("false"))) != 0)
                {
                    //* NO OpenGL, use fallback
                    ho_ModelContours.Dispose();
                    disp_object_model_no_opengl(out ho_ModelContours, hv_ObjectModel3DID, hv_GenParamName,
                        hv_GenParamValue, hv_ExpDefaultWinHandle, hv_CamParam, hv_Poses);
                }
            }
            else
            {
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_AlphaOrig.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((hv_SelectedObject.TupleSelect(hv_Index))).TupleEqual(
                        1))) != 0)
                    {
                        HOperatorSet.SetScene3dInstanceParam(hv_Scene3D, hv_Index, "alpha", hv_AlphaOrig.TupleSelect(
                            hv_Index));
                    }
                    else
                    {
                        HOperatorSet.SetScene3dInstanceParam(hv_Scene3D, hv_Index, "alpha", ExpGetGlobalVar_gAlphaDeselected());
                    }
                }
                try
                {
                    if ((int)(new HTuple(ExpGetGlobalVar_gUsesOpenGL().TupleEqual("false"))) != 0)
                    {
                        throw new HalconException(new HTuple());
                    }
                    HOperatorSet.DisplayScene3d(hv_ExpDefaultWinHandle, hv_Scene3D, 0);
                }
                // catch (Exception1) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception1);
                    //* NO OpenGL, use fallback
                    hv_DeselectedIdx = hv_SelectedObject.TupleFind(0);
                    if ((int)(new HTuple(hv_DeselectedIdx.TupleNotEqual(-1))) != 0)
                    {
                        hv_DeselectedName = "color_" + hv_DeselectedIdx;
                        hv_DeselectedValue = HTuple.TupleGenConst(new HTuple(hv_DeselectedName.TupleLength()
                            ), "gray");
                    }
                    ho_ModelContours.Dispose();
                    disp_object_model_no_opengl(out ho_ModelContours, hv_ObjectModel3DID, hv_GenParamName.TupleConcat(
                        hv_DeselectedName), hv_GenParamValue.TupleConcat(hv_DeselectedValue),
                        hv_ExpDefaultWinHandle, hv_CamParam, hv_Poses);
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_AlphaOrig.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.SetScene3dInstanceParam(hv_Scene3D, hv_Index, "alpha", hv_AlphaOrig.TupleSelect(
                        hv_Index));
                }
            }
            ho_Image.Dispose();
            HOperatorSet.DumpWindowImage(out ho_Image, hv_ExpDefaultWinHandle);
            //
            //Display labels
            if ((int)(new HTuple(hv_Labels.TupleNotEqual(0))) != 0)
            {
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, ExpGetGlobalVar_gLabelsDecor().TupleSelect(
                    0));
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ObjectModel3DID.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    //Project the center point of the current model
                    hv_Pose = hv_Poses.TupleSelectRange(hv_Index * 7, (hv_Index * 7) + 6);
                    HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3D);
                    HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID.TupleSelect(hv_Index),
                        "center", out hv_Center);
                    HOperatorSet.AffineTransPoint3d(hv_HomMat3D, hv_Center.TupleSelect(0), hv_Center.TupleSelect(
                        1), hv_Center.TupleSelect(2), out hv_CenterCamX, out hv_CenterCamY, out hv_CenterCamZ);
                    HOperatorSet.Project3dPoint(hv_CenterCamX, hv_CenterCamY, hv_CenterCamZ,
                        hv_CamParam, out hv_CenterRow, out hv_CenterCol);
                    hv_Label = hv_Labels.TupleSelect(hv_Index);
                    if ((int)(new HTuple(hv_Label.TupleNotEqual(""))) != 0)
                    {
                        HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_Label, out hv_Ascent,
                            out hv_Descent, out hv_TextWidth, out hv_TextHeight);
                        disp_message(hv_ExpDefaultWinHandle, hv_Label, "window", (hv_CenterRow - (hv_TextHeight / 2)) + (ExpGetGlobalVar_gDispObjOffset().TupleSelect(
                            0)), (hv_CenterCol - (hv_TextWidth / 2)) + (ExpGetGlobalVar_gDispObjOffset().TupleSelect(
                            1)), new HTuple(), ExpGetGlobalVar_gLabelsDecor().TupleSelect(1));
                    }
                }
            }
            //
            //Visualize the trackball if desired
            if ((int)(hv_VisualizeTrackball) != 0)
            {
                HOperatorSet.SetLineWidth(hv_ExpDefaultWinHandle, 1);
                ho_TrackballContour.Dispose();
                HOperatorSet.GenEllipseContourXld(out ho_TrackballContour, hv_TrackballCenterRow,
                    hv_TrackballCenterCol, 0, hv_TrackballRadiusPixel, hv_TrackballRadiusPixel,
                    0, 6.28318, "positive", 1.5);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "dim gray");
                HOperatorSet.DispXld(ho_TrackballContour, hv_ExpDefaultWinHandle);
            }
            //
            //Visualize the rotation center if desired
            if ((int)((new HTuple(hv_VisualizeRotationCenter.TupleNotEqual(0))).TupleAnd(
                new HTuple((new HTuple(hv_RotationCenter_COPY_INP_TMP.TupleLength())).TupleEqual(
                3)))) != 0)
            {
                if ((int)(new HTuple(((hv_RotationCenter_COPY_INP_TMP.TupleSelect(2))).TupleLess(
                    1e-10))) != 0)
                {
                    if (hv_RotationCenter_COPY_INP_TMP == null)
                        hv_RotationCenter_COPY_INP_TMP = new HTuple();
                    hv_RotationCenter_COPY_INP_TMP[2] = 1e-10;
                }
                HOperatorSet.Project3dPoint(hv_RotationCenter_COPY_INP_TMP.TupleSelect(0),
                    hv_RotationCenter_COPY_INP_TMP.TupleSelect(1), hv_RotationCenter_COPY_INP_TMP.TupleSelect(
                    2), hv_CamParam, out hv_RotCenterRow, out hv_RotCenterCol);
                hv_Orientation = (new HTuple(90)).TupleRad();
                if ((int)(new HTuple(hv_VisualizeRotationCenter.TupleEqual(1))) != 0)
                {
                    hv_Orientation = (new HTuple(45)).TupleRad();
                }
                ho_CrossRotCenter.Dispose();
                HOperatorSet.GenCrossContourXld(out ho_CrossRotCenter, hv_RotCenterRow, hv_RotCenterCol,
                    hv_TrackballRadiusPixel / 25.0, hv_Orientation);
                HOperatorSet.SetLineWidth(hv_ExpDefaultWinHandle, 3);
                HOperatorSet.QueryColor(hv_ExpDefaultWinHandle, out hv_Colors);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "light gray");
                HOperatorSet.DispXld(ho_CrossRotCenter, hv_ExpDefaultWinHandle);
                HOperatorSet.SetLineWidth(hv_ExpDefaultWinHandle, 1);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "dim gray");
                HOperatorSet.DispXld(ho_CrossRotCenter, hv_ExpDefaultWinHandle);
            }
            //
            //Display title
            disp_title_and_information(hv_WindowHandleBuffer, hv_Title, hv_Information);
            //
            //Display the 'Exit' button
            if ((int)(new HTuple(hv_DisplayContinueButton.TupleEqual("true"))) != 0)
            {
                disp_continue_button(hv_ExpDefaultWinHandle);
            }
            //
            ho_ModelContours.Dispose();
            ho_Image.Dispose();
            ho_TrackballContour.Dispose();
            ho_CrossRotCenter.Dispose();

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Get the center of the virtual trackback that is used to move the camera. 
        public void get_trackball_center(HTuple hv_SelectedObject, HTuple hv_TrackballRadiusPixel,
            HTuple hv_ObjectModel3D, HTuple hv_Poses, out HTuple hv_TBCenter, out HTuple hv_TBSize)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_NumModels = null, hv_Centers = null;
            HTuple hv_Diameter = null, hv_MD = null, hv_Weight = new HTuple();
            HTuple hv_SumW = null, hv_Index = null, hv_ObjectModel3DIDSelected = new HTuple();
            HTuple hv_PoseSelected = new HTuple(), hv_HomMat3D = new HTuple();
            HTuple hv_TBCenterCamX = new HTuple(), hv_TBCenterCamY = new HTuple();
            HTuple hv_TBCenterCamZ = new HTuple(), hv_InvSum = new HTuple();
            // Initialize local and output iconic variables 
            hv_TBCenter = new HTuple();
            hv_TBSize = new HTuple();
            hv_NumModels = new HTuple(hv_ObjectModel3D.TupleLength());
            if (hv_TBCenter == null)
                hv_TBCenter = new HTuple();
            hv_TBCenter[0] = 0;
            if (hv_TBCenter == null)
                hv_TBCenter = new HTuple();
            hv_TBCenter[1] = 0;
            if (hv_TBCenter == null)
                hv_TBCenter = new HTuple();
            hv_TBCenter[2] = 0;
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3D, "center", out hv_Centers);
            HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3D, "diameter_axis_aligned_bounding_box",
                out hv_Diameter);
            //Normalize Diameter to use it as weights for a weighted mean of the individual centers
            hv_MD = hv_Diameter.TupleMean();
            if ((int)(new HTuple(hv_MD.TupleGreater(1e-10))) != 0)
            {
                hv_Weight = hv_Diameter / hv_MD;
            }
            else
            {
                hv_Weight = hv_Diameter.Clone();
            }
            hv_SumW = ((hv_Weight.TupleSelectMask(((hv_SelectedObject.TupleSgn())).TupleAbs()
                ))).TupleSum();
            if ((int)(new HTuple(hv_SumW.TupleLess(1e-10))) != 0)
            {
                hv_Weight = HTuple.TupleGenConst(new HTuple(hv_Weight.TupleLength()), 1.0);
                hv_SumW = ((hv_Weight.TupleSelectMask(((hv_SelectedObject.TupleSgn())).TupleAbs()
                    ))).TupleSum();
            }
            HTuple end_val18 = hv_NumModels - 1;
            HTuple step_val18 = 1;
            for (hv_Index = 0; hv_Index.Continue(end_val18, step_val18); hv_Index = hv_Index.TupleAdd(step_val18))
            {
                if ((int)(hv_SelectedObject.TupleSelect(hv_Index)) != 0)
                {
                    hv_ObjectModel3DIDSelected = hv_ObjectModel3D.TupleSelect(hv_Index);
                    hv_PoseSelected = hv_Poses.TupleSelectRange(hv_Index * 7, (hv_Index * 7) + 6);
                    HOperatorSet.PoseToHomMat3d(hv_PoseSelected, out hv_HomMat3D);
                    HOperatorSet.AffineTransPoint3d(hv_HomMat3D, hv_Centers.TupleSelect((hv_Index * 3) + 0),
                        hv_Centers.TupleSelect((hv_Index * 3) + 1), hv_Centers.TupleSelect((hv_Index * 3) + 2),
                        out hv_TBCenterCamX, out hv_TBCenterCamY, out hv_TBCenterCamZ);
                    if (hv_TBCenter == null)
                        hv_TBCenter = new HTuple();
                    hv_TBCenter[0] = (hv_TBCenter.TupleSelect(0)) + (hv_TBCenterCamX * (hv_Weight.TupleSelect(
                        hv_Index)));
                    if (hv_TBCenter == null)
                        hv_TBCenter = new HTuple();
                    hv_TBCenter[1] = (hv_TBCenter.TupleSelect(1)) + (hv_TBCenterCamY * (hv_Weight.TupleSelect(
                        hv_Index)));
                    if (hv_TBCenter == null)
                        hv_TBCenter = new HTuple();
                    hv_TBCenter[2] = (hv_TBCenter.TupleSelect(2)) + (hv_TBCenterCamZ * (hv_Weight.TupleSelect(
                        hv_Index)));
                }
            }
            if ((int)(new HTuple(((hv_SelectedObject.TupleMax())).TupleNotEqual(0))) != 0)
            {
                hv_InvSum = 1.0 / hv_SumW;
                if (hv_TBCenter == null)
                    hv_TBCenter = new HTuple();
                hv_TBCenter[0] = (hv_TBCenter.TupleSelect(0)) * hv_InvSum;
                if (hv_TBCenter == null)
                    hv_TBCenter = new HTuple();
                hv_TBCenter[1] = (hv_TBCenter.TupleSelect(1)) * hv_InvSum;
                if (hv_TBCenter == null)
                    hv_TBCenter = new HTuple();
                hv_TBCenter[2] = (hv_TBCenter.TupleSelect(2)) * hv_InvSum;
                hv_TBSize = (0.5 + ((0.5 * (hv_SelectedObject.TupleSum())) / hv_NumModels)) * hv_TrackballRadiusPixel;
            }
            else
            {
                hv_TBCenter = new HTuple();
                hv_TBSize = 0;
            }

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Project an image point onto the trackball 
        public void project_point_on_trackball(HTuple hv_X, HTuple hv_Y, HTuple hv_VirtualTrackball,
            HTuple hv_TrackballSize, out HTuple hv_V)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_R = new HTuple(), hv_XP = new HTuple();
            HTuple hv_YP = new HTuple(), hv_ZP = new HTuple();
            // Initialize local and output iconic variables 
            if ((int)(new HTuple(hv_VirtualTrackball.TupleEqual("shoemake"))) != 0)
            {
                //Virtual Trackball according to Shoemake
                hv_R = (((hv_X * hv_X) + (hv_Y * hv_Y))).TupleSqrt();
                if ((int)(new HTuple(hv_R.TupleLessEqual(hv_TrackballSize))) != 0)
                {
                    hv_XP = hv_X.Clone();
                    hv_YP = hv_Y.Clone();
                    hv_ZP = (((hv_TrackballSize * hv_TrackballSize) - (hv_R * hv_R))).TupleSqrt();
                }
                else
                {
                    hv_XP = (hv_X * hv_TrackballSize) / hv_R;
                    hv_YP = (hv_Y * hv_TrackballSize) / hv_R;
                    hv_ZP = 0;
                }
            }
            else
            {
                //Virtual Trackball according to Bell
                hv_R = (((hv_X * hv_X) + (hv_Y * hv_Y))).TupleSqrt();
                if ((int)(new HTuple(hv_R.TupleLessEqual(hv_TrackballSize * 0.70710678))) != 0)
                {
                    hv_XP = hv_X.Clone();
                    hv_YP = hv_Y.Clone();
                    hv_ZP = (((hv_TrackballSize * hv_TrackballSize) - (hv_R * hv_R))).TupleSqrt();
                }
                else
                {
                    hv_XP = hv_X.Clone();
                    hv_YP = hv_Y.Clone();
                    hv_ZP = ((0.6 * hv_TrackballSize) * hv_TrackballSize) / hv_R;
                }
            }
            hv_V = new HTuple();
            hv_V = hv_V.TupleConcat(hv_XP);
            hv_V = hv_V.TupleConcat(hv_YP);
            hv_V = hv_V.TupleConcat(hv_ZP);

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Get string extends of several lines. 
        public void max_line_width(HTuple hv_WindowHandle, HTuple hv_Lines, out HTuple hv_MaxWidth)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Index = null, hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv_LineWidth = new HTuple();
            HTuple hv_LineHeight = new HTuple();
            // Initialize local and output iconic variables 
            hv_MaxWidth = 0;
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Lines.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_Lines.TupleSelect(
                    hv_Index), out hv_Ascent, out hv_Descent, out hv_LineWidth, out hv_LineHeight);
                hv_MaxWidth = ((hv_LineWidth.TupleConcat(hv_MaxWidth))).TupleMax();
            }

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Compute the 3d rotation from the mose movement 
        public void trackball(HTuple hv_MX1, HTuple hv_MY1, HTuple hv_MX2, HTuple hv_MY2,
            HTuple hv_VirtualTrackball, HTuple hv_TrackballSize, HTuple hv_SensFactor, out HTuple hv_QuatRotation)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_D = null, hv_P2 = null, hv_P1 = null;
            HTuple hv_T = null, hv_RotAngle = null, hv_Len = null;
            HTuple hv_RotAxis = null;
            // Initialize local and output iconic variables 
            hv_QuatRotation = new HTuple();
            //Compute the 3d rotation from the mouse movement
            //
            if ((int)((new HTuple(hv_MX1.TupleEqual(hv_MX2))).TupleAnd(new HTuple(hv_MY1.TupleEqual(
                hv_MY2)))) != 0)
            {
                hv_QuatRotation = new HTuple();
                hv_QuatRotation[0] = 1;
                hv_QuatRotation[1] = 0;
                hv_QuatRotation[2] = 0;
                hv_QuatRotation[3] = 0;

                return;
            }
            //Project the image point onto the trackball
            project_point_on_trackball(hv_MX1, hv_MY1, hv_VirtualTrackball, hv_TrackballSize,
                out hv_P1);
            project_point_on_trackball(hv_MX2, hv_MY2, hv_VirtualTrackball, hv_TrackballSize,
                out hv_P2);
            //The cross product of the projected points defines the rotation axis
            tuple_vector_cross_product(hv_P1, hv_P2, out hv_RotAxis);
            //Compute the rotation angle
            hv_D = hv_P2 - hv_P1;
            hv_T = (((((hv_D * hv_D)).TupleSum())).TupleSqrt()) / (2.0 * hv_TrackballSize);
            if ((int)(new HTuple(hv_T.TupleGreater(1.0))) != 0)
            {
                hv_T = 1.0;
            }
            if ((int)(new HTuple(hv_T.TupleLess(-1.0))) != 0)
            {
                hv_T = -1.0;
            }
            hv_RotAngle = (2.0 * (hv_T.TupleAsin())) * hv_SensFactor;
            hv_Len = ((((hv_RotAxis * hv_RotAxis)).TupleSum())).TupleSqrt();
            if ((int)(new HTuple(hv_Len.TupleGreater(0.0))) != 0)
            {
                hv_RotAxis = hv_RotAxis / hv_Len;
            }
            HOperatorSet.AxisAngleToQuat(hv_RotAxis.TupleSelect(0), hv_RotAxis.TupleSelect(
                1), hv_RotAxis.TupleSelect(2), hv_RotAngle, out hv_QuatRotation);

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: This procedure contains all relevant information about the supported features. 
        public void get_features(HObject ho_Region, HObject ho_Image, HTuple hv_Namelist,
            HTuple hv_Mode, out HTuple hv_Output)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_EmptyRegionResult = null, hv_AccumulatedResults = null;
            HTuple hv_CustomResults = null, hv_NumRegions = null, hv_ImageWidth = null;
            HTuple hv_ImageHeight = null, hv_I = null, hv_CurrentName = new HTuple();
            HTuple hv_Name = new HTuple(), hv_Groups = new HTuple();
            HTuple hv_Feature = new HTuple(), hv_ExpDefaultCtrlDummyVar = new HTuple();
            HTuple hv_ExtendedResults = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Ra = new HTuple();
            HTuple hv_Rb = new HTuple(), hv_Phi = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_Sigma = new HTuple(), hv_Roundness = new HTuple();
            HTuple hv_Sides = new HTuple(), hv_NumConnected = new HTuple();
            HTuple hv_NumHoles = new HTuple(), hv_Diameter = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Anisometry = new HTuple(), hv_Bulkiness = new HTuple();
            HTuple hv_StructureFactor = new HTuple(), hv_Length1 = new HTuple();
            HTuple hv_Length2 = new HTuple(), hv_ContLength = new HTuple();
            HTuple hv_AreaHoles = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Min = new HTuple(), hv_Max = new HTuple(), hv_Range = new HTuple();
            HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
            HTuple hv_Entropy = new HTuple(), hv_Anisotropy = new HTuple();
            HTuple hv_Size = new HTuple(), hv_NumBins = new HTuple();
            HTuple hv_NameRegExp = new HTuple(), hv_Names = new HTuple();
            HTuple hv_NumPyramids = new HTuple(), hv_Energy = new HTuple();
            HTuple hv_Correlation = new HTuple(), hv_Homogeneity = new HTuple();
            HTuple hv_Contrast = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Start = new HTuple(), hv_Projection = new HTuple();
            HTuple hv_Histo = new HTuple(), hv_BinSize = new HTuple();
            // Initialize local and output iconic variables 
            //*********************************************************
            //Feature procedure
            //Contains the names, properties and calculation of
            //all supproted features.
            //It consists of similar blocks for each feature.
            //
            //If you like to add your own features, please use
            //the external procedure get_custom_features.hdvp
            //in the HALCON procedures/templates directory.
            //*********************************************************
            //
            //Insert location of your custom procedure here
            //
            HOperatorSet.GetSystem("empty_region_result", out hv_EmptyRegionResult);
            HOperatorSet.SetSystem("empty_region_result", "true");
            hv_AccumulatedResults = new HTuple();
            hv_CustomResults = new HTuple();
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
            //
            for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Namelist.TupleLength())) - 1); hv_I = (int)hv_I + 1)
            {
                hv_CurrentName = hv_Namelist.TupleSelect(hv_I);
                //
                get_custom_features(ho_Region, ho_Image, hv_CurrentName, hv_Mode, out hv_CustomResults);
                hv_AccumulatedResults = hv_AccumulatedResults.TupleConcat(hv_CustomResults);
                //
                //
                //************************************
                //HALCON REGION FEATURES
                //************************************
                //
                //************************************
                //BASIC
                //************************************
                //** area ***
                hv_Name = "area";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.AreaCenter(ho_Region, out hv_Feature, out hv_ExpDefaultCtrlDummyVar,
                        out hv_ExpDefaultCtrlDummyVar);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** width ***
                hv_Name = "width";
                hv_Groups = "region";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row1, out hv_Column1, out hv_Row2,
                        out hv_Column2);
                    hv_Feature = (hv_Column2 - hv_Column1) + 1;
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** height ***
                hv_Name = "height";
                hv_Groups = "region";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row1, out hv_Column1, out hv_Row2,
                        out hv_Column2);
                    hv_Feature = (hv_Row2 - hv_Row1) + 1;
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** ra ***
                hv_Name = "ra";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EllipticAxis(ho_Region, out hv_Ra, out hv_Rb, out hv_Phi);
                    hv_Feature = hv_Ra.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** rb ***
                hv_Name = "rb";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EllipticAxis(ho_Region, out hv_Ra, out hv_Rb, out hv_Phi);
                    hv_Feature = hv_Rb.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** phi ***
                hv_Name = "phi";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EllipticAxis(ho_Region, out hv_Ra, out hv_Rb, out hv_Phi);
                    hv_Feature = hv_Phi.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** roundness ***
                hv_Name = "roundness";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Roundness(ho_Region, out hv_Distance, out hv_Sigma, out hv_Roundness,
                        out hv_Sides);
                    hv_Feature = hv_Roundness.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** num_sides ***
                hv_Name = "num_sides";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Roundness(ho_Region, out hv_Distance, out hv_Sigma, out hv_Roundness,
                        out hv_Sides);
                    hv_Feature = hv_Sides.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** num_connected ***
                hv_Name = "num_connected";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.ConnectAndHoles(ho_Region, out hv_NumConnected, out hv_NumHoles);
                    hv_Feature = hv_NumConnected.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** num_holes ***
                hv_Name = "num_holes";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.ConnectAndHoles(ho_Region, out hv_NumConnected, out hv_NumHoles);
                    hv_Feature = hv_NumHoles.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** area_holes ***
                hv_Name = "area_holes";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.AreaHoles(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** max_diameter ***
                hv_Name = "max_diameter";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.DiameterRegion(ho_Region, out hv_Row1, out hv_Column1, out hv_Row2,
                        out hv_Column2, out hv_Diameter);
                    hv_Feature = hv_Diameter.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** orientation ***
                hv_Name = "orientation";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.OrientationRegion(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //SHAPE
                //************************************
                //
                //************************************
                //** outer_radius ***
                hv_Name = "outer_radius";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.SmallestCircle(ho_Region, out hv_Row, out hv_Column, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** inner_radius ***
                hv_Name = "inner_radius";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.InnerCircle(ho_Region, out hv_Row, out hv_Column, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** inner_width ***
                hv_Name = "inner_width";
                hv_Groups = "region";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.InnerRectangle1(ho_Region, out hv_Row1, out hv_Column1, out hv_Row2,
                        out hv_Column2);
                    hv_Feature = (hv_Column2 - hv_Column1) + 1;
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** inner_height ***
                hv_Name = "inner_height";
                hv_Groups = "region";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.InnerRectangle1(ho_Region, out hv_Row1, out hv_Column1, out hv_Row2,
                        out hv_Column2);
                    hv_Feature = (hv_Row2 - hv_Row1) + 1;
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** circularity ***
                hv_Name = "circularity";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Circularity(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** compactness ***
                hv_Name = "compactness";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Compactness(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** convexity ***
                hv_Name = "convexity";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Convexity(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** rectangularity ***
                hv_Name = "rectangularity";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Rectangularity(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** anisometry ***
                hv_Name = "anisometry";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Eccentricity(ho_Region, out hv_Anisometry, out hv_Bulkiness,
                        out hv_StructureFactor);
                    hv_Feature = hv_Anisometry.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** bulkiness ***
                hv_Name = "bulkiness";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Eccentricity(ho_Region, out hv_Anisometry, out hv_Bulkiness,
                        out hv_StructureFactor);
                    hv_Feature = hv_Bulkiness.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** struct_factor ***
                hv_Name = "struct_factor";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Eccentricity(ho_Region, out hv_Anisometry, out hv_Bulkiness,
                        out hv_StructureFactor);
                    hv_Feature = hv_StructureFactor.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** dist_mean ***
                hv_Name = "dist_mean";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Roundness(ho_Region, out hv_Distance, out hv_Sigma, out hv_Roundness,
                        out hv_Sides);
                    hv_Feature = hv_Distance.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** dist_deviation ***
                hv_Name = "dist_deviation";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Roundness(ho_Region, out hv_Distance, out hv_Sigma, out hv_Roundness,
                        out hv_Sides);
                    hv_Feature = hv_Sigma.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** euler_number ***
                hv_Name = "euler_number";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EulerNumber(ho_Region, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** rect2_phi ***
                hv_Name = "rect2_phi";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.SmallestRectangle2(ho_Region, out hv_Row, out hv_Column, out hv_Phi,
                        out hv_Length1, out hv_Length2);
                    hv_Feature = hv_Phi.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** rect2_len1 ***
                hv_Name = "rect2_len1";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.SmallestRectangle2(ho_Region, out hv_Row, out hv_Column, out hv_Phi,
                        out hv_Length1, out hv_Length2);
                    hv_Feature = hv_Length1.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** rect2_len2 ***
                hv_Name = "rect2_len2";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.SmallestRectangle2(ho_Region, out hv_Row, out hv_Column, out hv_Phi,
                        out hv_Length1, out hv_Length2);
                    hv_Feature = hv_Length2.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** contlength ***
                hv_Name = "contlength";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Contlength(ho_Region, out hv_ContLength);
                    hv_Feature = hv_ContLength.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //REGION FEATURES
                //************************************
                //MISC
                //************************************
                //** porosity ***
                hv_Name = "porosity";
                hv_Groups = new HTuple();
                hv_Groups[0] = "region";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.AreaHoles(ho_Region, out hv_AreaHoles);
                    HOperatorSet.AreaCenter(ho_Region, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple(hv_Area.TupleEqual(0))) != 0)
                    {
                        hv_Feature = 0.0;
                    }
                    else
                    {
                        hv_Feature = (hv_AreaHoles.TupleReal()) / (hv_Area + hv_AreaHoles);
                    }
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //HALCON GRAY VALUE FEATURES
                //************************************
                //BASIC
                //************************************
                //
                //** gray_area ***
                hv_Name = "gray_area";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.AreaCenterGray(ho_Region, ho_Image, out hv_Area, out hv_Row,
                        out hv_Column);
                    hv_Feature = hv_Area.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_ra ***
                hv_Name = "gray_ra";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EllipticAxisGray(ho_Region, ho_Image, out hv_Ra, out hv_Rb,
                        out hv_Phi);
                    hv_Feature = hv_Ra.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_rb ***
                hv_Name = "gray_rb";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EllipticAxisGray(ho_Region, ho_Image, out hv_Ra, out hv_Rb,
                        out hv_Phi);
                    hv_Feature = hv_Rb.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_phi ***
                hv_Name = "gray_phi";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EllipticAxisGray(ho_Region, ho_Image, out hv_Ra, out hv_Rb,
                        out hv_Phi);
                    hv_Feature = hv_Phi.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_min ***
                hv_Name = "gray_min";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.MinMaxGray(ho_Region, ho_Image, 0, out hv_Min, out hv_Max, out hv_Range);
                    hv_Feature = hv_Min.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_max ***
                hv_Name = "gray_max";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.MinMaxGray(ho_Region, ho_Image, 0, out hv_Min, out hv_Max, out hv_Range);
                    hv_Feature = hv_Max.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_range ***
                hv_Name = "gray_range";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.MinMaxGray(ho_Region, ho_Image, 0, out hv_Min, out hv_Max, out hv_Range);
                    hv_Feature = hv_Range.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //TEXTURE
                //************************************
                //
                //************************************
                //** gray_mean ***
                hv_Name = "gray_mean";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Intensity(ho_Region, ho_Image, out hv_Mean, out hv_Deviation);
                    hv_Feature = hv_Mean.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_deviation ***
                hv_Name = "gray_deviation";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.Intensity(ho_Region, ho_Image, out hv_Mean, out hv_Deviation);
                    hv_Feature = hv_Deviation.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_plane_deviation ***
                hv_Name = "gray_plane_deviation";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.PlaneDeviation(ho_Region, ho_Image, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_anisotropy ***
                hv_Name = "gray_anisotropy";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EntropyGray(ho_Region, ho_Image, out hv_Entropy, out hv_Anisotropy);
                    hv_Feature = hv_Anisotropy.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_entropy ***
                hv_Name = "gray_entropy";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    HOperatorSet.EntropyGray(ho_Region, ho_Image, out hv_Entropy, out hv_Anisotropy);
                    hv_Feature = hv_Entropy.Clone();
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_hor_proj ***
                hv_Name = "gray_hor_proj";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Size = 20;
                    calc_feature_gray_proj(ho_Region, ho_Image, "hor", hv_Size, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_vert_proj ***
                hv_Name = "gray_vert_proj";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Size = 20;
                    calc_feature_gray_proj(ho_Region, ho_Image, "vert", hv_Size, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_hor_proj_histo ***
                hv_Name = "gray_hor_proj_histo";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Size = 20;
                    calc_feature_gray_proj(ho_Region, ho_Image, "hor_histo", hv_Size, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** gray_vert_proj_histo ***
                hv_Name = "gray_vert_proj_histo";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Size = 20;
                    calc_feature_gray_proj(ho_Region, ho_Image, "vert_histo", hv_Size, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** grad_dir_histo ***
                hv_Name = "grad_dir_histo";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_NumBins = 20;
                    calc_feature_grad_dir_histo(ho_Region, ho_Image, hv_NumBins, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** edge_density ***
                hv_Name = "edge_density";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_edge_density(ho_Region, ho_Image, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** edge_density_histogram ***
                hv_Name = "edge_density_histogram";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_NumBins = 4;
                    calc_feature_edge_density_histogram(ho_Region, ho_Image, hv_NumBins, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** edge_density_pyramid ***
                hv_NameRegExp = "edge_density_pyramid_([234])";
                hv_Names = new HTuple("edge_density_pyramid_") + HTuple.TupleGenSequence(2, 4, 1);
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(hv_CurrentName.TupleRegexpTest(hv_NameRegExp)) != 0)
                {
                    //** Calculate feature ***
                    hv_NumPyramids = ((hv_CurrentName.TupleRegexpMatch(hv_NameRegExp))).TupleNumber()
                        ;
                    calc_feature_pyramid(ho_Region, ho_Image, "edge_density", hv_NumPyramids,
                        out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups_pyramid(hv_Mode, hv_Groups, hv_CurrentName, hv_Names,
                    hv_NameRegExp, hv_AccumulatedResults, out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** edge_density_histogram_pyramid ***
                hv_NameRegExp = "edge_density_histogram_pyramid_([234])";
                hv_Names = new HTuple("edge_density_histogram_pyramid_") + HTuple.TupleGenSequence(
                    2, 4, 1);
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                hv_Groups[2] = "rot_invar";
                hv_Groups[3] = "scale_invar";
                //****************
                if ((int)(hv_CurrentName.TupleRegexpTest(hv_NameRegExp)) != 0)
                {
                    //** Calculate feature ***
                    hv_NumPyramids = ((hv_CurrentName.TupleRegexpMatch(hv_NameRegExp))).TupleNumber()
                        ;
                    calc_feature_pyramid(ho_Region, ho_Image, "edge_density_histogram", hv_NumPyramids,
                        out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups_pyramid(hv_Mode, hv_Groups, hv_CurrentName, hv_Names,
                    hv_NameRegExp, hv_AccumulatedResults, out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //** cooc ***
                hv_Name = "cooc";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                //****************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Feature = new HTuple();
                    HOperatorSet.CoocFeatureImage(ho_Region, ho_Image, 6, 0, out hv_Energy, out hv_Correlation,
                        out hv_Homogeneity, out hv_Contrast);
                    if ((int)(new HTuple(hv_NumRegions.TupleGreater(0))) != 0)
                    {
                        hv_Index = HTuple.TupleGenSequence(0, (4 * hv_NumRegions) - 1, 4);
                        if (hv_Feature == null)
                            hv_Feature = new HTuple();
                        hv_Feature[hv_Index] = hv_Energy;
                        if (hv_Feature == null)
                            hv_Feature = new HTuple();
                        hv_Feature[1 + hv_Index] = hv_Correlation;
                        if (hv_Feature == null)
                            hv_Feature = new HTuple();
                        hv_Feature[2 + hv_Index] = hv_Homogeneity;
                        if (hv_Feature == null)
                            hv_Feature = new HTuple();
                        hv_Feature[3 + hv_Index] = hv_Contrast;
                    }
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** cooc_pyramid ***
                hv_NameRegExp = "cooc_pyramid_([234])";
                hv_Names = new HTuple("cooc_pyramid_") + HTuple.TupleGenSequence(2, 4, 1);
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "texture";
                //****************
                if ((int)(hv_CurrentName.TupleRegexpTest(hv_NameRegExp)) != 0)
                {
                    //** Calculate feature ***
                    hv_NumPyramids = ((hv_CurrentName.TupleRegexpMatch(hv_NameRegExp))).TupleNumber()
                        ;
                    calc_feature_pyramid(ho_Region, ho_Image, "cooc", hv_NumPyramids, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups_pyramid(hv_Mode, hv_Groups, hv_CurrentName, hv_Names,
                    hv_NameRegExp, hv_AccumulatedResults, out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //
                //************************************
                //
                //************************************
                //POLAR TRANSFORM FEATURES
                //************************************
                //
                //************************************
                //** polar_gray_proj ***
                hv_Name = "polar_gray_proj";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Width = 100;
                    hv_Height = 40;
                    calc_feature_polar_gray_proj(ho_Region, ho_Image, "hor_gray", hv_Width, hv_Height,
                        out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** polar_grad_proj ***
                hv_Name = "polar_grad_proj";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Width = 100;
                    hv_Height = 40;
                    calc_feature_polar_gray_proj(ho_Region, ho_Image, "hor_sobel_amp", hv_Width,
                        hv_Height, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** polar_grad_x_proj ***
                hv_Name = "polar_grad_x_proj";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Width = 100;
                    hv_Height = 40;
                    calc_feature_polar_gray_proj(ho_Region, ho_Image, "hor_sobel_x", hv_Width,
                        hv_Height, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** polar_grad_y_proj ***
                hv_Name = "polar_grad_y_proj";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Width = 100;
                    hv_Height = 40;
                    calc_feature_polar_gray_proj(ho_Region, ho_Image, "hor_sobel_y", hv_Width,
                        hv_Height, out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** polar_gray_proj_histo ***
                hv_Name = "polar_gray_proj_histo";
                hv_Groups = new HTuple();
                hv_Groups[0] = "gray";
                hv_Groups[1] = "rot_invar";
                hv_Groups[2] = "scale_invar";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    hv_Width = 100;
                    hv_Height = 40;
                    calc_feature_polar_gray_proj(ho_Region, ho_Image, "vert_gray", hv_Width,
                        hv_Height, out hv_Projection);
                    hv_NumBins = 20;
                    hv_Feature = new HTuple();
                    HTuple end_val1093 = hv_NumRegions;
                    HTuple step_val1093 = 1;
                    for (hv_Index = 1; hv_Index.Continue(end_val1093, step_val1093); hv_Index = hv_Index.TupleAdd(step_val1093))
                    {
                        hv_Start = (hv_Index - 1) * hv_Width;
                        HOperatorSet.TupleHistoRange(hv_Projection.TupleSelectRange(hv_Start, (hv_Start + hv_Width) - 1),
                            0, 255, hv_NumBins, out hv_Histo, out hv_BinSize);
                        hv_Feature = hv_Feature.TupleConcat(hv_Histo);
                    }
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //COLOR FEATURES
                //************************************
                //
                //************************************
                //** cielab_mean ***
                hv_Name = "cielab_mean";
                hv_Groups = "color";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_color_intensity(ho_Region, ho_Image, "cielab", "mean", out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** cielab_dev ***
                hv_Name = "cielab_dev";
                hv_Groups = "color";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_color_intensity(ho_Region, ho_Image, "cielab", "deviation",
                        out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** hls_mean ***
                hv_Name = "hls_mean";
                hv_Groups = "color";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_color_intensity(ho_Region, ho_Image, "hls", "mean", out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** hls_dev ***
                hv_Name = "hls_dev";
                hv_Groups = "color";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_color_intensity(ho_Region, ho_Image, "hls", "deviation", out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** rgb_mean ***
                hv_Name = "rgb_mean";
                hv_Groups = "color";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_color_intensity(ho_Region, ho_Image, "rgb", "mean", out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
                //************************************
                //
                //************************************
                //** rgb_dev ***
                hv_Name = "rgb_dev";
                hv_Groups = "color";
                //*************
                if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
                {
                    //** Calculate feature ***
                    calc_feature_color_intensity(ho_Region, ho_Image, "rgb", "deviation", out hv_Feature);
                    //*************************
                    append_length_or_values(hv_Mode, hv_Feature, hv_AccumulatedResults, out hv_ExtendedResults);
                    hv_AccumulatedResults = hv_ExtendedResults.Clone();
                }
                append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_AccumulatedResults,
                    out hv_ExtendedResults);
                hv_AccumulatedResults = hv_ExtendedResults.Clone();
            }
            hv_Output = hv_AccumulatedResults.Clone();
            HOperatorSet.SetSystem("empty_region_result", hv_EmptyRegionResult);

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate gray-value projections of polar-transformed image regions. 
        public void calc_feature_polar_gray_proj(HObject ho_Region, HObject ho_Image,
            HTuple hv_Mode, HTuple hv_Width, HTuple hv_Height, out HTuple hv_Features)
        {




            // Local iconic variables 

            HObject ho_RegionSelected = null, ho_PolarTransImage = null;
            HObject ho_EdgeAmplitude = null, ho_ImageAbs = null;

            // Local control variables 

            HTuple hv_NumRegions = null, hv_Index = null;
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Radius = new HTuple(), hv_HorProjection = new HTuple();
            HTuple hv_VertProjection = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionSelected);
            HOperatorSet.GenEmptyObj(out ho_PolarTransImage);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_ImageAbs);
            //
            //Calculate gray-value projections of
            //polar-transformed image regions.
            //
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            hv_Features = new HTuple();
            HTuple end_val6 = hv_NumRegions;
            HTuple step_val6 = 1;
            for (hv_Index = 1; hv_Index.Continue(end_val6, step_val6); hv_Index = hv_Index.TupleAdd(step_val6))
            {
                ho_RegionSelected.Dispose();
                HOperatorSet.SelectObj(ho_Region, out ho_RegionSelected, hv_Index);
                HOperatorSet.SmallestCircle(ho_RegionSelected, out hv_Row, out hv_Column, out hv_Radius);
                ho_PolarTransImage.Dispose();
                HOperatorSet.PolarTransImageExt(ho_Image, out ho_PolarTransImage, hv_Row, hv_Column,
                    0, (new HTuple(360)).TupleRad(), 0, ((hv_Radius.TupleConcat(1))).TupleMax()
                    , hv_Width, hv_Height, "bilinear");
                //
                if ((int)(new HTuple(hv_Mode.TupleEqual("hor_gray"))) != 0)
                {
                    HOperatorSet.GrayProjections(ho_PolarTransImage, ho_PolarTransImage, "simple",
                        out hv_HorProjection, out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_HorProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("vert_gray"))) != 0)
                {
                    HOperatorSet.GrayProjections(ho_PolarTransImage, ho_PolarTransImage, "simple",
                        out hv_HorProjection, out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_VertProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("hor_sobel_amp"))) != 0)
                {
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_PolarTransImage, out ho_EdgeAmplitude, "sum_abs",
                        3);
                    ho_ImageAbs.Dispose();
                    HOperatorSet.AbsImage(ho_EdgeAmplitude, out ho_ImageAbs);
                    HOperatorSet.GrayProjections(ho_ImageAbs, ho_ImageAbs, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_HorProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("vert_sobel_amp"))) != 0)
                {
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_PolarTransImage, out ho_EdgeAmplitude, "sum_abs",
                        3);
                    ho_ImageAbs.Dispose();
                    HOperatorSet.AbsImage(ho_EdgeAmplitude, out ho_ImageAbs);
                    HOperatorSet.GrayProjections(ho_ImageAbs, ho_ImageAbs, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_VertProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("hor_sobel_x"))) != 0)
                {
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_PolarTransImage, out ho_EdgeAmplitude, "x_binomial",
                        3);
                    ho_ImageAbs.Dispose();
                    HOperatorSet.AbsImage(ho_EdgeAmplitude, out ho_ImageAbs);
                    HOperatorSet.GrayProjections(ho_ImageAbs, ho_ImageAbs, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_HorProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("vert_sobel_x"))) != 0)
                {
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_PolarTransImage, out ho_EdgeAmplitude, "x_binomial",
                        3);
                    ho_ImageAbs.Dispose();
                    HOperatorSet.AbsImage(ho_EdgeAmplitude, out ho_ImageAbs);
                    HOperatorSet.GrayProjections(ho_ImageAbs, ho_ImageAbs, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_VertProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("hor_sobel_y"))) != 0)
                {
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_PolarTransImage, out ho_EdgeAmplitude, "y_binomial",
                        3);
                    ho_ImageAbs.Dispose();
                    HOperatorSet.AbsImage(ho_EdgeAmplitude, out ho_ImageAbs);
                    HOperatorSet.GrayProjections(ho_ImageAbs, ho_ImageAbs, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_HorProjection);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("vert_sobel_y"))) != 0)
                {
                    ho_EdgeAmplitude.Dispose();
                    HOperatorSet.SobelAmp(ho_PolarTransImage, out ho_EdgeAmplitude, "y_binomial",
                        3);
                    ho_ImageAbs.Dispose();
                    HOperatorSet.AbsImage(ho_EdgeAmplitude, out ho_ImageAbs);
                    HOperatorSet.GrayProjections(ho_ImageAbs, ho_ImageAbs, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    hv_Features = hv_Features.TupleConcat(hv_VertProjection);
                }
                else
                {
                    throw new HalconException(("Unknown Mode: " + hv_Mode) + " in calc_feature_polar_proj");
                }
            }
            ho_RegionSelected.Dispose();
            ho_PolarTransImage.Dispose();
            ho_EdgeAmplitude.Dispose();
            ho_ImageAbs.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate the gradient direction histogram. 
        public void calc_feature_grad_dir_histo(HObject ho_Region, HObject ho_Image, HTuple hv_NumBins,
            out HTuple hv_Feature)
        {




            // Local iconic variables 

            HObject ho_Channel1, ho_RegionSelected = null;
            HObject ho_ImageReduced = null, ho_EdgeAmplitude = null, ho_EdgeDirection = null;

            // Local control variables 

            HTuple hv_NumRegions = null, hv_Index = null;
            HTuple hv_Histo = new HTuple(), hv_BinSize = new HTuple();
            HTuple hv_Sum = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Channel1);
            HOperatorSet.GenEmptyObj(out ho_RegionSelected);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_EdgeDirection);
            //
            //Calculate gradient direction histogram
            //
            ho_Channel1.Dispose();
            HOperatorSet.AccessChannel(ho_Image, out ho_Channel1, 1);
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            hv_Feature = new HTuple();
            HTuple end_val6 = hv_NumRegions;
            HTuple step_val6 = 1;
            for (hv_Index = 1; hv_Index.Continue(end_val6, step_val6); hv_Index = hv_Index.TupleAdd(step_val6))
            {
                ho_RegionSelected.Dispose();
                HOperatorSet.SelectObj(ho_Region, out ho_RegionSelected, hv_Index);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Channel1, ho_RegionSelected, out ho_ImageReduced
                    );
                ho_EdgeAmplitude.Dispose(); ho_EdgeDirection.Dispose();
                HOperatorSet.SobelDir(ho_ImageReduced, out ho_EdgeAmplitude, out ho_EdgeDirection,
                    "sum_abs_binomial", 3);
                HOperatorSet.GrayHistoRange(ho_RegionSelected, ho_EdgeDirection, 0, 179, hv_NumBins,
                    out hv_Histo, out hv_BinSize);
                hv_Sum = hv_Histo.TupleSum();
                if ((int)(new HTuple(hv_Sum.TupleNotEqual(0))) != 0)
                {
                    hv_Feature = hv_Feature.TupleConcat((hv_Histo.TupleReal()) / hv_Sum);
                }
                else
                {
                    hv_Feature = hv_Feature.TupleConcat(hv_Histo);
                }
            }
            ho_Channel1.Dispose();
            ho_RegionSelected.Dispose();
            ho_ImageReduced.Dispose();
            ho_EdgeAmplitude.Dispose();
            ho_EdgeDirection.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate color intensity features. 
        public void calc_feature_color_intensity(HObject ho_Region, HObject ho_Image,
            HTuple hv_ColorSpace, HTuple hv_Mode, out HTuple hv_Feature)
        {




            // Local iconic variables 

            HObject ho_R, ho_G, ho_B, ho_I1 = null, ho_I2 = null;
            HObject ho_I3 = null;

            // Local control variables 

            HTuple hv_Channels = null, hv_Mean1 = new HTuple();
            HTuple hv_Deviation1 = new HTuple(), hv_Mean2 = new HTuple();
            HTuple hv_Deviation2 = new HTuple(), hv_Mean3 = new HTuple();
            HTuple hv_Deviation3 = new HTuple(), hv_Tmp1 = new HTuple();
            HTuple hv_Tmp2 = new HTuple(), hv_Tmp3 = new HTuple();
            HTuple hv_NumRegions = null, hv_Index = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_R);
            HOperatorSet.GenEmptyObj(out ho_G);
            HOperatorSet.GenEmptyObj(out ho_B);
            HOperatorSet.GenEmptyObj(out ho_I1);
            HOperatorSet.GenEmptyObj(out ho_I2);
            HOperatorSet.GenEmptyObj(out ho_I3);
            hv_Feature = new HTuple();
            //
            //Calculate color features
            //
            //Transform an RGB image into the given ColorSpace
            //and calculate the mean gray value and the deviation
            //for all three channels.
            //
            HOperatorSet.CountChannels(ho_Image, out hv_Channels);
            if ((int)(new HTuple(hv_Channels.TupleNotEqual(3))) != 0)
            {
                throw new HalconException((((("Error when calculating feature " + hv_ColorSpace) + "_") + hv_Mode)).TupleConcat(
                    "Please use a 3-channel RGB image or remove color feature from the list."));
            }
            ho_R.Dispose(); ho_G.Dispose(); ho_B.Dispose();
            HOperatorSet.Decompose3(ho_Image, out ho_R, out ho_G, out ho_B);
            if ((int)(new HTuple(hv_ColorSpace.TupleEqual("rgb"))) != 0)
            {
                HOperatorSet.Intensity(ho_Region, ho_R, out hv_Mean1, out hv_Deviation1);
                HOperatorSet.Intensity(ho_Region, ho_G, out hv_Mean2, out hv_Deviation2);
                HOperatorSet.Intensity(ho_Region, ho_B, out hv_Mean3, out hv_Deviation3);
            }
            else
            {
                ho_I1.Dispose(); ho_I2.Dispose(); ho_I3.Dispose();
                HOperatorSet.TransFromRgb(ho_R, ho_G, ho_B, out ho_I1, out ho_I2, out ho_I3,
                    hv_ColorSpace);
                HOperatorSet.Intensity(ho_Region, ho_I1, out hv_Mean1, out hv_Deviation1);
                HOperatorSet.Intensity(ho_Region, ho_I2, out hv_Mean2, out hv_Deviation2);
                HOperatorSet.Intensity(ho_Region, ho_I3, out hv_Mean3, out hv_Deviation3);
            }
            if ((int)(new HTuple(hv_Mode.TupleEqual("mean"))) != 0)
            {
                hv_Tmp1 = hv_Mean1.Clone();
                hv_Tmp2 = hv_Mean2.Clone();
                hv_Tmp3 = hv_Mean3.Clone();
            }
            else if ((int)(new HTuple(hv_Mode.TupleEqual("deviation"))) != 0)
            {
                hv_Tmp1 = hv_Deviation1.Clone();
                hv_Tmp2 = hv_Deviation2.Clone();
                hv_Tmp3 = hv_Deviation3.Clone();
            }
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            if ((int)(new HTuple(hv_NumRegions.TupleGreater(0))) != 0)
            {
                hv_Index = HTuple.TupleGenSequence(0, (3 * hv_NumRegions) - 1, 3);
                if (hv_Feature == null)
                    hv_Feature = new HTuple();
                hv_Feature[hv_Index] = hv_Tmp1;
                if (hv_Feature == null)
                    hv_Feature = new HTuple();
                hv_Feature[1 + hv_Index] = hv_Tmp2;
                if (hv_Feature == null)
                    hv_Feature = new HTuple();
                hv_Feature[2 + hv_Index] = hv_Tmp3;
            }
            else
            {
                hv_Feature = new HTuple();
            }
            ho_R.Dispose();
            ho_G.Dispose();
            ho_B.Dispose();
            ho_I1.Dispose();
            ho_I2.Dispose();
            ho_I3.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate a feature on different image pyramid levels. 
        public void calc_feature_pyramid(HObject ho_Region, HObject ho_Image, HTuple hv_FeatureName,
            HTuple hv_NumLevels, out HTuple hv_Feature)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ImageZoom = null, ho_RegionZoom = null;

            // Local control variables 

            HTuple hv_Zoom = null, hv_NumRegions = null;
            HTuple hv_I = new HTuple(), hv_Features = new HTuple();
            HTuple hv_FeatureLength = new HTuple(), hv_Step = new HTuple();
            HTuple hv_Indices = new HTuple(), hv_J = new HTuple();
            HTuple hv_Start = new HTuple(), hv_End = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageZoom);
            HOperatorSet.GenEmptyObj(out ho_RegionZoom);
            //
            //Calculate a feature for different pyramid levels
            //
            hv_Zoom = 0.5;
            hv_Feature = new HTuple();
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            if ((int)(new HTuple(hv_NumRegions.TupleGreater(0))) != 0)
            {
                HTuple end_val7 = hv_NumLevels;
                HTuple step_val7 = 1;
                for (hv_I = 1; hv_I.Continue(end_val7, step_val7); hv_I = hv_I.TupleAdd(step_val7))
                {
                    if ((int)(new HTuple(hv_I.TupleGreater(1))) != 0)
                    {
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ZoomImageFactor(ho_ImageZoom, out ExpTmpOutVar_0, hv_Zoom,
                                hv_Zoom, "constant");
                            ho_ImageZoom.Dispose();
                            ho_ImageZoom = ExpTmpOutVar_0;
                        }
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ZoomRegion(ho_RegionZoom, out ExpTmpOutVar_0, hv_Zoom, hv_Zoom);
                            ho_RegionZoom.Dispose();
                            ho_RegionZoom = ExpTmpOutVar_0;
                        }
                        calculate_features(ho_RegionZoom, ho_ImageZoom, hv_FeatureName, out hv_Features);
                    }
                    else
                    {
                        ho_ImageZoom.Dispose();
                        HOperatorSet.CopyObj(ho_Image, out ho_ImageZoom, 1, 1);
                        ho_RegionZoom.Dispose();
                        HOperatorSet.CopyObj(ho_Region, out ho_RegionZoom, 1, hv_NumRegions);
                        calculate_features(ho_RegionZoom, ho_ImageZoom, hv_FeatureName, out hv_Features);
                        hv_FeatureLength = (new HTuple(hv_Features.TupleLength())) / hv_NumRegions;
                        hv_Step = hv_NumLevels * hv_FeatureLength;
                    }
                    hv_Indices = new HTuple();
                    HTuple end_val20 = hv_NumRegions - 1;
                    HTuple step_val20 = 1;
                    for (hv_J = 0; hv_J.Continue(end_val20, step_val20); hv_J = hv_J.TupleAdd(step_val20))
                    {
                        hv_Start = (hv_J * hv_Step) + ((hv_I - 1) * hv_FeatureLength);
                        hv_End = (hv_Start + hv_FeatureLength) - 1;
                        hv_Indices = hv_Indices.TupleConcat(HTuple.TupleGenSequence(hv_Start, hv_End, 1));
                    }
                    if (hv_Feature == null)
                        hv_Feature = new HTuple();
                    hv_Feature[hv_Indices] = hv_Features;
                }
            }
            ho_ImageZoom.Dispose();
            ho_RegionZoom.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate edge density. 
        public void calc_feature_edge_density(HObject ho_Region, HObject ho_Image, out HTuple hv_Feature)
        {



            // Local iconic variables 

            HObject ho_RegionUnion, ho_ImageReduced, ho_EdgeAmplitude = null;

            // Local control variables 

            HTuple hv_Area = null, hv_Row = null, hv_Column = null;
            HTuple hv_Width = null, hv_Height = null, hv_AreaGray = new HTuple();
            HTuple hv_ZeroIndex = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            hv_Feature = new HTuple();
            //
            //Calculate the edge density, i.e.
            //the ratio of the edge amplitudes to the area of the region.
            //
            ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_Region, out ho_RegionUnion);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_RegionUnion, out ho_ImageReduced);
            HOperatorSet.AreaCenter(ho_Region, out hv_Area, out hv_Row, out hv_Column);
            HOperatorSet.GetImageSize(ho_ImageReduced, out hv_Width, out hv_Height);
            if ((int)((new HTuple(hv_Width.TupleGreater(1))).TupleAnd(new HTuple(hv_Height.TupleGreater(
                1)))) != 0)
            {
                ho_EdgeAmplitude.Dispose();
                HOperatorSet.SobelAmp(ho_ImageReduced, out ho_EdgeAmplitude, "sum_abs", 3);
                HOperatorSet.AreaCenterGray(ho_Region, ho_EdgeAmplitude, out hv_AreaGray, out hv_Row,
                    out hv_Column);
                hv_ZeroIndex = hv_Area.TupleFind(0);
                if ((int)(new HTuple(hv_ZeroIndex.TupleNotEqual(-1))) != 0)
                {
                    if (hv_Area == null)
                        hv_Area = new HTuple();
                    hv_Area[hv_ZeroIndex] = 1;
                    if (hv_AreaGray == null)
                        hv_AreaGray = new HTuple();
                    hv_AreaGray[hv_ZeroIndex] = 0;
                }
                hv_Feature = hv_AreaGray / hv_Area;
            }
            else
            {
                hv_Feature = HTuple.TupleGenConst(new HTuple(hv_Area.TupleLength()), 0.0);
            }
            ho_RegionUnion.Dispose();
            ho_ImageReduced.Dispose();
            ho_EdgeAmplitude.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Returns a table of feature names sorted by groups. 
        public void query_feature_names_by_group(HTuple hv_GroupNames, out HTuple hv_FeatureNames,
            out HTuple hv_Groups)
        {



            // Local iconic variables 

            HObject ho_Region, ho_Image;

            // Local control variables 

            HTuple hv_I = null, hv_Names = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Image);
            //
            //Return a table (consisting of two tuples)
            //of all features and the groups they belong to.
            //
            hv_FeatureNames = new HTuple();
            hv_Groups = new HTuple();
            ho_Region.Dispose(); ho_Image.Dispose();
            gen_dummy_objects(out ho_Region, out ho_Image);
            for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_GroupNames.TupleLength())) - 1); hv_I = (int)hv_I + 1)
            {
                get_features(ho_Region, ho_Image, hv_GroupNames.TupleSelect(hv_I), "get_names",
                    out hv_Names);
                hv_FeatureNames = hv_FeatureNames.TupleConcat(hv_Names);
                hv_Groups = hv_Groups.TupleConcat(HTuple.TupleGenConst(new HTuple(hv_Names.TupleLength()
                    ), hv_GroupNames.TupleSelect(hv_I)));
            }
            ho_Region.Dispose();
            ho_Image.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Auxiliary procedure for get_custom_features and get_features. 
        public void append_names_or_groups(HTuple hv_Mode, HTuple hv_Name, HTuple hv_Groups,
            HTuple hv_CurrentName, HTuple hv_AccumulatedResults, out HTuple hv_ExtendedResults)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_FirstOccurrence = new HTuple(), hv_BelongsToGroup = new HTuple();
            // Initialize local and output iconic variables 
            //
            //Auxiliary procedure used only by get_features and get_custom_features
            //
            hv_ExtendedResults = hv_AccumulatedResults.Clone();
            if ((int)(new HTuple(hv_Mode.TupleEqual("get_names"))) != 0)
            {
                hv_FirstOccurrence = (new HTuple((new HTuple(hv_AccumulatedResults.TupleLength()
                    )).TupleEqual(0))).TupleOr(new HTuple(((hv_AccumulatedResults.TupleFind(
                    hv_Name))).TupleEqual(-1)));
                hv_BelongsToGroup = (new HTuple(((((hv_Name.TupleConcat(hv_Groups))).TupleFind(
                    hv_CurrentName))).TupleNotEqual(-1))).TupleOr(new HTuple(hv_CurrentName.TupleEqual(
                    "all")));
                if ((int)(hv_FirstOccurrence.TupleAnd(hv_BelongsToGroup)) != 0)
                {
                    //Output in 'get_names' mode is the name of the feature
                    hv_ExtendedResults = new HTuple();
                    hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_AccumulatedResults);
                    hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_Name);
                }
            }
            else if ((int)(new HTuple(hv_Mode.TupleEqual("get_groups"))) != 0)
            {
                hv_ExtendedResults = new HTuple();
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_AccumulatedResults);
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_Groups);
            }

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Auxiliary procedure for get_custom_features and get_features. 
        public void append_length_or_values(HTuple hv_Mode, HTuple hv_Feature, HTuple hv_AccumulatedResults,
            out HTuple hv_ExtendedResults)
        {



            // Local iconic variables 
            // Initialize local and output iconic variables 
            hv_ExtendedResults = new HTuple();
            //
            //Auxiliary procedure used only by get_features and get_custom_features
            //
            if ((int)(new HTuple(hv_Mode.TupleEqual("get_lengths"))) != 0)
            {
                //Output in 'get_lengths' mode is the length of the feature
                hv_ExtendedResults = new HTuple();
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_AccumulatedResults);
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(new HTuple(hv_Feature.TupleLength()
                    ));
            }
            else if ((int)(new HTuple(hv_Mode.TupleEqual("calculate"))) != 0)
            {
                //Output in 'calculate' mode is the feature vector
                hv_ExtendedResults = new HTuple();
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_AccumulatedResults);
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_Feature);
            }
            else
            {
                hv_ExtendedResults = hv_AccumulatedResults.Clone();
            }

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Returns a list of feature names that belong to the feature groups given in GroupNames. 
        public void get_feature_names(HTuple hv_GroupNames, out HTuple hv_Names)
        {



            // Local iconic variables 

            HObject ho_Region, ho_Image;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Image);
            //
            //Return all features that belong to
            //at least one of the groups in GroupNames
            //
            ho_Region.Dispose(); ho_Image.Dispose();
            gen_dummy_objects(out ho_Region, out ho_Image);
            get_features(ho_Region, ho_Image, hv_GroupNames, "get_names", out hv_Names);
            ho_Region.Dispose();
            ho_Image.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate edge density histogram feature. 
        public void calc_feature_edge_density_histogram(HObject ho_Region, HObject ho_Image,
            HTuple hv_NumBins, out HTuple hv_Feature)
        {




            // Local iconic variables 

            HObject ho_Channel1 = null, ho_EdgeAmplitude = null;
            HObject ho_RegionSelected = null;

            // Local control variables 

            HTuple hv_ImageWidth = null, hv_ImageHeight = null;
            HTuple hv_NumRegions = null, hv_J = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Histo = new HTuple(), hv_BinSize = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Channel1);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_RegionSelected);
            //
            //Calculate the edge density histogram, i.e.
            //the ratio of the edge amplitude histogram to the area of the region.
            //
            hv_Feature = new HTuple();
            HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            if ((int)((new HTuple(hv_ImageWidth.TupleGreater(1))).TupleAnd(new HTuple(hv_ImageHeight.TupleGreater(
                1)))) != 0)
            {
                ho_Channel1.Dispose();
                HOperatorSet.AccessChannel(ho_Image, out ho_Channel1, 1);
                ho_EdgeAmplitude.Dispose();
                HOperatorSet.SobelAmp(ho_Channel1, out ho_EdgeAmplitude, "sum_abs", 3);
                HTuple end_val10 = hv_NumRegions;
                HTuple step_val10 = 1;
                for (hv_J = 1; hv_J.Continue(end_val10, step_val10); hv_J = hv_J.TupleAdd(step_val10))
                {
                    ho_RegionSelected.Dispose();
                    HOperatorSet.SelectObj(ho_Region, out ho_RegionSelected, hv_J);
                    HOperatorSet.AreaCenter(ho_RegionSelected, out hv_Area, out hv_Row, out hv_Column);
                    if ((int)(new HTuple(hv_Area.TupleGreater(0))) != 0)
                    {
                        HOperatorSet.GrayHistoRange(ho_RegionSelected, ho_EdgeAmplitude, 0, 255,
                            hv_NumBins, out hv_Histo, out hv_BinSize);
                        hv_Feature = hv_Feature.TupleConcat((hv_Histo.TupleReal()) / (hv_Histo.TupleSum()
                            ));
                    }
                    else
                    {
                        hv_Feature = ((hv_Feature.TupleConcat(1.0))).TupleConcat(HTuple.TupleGenConst(
                            hv_NumBins - 1, 0.0));
                    }
                }
            }
            else
            {
                hv_Feature = HTuple.TupleGenConst(hv_NumRegions * hv_NumBins, 0.0);
            }
            ho_Channel1.Dispose();
            ho_EdgeAmplitude.Dispose();
            ho_RegionSelected.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Auxiliary procedure for get_features. 
        public void append_names_or_groups_pyramid(HTuple hv_Mode, HTuple hv_Groups, HTuple hv_CurrentName,
            HTuple hv_Names, HTuple hv_NameRegExp, HTuple hv_AccumulatedResults, out HTuple hv_ExtendedResults)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_BelongsToGroup = new HTuple(), hv_TmpNames = new HTuple();
            HTuple hv_J = new HTuple(), hv_FirstOccurrence = new HTuple();
            HTuple hv_Names_COPY_INP_TMP = hv_Names.Clone();

            // Initialize local and output iconic variables 
            //
            //Auxiliary procedure used only by get_features and get_custom_features
            //
            hv_ExtendedResults = hv_AccumulatedResults.Clone();
            if ((int)(new HTuple(hv_Mode.TupleEqual("get_names"))) != 0)
            {
                hv_BelongsToGroup = (new HTuple(((hv_Groups.TupleFind(hv_CurrentName))).TupleNotEqual(
                    -1))).TupleOr(new HTuple(hv_CurrentName.TupleEqual("all")));
                if ((int)(hv_CurrentName.TupleRegexpTest(hv_NameRegExp)) != 0)
                {
                    hv_Names_COPY_INP_TMP = hv_CurrentName.Clone();
                }
                else if ((int)(hv_BelongsToGroup.TupleNot()) != 0)
                {
                    hv_Names_COPY_INP_TMP = new HTuple();
                }
                hv_TmpNames = new HTuple();
                for (hv_J = 0; (int)hv_J <= (int)((new HTuple(hv_Names_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_J = (int)hv_J + 1)
                {
                    hv_FirstOccurrence = (new HTuple((new HTuple(hv_AccumulatedResults.TupleLength()
                        )).TupleEqual(0))).TupleOr(new HTuple(((hv_AccumulatedResults.TupleFind(
                        hv_Names_COPY_INP_TMP.TupleSelect(hv_J)))).TupleEqual(-1)));
                    if ((int)(hv_FirstOccurrence) != 0)
                    {
                        //Output in 'get_names' mode is the name of the feature
                        hv_TmpNames = hv_TmpNames.TupleConcat(hv_Names_COPY_INP_TMP.TupleSelect(
                            hv_J));
                    }
                }
                hv_ExtendedResults = new HTuple();
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_AccumulatedResults);
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_TmpNames);
            }
            else if ((int)(new HTuple(hv_Mode.TupleEqual("get_groups"))) != 0)
            {
                hv_ExtendedResults = new HTuple();
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_AccumulatedResults);
                hv_ExtendedResults = hv_ExtendedResults.TupleConcat(hv_Groups);
            }

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Test procedure for custom features. 
        public void test_features(HTuple hv_FeatureNames)
        {



            // Local iconic variables 

            HObject ho_Image, ho_Region, ho_TestRegion = null;
            HObject ho_TestRegionSelected, ho_ObjectSelected = null;

            // Local control variables 

            HTuple hv_TestSuccessful = null, hv_Lengths = null;
            HTuple hv_TestString = null, hv_Test = null, hv_NumRegions = new HTuple();
            HTuple hv_AllFeatures = new HTuple(), hv_Index = new HTuple();
            HTuple hv_CurName = new HTuple(), hv_CurLength = new HTuple();
            HTuple hv_Features = new HTuple(), hv_SumLengths = new HTuple();
            HTuple hv_Total = new HTuple(), hv_I = null, hv_Features1 = new HTuple();
            HTuple hv_Features2 = new HTuple(), hv_J = new HTuple();
            HTuple hv_CorrectOrder = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_TestRegion);
            HOperatorSet.GenEmptyObj(out ho_TestRegionSelected);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            //
            //Test procedure for custom features
            //
            //This procedure can be used to test, if custom features
            //implemented in get_custom_features comply with the
            //specifications of the calculate_feature_set library.
            //
            //In particular, the feature vector Feature, that is
            //calculated with calculate_feature must fulfil
            //following conditions:
            //
            //- For a single input region the result of
            //  get_feature_length has to be equal to the length
            //  of the featue vector: |Feature| == Length
            //
            //- For an empty input region array, the feature
            //  vector has to be empty:
            //  Feature == []
            //
            //- For input region arrays with multiple regions, the
            //  following condition must be met:
            //  |Feature| == NumRegions * Length
            //
            //- Additionally, the feature vector has to be sorted
            //  according to the 'feature_column' order of
            //  add_sample_class_train_data.
            //
            hv_TestSuccessful = 0;
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, "patras");
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_Image, out ho_Region, 128, 255);
            get_feature_lengths(hv_FeatureNames, out hv_Lengths);
            //
            if (hv_TestString == null)
                hv_TestString = new HTuple();
            hv_TestString[0] = "Empty region array test (no region)";
            if (hv_TestString == null)
                hv_TestString = new HTuple();
            hv_TestString[1] = "Empty region test";
            if (hv_TestString == null)
                hv_TestString = new HTuple();
            hv_TestString[2] = "Single region test";
            for (hv_Test = 0; (int)hv_Test <= 2; hv_Test = (int)hv_Test + 1)
            {
                switch (hv_Test.I)
                {
                    case 0:
                        ho_TestRegion.Dispose();
                        HOperatorSet.SelectShape(ho_Region, out ho_TestRegion, "area", "and", 0,
                            0);
                        break;
                    case 1:
                        ho_TestRegion.Dispose();
                        HOperatorSet.GenEmptyRegion(out ho_TestRegion);
                        break;
                    case 2:
                        ho_TestRegion.Dispose();
                        HOperatorSet.CopyObj(ho_Region, out ho_TestRegion, 1, 1);
                        break;
                    default:
                        break;
                }
                HOperatorSet.CountObj(ho_TestRegion, out hv_NumRegions);
                hv_AllFeatures = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_FeatureNames.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    hv_CurName = hv_FeatureNames.TupleSelect(hv_Index);
                    hv_CurLength = hv_Lengths.TupleSelect(hv_Index);
                    calculate_features(ho_TestRegion, ho_Image, hv_CurName, out hv_Features);
                    if ((int)(new HTuple(((hv_NumRegions * hv_CurLength)).TupleNotEqual(new HTuple(hv_Features.TupleLength()
                        )))) != 0)
                    {
                        throw new HalconException((((hv_TestString.TupleSelect(
                            hv_Test)) + " failed for feature '") + hv_CurName) + "'");
                    }
                    hv_AllFeatures = hv_AllFeatures.TupleConcat(hv_Features);
                }
                hv_SumLengths = hv_Lengths.TupleSum();
                hv_Total = hv_SumLengths * hv_NumRegions;
                if ((int)(new HTuple(hv_Total.TupleNotEqual(new HTuple(hv_AllFeatures.TupleLength()
                    )))) != 0)
                {
                    throw new HalconException(((("Test " + hv_Test) + " failed")).TupleConcat(
                        hv_TestString.TupleSelect(hv_Test)));
                }
            }
            //
            //Test multiple input regions
            ho_TestRegion.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_TestRegion);
            ho_TestRegionSelected.Dispose();
            HOperatorSet.SelectObj(ho_TestRegion, out ho_TestRegionSelected, HTuple.TupleGenSequence(
                1, 3, 1));
            for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_FeatureNames.TupleLength())) - 1); hv_I = (int)hv_I + 1)
            {
                hv_CurName = hv_FeatureNames.TupleSelect(hv_I);
                calculate_features(ho_TestRegionSelected, ho_Image, hv_CurName, out hv_Features1);
                hv_Features2 = new HTuple();
                HOperatorSet.CountObj(ho_TestRegionSelected, out hv_NumRegions);
                HTuple end_val74 = hv_NumRegions;
                HTuple step_val74 = 1;
                for (hv_J = 1; hv_J.Continue(end_val74, step_val74); hv_J = hv_J.TupleAdd(step_val74))
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_TestRegionSelected, out ho_ObjectSelected, hv_J);
                    calculate_features(ho_ObjectSelected, ho_Image, hv_CurName, out hv_Features);
                    hv_Features2 = hv_Features2.TupleConcat(hv_Features);
                }
                hv_CorrectOrder = new HTuple(hv_Features1.TupleEqual(hv_Features2));
                if ((int)(hv_CorrectOrder.TupleNot()) != 0)
                {
                    throw new HalconException(("Multiple region test failed for feature '" + hv_CurName) + "'");
                }
            }
            hv_TestSuccessful = 1;
            ho_Image.Dispose();
            ho_Region.Dispose();
            ho_TestRegion.Dispose();
            ho_TestRegionSelected.Dispose();
            ho_ObjectSelected.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Calculate gray-value projections and their histograms. 
        public void calc_feature_gray_proj(HObject ho_Region, HObject ho_Image, HTuple hv_Mode,
            HTuple hv_Size, out HTuple hv_Feature)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_RegionTmp = null, ho_RegionMoved = null;
            HObject ho_ImageTmp = null;

            // Local control variables 

            HTuple hv_NumRegions = null, hv_Index = null;
            HTuple hv_RowsTmp = new HTuple(), hv_ColumnsTmp = new HTuple();
            HTuple hv_HorProjectionFilledUp = new HTuple(), hv_VertProjectionFilledUp = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_ScaleHeight = new HTuple(), hv_ScaleWidth = new HTuple();
            HTuple hv_HorProjection = new HTuple(), hv_VertProjection = new HTuple();
            HTuple hv_HorProjectionFilledUpFront = new HTuple(), hv_VertProjectionFilledUpFront = new HTuple();
            HTuple hv_Histo = new HTuple(), hv_BinSize = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionTmp);
            HOperatorSet.GenEmptyObj(out ho_RegionMoved);
            HOperatorSet.GenEmptyObj(out ho_ImageTmp);
            //
            //Calculate gray-value projections and their histograms
            //
            HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
            hv_Feature = new HTuple();
            //
            HTuple end_val6 = hv_NumRegions;
            HTuple step_val6 = 1;
            for (hv_Index = 1; hv_Index.Continue(end_val6, step_val6); hv_Index = hv_Index.TupleAdd(step_val6))
            {
                ho_RegionTmp.Dispose();
                HOperatorSet.SelectObj(ho_Region, out ho_RegionTmp, hv_Index);
                //Test empty region
                HOperatorSet.GetRegionPoints(ho_RegionTmp, out hv_RowsTmp, out hv_ColumnsTmp);
                if ((int)(new HTuple((new HTuple(hv_RowsTmp.TupleLength())).TupleEqual(0))) != 0)
                {
                    hv_HorProjectionFilledUp = HTuple.TupleGenConst(hv_Size, -1.0);
                    hv_VertProjectionFilledUp = HTuple.TupleGenConst(hv_Size, -1.0);
                }
                else
                {
                    //Zoom image and region to Size x Size pixels
                    HOperatorSet.SmallestRectangle1(ho_RegionTmp, out hv_Row1, out hv_Column1,
                        out hv_Row2, out hv_Column2);
                    ho_RegionMoved.Dispose();
                    HOperatorSet.MoveRegion(ho_RegionTmp, out ho_RegionMoved, -hv_Row1, -hv_Column1);
                    ho_ImageTmp.Dispose();
                    HOperatorSet.CropRectangle1(ho_Image, out ho_ImageTmp, hv_Row1, hv_Column1,
                        hv_Row2, hv_Column2);
                    hv_ScaleHeight = (hv_Size.TupleReal()) / ((hv_Row2 - hv_Row1) + 1);
                    hv_ScaleWidth = (hv_Size.TupleReal()) / ((hv_Column2 - hv_Column1) + 1);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ZoomImageFactor(ho_ImageTmp, out ExpTmpOutVar_0, hv_ScaleWidth,
                            hv_ScaleHeight, "constant");
                        ho_ImageTmp.Dispose();
                        ho_ImageTmp = ExpTmpOutVar_0;
                    }
                    ho_RegionTmp.Dispose();
                    HOperatorSet.ZoomRegion(ho_RegionMoved, out ho_RegionTmp, hv_ScaleWidth,
                        hv_ScaleHeight);
                    //Calculate gray value projection
                    HOperatorSet.GrayProjections(ho_RegionTmp, ho_ImageTmp, "simple", out hv_HorProjection,
                        out hv_VertProjection);
                    //Fill up projection in case the zoomed region is smaller than
                    //Size x Size pixels due to interpolation effects
                    HOperatorSet.SmallestRectangle1(ho_RegionTmp, out hv_Row1, out hv_Column1,
                        out hv_Row2, out hv_Column2);
                    hv_HorProjectionFilledUpFront = new HTuple();
                    hv_HorProjectionFilledUpFront = hv_HorProjectionFilledUpFront.TupleConcat(HTuple.TupleGenConst(
                        (new HTuple(0)).TupleMax2(hv_Row1), -1.0));
                    hv_HorProjectionFilledUpFront = hv_HorProjectionFilledUpFront.TupleConcat(hv_HorProjection);
                    hv_HorProjectionFilledUp = new HTuple();
                    hv_HorProjectionFilledUp = hv_HorProjectionFilledUp.TupleConcat(hv_HorProjectionFilledUpFront);
                    hv_HorProjectionFilledUp = hv_HorProjectionFilledUp.TupleConcat(HTuple.TupleGenConst(
                        hv_Size - (new HTuple(hv_HorProjectionFilledUpFront.TupleLength())), -1.0));
                    hv_VertProjectionFilledUpFront = new HTuple();
                    hv_VertProjectionFilledUpFront = hv_VertProjectionFilledUpFront.TupleConcat(HTuple.TupleGenConst(
                        (new HTuple(0)).TupleMax2(hv_Column1), -1.0));
                    hv_VertProjectionFilledUpFront = hv_VertProjectionFilledUpFront.TupleConcat(hv_VertProjection);
                    hv_VertProjectionFilledUp = new HTuple();
                    hv_VertProjectionFilledUp = hv_VertProjectionFilledUp.TupleConcat(hv_VertProjectionFilledUpFront);
                    hv_VertProjectionFilledUp = hv_VertProjectionFilledUp.TupleConcat(HTuple.TupleGenConst(
                        hv_Size - (new HTuple(hv_VertProjectionFilledUpFront.TupleLength())), -1.0));
                }
                if ((int)(new HTuple(hv_Mode.TupleEqual("hor"))) != 0)
                {
                    hv_Feature = hv_Feature.TupleConcat(hv_HorProjectionFilledUp);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("vert"))) != 0)
                {
                    hv_Feature = hv_Feature.TupleConcat(hv_VertProjectionFilledUp);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("hor_histo"))) != 0)
                {
                    HOperatorSet.TupleHistoRange(hv_HorProjectionFilledUp, 0, 255, hv_Size, out hv_Histo,
                        out hv_BinSize);
                    hv_Feature = hv_Feature.TupleConcat(hv_Histo);
                }
                else if ((int)(new HTuple(hv_Mode.TupleEqual("vert_histo"))) != 0)
                {
                    HOperatorSet.TupleHistoRange(hv_VertProjectionFilledUp, 0, 255, hv_Size,
                        out hv_Histo, out hv_BinSize);
                    hv_Feature = hv_Feature.TupleConcat(hv_Histo);
                }
            }
            ho_RegionTmp.Dispose();
            ho_RegionMoved.Dispose();
            ho_ImageTmp.Dispose();

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Interactively display 3D object models 
        public void visualize_object_model_3d(HTuple hv_WindowHandle, HTuple hv_ObjectModel3D,
            HTuple hv_CamParam, HTuple hv_PoseIn, HTuple hv_GenParamName, HTuple hv_GenParamValue,
            HTuple hv_Title, HTuple hv_Label, HTuple hv_Information, out HTuple hv_PoseOut)
        {



            // Local iconic variables 

            HObject ho_Image, ho_ImageDump = null;

            // Local control variables 

            HTuple ExpTmpLocalVar_gDispObjOffset = null;
            HTuple ExpTmpLocalVar_gLabelsDecor = null, ExpTmpLocalVar_gInfoDecor = null;
            HTuple ExpTmpLocalVar_gInfoPos = null, ExpTmpLocalVar_gTitlePos = null;
            HTuple ExpTmpLocalVar_gTitleDecor = null, ExpTmpLocalVar_gTerminationButtonLabel = null;
            HTuple ExpTmpLocalVar_gAlphaDeselected = null, ExpTmpLocalVar_gIsSinglePose = new HTuple();
            HTuple ExpTmpLocalVar_gUsesOpenGL = null, hv_TrackballSize = null;
            HTuple hv_VirtualTrackball = null, hv_MouseMapping = null;
            HTuple hv_WaitForButtonRelease = null, hv_MaxNumModels = null;
            HTuple hv_WindowCenteredRotation = null, hv_NumModels = null;
            HTuple hv_SelectedObject = null, hv_ClipRegion = null;
            HTuple hv_CPLength = null, hv_RowNotUsed = null, hv_ColumnNotUsed = null;
            HTuple hv_Width = null, hv_Height = null, hv_WPRow1 = null;
            HTuple hv_WPColumn1 = null, hv_WPRow2 = null, hv_WPColumn2 = null;
            HTuple hv_CamWidth = new HTuple(), hv_CamHeight = new HTuple();
            HTuple hv_Scale = new HTuple(), hv_Indices = null, hv_DispBackground = null;
            HTuple hv_Mask = new HTuple(), hv_Center = null, hv_Poses = new HTuple();
            HTuple hv_HomMat3Ds = new HTuple(), hv_Sequence = new HTuple();
            HTuple hv_PoseEstimated = new HTuple(), hv_WindowHandleBuffer = new HTuple();
            HTuple hv_Font = null, hv_Exception = null, hv_OpenGLInfo = new HTuple();
            HTuple hv_DummyObjectModel3D = new HTuple(), hv_Scene3DTest = new HTuple();
            HTuple hv_CameraIndexTest = new HTuple(), hv_PoseTest = new HTuple();
            HTuple hv_InstanceIndexTest = new HTuple(), hv_MinImageSize = null;
            HTuple hv_TrackballRadiusPixel = null, hv_Ascent = null;
            HTuple hv_Descent = null, hv_TextWidth = null, hv_TextHeight = null;
            HTuple hv_NumChannels = null, hv_ColorImage = null, hv_Scene3D = null;
            HTuple hv_CameraIndex = null, hv_AllInstances = null, hv_SetLight = null;
            HTuple hv_LightParam = new HTuple(), hv_LightPosition = new HTuple();
            HTuple hv_LightKind = new HTuple(), hv_LightIndex = new HTuple();
            HTuple hv_PersistenceParamName = null, hv_PersistenceParamValue = null;
            HTuple hv_ValueListSS3P = null, hv_ValueListSS3IP = null;
            HTuple hv_AlphaOrig = null, hv_UsedParamMask = null, hv_I = null;
            HTuple hv_ParamName = new HTuple(), hv_ParamValue = new HTuple();
            HTuple hv_UseParam = new HTuple(), hv_ParamNameTrunk = new HTuple();
            HTuple hv_Instance = new HTuple(), hv_GenParamNameRemaining = new HTuple();
            HTuple hv_GenParamValueRemaining = new HTuple(), hv_HomMat3D = null;
            HTuple hv_Qx = null, hv_Qy = null, hv_Qz = null, hv_TBCenter = null;
            HTuple hv_TBSize = null, hv_ButtonHold = null, hv_VisualizeTB = new HTuple();
            HTuple hv_MaxIndex = new HTuple(), hv_TrackballCenterRow = new HTuple();
            HTuple hv_TrackballCenterCol = new HTuple(), hv_GraphEvent = new HTuple();
            HTuple hv_Exit = new HTuple(), hv_GraphButtonRow = new HTuple();
            HTuple hv_GraphButtonColumn = new HTuple(), hv_GraphButton = new HTuple();
            HTuple hv_ButtonReleased = new HTuple();
            HTuple hv_CamParam_COPY_INP_TMP = hv_CamParam.Clone();
            HTuple hv_GenParamName_COPY_INP_TMP = hv_GenParamName.Clone();
            HTuple hv_GenParamValue_COPY_INP_TMP = hv_GenParamValue.Clone();
            HTuple hv_Label_COPY_INP_TMP = hv_Label.Clone();
            HTuple hv_PoseIn_COPY_INP_TMP = hv_PoseIn.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageDump);
            hv_PoseOut = new HTuple();
            //The procedure visualize_object_model_3d can be used to display
            //one or more 3d object models and to interactively modify
            //the object poses by using the mouse.
            //
            //The pose can be modified by moving the mouse while
            //pressing a mouse button. The default settings are:
            //
            // Left mouse button:   Modify the object orientation
            // Shift+ left mouse button  or
            // center mouse button: Modify the object distance
            // Right mouse button:  Modify the object position
            // Ctrl + Left mouse button: (De-)select object(s)
            // Alt + Mouse button: Low mouse sensitiviy
            // (Default may be changed with the variable MouseMapping below)
            //
            //In GenParamName and GenParamValue all generic Paramters
            //of disp_object_model_3d are supported.
            //
            //**********************************************************
            //Define global variables
            //**********************************************************
            //
            //global def tuple gDispObjOffset
            //global def tuple gLabelsDecor
            //global def tuple gInfoDecor
            //global def tuple gInfoPos
            //global def tuple gTitlePos
            //global def tuple gTitleDecor
            //global def tuple gTerminationButtonLabel
            //global def tuple gAlphaDeselected
            //global def tuple gIsSinglePose
            //global def tuple gUsesOpenGL
            //
            //**********************************************************
            //First some user defines that may be adapted if desired
            //**********************************************************
            //
            //TrackballSize defines the diameter of the trackball in
            //the image with respect to the smaller image dimension.
            hv_TrackballSize = 0.8;
            //
            //VirtualTrackball defines the type of virtual trackball that
            //shall be used ('shoemake' or 'bell').
            hv_VirtualTrackball = "shoemake";
            //VirtualTrackball := 'bell'
            //
            //Functionality of mouse buttons
            //    1: Left Button
            //    2: Middle Button
            //    4: Right Button
            //    5: Left+Right Mousebutton
            //  8+x: Shift + Mousebutton
            // 16+x: Ctrl + Mousebutton
            // 48+x: Ctrl + Alt + Mousebutton
            //in the order [Translate, Rotate, Scale, ScaleAlternative1, ScaleAlternative2, SelectObjects, ToggleSelectionMode]
            hv_MouseMapping = new HTuple();
            hv_MouseMapping[0] = 17;
            hv_MouseMapping[1] = 1;
            hv_MouseMapping[2] = 2;
            hv_MouseMapping[3] = 5;
            hv_MouseMapping[4] = 9;
            hv_MouseMapping[5] = 4;
            hv_MouseMapping[6] = 49;
            //
            //The labels of the objects appear next to their projected
            //center. With gDispObjOffset a fixed offset is added
            //                  R,  C
            ExpTmpLocalVar_gDispObjOffset = new HTuple();
            ExpTmpLocalVar_gDispObjOffset[0] = -30;
            ExpTmpLocalVar_gDispObjOffset[1] = 0;
            ExpSetGlobalVar_gDispObjOffset(ExpTmpLocalVar_gDispObjOffset);
            //
            //Customize the decoration of the different text elements
            //              Color,   Box
            ExpTmpLocalVar_gInfoDecor = new HTuple();
            ExpTmpLocalVar_gInfoDecor[0] = "white";
            ExpTmpLocalVar_gInfoDecor[1] = "false";
            ExpSetGlobalVar_gInfoDecor(ExpTmpLocalVar_gInfoDecor);
            ExpTmpLocalVar_gLabelsDecor = new HTuple();
            ExpTmpLocalVar_gLabelsDecor[0] = "white";
            ExpTmpLocalVar_gLabelsDecor[1] = "false";
            ExpSetGlobalVar_gLabelsDecor(ExpTmpLocalVar_gLabelsDecor);
            ExpTmpLocalVar_gTitleDecor = new HTuple();
            ExpTmpLocalVar_gTitleDecor[0] = "black";
            ExpTmpLocalVar_gTitleDecor[1] = "true";
            ExpSetGlobalVar_gTitleDecor(ExpTmpLocalVar_gTitleDecor);
            //
            //Customize the position of some text elements
            //  gInfoPos has one of the values
            //  {'UpperLeft', 'LowerLeft', 'UpperRight'}
            ExpTmpLocalVar_gInfoPos = "LowerLeft";
            ExpSetGlobalVar_gInfoPos(ExpTmpLocalVar_gInfoPos);
            //  gTitlePos has one of the values
            //  {'UpperLeft', 'UpperCenter', 'UpperRight'}
            ExpTmpLocalVar_gTitlePos = "UpperLeft";
            ExpSetGlobalVar_gTitlePos(ExpTmpLocalVar_gTitlePos);
            //Alpha value (=1-transparency) that is used for visualizing
            //the objects that are not selected
            ExpTmpLocalVar_gAlphaDeselected = 0.3;
            ExpSetGlobalVar_gAlphaDeselected(ExpTmpLocalVar_gAlphaDeselected);
            //Customize the label of the continue button
            ExpTmpLocalVar_gTerminationButtonLabel = " Continue ";
            ExpSetGlobalVar_gTerminationButtonLabel(ExpTmpLocalVar_gTerminationButtonLabel);
            //Define if the continue button responds to a single click event or
            //if it responds only if the mouse button is released while being placed
            //over the continue button.
            //'true':  Wait until the continue button has been released.
            //         This should be used to avoid unwanted continuations of
            //         subsequent calls of visualize_object_model_3d, which can
            //         otherwise occur if the mouse button remains pressed while the
            //         next visualization is active.
            //'false': Continue the execution already if the continue button is
            //         pressed. This option allows a fast forwarding through
            //         subsequent calls of visualize_object_model_3d.
            hv_WaitForButtonRelease = "true";
            //Number of 3D Object models that can be handled individually
            //if there are more models passed then this number, some calculations
            //are performed differently. And the individual handling of models is not
            //supported anymore
            hv_MaxNumModels = 5;
            //Defines the default for the initial state of the rotation center:
            //(1) The rotation center is fixed in the center of the image and lies
            //    on the surface of the object.
            //(2) The rotation center lies in the center of the object.
            hv_WindowCenteredRotation = 2;
            //
            //**********************************************************
            //
            //Initialize some values
            hv_NumModels = new HTuple(hv_ObjectModel3D.TupleLength());
            hv_SelectedObject = HTuple.TupleGenConst(hv_NumModels, 1);
            //
            //Apply some system settings
            // dev_set_preferences(...); only in hdevelop
            // dev_get_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            // dev_get_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            HOperatorSet.GetSystem("clip_region", out hv_ClipRegion);
            HOperatorSet.SetSystem("clip_region", "false");
            dev_update_off();
            //
            //Refactor camera parameters to fit to window size
            //
            hv_CPLength = new HTuple(hv_CamParam_COPY_INP_TMP.TupleLength());
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_RowNotUsed, out hv_ColumnNotUsed,
                out hv_Width, out hv_Height);
            HOperatorSet.GetPart(hv_ExpDefaultWinHandle, out hv_WPRow1, out hv_WPColumn1,
                out hv_WPRow2, out hv_WPColumn2);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_Height - 1, hv_Width - 1);
            if ((int)(new HTuple(hv_CPLength.TupleEqual(0))) != 0)
            {
                hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[0] = 0.06;
                hv_CamParam_COPY_INP_TMP[1] = 0;
                hv_CamParam_COPY_INP_TMP[2] = 8.5e-6;
                hv_CamParam_COPY_INP_TMP[3] = 8.5e-6;
                hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Width / 2);
                hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Height / 2);
                hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Width);
                hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Height);
                hv_CPLength = new HTuple(hv_CamParam_COPY_INP_TMP.TupleLength());
            }
            else
            {
                hv_CamWidth = ((hv_CamParam_COPY_INP_TMP.TupleSelect(hv_CPLength - 2))).TupleReal()
                    ;
                hv_CamHeight = ((hv_CamParam_COPY_INP_TMP.TupleSelect(hv_CPLength - 1))).TupleReal()
                    ;
                hv_Scale = ((((hv_Width / hv_CamWidth)).TupleConcat(hv_Height / hv_CamHeight))).TupleMin()
                    ;
                if (hv_CamParam_COPY_INP_TMP == null)
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[hv_CPLength - 6] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                    hv_CPLength - 6)) / hv_Scale;
                if (hv_CamParam_COPY_INP_TMP == null)
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[hv_CPLength - 5] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                    hv_CPLength - 5)) / hv_Scale;
                if (hv_CamParam_COPY_INP_TMP == null)
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[hv_CPLength - 4] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                    hv_CPLength - 4)) * hv_Scale;
                if (hv_CamParam_COPY_INP_TMP == null)
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[hv_CPLength - 3] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                    hv_CPLength - 3)) * hv_Scale;
                if (hv_CamParam_COPY_INP_TMP == null)
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[hv_CPLength - 2] = (((hv_CamParam_COPY_INP_TMP.TupleSelect(
                    hv_CPLength - 2)) * hv_Scale)).TupleInt();
                if (hv_CamParam_COPY_INP_TMP == null)
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                hv_CamParam_COPY_INP_TMP[hv_CPLength - 1] = (((hv_CamParam_COPY_INP_TMP.TupleSelect(
                    hv_CPLength - 1)) * hv_Scale)).TupleInt();
            }
            //
            //Check the generic parameters for window_centered_rotation
            //(Note that the default is set above to WindowCenteredRotation := 2)
            hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("inspection_mode");
            if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                new HTuple())))) != 0)
            {
                if ((int)(new HTuple(((hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                    0)))).TupleEqual("surface"))) != 0)
                {
                    hv_WindowCenteredRotation = 1;
                }
                else if ((int)(new HTuple(((hv_GenParamValue_COPY_INP_TMP.TupleSelect(
                    hv_Indices.TupleSelect(0)))).TupleEqual("standard"))) != 0)
                {
                    hv_WindowCenteredRotation = 2;
                }
                else
                {
                    //Wrong parameter value, use default value
                }
                hv_GenParamName_COPY_INP_TMP = hv_GenParamName_COPY_INP_TMP.TupleRemove(hv_Indices);
                hv_GenParamValue_COPY_INP_TMP = hv_GenParamValue_COPY_INP_TMP.TupleRemove(hv_Indices);
            }
            //
            //Check the generic parameters for disp_background
            //(The former parameter name 'use_background' is still supported
            // for compatibility reasons)
            hv_DispBackground = "false";
            if ((int)(new HTuple((new HTuple(hv_GenParamName_COPY_INP_TMP.TupleLength())).TupleGreater(
                0))) != 0)
            {
                hv_Mask = ((hv_GenParamName_COPY_INP_TMP.TupleEqualElem("disp_background"))).TupleOr(
                    hv_GenParamName_COPY_INP_TMP.TupleEqualElem("use_background"));
                hv_Indices = hv_Mask.TupleFind(1);
            }
            else
            {
                hv_Indices = -1;
            }
            if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                new HTuple())))) != 0)
            {
                hv_DispBackground = hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                    0));
                if ((int)((new HTuple(hv_DispBackground.TupleNotEqual("true"))).TupleAnd(new HTuple(hv_DispBackground.TupleNotEqual(
                    "false")))) != 0)
                {
                    //Wrong parameter value: Only 'true' and 'false' are allowed
                    throw new HalconException("Wrong value for parameter 'disp_background' (must be either 'true' or 'false')");
                }
                //Note the the background is handled explicitely in this procedure
                //and therefore, the parameter is removed from the list of
                //parameters and disp_background is always set to true (see below)
                hv_GenParamName_COPY_INP_TMP = hv_GenParamName_COPY_INP_TMP.TupleRemove(hv_Indices);
                hv_GenParamValue_COPY_INP_TMP = hv_GenParamValue_COPY_INP_TMP.TupleRemove(hv_Indices);
            }
            //
            //Read and check the parameter Label for each object
            if ((int)(new HTuple((new HTuple(hv_Label_COPY_INP_TMP.TupleLength())).TupleEqual(
                0))) != 0)
            {
                hv_Label_COPY_INP_TMP = 0;
            }
            else if ((int)(new HTuple((new HTuple(hv_Label_COPY_INP_TMP.TupleLength()
                )).TupleEqual(1))) != 0)
            {
                hv_Label_COPY_INP_TMP = HTuple.TupleGenConst(hv_NumModels, hv_Label_COPY_INP_TMP);
            }
            else
            {
                if ((int)(new HTuple((new HTuple(hv_Label_COPY_INP_TMP.TupleLength())).TupleNotEqual(
                    hv_NumModels))) != 0)
                {
                    //Error: Number of elements in Label does not match the
                    //number of object models
                    HDevelopStop();
                }
            }
            //
            //Read and check the parameter PoseIn for each object
            get_object_models_center(hv_ObjectModel3D, out hv_Center);
            if ((int)(new HTuple((new HTuple(hv_PoseIn_COPY_INP_TMP.TupleLength())).TupleEqual(
                0))) != 0)
            {
                //If no pose was specified by the caller, automatically calculate
                //a pose that is appropriate for the visualization.
                //Set the initial model reference pose. The orientation is parallel
                //to the object coordinate system, the position is at the center
                //of gravity of all models.
                HOperatorSet.CreatePose(-(hv_Center.TupleSelect(0)), -(hv_Center.TupleSelect(
                    1)), -(hv_Center.TupleSelect(2)), 0, 0, 0, "Rp+T", "gba", "point", out hv_PoseIn_COPY_INP_TMP);
                determine_optimum_pose_distance(hv_ObjectModel3D, hv_CamParam_COPY_INP_TMP,
                    0.9, hv_PoseIn_COPY_INP_TMP, out hv_PoseEstimated);
                hv_Poses = new HTuple();
                hv_HomMat3Ds = new HTuple();
                hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                hv_Poses = hv_PoseEstimated.TupleSelect(hv_Sequence % 7);
                ExpTmpLocalVar_gIsSinglePose = 1;
                ExpSetGlobalVar_gIsSinglePose(ExpTmpLocalVar_gIsSinglePose);
            }
            else if ((int)(new HTuple((new HTuple(hv_PoseIn_COPY_INP_TMP.TupleLength()
                )).TupleEqual(7))) != 0)
            {
                hv_Poses = new HTuple();
                hv_HomMat3Ds = new HTuple();
                hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                hv_Poses = hv_PoseIn_COPY_INP_TMP.TupleSelect(hv_Sequence % 7);
                ExpTmpLocalVar_gIsSinglePose = 1;
                ExpSetGlobalVar_gIsSinglePose(ExpTmpLocalVar_gIsSinglePose);
            }
            else
            {
                if ((int)(new HTuple((new HTuple(hv_PoseIn_COPY_INP_TMP.TupleLength())).TupleNotEqual(
                    (new HTuple(hv_ObjectModel3D.TupleLength())) * 7))) != 0)
                {
                    //Error: Wrong number of values of input control parameter 'PoseIn'
                    HDevelopStop();
                }
                else
                {
                    hv_Poses = hv_PoseIn_COPY_INP_TMP.Clone();
                }
                ExpTmpLocalVar_gIsSinglePose = 0;
                ExpSetGlobalVar_gIsSinglePose(ExpTmpLocalVar_gIsSinglePose);
            }
            //
            //Open (invisible) buffer window to avoid flickering
            //open_window(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_Height - 1, hv_Width - 1);
            HOperatorSet.GetFont(hv_ExpDefaultWinHandle, out hv_Font);
            try
            {
                HOperatorSet.SetFont(hv_ExpDefaultWinHandle, hv_Font);
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
            }
            //
            // Is OpenGL available and should it be used?
            ExpTmpLocalVar_gUsesOpenGL = "true";
            ExpSetGlobalVar_gUsesOpenGL(ExpTmpLocalVar_gUsesOpenGL);
            hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("opengl");
            if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                new HTuple())))) != 0)
            {
                ExpTmpLocalVar_gUsesOpenGL = hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                    0));
                ExpSetGlobalVar_gUsesOpenGL(ExpTmpLocalVar_gUsesOpenGL);
                hv_GenParamName_COPY_INP_TMP = hv_GenParamName_COPY_INP_TMP.TupleRemove(hv_Indices);
                hv_GenParamValue_COPY_INP_TMP = hv_GenParamValue_COPY_INP_TMP.TupleRemove(hv_Indices);
                if ((int)((new HTuple(ExpGetGlobalVar_gUsesOpenGL().TupleNotEqual("true"))).TupleAnd(
                    new HTuple(ExpGetGlobalVar_gUsesOpenGL().TupleNotEqual("false")))) != 0)
                {
                    //Wrong parameter value: Only 'true' and 'false' are allowed
                    throw new HalconException("Wrong value for parameter 'opengl' (must be either 'true' or 'false')");
                }
            }
            if ((int)(new HTuple(ExpGetGlobalVar_gUsesOpenGL().TupleEqual("true"))) != 0)
            {
                HOperatorSet.GetSystem("opengl_info", out hv_OpenGLInfo);
                if ((int)(new HTuple(hv_OpenGLInfo.TupleEqual("No OpenGL support included."))) != 0)
                {
                    ExpTmpLocalVar_gUsesOpenGL = "false";
                    ExpSetGlobalVar_gUsesOpenGL(ExpTmpLocalVar_gUsesOpenGL);
                }
                else
                {
                    HOperatorSet.GenObjectModel3dFromPoints(0, 0, 0, out hv_DummyObjectModel3D);
                    HOperatorSet.CreateScene3d(out hv_Scene3DTest);
                    HOperatorSet.AddScene3dCamera(hv_Scene3DTest, hv_CamParam_COPY_INP_TMP, out hv_CameraIndexTest);
                    determine_optimum_pose_distance(hv_DummyObjectModel3D, hv_CamParam_COPY_INP_TMP,
                        0.9, ((((((new HTuple(0)).TupleConcat(0)).TupleConcat(0)).TupleConcat(
                        0)).TupleConcat(0)).TupleConcat(0)).TupleConcat(0), out hv_PoseTest);
                    HOperatorSet.AddScene3dInstance(hv_Scene3DTest, hv_DummyObjectModel3D, hv_PoseTest,
                        out hv_InstanceIndexTest);
                    try
                    {
                        HOperatorSet.DisplayScene3d(hv_ExpDefaultWinHandle, hv_Scene3DTest, hv_InstanceIndexTest);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        ExpTmpLocalVar_gUsesOpenGL = "false";
                        ExpSetGlobalVar_gUsesOpenGL(ExpTmpLocalVar_gUsesOpenGL);
                    }
                    HOperatorSet.ClearScene3d(hv_Scene3DTest);
                    HOperatorSet.ClearObjectModel3d(hv_DummyObjectModel3D);
                }
            }
            //
            //Compute the trackball
            hv_MinImageSize = ((hv_Width.TupleConcat(hv_Height))).TupleMin();
            hv_TrackballRadiusPixel = (hv_TrackballSize * hv_MinImageSize) / 2.0;
            //
            //Measure the text extents for the continue button in the
            //graphics window
            HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, ExpGetGlobalVar_gTerminationButtonLabel() + "  ",
                out hv_Ascent, out hv_Descent, out hv_TextWidth, out hv_TextHeight);
            //
            //Store background image
            if ((int)(new HTuple(hv_DispBackground.TupleEqual("false"))) != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            ho_Image.Dispose();
            HOperatorSet.DumpWindowImage(out ho_Image, hv_ExpDefaultWinHandle);
            //Special treatment for color background images necessary
            HOperatorSet.CountChannels(ho_Image, out hv_NumChannels);
            hv_ColorImage = new HTuple(hv_NumChannels.TupleEqual(3));
            //
            HOperatorSet.CreateScene3d(out hv_Scene3D);
            HOperatorSet.AddScene3dCamera(hv_Scene3D, hv_CamParam_COPY_INP_TMP, out hv_CameraIndex);
            HOperatorSet.AddScene3dInstance(hv_Scene3D, hv_ObjectModel3D, hv_Poses, out hv_AllInstances);
            //Always set 'disp_background' to true,  because it is handled explicitely
            //in this procedure (see above)
            HOperatorSet.SetScene3dParam(hv_Scene3D, "disp_background", "true");
            //Check if we have to set light specific parameters
            hv_SetLight = new HTuple(hv_GenParamName_COPY_INP_TMP.TupleRegexpTest("light_"));
            if ((int)(hv_SetLight) != 0)
            {
                //set position of light source
                hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("light_position");
                if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                    new HTuple())))) != 0)
                {
                    //If multiple light positions are given, use the last one
                    hv_LightParam = ((((hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                        (new HTuple(hv_Indices.TupleLength())) - 1)))).TupleSplit(", "))).TupleNumber()
                        ;
                    if ((int)(new HTuple((new HTuple(hv_LightParam.TupleLength())).TupleNotEqual(
                        4))) != 0)
                    {
                        throw new HalconException("light_position must be given as a string that contains four space separated floating point numbers");
                    }
                    hv_LightPosition = hv_LightParam.TupleSelectRange(0, 2);
                    hv_LightKind = "point_light";
                    if ((int)(new HTuple(((hv_LightParam.TupleSelect(3))).TupleEqual(0))) != 0)
                    {
                        hv_LightKind = "directional_light";
                    }
                    //Currently, only one light source is supported
                    HOperatorSet.RemoveScene3dLight(hv_Scene3D, 0);
                    HOperatorSet.AddScene3dLight(hv_Scene3D, hv_LightPosition, hv_LightKind,
                        out hv_LightIndex);
                    HOperatorSet.TupleRemove(hv_GenParamName_COPY_INP_TMP, hv_Indices, out hv_GenParamName_COPY_INP_TMP);
                    HOperatorSet.TupleRemove(hv_GenParamValue_COPY_INP_TMP, hv_Indices, out hv_GenParamValue_COPY_INP_TMP);
                }
                //set ambient part of light source
                hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("light_ambient");
                if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                    new HTuple())))) != 0)
                {
                    //If the ambient part is set multiple times, use the last setting
                    hv_LightParam = ((((hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                        (new HTuple(hv_Indices.TupleLength())) - 1)))).TupleSplit(", "))).TupleNumber()
                        ;
                    if ((int)(new HTuple((new HTuple(hv_LightParam.TupleLength())).TupleLess(
                        3))) != 0)
                    {
                        throw new HalconException("light_ambient must be given as a string that contains three space separated floating point numbers");
                    }
                    HOperatorSet.SetScene3dLightParam(hv_Scene3D, 0, "ambient", hv_LightParam.TupleSelectRange(
                        0, 2));
                    HOperatorSet.TupleRemove(hv_GenParamName_COPY_INP_TMP, hv_Indices, out hv_GenParamName_COPY_INP_TMP);
                    HOperatorSet.TupleRemove(hv_GenParamValue_COPY_INP_TMP, hv_Indices, out hv_GenParamValue_COPY_INP_TMP);
                }
                //set diffuse part of light source
                hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("light_diffuse");
                if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                    new HTuple())))) != 0)
                {
                    //If the diffuse part is set multiple times, use the last setting
                    hv_LightParam = ((((hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                        (new HTuple(hv_Indices.TupleLength())) - 1)))).TupleSplit(", "))).TupleNumber()
                        ;
                    if ((int)(new HTuple((new HTuple(hv_LightParam.TupleLength())).TupleLess(
                        3))) != 0)
                    {
                        throw new HalconException("light_diffuse must be given as a string that contains three space separated floating point numbers");
                    }
                    HOperatorSet.SetScene3dLightParam(hv_Scene3D, 0, "diffuse", hv_LightParam.TupleSelectRange(
                        0, 2));
                    HOperatorSet.TupleRemove(hv_GenParamName_COPY_INP_TMP, hv_Indices, out hv_GenParamName_COPY_INP_TMP);
                    HOperatorSet.TupleRemove(hv_GenParamValue_COPY_INP_TMP, hv_Indices, out hv_GenParamValue_COPY_INP_TMP);
                }
            }
            //
            //Handle persistence parameters separately because persistence will
            //only be activated immediately before leaving the visualization
            //procedure
            hv_PersistenceParamName = new HTuple();
            hv_PersistenceParamValue = new HTuple();
            //set position of light source
            hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("object_index_persistence");
            if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                new HTuple())))) != 0)
            {
                if ((int)(new HTuple(((hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                    (new HTuple(hv_Indices.TupleLength())) - 1)))).TupleEqual("true"))) != 0)
                {
                    hv_PersistenceParamName = hv_PersistenceParamName.TupleConcat("object_index_persistence");
                    hv_PersistenceParamValue = hv_PersistenceParamValue.TupleConcat("true");
                }
                else if ((int)(new HTuple(((hv_GenParamValue_COPY_INP_TMP.TupleSelect(
                    hv_Indices.TupleSelect((new HTuple(hv_Indices.TupleLength())) - 1)))).TupleEqual(
                    "false"))) != 0)
                {
                }
                else
                {
                    throw new HalconException("Wrong value for parameter 'object_index_persistence' (must be either 'true' or 'false')");
                }
                HOperatorSet.TupleRemove(hv_GenParamName_COPY_INP_TMP, hv_Indices, out hv_GenParamName_COPY_INP_TMP);
                HOperatorSet.TupleRemove(hv_GenParamValue_COPY_INP_TMP, hv_Indices, out hv_GenParamValue_COPY_INP_TMP);
            }
            hv_Indices = hv_GenParamName_COPY_INP_TMP.TupleFind("depth_persistence");
            if ((int)((new HTuple(hv_Indices.TupleNotEqual(-1))).TupleAnd(new HTuple(hv_Indices.TupleNotEqual(
                new HTuple())))) != 0)
            {
                if ((int)(new HTuple(((hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_Indices.TupleSelect(
                    (new HTuple(hv_Indices.TupleLength())) - 1)))).TupleEqual("true"))) != 0)
                {
                    hv_PersistenceParamName = hv_PersistenceParamName.TupleConcat("depth_persistence");
                    hv_PersistenceParamValue = hv_PersistenceParamValue.TupleConcat("true");
                }
                else if ((int)(new HTuple(((hv_GenParamValue_COPY_INP_TMP.TupleSelect(
                    hv_Indices.TupleSelect((new HTuple(hv_Indices.TupleLength())) - 1)))).TupleEqual(
                    "false"))) != 0)
                {
                }
                else
                {
                    throw new HalconException("Wrong value for parameter 'depth_persistence' (must be either 'true' or 'false')");
                }
                HOperatorSet.TupleRemove(hv_GenParamName_COPY_INP_TMP, hv_Indices, out hv_GenParamName_COPY_INP_TMP);
                HOperatorSet.TupleRemove(hv_GenParamValue_COPY_INP_TMP, hv_Indices, out hv_GenParamValue_COPY_INP_TMP);
            }
            //
            //Parse the generic parameters
            //- First, all parameters that are understood by set_scene_3d_instance_param
            HOperatorSet.GetParamInfo("set_scene_3d_param", "GenParamName", "value_list",
                out hv_ValueListSS3P);
            HOperatorSet.GetParamInfo("set_scene_3d_instance_param", "GenParamName", "value_list",
                out hv_ValueListSS3IP);
            hv_AlphaOrig = HTuple.TupleGenConst(hv_NumModels, 1);
            hv_UsedParamMask = HTuple.TupleGenConst(new HTuple(hv_GenParamName_COPY_INP_TMP.TupleLength()
                ), 0);
            for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_GenParamName_COPY_INP_TMP.TupleLength()
                )) - 1); hv_I = (int)hv_I + 1)
            {
                hv_ParamName = hv_GenParamName_COPY_INP_TMP.TupleSelect(hv_I);
                hv_ParamValue = hv_GenParamValue_COPY_INP_TMP.TupleSelect(hv_I);
                //Check if this parameter is understood by set_scene_3d_param
                hv_UseParam = new HTuple(hv_ValueListSS3P.TupleRegexpTest(("^" + hv_ParamName) + "$"));
                if ((int)(hv_UseParam) != 0)
                {
                    try
                    {
                        HOperatorSet.SetScene3dParam(hv_Scene3D, hv_ParamName, hv_ParamValue);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)((new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(1204))).TupleOr(
                            new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(1304)))) != 0)
                        {
                            throw new HalconException((("Wrong type or value for parameter " + hv_ParamName) + ": ") + hv_ParamValue);
                        }
                        else
                        {
                            throw new HalconException(hv_Exception);
                        }
                    }
                    if (hv_UsedParamMask == null)
                        hv_UsedParamMask = new HTuple();
                    hv_UsedParamMask[hv_I] = 1;
                    if ((int)(new HTuple(hv_ParamName.TupleEqual("alpha"))) != 0)
                    {
                        hv_AlphaOrig = HTuple.TupleGenConst(hv_NumModels, hv_ParamValue);
                    }
                    continue;
                }
                //Check if it is a parameter that is valid for only one instance
                //and therefore can be set only with set_scene_3d_instance_param
                hv_ParamNameTrunk = hv_ParamName.TupleRegexpReplace("_\\d+$", "");
                hv_UseParam = new HTuple(hv_ValueListSS3IP.TupleRegexpTest(("^" + hv_ParamNameTrunk) + "$"));
                if ((int)(hv_UseParam) != 0)
                {
                    hv_Instance = ((hv_ParamName.TupleRegexpReplace(("^" + hv_ParamNameTrunk) + "_(\\d+)$",
                        "$1"))).TupleNumber();
                    if ((int)((new HTuple(hv_Instance.TupleLess(0))).TupleOr(new HTuple(hv_Instance.TupleGreater(
                        hv_NumModels - 1)))) != 0)
                    {
                        throw new HalconException(("Parameter " + hv_ParamName) + " refers to a non existing 3D object model");
                    }
                    try
                    {
                        HOperatorSet.SetScene3dInstanceParam(hv_Scene3D, hv_Instance, hv_ParamNameTrunk,
                            hv_ParamValue);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)((new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(1204))).TupleOr(
                            new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(1304)))) != 0)
                        {
                            throw new HalconException((("Wrong type or value for parameter " + hv_ParamName) + ": ") + hv_ParamValue);
                        }
                        else
                        {
                            throw new HalconException(hv_Exception);
                        }
                    }
                    if (hv_UsedParamMask == null)
                        hv_UsedParamMask = new HTuple();
                    hv_UsedParamMask[hv_I] = 1;
                    if ((int)(new HTuple(hv_ParamNameTrunk.TupleEqual("alpha"))) != 0)
                    {
                        if (hv_AlphaOrig == null)
                            hv_AlphaOrig = new HTuple();
                        hv_AlphaOrig[hv_Instance] = hv_ParamValue;
                    }
                    continue;
                }
            }
            //
            //Check if there are remaining parameters
            if ((int)(new HTuple((new HTuple(hv_GenParamName_COPY_INP_TMP.TupleLength())).TupleGreater(
                0))) != 0)
            {
                hv_GenParamNameRemaining = hv_GenParamName_COPY_INP_TMP.TupleSelectMask(hv_UsedParamMask.TupleNot()
                    );
                hv_GenParamValueRemaining = hv_GenParamValue_COPY_INP_TMP.TupleSelectMask(hv_UsedParamMask.TupleNot()
                    );
                if ((int)(new HTuple(hv_GenParamNameRemaining.TupleNotEqual(new HTuple()))) != 0)
                {
                    throw new HalconException("Parameters that cannot be handled: " + (((((hv_GenParamNameRemaining + " := ") + hv_GenParamValueRemaining) + ", ")).TupleSum()
                        ));
                }
            }
            //
            //Start the visualization loop
            HOperatorSet.PoseToHomMat3d(hv_Poses.TupleSelectRange(0, 6), out hv_HomMat3D);
            HOperatorSet.AffineTransPoint3d(hv_HomMat3D, hv_Center.TupleSelect(0), hv_Center.TupleSelect(
                1), hv_Center.TupleSelect(2), out hv_Qx, out hv_Qy, out hv_Qz);
            hv_TBCenter = new HTuple();
            hv_TBCenter = hv_TBCenter.TupleConcat(hv_Qx);
            hv_TBCenter = hv_TBCenter.TupleConcat(hv_Qy);
            hv_TBCenter = hv_TBCenter.TupleConcat(hv_Qz);
            hv_TBSize = (0.5 + ((0.5 * (hv_SelectedObject.TupleSum())) / hv_NumModels)) * hv_TrackballRadiusPixel;
            hv_ButtonHold = 0;
            while ((int)(1) != 0)
            {
                hv_VisualizeTB = new HTuple(((hv_SelectedObject.TupleMax())).TupleNotEqual(
                    0));
                hv_MaxIndex = ((((new HTuple(hv_ObjectModel3D.TupleLength())).TupleConcat(hv_MaxNumModels))).TupleMin()
                    ) - 1;
                //Set trackball fixed in the center of the window
                hv_TrackballCenterRow = hv_Height / 2;
                hv_TrackballCenterCol = hv_Width / 2;
                if ((int)(new HTuple(hv_WindowCenteredRotation.TupleEqual(1))) != 0)
                {
                    try
                    {
                        get_trackball_center_fixed(hv_SelectedObject.TupleSelectRange(0, hv_MaxIndex),
                            hv_TrackballCenterRow, hv_TrackballCenterCol, hv_TrackballRadiusPixel,
                            hv_Scene3D, hv_ObjectModel3D.TupleSelectRange(0, hv_MaxIndex), hv_Poses.TupleSelectRange(
                            0, ((hv_MaxIndex + 1) * 7) - 1), hv_WindowHandleBuffer, hv_CamParam_COPY_INP_TMP,
                            hv_GenParamName_COPY_INP_TMP, hv_GenParamValue_COPY_INP_TMP, out hv_TBCenter,
                            out hv_TBSize);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        disp_message(hv_ExpDefaultWinHandle, "Surface inspection mode is not available.",
                            "image", 5, 20, "red", "true");
                        hv_WindowCenteredRotation = 2;
                        get_trackball_center(hv_SelectedObject.TupleSelectRange(0, hv_MaxIndex),
                            hv_TrackballRadiusPixel, hv_ObjectModel3D.TupleSelectRange(0, hv_MaxIndex),
                            hv_Poses.TupleSelectRange(0, ((hv_MaxIndex + 1) * 7) - 1), out hv_TBCenter,
                            out hv_TBSize);
                        HOperatorSet.WaitSeconds(1);
                    }
                }
                else
                {
                    get_trackball_center(hv_SelectedObject.TupleSelectRange(0, hv_MaxIndex), hv_TrackballRadiusPixel,
                        hv_ObjectModel3D.TupleSelectRange(0, hv_MaxIndex), hv_Poses.TupleSelectRange(
                        0, ((hv_MaxIndex + 1) * 7) - 1), out hv_TBCenter, out hv_TBSize);
                }
                dump_image_output(ho_Image, hv_ExpDefaultWinHandle, hv_Scene3D, hv_AlphaOrig,
                    hv_ObjectModel3D, hv_GenParamName_COPY_INP_TMP, hv_GenParamValue_COPY_INP_TMP,
                    hv_CamParam_COPY_INP_TMP, hv_Poses, hv_ColorImage, hv_Title, hv_Information,
                    hv_Label_COPY_INP_TMP, hv_VisualizeTB, "true", hv_TrackballCenterRow, hv_TrackballCenterCol,
                    hv_TBSize, hv_SelectedObject, hv_WindowCenteredRotation, hv_TBCenter);
                ho_ImageDump.Dispose();
                HOperatorSet.DumpWindowImage(out ho_ImageDump, hv_ExpDefaultWinHandle);
                //dev_set_window(...);
                HOperatorSet.DispObj(ho_ImageDump, hv_ExpDefaultWinHandle);
                //
                //Check for mouse events
                hv_GraphEvent = 0;
                hv_Exit = 0;
                while ((int)(1) != 0)
                {
                    //
                    //Check graphic event
                    try
                    {
                        HOperatorSet.GetMpositionSubPix(hv_ExpDefaultWinHandle, out hv_GraphButtonRow,
                            out hv_GraphButtonColumn, out hv_GraphButton);
                        if ((int)(new HTuple(hv_GraphButton.TupleNotEqual(0))) != 0)
                        {
                            if ((int)((new HTuple((new HTuple((new HTuple(hv_GraphButtonRow.TupleGreater(
                                (hv_Height - hv_TextHeight) - 13))).TupleAnd(new HTuple(hv_GraphButtonRow.TupleLess(
                                hv_Height))))).TupleAnd(new HTuple(hv_GraphButtonColumn.TupleGreater(
                                (hv_Width - hv_TextWidth) - 13))))).TupleAnd(new HTuple(hv_GraphButtonColumn.TupleLess(
                                hv_Width)))) != 0)
                            {
                                //Wait until the continue button has been released
                                if ((int)(new HTuple(hv_WaitForButtonRelease.TupleEqual("true"))) != 0)
                                {
                                    while ((int)(1) != 0)
                                    {
                                        HOperatorSet.GetMpositionSubPix(hv_ExpDefaultWinHandle, out hv_GraphButtonRow,
                                            out hv_GraphButtonColumn, out hv_GraphButton);
                                        if ((int)((new HTuple(hv_GraphButton.TupleEqual(0))).TupleOr(new HTuple(hv_GraphButton.TupleEqual(
                                            new HTuple())))) != 0)
                                        {
                                            if ((int)((new HTuple((new HTuple((new HTuple(hv_GraphButtonRow.TupleGreater(
                                                (hv_Height - hv_TextHeight) - 13))).TupleAnd(new HTuple(hv_GraphButtonRow.TupleLess(
                                                hv_Height))))).TupleAnd(new HTuple(hv_GraphButtonColumn.TupleGreater(
                                                (hv_Width - hv_TextWidth) - 13))))).TupleAnd(new HTuple(hv_GraphButtonColumn.TupleLess(
                                                hv_Width)))) != 0)
                                            {
                                                hv_ButtonReleased = 1;
                                            }
                                            else
                                            {
                                                hv_ButtonReleased = 0;
                                            }
                                            //
                                            break;
                                        }
                                        //Keep waiting until mouse button is released or moved out of the window
                                    }
                                }
                                else
                                {
                                    hv_ButtonReleased = 1;
                                }
                                //Exit the visualization loop
                                if ((int)(hv_ButtonReleased) != 0)
                                {
                                    hv_Exit = 1;
                                    break;
                                }
                            }
                            hv_GraphEvent = 1;
                            break;
                        }
                        else
                        {
                            hv_ButtonHold = 0;
                        }
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        //Keep waiting
                    }
                }
                if ((int)(hv_GraphEvent) != 0)
                {
                    analyze_graph_event(ho_Image, hv_MouseMapping, hv_GraphButton, hv_GraphButtonRow,
                        hv_GraphButtonColumn, hv_WindowHandle, hv_WindowHandleBuffer, hv_VirtualTrackball,
                        hv_TrackballSize, hv_SelectedObject, hv_Scene3D, hv_AlphaOrig, hv_ObjectModel3D,
                        hv_CamParam_COPY_INP_TMP, hv_Label_COPY_INP_TMP, hv_Title, hv_Information,
                        hv_GenParamName_COPY_INP_TMP, hv_GenParamValue_COPY_INP_TMP, hv_Poses,
                        hv_ButtonHold, hv_TBCenter, hv_TBSize, hv_WindowCenteredRotation, hv_MaxNumModels,
                        out hv_Poses, out hv_SelectedObject, out hv_ButtonHold, out hv_WindowCenteredRotation);
                }
                if ((int)(hv_Exit) != 0)
                {
                    break;
                }
            }
            //
            //Display final state with persistence, if requested
            //Note that disp_object_model_3d must be used instead of the 3D scene
            if ((int)(new HTuple((new HTuple(hv_PersistenceParamName.TupleLength())).TupleGreater(
                0))) != 0)
            {
                try
                {
                    HOperatorSet.DispObjectModel3d(hv_ExpDefaultWinHandle, hv_ObjectModel3D,
                        hv_CamParam_COPY_INP_TMP, hv_Poses, ((new HTuple("disp_background")).TupleConcat(
                        "alpha")).TupleConcat(hv_PersistenceParamName), ((new HTuple("true")).TupleConcat(
                        0.0)).TupleConcat(hv_PersistenceParamValue));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    HDevelopStop();
                }
            }
            //
            //Compute the output pose
            if ((int)(ExpGetGlobalVar_gIsSinglePose()) != 0)
            {
                hv_PoseOut = hv_Poses.TupleSelectRange(0, 6);
            }
            else
            {
                hv_PoseOut = hv_Poses.Clone();
            }
            //
            //Clean up
            HOperatorSet.SetSystem("clip_region", hv_ClipRegion);
            // dev_set_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            dump_image_output(ho_Image, hv_ExpDefaultWinHandle, hv_Scene3D, hv_AlphaOrig,
                hv_ObjectModel3D, hv_GenParamName_COPY_INP_TMP, hv_GenParamValue_COPY_INP_TMP,
                hv_CamParam_COPY_INP_TMP, hv_Poses, hv_ColorImage, hv_Title, new HTuple(),
                hv_Label_COPY_INP_TMP, 0, "false", hv_TrackballCenterRow, hv_TrackballCenterCol,
                hv_TBSize, hv_SelectedObject, hv_WindowCenteredRotation, hv_TBCenter);
            ho_ImageDump.Dispose();
            HOperatorSet.DumpWindowImage(out ho_ImageDump, hv_ExpDefaultWinHandle);
            //dev_set_window(...);
            HOperatorSet.DispObj(ho_ImageDump, hv_ExpDefaultWinHandle);
            //close_window(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, hv_WPRow1, hv_WPColumn1, hv_WPRow2,
                hv_WPColumn2);
            HOperatorSet.ClearScene3d(hv_Scene3D);
            ho_Image.Dispose();
            ho_ImageDump.Dispose();

            return;
        }

        // Chapter: Tuple / Arithmetic
        // Short Description: Calculates the cross product of two vectors of length 3. 
        public void tuple_vector_cross_product(HTuple hv_V1, HTuple hv_V2, out HTuple hv_VC)
        {



            // Local iconic variables 
            // Initialize local and output iconic variables 
            //The caller must ensure that the length of both input vectors is 3
            hv_VC = ((hv_V1.TupleSelect(1)) * (hv_V2.TupleSelect(2))) - ((hv_V1.TupleSelect(2)) * (hv_V2.TupleSelect(
                1)));
            hv_VC = hv_VC.TupleConcat(((hv_V1.TupleSelect(2)) * (hv_V2.TupleSelect(0))) - ((hv_V1.TupleSelect(
                0)) * (hv_V2.TupleSelect(2))));
            hv_VC = hv_VC.TupleConcat(((hv_V1.TupleSelect(0)) * (hv_V2.TupleSelect(1))) - ((hv_V1.TupleSelect(
                1)) * (hv_V2.TupleSelect(0))));

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Compute the center of all given 3D object models. 
        public void get_object_models_center(HTuple hv_ObjectModel3DID, out HTuple hv_Center)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Diameter = new HTuple(), hv_MD = new HTuple();
            HTuple hv_Weight = new HTuple(), hv_SumW = new HTuple();
            HTuple hv_Index = new HTuple(), hv_ObjectModel3DIDSelected = new HTuple();
            HTuple hv_C = new HTuple(), hv_InvSum = new HTuple();
            // Initialize local and output iconic variables 
            hv_Center = new HTuple();
            //Compute the mean of all model centers (weighted by the diameter of the object models)
            if ((int)(new HTuple((new HTuple(hv_ObjectModel3DID.TupleLength())).TupleGreater(
                0))) != 0)
            {
                HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DID, "diameter_axis_aligned_bounding_box",
                    out hv_Diameter);
                //Normalize Diameter to use it as weights for a weighted mean of the individual centers
                hv_MD = hv_Diameter.TupleMean();
                if ((int)(new HTuple(hv_MD.TupleGreater(1e-10))) != 0)
                {
                    hv_Weight = hv_Diameter / hv_MD;
                }
                else
                {
                    hv_Weight = hv_Diameter.Clone();
                }
                hv_SumW = hv_Weight.TupleSum();
                if ((int)(new HTuple(hv_SumW.TupleLess(1e-10))) != 0)
                {
                    hv_Weight = HTuple.TupleGenConst(new HTuple(hv_Weight.TupleLength()), 1.0);
                    hv_SumW = hv_Weight.TupleSum();
                }
                hv_Center = new HTuple();
                hv_Center[0] = 0;
                hv_Center[1] = 0;
                hv_Center[2] = 0;
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ObjectModel3DID.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    hv_ObjectModel3DIDSelected = hv_ObjectModel3DID.TupleSelect(hv_Index);
                    HOperatorSet.GetObjectModel3dParams(hv_ObjectModel3DIDSelected, "center",
                        out hv_C);
                    if (hv_Center == null)
                        hv_Center = new HTuple();
                    hv_Center[0] = (hv_Center.TupleSelect(0)) + ((hv_C.TupleSelect(0)) * (hv_Weight.TupleSelect(
                        hv_Index)));
                    if (hv_Center == null)
                        hv_Center = new HTuple();
                    hv_Center[1] = (hv_Center.TupleSelect(1)) + ((hv_C.TupleSelect(1)) * (hv_Weight.TupleSelect(
                        hv_Index)));
                    if (hv_Center == null)
                        hv_Center = new HTuple();
                    hv_Center[2] = (hv_Center.TupleSelect(2)) + ((hv_C.TupleSelect(2)) * (hv_Weight.TupleSelect(
                        hv_Index)));
                }
                hv_InvSum = 1.0 / hv_SumW;
                if (hv_Center == null)
                    hv_Center = new HTuple();
                hv_Center[0] = (hv_Center.TupleSelect(0)) * hv_InvSum;
                if (hv_Center == null)
                    hv_Center = new HTuple();
                hv_Center[1] = (hv_Center.TupleSelect(1)) * hv_InvSum;
                if (hv_Center == null)
                    hv_Center = new HTuple();
                hv_Center[2] = (hv_Center.TupleSelect(2)) * hv_InvSum;
            }
            else
            {
                hv_Center = new HTuple();
            }

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Displays a continue button. 
        public void disp_continue_button(HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ContinueMessage = new HTuple(), hv_Exception = null;
            HTuple hv_Row = null, hv_Column = null, hv_Width = null;
            HTuple hv_Height = null, hv_Ascent = null, hv_Descent = null;
            HTuple hv_TextWidth = null, hv_TextHeight = null;
            // Initialize local and output iconic variables 
            //This procedure displays a 'Continue' text button
            //in the lower right corner of the screen.
            //It uses the procedure disp_message.
            //
            //Input parameters:
            //WindowHandle: The window, where the text shall be displayed
            //
            //Use the continue message set in the global variable gTerminationButtonLabel.
            //If this variable is not defined, set a standard text instead.
            //global tuple gTerminationButtonLabel
            try
            {
                hv_ContinueMessage = ExpGetGlobalVar_gTerminationButtonLabel().Clone();
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                hv_ContinueMessage = "Continue";
            }
            //Display the continue button
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_Row, out hv_Column,
                out hv_Width, out hv_Height);
            HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, (" " + hv_ContinueMessage) + " ",
                out hv_Ascent, out hv_Descent, out hv_TextWidth, out hv_TextHeight);
            disp_text_button(hv_ExpDefaultWinHandle, hv_ContinueMessage, "window", (hv_Height - hv_TextHeight) - 12,
                (hv_Width - hv_TextWidth) - 12, "black", "#f28f26");

            return;
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        public void disp_text_button(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_TextColor, HTuple hv_ButtonColor)
        {



            // Local iconic variables 

            HObject ho_UpperLeft, ho_LowerRight, ho_Rectangle;

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = new HTuple(), hv_HeightWin = null;
            HTuple hv_Exception = null, hv_Fac = null, hv_RGBL = null;
            HTuple hv_RGB = new HTuple(), hv_RGBD = null, hv_ButtonColorBorderL = null;
            HTuple hv_ButtonColorBorderD = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_Width = null;
            HTuple hv_Index = null, hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = null;
            HTuple hv_FrameWidth = null, hv_R2 = null, hv_C2 = null;
            HTuple hv_ClipRegion = null, hv_DrawMode = null, hv_BorderWidth = null;
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
            HTuple hv_TextColor_COPY_INP_TMP = hv_TextColor.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_UpperLeft);
            HOperatorSet.GenEmptyObj(out ho_LowerRight);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //ButtonColor: Must be set to a color string (e.g. 'white', '#FF00CC', etc.).
            //             The text is written in a box of that color.
            //
            //prepare window
            HOperatorSet.GetRgb(hv_ExpDefaultWinHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_ExpDefaultWinHandle, out hv_Row1Part, out hv_Column1Part,
                out hv_Row2Part, out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_TextColor_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_TextColor_COPY_INP_TMP = "";
            }
            //
            try
            {
                color_string_to_rgb(hv_ButtonColor, out hv_RGB);
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                hv_Exception = "Wrong value of control parameter ButtonColor (must be a valid color string)";
                throw new HalconException(hv_Exception);
            }
            hv_Fac = 0.4;
            hv_RGBL = hv_RGB + (((((255.0 - hv_RGB) * hv_Fac) + 0.5)).TupleInt());
            hv_RGBD = hv_RGB - ((((hv_RGB * hv_Fac) + 0.5)).TupleInt());
            hv_ButtonColorBorderL = "#" + ((("" + (hv_RGBL.TupleString("02x")))).TupleSum());
            hv_ButtonColorBorderD = "#" + ((("" + (hv_RGBD.TupleString("02x")))).TupleSum());
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_ExpDefaultWinHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //display text box depending on text size
            //
            //calculate box extents
            hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
            hv_Width = new HTuple();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                HOperatorSet.GetStringExtents(hv_ExpDefaultWinHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                hv_Width = hv_Width.TupleConcat(hv_W);
            }
            hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                ));
            hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
            hv_R2 = hv_R1 + hv_FrameHeight;
            hv_C2 = hv_C1 + hv_FrameWidth;
            //display rectangles
            HOperatorSet.GetSystem("clip_region", out hv_ClipRegion);
            HOperatorSet.SetSystem("clip_region", "false");
            HOperatorSet.GetDraw(hv_ExpDefaultWinHandle, out hv_DrawMode);
            HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, "fill");
            hv_BorderWidth = 2;
            ho_UpperLeft.Dispose();
            HOperatorSet.GenRegionPolygonFilled(out ho_UpperLeft, ((((((((hv_R1 - hv_BorderWidth)).TupleConcat(
                hv_R1 - hv_BorderWidth))).TupleConcat(hv_R1))).TupleConcat(hv_R2))).TupleConcat(
                hv_R2 + hv_BorderWidth), ((((((((hv_C1 - hv_BorderWidth)).TupleConcat(hv_C2 + hv_BorderWidth))).TupleConcat(
                hv_C2))).TupleConcat(hv_C1))).TupleConcat(hv_C1 - hv_BorderWidth));
            ho_LowerRight.Dispose();
            HOperatorSet.GenRegionPolygonFilled(out ho_LowerRight, ((((((((hv_R2 + hv_BorderWidth)).TupleConcat(
                hv_R1 - hv_BorderWidth))).TupleConcat(hv_R1))).TupleConcat(hv_R2))).TupleConcat(
                hv_R2 + hv_BorderWidth), ((((((((hv_C2 + hv_BorderWidth)).TupleConcat(hv_C2 + hv_BorderWidth))).TupleConcat(
                hv_C2))).TupleConcat(hv_C1))).TupleConcat(hv_C1 - hv_BorderWidth));
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle, hv_R1, hv_C1, hv_R2, hv_C2);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_ButtonColorBorderL);
            HOperatorSet.DispObj(ho_UpperLeft, hv_ExpDefaultWinHandle);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_ButtonColorBorderD);
            HOperatorSet.DispObj(ho_LowerRight, hv_ExpDefaultWinHandle);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_ButtonColor);
            HOperatorSet.DispObj(ho_Rectangle, hv_ExpDefaultWinHandle);
            HOperatorSet.SetDraw(hv_ExpDefaultWinHandle, hv_DrawMode);
            HOperatorSet.SetSystem("clip_region", hv_ClipRegion);
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_TextColor_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_TextColor_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //reset changed window settings
            HOperatorSet.SetRgb(hv_ExpDefaultWinHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);
            ho_UpperLeft.Dispose();
            ho_LowerRight.Dispose();
            ho_Rectangle.Dispose();

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: This procedure calls disp_object_model_3d and a fallback solution if there is no OpenGL Available. 
        public void disp_object_model_3d_safe(HTuple hv_WindowHandle, HTuple hv_ObjectModel3D,
            HTuple hv_CamParam, HTuple hv_Pose, HTuple hv_GenParamName, HTuple hv_GenParamValue)
        {



            // Local iconic variables 

            HObject ho_ModelContours = null;

            // Local control variables 

            HTuple hv_Exception = null, hv_Center = new HTuple();
            HTuple hv_CPLength = new HTuple(), hv_RowNotUsed = new HTuple();
            HTuple hv_ColumnNotUsed = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_CamWidth = new HTuple();
            HTuple hv_CamHeight = new HTuple(), hv_Scale = new HTuple();
            HTuple hv_NumModels = new HTuple(), hv_PoseEstimated = new HTuple();
            HTuple hv_Poses = new HTuple(), hv_HomMat3Ds = new HTuple();
            HTuple hv_Sequence = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_CamParam_COPY_INP_TMP = hv_CamParam.Clone();
            HTuple hv_Pose_COPY_INP_TMP = hv_Pose.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            // dev_get_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            try
            {
                HOperatorSet.DispObjectModel3d(hv_ExpDefaultWinHandle, hv_ObjectModel3D, hv_CamParam_COPY_INP_TMP,
                    hv_Pose_COPY_INP_TMP, hv_GenParamName, hv_GenParamValue);
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                //Read and check the parameter PoseIn for each object
                get_object_models_center(hv_ObjectModel3D, out hv_Center);
                hv_CPLength = new HTuple(hv_CamParam_COPY_INP_TMP.TupleLength());
                HOperatorSet.GetWindowExtents(hv_ExpDefaultWinHandle, out hv_RowNotUsed, out hv_ColumnNotUsed,
                    out hv_Width, out hv_Height);
                if ((int)(new HTuple(hv_CPLength.TupleEqual(0))) != 0)
                {
                    hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[0] = 0.06;
                    hv_CamParam_COPY_INP_TMP[1] = 0;
                    hv_CamParam_COPY_INP_TMP[2] = 8.5e-6;
                    hv_CamParam_COPY_INP_TMP[3] = 8.5e-6;
                    hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Width / 2);
                    hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Height / 2);
                    hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Width);
                    hv_CamParam_COPY_INP_TMP = hv_CamParam_COPY_INP_TMP.TupleConcat(hv_Height);
                    hv_CPLength = new HTuple(hv_CamParam_COPY_INP_TMP.TupleLength());
                }
                else
                {
                    hv_CamWidth = ((hv_CamParam_COPY_INP_TMP.TupleSelect(hv_CPLength - 2))).TupleReal()
                        ;
                    hv_CamHeight = ((hv_CamParam_COPY_INP_TMP.TupleSelect(hv_CPLength - 1))).TupleReal()
                        ;
                    hv_Scale = ((((hv_Width / hv_CamWidth)).TupleConcat(hv_Height / hv_CamHeight))).TupleMin()
                        ;
                    if (hv_CamParam_COPY_INP_TMP == null)
                        hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[hv_CPLength - 6] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                        hv_CPLength - 6)) / hv_Scale;
                    if (hv_CamParam_COPY_INP_TMP == null)
                        hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[hv_CPLength - 5] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                        hv_CPLength - 5)) / hv_Scale;
                    if (hv_CamParam_COPY_INP_TMP == null)
                        hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[hv_CPLength - 4] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                        hv_CPLength - 4)) * hv_Scale;
                    if (hv_CamParam_COPY_INP_TMP == null)
                        hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[hv_CPLength - 3] = (hv_CamParam_COPY_INP_TMP.TupleSelect(
                        hv_CPLength - 3)) * hv_Scale;
                    if (hv_CamParam_COPY_INP_TMP == null)
                        hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[hv_CPLength - 2] = (((hv_CamParam_COPY_INP_TMP.TupleSelect(
                        hv_CPLength - 2)) * hv_Scale)).TupleInt();
                    if (hv_CamParam_COPY_INP_TMP == null)
                        hv_CamParam_COPY_INP_TMP = new HTuple();
                    hv_CamParam_COPY_INP_TMP[hv_CPLength - 1] = (((hv_CamParam_COPY_INP_TMP.TupleSelect(
                        hv_CPLength - 1)) * hv_Scale)).TupleInt();
                }
                hv_NumModels = new HTuple(hv_ObjectModel3D.TupleLength());
                if ((int)(new HTuple((new HTuple(hv_Pose_COPY_INP_TMP.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    //If no pose was specified by the caller, automatically calculate
                    //a pose that is appropriate for the visualization.
                    //Set the initial model reference pose. The orientation is parallel
                    //to the object coordinate system, the position is at the center
                    //of gravity of all models.
                    HOperatorSet.CreatePose(-(hv_Center.TupleSelect(0)), -(hv_Center.TupleSelect(
                        1)), -(hv_Center.TupleSelect(2)), 0, 0, 0, "Rp+T", "gba", "point", out hv_Pose_COPY_INP_TMP);
                    determine_optimum_pose_distance(hv_ObjectModel3D, hv_CamParam_COPY_INP_TMP,
                        0.9, hv_Pose_COPY_INP_TMP, out hv_PoseEstimated);
                    hv_Poses = new HTuple();
                    hv_HomMat3Ds = new HTuple();
                    hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                    hv_Poses = hv_PoseEstimated.TupleSelect(hv_Sequence % 7);
                }
                else if ((int)(new HTuple((new HTuple(hv_Pose_COPY_INP_TMP.TupleLength()
                    )).TupleEqual(7))) != 0)
                {
                    hv_Poses = new HTuple();
                    hv_HomMat3Ds = new HTuple();
                    hv_Sequence = HTuple.TupleGenSequence(0, (hv_NumModels * 7) - 1, 1);
                    hv_Poses = hv_Pose_COPY_INP_TMP.TupleSelect(hv_Sequence % 7);
                }
                else
                {
                    if ((int)(new HTuple((new HTuple(hv_Pose_COPY_INP_TMP.TupleLength())).TupleNotEqual(
                        (new HTuple(hv_ObjectModel3D.TupleLength())) * 7))) != 0)
                    {
                        //Error: Wrong number of values of input control parameter 'PoseIn'
                        HDevelopStop();
                    }
                    else
                    {
                        hv_Poses = hv_Pose_COPY_INP_TMP.Clone();
                    }
                }
                HOperatorSet.TupleFind(hv_GenParamName, "disp_background", out hv_Indices);
                if ((int)(new HTuple(hv_Indices.TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple(((hv_GenParamValue.TupleSelect(hv_Indices))).TupleEqual(
                        "true"))) != 0)
                    {
                        //display background do not clear background
                    }
                    else
                    {
                        //dev_set_window(...);
                        HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
                    }
                }
                else
                {
                    //No indication of  'disp_background' clear window
                    //dev_set_window(...);
                    HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
                }
                ho_ModelContours.Dispose();
                disp_object_model_no_opengl(out ho_ModelContours, hv_ObjectModel3D, hv_GenParamName,
                    hv_GenParamValue, hv_ExpDefaultWinHandle, hv_CamParam_COPY_INP_TMP, hv_Poses);
            }
            // dev_set_preferences(...); only in hdevelop
            ho_ModelContours.Dispose();

            return;
        }

        // Chapter: Graphics / Output
        // Short Description: Get the center of the virtual trackback that is used to move the camera (version for inspection_mode = 'surface'). 
        public void get_trackball_center_fixed(HTuple hv_SelectedObject, HTuple hv_TrackballCenterRow,
            HTuple hv_TrackballCenterCol, HTuple hv_TrackballRadiusPixel, HTuple hv_Scene3D,
            HTuple hv_ObjectModel3DID, HTuple hv_Poses, HTuple hv_WindowHandleBuffer, HTuple hv_CamParam,
            HTuple hv_GenParamName, HTuple hv_GenParamValue, out HTuple hv_TBCenter, out HTuple hv_TBSize)
        {



            // Local iconic variables 

            HObject ho_RegionCenter, ho_DistanceImage;
            HObject ho_Domain;

            // Local control variables 

            HTuple hv_NumModels = null, hv_Width = null;
            HTuple hv_Height = null, hv_SelectPose = null, hv_Index1 = null;
            HTuple hv_Rows = null, hv_Columns = null, hv_Grayval = null;
            HTuple hv_IndicesG = null, hv_Value = null, hv_Pos = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionCenter);
            HOperatorSet.GenEmptyObj(out ho_DistanceImage);
            HOperatorSet.GenEmptyObj(out ho_Domain);
            hv_TBCenter = new HTuple();
            hv_TBSize = new HTuple();
            //Determine the trackball center for the fixed trackball
            hv_NumModels = new HTuple(hv_ObjectModel3DID.TupleLength());
            hv_Width = hv_CamParam[(new HTuple(hv_CamParam.TupleLength())) - 2];
            hv_Height = hv_CamParam[(new HTuple(hv_CamParam.TupleLength())) - 1];
            //
            //Project the selected objects
            hv_SelectPose = new HTuple();
            for (hv_Index1 = 0; (int)hv_Index1 <= (int)((new HTuple(hv_SelectedObject.TupleLength()
                )) - 1); hv_Index1 = (int)hv_Index1 + 1)
            {
                hv_SelectPose = hv_SelectPose.TupleConcat(HTuple.TupleGenConst(7, hv_SelectedObject.TupleSelect(
                    hv_Index1)));
                if ((int)(new HTuple(((hv_SelectedObject.TupleSelect(hv_Index1))).TupleEqual(
                    0))) != 0)
                {
                    HOperatorSet.SetScene3dInstanceParam(hv_Scene3D, hv_Index1, "visible", "false");
                }
            }
            HOperatorSet.SetScene3dParam(hv_Scene3D, "depth_persistence", "true");
            HOperatorSet.DisplayScene3d(hv_ExpDefaultWinHandle, hv_Scene3D, 0);
            HOperatorSet.SetScene3dParam(hv_Scene3D, "visible", "true");
            //
            //determine the depth of the object point that appears closest to the trackball
            //center
            ho_RegionCenter.Dispose();
            HOperatorSet.GenRegionPoints(out ho_RegionCenter, hv_TrackballCenterRow, hv_TrackballCenterCol);
            ho_DistanceImage.Dispose();
            HOperatorSet.DistanceTransform(ho_RegionCenter, out ho_DistanceImage, "chamfer-3-4-unnormalized",
                "false", hv_Width, hv_Height);
            ho_Domain.Dispose();
            HOperatorSet.GetDomain(ho_DistanceImage, out ho_Domain);
            HOperatorSet.GetRegionPoints(ho_Domain, out hv_Rows, out hv_Columns);
            HOperatorSet.GetGrayval(ho_DistanceImage, hv_Rows, hv_Columns, out hv_Grayval);
            HOperatorSet.TupleSortIndex(hv_Grayval, out hv_IndicesG);
            HOperatorSet.GetDisplayScene3dInfo(hv_ExpDefaultWinHandle, hv_Scene3D, hv_Rows.TupleSelect(
                hv_IndicesG), hv_Columns.TupleSelect(hv_IndicesG), "depth", out hv_Value);
            HOperatorSet.TupleFind(hv_Value.TupleSgn(), 1, out hv_Pos);
            //
            HOperatorSet.SetScene3dParam(hv_Scene3D, "depth_persistence", "false");
            //
            //
            //set TBCenter
            if ((int)(new HTuple(hv_Pos.TupleNotEqual(-1))) != 0)
            {
                //if the object is visible in the image
                hv_TBCenter = new HTuple();
                hv_TBCenter[0] = 0;
                hv_TBCenter[1] = 0;
                hv_TBCenter = hv_TBCenter.TupleConcat(hv_Value.TupleSelect(
                    hv_Pos.TupleSelect(0)));
            }
            else
            {
                //if the object is not visible in the image, set the z coordinate to -1
                //to indicate, the the previous z value should be used instead
                hv_TBCenter = new HTuple();
                hv_TBCenter[0] = 0;
                hv_TBCenter[1] = 0;
                hv_TBCenter[2] = -1;
            }
            //
            if ((int)(new HTuple(((hv_SelectedObject.TupleMax())).TupleNotEqual(0))) != 0)
            {
                hv_TBSize = (0.5 + ((0.5 * (hv_SelectedObject.TupleSum())) / hv_NumModels)) * hv_TrackballRadiusPixel;
            }
            else
            {
                hv_TBCenter = new HTuple();
                hv_TBSize = 0;
            }
            ho_RegionCenter.Dispose();
            ho_DistanceImage.Dispose();
            ho_Domain.Dispose();

            return;
        }

        // Chapter: Graphics / Parameters
        public void color_string_to_rgb(HTuple hv_Color, out HTuple hv_RGB)
        {



            // Local iconic variables 

            HObject ho_Rectangle, ho_Image;

            // Local control variables 

            HTuple hv_WindowHandleBuffer = new HTuple();
            HTuple hv_Exception = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Image);
            //open_window(...);
            HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, -1, -1);
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 0, 0, 0);
            try
            {
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Color);
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                hv_Exception = "Wrong value of control parameter Color (must be a valid color string)";
                throw new HalconException(hv_Exception);
            }
            HOperatorSet.DispObj(ho_Rectangle, hv_ExpDefaultWinHandle);
            ho_Image.Dispose();
            HOperatorSet.DumpWindowImage(out ho_Image, hv_ExpDefaultWinHandle);
            //close_window(...);
            HOperatorSet.GetGrayval(ho_Image, 0, 0, out hv_RGB);
            hv_RGB = hv_RGB + ((new HTuple(0)).TupleConcat(0)).TupleConcat(0);
            ho_Rectangle.Dispose();
            ho_Image.Dispose();

            return;
        }

        // Chapter: Classification / Misc
        // Short Description: Describe and calculate user-defined features to be used in conjunction with the calculate_feature_set procedure library. 
        public void get_custom_features(HObject ho_Region, HObject ho_Image, HTuple hv_CurrentName,
            HTuple hv_Mode, out HTuple hv_Output)
        {




            // Local iconic variables 

            HObject ho_RegionSelected = null, ho_Contours = null;
            HObject ho_ContoursSelected = null, ho_ContoursSplit = null;

            // Local control variables 

            HTuple hv_TmpResults = null, hv_Name = null;
            HTuple hv_Groups = null, hv_Feature = new HTuple(), hv_NumRegions = new HTuple();
            HTuple hv_I = new HTuple(), hv_NumContours = new HTuple();
            HTuple hv_NumLines = new HTuple(), hv_J = new HTuple();
            HTuple hv_NumSplit = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionSelected);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ContoursSelected);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            //
            //This procedure can be used to extend the functionality
            //of the calculate_feature_set procedure library by
            //user-defined features.
            //
            //Instructions:
            //
            //1. Find the template block at the beginning the procedure
            //(marked by comments) and duplicate it.
            //
            //2. In the copy edit the two marked areas as follows:
            //
            //2.1. Feature name and groups:
            //Assign a unique identifier for your feature to the variable "Name".
            //Then, assign the groups that you want your feature to belong to
            //to the variable "Groups".
            //
            //2.2. Feature calculation:
            //Enter the code that calculates your feature and
            //assign the result to the variable "Feature".
            //
            //3. Test
            //Use the "test_feature" procedure to check,
            //if the feature is calculated correctly.
            //If the procedure throws an exception,
            //maybe the order of the feature vector is wrong
            //(See note below).
            //
            //4. Integration
            //- Save your modified procedure get_custom_features.hdvp
            //  to a location of your choice.
            //  (We recommend not to overwrite the template.)
            //- Make sure, that your version of get_custom_procedures
            //  is included in the procedure directories of HDevelop.
            //  (Choose Procedures -> Manage Procedures -> Directories -> Add from the HDevelop menu bar.)
            //
            //Note:
            //The current implementation supports region arrays as input.
            //In that case, multi-dimensional feature vectors are simply concatenated.
            //Example: The feature 'center' has two dimensions [Row,Column].
            //If an array of three regions is passed, the correct order of the "Feature" variable is
            //[Row1, Column1, Row2, Column2, Row3, Column3].
            //
            hv_TmpResults = new HTuple();
            //************************************************
            //************************************************
            //**** Copy the following template block     *****
            //**** and edit the two marked code sections *****
            //**** to add user-defined features          *****
            //************************************************
            //************************************************
            //
            //***************************************
            //*********** TEMPLATE BLOCK ************
            //***************************************
            //
            //********************************************************************
            //** Section 1:
            //** Enter unique feature name and groups to which it belongs here ***
            hv_Name = "custom_feature_numlines";
            hv_Groups = "custom";
            //** Enter unique feature name and groups above this line ************
            //********************************************************************
            if ((int)(new HTuple(hv_Name.TupleEqual(hv_CurrentName))) != 0)
            {
                //******************************************************
                //** Section 2:
                //** Enter code to calculate feature here **************
                hv_Feature = new HTuple();
                HOperatorSet.CountObj(ho_Region, out hv_NumRegions);
                HTuple end_val69 = hv_NumRegions;
                HTuple step_val69 = 1;
                for (hv_I = 1; hv_I.Continue(end_val69, step_val69); hv_I = hv_I.TupleAdd(step_val69))
                {
                    ho_RegionSelected.Dispose();
                    HOperatorSet.SelectObj(ho_Region, out ho_RegionSelected, hv_I);
                    ho_Contours.Dispose();
                    HOperatorSet.GenContourRegionXld(ho_RegionSelected, out ho_Contours, "border");
                    HOperatorSet.CountObj(ho_Contours, out hv_NumContours);
                    hv_NumLines = 0;
                    HTuple end_val74 = hv_NumContours;
                    HTuple step_val74 = 1;
                    for (hv_J = 1; hv_J.Continue(end_val74, step_val74); hv_J = hv_J.TupleAdd(step_val74))
                    {
                        ho_ContoursSelected.Dispose();
                        HOperatorSet.SelectObj(ho_Contours, out ho_ContoursSelected, hv_J);
                        ho_ContoursSplit.Dispose();
                        HOperatorSet.SegmentContoursXld(ho_ContoursSelected, out ho_ContoursSplit,
                            "lines", 5, 2, 1);
                        HOperatorSet.CountObj(ho_ContoursSplit, out hv_NumSplit);
                        hv_NumLines = hv_NumLines + hv_NumSplit;
                    }
                    hv_Feature = hv_Feature.TupleConcat(hv_NumLines);
                }
                //** Enter code to calculate feature above this line ***
                //******************************************************
                append_length_or_values(hv_Mode, hv_Feature, hv_TmpResults, out hv_TmpResults);
            }
            append_names_or_groups(hv_Mode, hv_Name, hv_Groups, hv_CurrentName, hv_TmpResults,
                out hv_TmpResults);
            //
            //************************************
            //****** END OF TEMPLATE BLOCK *******
            //************************************
            //
            hv_Output = hv_TmpResults.Clone();
            ho_RegionSelected.Dispose();
            ho_Contours.Dispose();
            ho_ContoursSelected.Dispose();
            ho_ContoursSplit.Dispose();

            return;
        }
    }
}