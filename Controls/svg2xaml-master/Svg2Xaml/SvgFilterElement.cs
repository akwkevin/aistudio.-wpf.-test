////////////////////////////////////////////////////////////////////////////////
//
//  SvgFilterElement.cs - This file is part of Svg2Xaml.
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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Effects;
using System.Xml.Linq;

namespace Svg2Xaml
{
  
  //****************************************************************************
  /// <summary>
  ///   Represents a &lt;filter&gt; element.
  /// </summary>
  class SvgFilterElement
    : SvgBaseElement
  {
    //==========================================================================
    public readonly List<SvgFilterEffectBaseElement> FilterEffects = new List<SvgFilterEffectBaseElement>();

    //==========================================================================
    public SvgFilterElement(SvgDocument document, SvgBaseElement parent, XElement filterElement)
      : base(document, parent, filterElement)
    {
      foreach(XElement element in from element in filterElement.Elements()
                                  where element.Name.NamespaceName == "http://www.w3.org/2000/svg"
                                  select element)
        switch(element.Name.LocalName)
        {

          case "feGaussianBlur":
            FilterEffects.Add(new SvgFEGaussianBlurElement(document, this, element));
            break;

          case "feBlend":
            FilterEffects.Add(new SvgFEBlendElement(document, this, element));
            break;

          case "feColorMatrix":
            FilterEffects.Add(new SvgFEColorMatrixElement(document, this, element));
            break;

          default:
            throw new NotImplementedException(String.Format("Unhandled element: {0}", element));
        }

    }

    //==========================================================================
    public BitmapEffect ToBitmapEffect()
    {
      if(Document.Options.IgnoreEffects)
        return null;

      BitmapEffectGroup bitmap_effect_group = new BitmapEffectGroup();

      foreach(SvgFilterEffectBaseElement filter_effect in FilterEffects)
      {
        BitmapEffect bitmap_effect = filter_effect.ToBitmapEffect();
        if(bitmap_effect != null)
          bitmap_effect_group.Children.Add(bitmap_effect);
      }

      if(bitmap_effect_group.Children.Count == 0)
        return null;

      return bitmap_effect_group;
    }

  } // class SvgFilterElement

}
