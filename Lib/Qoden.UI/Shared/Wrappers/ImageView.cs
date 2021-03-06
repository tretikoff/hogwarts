﻿using System;
namespace Qoden.UI.Wrappers
{
#if __IOS__
    using PlatformImageView = UIKit.UIImageView;
#endif
#if __ANDROID__
    using PlatformImageView = Android.Widget.ImageView;
#endif

    public struct ImageView
    {
        public static implicit operator PlatformImageView(ImageView area) { return area.PlatformView; }

        public PlatformImageView PlatformView { get; set; }

        public View AsView() { return new View() { PlatformView = PlatformView }; }

        public void SetImage(Image image) 
        {
#if __IOS__
            PlatformView.Image = image.PlatformImage;
#endif
#if __ANDROID__
            PlatformView.SetImageDrawable(image.PlatformImage);
            PlatformView.Invalidate();
#endif
        }
        public Image Image
        {
#if __IOS__
            get => new Image() { PlatformImage = PlatformView.Image };
#endif
#if __ANDROID__
            get => new Image() { PlatformImage = PlatformView.Drawable };
#endif
        }
    }

    public static class ImageViewExtensions
    {

        public static ImageView ImageView(this ViewBuilder b, bool addSubview = true)
        {
#if __IOS__
            var view = new ImageView() { PlatformView = new PlatformImageView() };
#endif
#if __ANDROID__
            var view = new ImageView() { PlatformView = new PlatformImageView(b.Context) };
#endif
            if (addSubview) b.AddSubview(view.PlatformView);
            return view;

        }

        public static ImageView AsImageView(this PlatformImageView imageView)
        {
            return new ImageView() { PlatformView = imageView };
        }
    }
}
