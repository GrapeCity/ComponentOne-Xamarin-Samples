﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace C1Input101
{
    public class Sample
    {
        public string Name { get; set; }

        public int SampleViewType { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public ImageSource ThumbnailImageSource
        {
            get
            {
                return ImageSource.FromResource("C1Input101.Images." + Thumbnail, typeof(App));
            }
        }
    }
}
