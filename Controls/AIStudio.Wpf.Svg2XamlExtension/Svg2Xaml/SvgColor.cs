﻿////////////////////////////////////////////////////////////////////////////////
//
//  SvgColor.cs - This file is part of Svg2Xaml.
//
//    Copyright (C) 2009,2011 Boris Richter <himself@boris-richter.net>
//
//  --------------------------------------------------------------------------
//
//  Svg2Xaml is free software: you can redistribute it and/or modify it under 
//  the terms of the GNU Lesser General Public License as published by the 
//  Free Software Foundation, either version 3 of the License, or (at your 
//  option) any later version.
//
//  Svg2Xaml is distributed in the hope that it will be useful, but WITHOUT 
//  ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//  FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public 
//  License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public License 
//  along with Svg2Xaml. If not, see <http://www.gnu.org/licenses/>.
//
//  --------------------------------------------------------------------------
//
//  $LastChangedRevision$
//  $LastChangedDate$
//  $LastChangedBy$
//
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Globalization;
using System.Windows.Media;

namespace Svg2Xaml
{

    //****************************************************************************
    /// <summary>
    ///   Represents an RGB color.
    /// </summary>
    class SvgColor
    {
        //扩展透明度，AIStudio扩展
        public readonly float ScA;
        //==========================================================================
        public readonly float Red;

        //==========================================================================
        public readonly float Green;

        //==========================================================================
        public readonly float Blue;

        //==========================================================================
        public SvgColor(float red, float green, float blue, float a = 1)
        {
            // SM: Parameters are expected in scRGB color space
            // see https://en.wikipedia.org/wiki/ScRGB
            ScA = a;
            Red = red;
            Green = green;
            Blue = blue;
        }

        //==========================================================================
        public SvgColor(byte red, byte green, byte blue, byte a = 0xff)
        {
            // SM: Parameters are expected in RGB color space
            // and need to convert to scRGB color space
            // see https://en.wikipedia.org/wiki/ScRGB
            var col = Color.FromArgb(a, red, green, blue);
            ScA = col.ScA;
            Red = col.ScR;
            Green = col.ScG;
            Blue = col.ScB;
        }

        //==========================================================================
        public Color ToColor()
        {
            return Color.FromScRgb(ScA, Red, Green, Blue);
        }

        //==========================================================================
        public static SvgColor Parse(string value)
        {
            if (value.StartsWith("#"))
            {
                string color = value.Substring(1).Trim();
                if (color.Length == 3)
                {
                    float r = (float)(Byte.Parse(String.Format("{0}{0}", color[0]), NumberStyles.HexNumber) / 255.0);
                    float g = (float)(Byte.Parse(String.Format("{0}{0}", color[1]), NumberStyles.HexNumber) / 255.0);
                    float b = (float)(Byte.Parse(String.Format("{0}{0}", color[2]), NumberStyles.HexNumber) / 255.0);
                    return new SvgColor(r, g, b);
                }

                if (color.Length == 6)
                {
                    float r = (float)(Byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber) / 255.0);
                    float g = (float)(Byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber) / 255.0);
                    float b = (float)(Byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber) / 255.0);
                    return new SvgColor(r, g, b);
                }

