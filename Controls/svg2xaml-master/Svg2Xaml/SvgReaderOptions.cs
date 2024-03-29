﻿////////////////////////////////////////////////////////////////////////////////
//
//  SvgReaderOptions.cs - This file is part of Svg2Xaml.
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
using System.IO;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media.Effects;

namespace Svg2Xaml
{

    //****************************************************************************
    /// <summary>
    ///   Defines a set of options to customize rendering repspectively reading 
    ///   of SVG documents.
    /// </summary>
    public class SvgReaderOptions
    {

        //==========================================================================
        private bool m_IgnoreEffects = false;

        //==========================================================================
        /// <summary>
        ///   Initializes a new <see cref="SvgReaderOptions"/> instance.
        /// </summary>
        public SvgReaderOptions()
        {
            // ...
        }

        //==========================================================================
        /// <summary>
        ///   Initializes a new <see cref="SvgReaderOptions"/> instance.
        /// </summary>
        /// <param name="ignoreEffects">
        ///   Specifies whether filter effects should be applied using WPF bitmap 
        ///   effects.
        /// </param>
        public SvgReaderOptions(bool ignoreEffects)
        {
            m_IgnoreEffects = ignoreEffects;
        }

        //==========================================================================
        /// <summary>
        ///   Gets or sets whether SVG effects should either be ignored or 
        ///   converted to <see cref="BitmapEffect">bitmap effects</see>.
        /// </summary>
        public bool IgnoreEffects
        {
            get
            {
                return m_IgnoreEffects;
            }

            set
            {
                m_IgnoreEffects = value;
            }
        }

        /// <summary>
        /// 使用Wpf设置填充颜色 格式为#888888，AIStudio扩展
        /// </summary>
        public string Fill
        {
            get; set;
        }

        /// <summary>
        /// 使用Wpf设置线条颜色 格式为#888888，AIStudio扩展
        /// </summary>
        public string Stroke
        {
            get; set;
        }

    } // class SvgReaderOptions

}