                //扩展透明度，AIStudio扩展
                if (color.Length == 8)
                {
                    //忽略前两位
                    float a = (float)(Byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber) / 255.0);
                    float r = (float)(Byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber) / 255.0);
                    float g = (float)(Byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber) / 255.0);
                    float b = (float)(Byte.Parse(color.Substring(6, 2), NumberStyles.HexNumber) / 255.0);
                    return new SvgColor(r, g, b, a);
                }
            }

            if (value.StartsWith("rgb"))
            {
                string color = value.Substring(3).Trim();
                if (color.StartsWith("(") && color.EndsWith(")"))
                {
                    color = color.Substring(1, color.Length - 2).Trim();

                    string[] components = color.Split(',');
                    if (components.Length == 3)
                    {
                        float r, g, b;

                        components[0] = components[0].Trim();
                        if (components[0].EndsWith("%"))
                        {
                            components[0] = components[0].Substring(0, components[0].Length - 1).Trim();
                            r = (float)(Double.Parse(components[0], CultureInfo.InvariantCulture.NumberFormat) / 100.0);
                        }
                        else
                            r = (float)(Byte.Parse(components[0]) / 255.0);

                        components[1] = components[1].Trim();
                        if (components[1].EndsWith("%"))
                        {
                            components[1] = components[1].Substring(0, components[1].Length - 1).Trim();
                            g = (float)(Double.Parse(components[1], CultureInfo.InvariantCulture.NumberFormat) / 100.0);
                        }
                        else
                            g = (float)(Byte.Parse(components[1]) / 255.0);

                        components[2] = components[1].Trim();
                        if (components[2].EndsWith("%"))
                        {
                            components[2] = components[2].Substring(0, components[2].Length - 1).Trim();
                            b = (float)(Double.Parse(components[2], CultureInfo.InvariantCulture.NumberFormat) / 100.0);
                        }
                        else
                            b = (float)(Byte.Parse(components[2]) / 255.0);

                        return new SvgColor(r, g, b);
                    }
                }
            }

            if (value == "none")
                return null;


            switch (value)
            {
                case "black":
                    return new SvgColor((float)(0 / 255.0), (float)(0 / 255.0), (float)(0 / 255.0));
                case "green":
                    return new SvgColor((float)(0 / 255.0), (float)(128 / 255.0), (float)(0 / 255.0));
                case "silver":
                    return new SvgColor((float)(192 / 255.0), (float)(192 / 255.0), (float)(192 / 255.0));
                case "lime":
                    return new SvgColor((float)(0 / 255.0), (float)(255 / 255.0), (float)(0 / 255.0));
                case "gray":
                    return new SvgColor((float)(128 / 255.0), (float)(128 / 255.0), (float)(128 / 255.0));
                case "olive":
                    return new SvgColor((float)(128 / 255.0), (float)(128 / 255.0), (float)(0 / 255.0));
                case "white":
                    return new SvgColor((float)(255 / 255.0), (float)(255 / 255.0), (float)(255 / 255.0));
                case "yellow":
                    return new SvgColor((float)(255 / 255.0), (float)(255 / 255.0), (float)(0 / 255.0));
                case "maroon":
                    return new SvgColor((float)(128 / 255.0), (float)(0 / 255.0), (float)(0 / 255.0));
                case "navy":
                    return new SvgColor((float)(0 / 255.0), (float)(0 / 255.0), (float)(128 / 255.0));
                case "red":
                    return new SvgColor((float)(255 / 255.0), (float)(0 / 255.0), (float)(0 / 255.0));
                case "blue":
                    return new SvgColor((float)(0 / 255.0), (float)(0 / 255.0), (float)(255 / 255.0));
                case "purple":
                    return new SvgColor((float)(128 / 255.0), (float)(0 / 255.0), (float)(128 / 255.0));
                case "teal":
                    return new SvgColor((float)(0 / 255.0), (float)(128 / 255.0), (float)(128 / 255.0));
                case "fuchsia":
                    return new SvgColor((float)(255 / 255.0), (float)(0 / 255.0), (float)(255 / 255.0));
                case "aqua":
                    return new SvgColor((float)(0 / 255.0), (float)(255 / 255.0), (float)(255 / 255.0));
            }
            if (value.Trim().StartsWith("rgb"))
            {
                var arr = value.RemoveChars(new[] { ' ', 'r', 'g', 'b', '(', ')' }).Split(new[] { ',' });
                if (arr.Length == 3)
                {
                    double r, g, b;
                    if (double.TryParse(arr[0], out r) && double.TryParse(arr[1], out g) && double.TryParse(arr[2], out b))
                    {
                        return new SvgColor((float)(r / 255.0), (float)(g / 255.0), (float)(b / 255.0));
                    }
                }
            }
            throw new ArgumentException(String.Format("Unsupported color value: {0}", value));

        }

    } // class SvgColor

}
