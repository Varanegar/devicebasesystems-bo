using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageMagick;
using NLog;

namespace DeviceBaseSystem.WebApi.Classes
{
    public class FileManager : IFileManager
    {
        private static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        #region Methods
        public async Task Save(System.Web.HttpPostedFileBase file, string imagetype, string token, string imageName)
        {
            try
            {
                await Task.Run(() =>
                {
                    Image image100x100 = Scale(Image.FromStream(file.InputStream), 100, 100);
                    image100x100.Save(GetPath(token, imagetype + "\\100x100", imageName + ".png"));

                    Image image320x320 = Scale(Image.FromStream(file.InputStream), 320, 320);
                    image320x320.Save(GetPath(token, imagetype + "\\320x320", imageName + ".png"));

                    var physicalPath = GetPath(token, imagetype + "\\orginal", imageName + ".png");
                    file.SaveAs(physicalPath);
                });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task SaveWithMagick(System.Web.HttpPostedFileBase file, string imagetype, string token, string imageName)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    using (var image = new MagickImage(file.InputStream))
                    {
                        var size = new MagickGeometry(100, 100);
                        // This will resize the image to a fixed size without maintaining the aspect ratio.
                        // Normally an image will be resized to fit inside the specified size.
                        size.IgnoreAspectRatio = true;
                        image.Resize(size);
                        image.Write(GetPath(token, imagetype + "\\100x100", imageName + ".png"));

                        size = new MagickGeometry(320, 320);
                        size.IgnoreAspectRatio = true;
                        image.Resize(size);
                        image.Write(GetPath(token, imagetype + "\\320x320", imageName + ".png"));
                    }

                    var physicalPath = GetPath(token, imagetype + "\\orginal", imageName + ".png");

                    file.SaveAs(physicalPath);
                });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                throw ex;
            }
        }


        public string GetPath(string token, string imagetype, string fileName)
        {
            try
            {
                fileName = Path.GetFileName(fileName);

                var folder = string.Format("{0}Content\\Images\\{1}\\{2}\\", AppDomain.CurrentDomain.BaseDirectory, imagetype, token);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var physicalPath = Path.Combine(folder, fileName);

                return physicalPath;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;

                return string.Empty;
            }
        }

        public async Task Remove(string token, string imagetype, string fileName)
        {
            try
            {
                await Task.Run(() =>
                {
                    var physicalPath = GetPath(token, imagetype, fileName);

                    if (File.Exists(physicalPath))
                        File.Delete(physicalPath);
                });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;

            }

        }

        public List<string> GetFileNames(string token, string imagetype)
        {
            try
            {
                var physicalPath = GetPath(token, imagetype, "");

                var model = new List<string>();

                Directory.GetFiles(physicalPath).ToList().ForEach(itm =>
                {
                    model.Add(itm.Substring(itm.LastIndexOf('\\') + 1));
                });

                return model;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
                return new List<string>();
            }
        }

        public Image Scale(Image imgPhoto, int Width = 0, int Height = 0)
        {
            float sourceWidth = imgPhoto.Width,
                    sourceHeight = imgPhoto.Height,
                    destHeight = imgPhoto.Height,
                    destWidth = imgPhoto.Width;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = Height * sourceWidth / sourceHeight;
                destHeight = Height;
            }
            else if (Width != 0)
            {
                destWidth = Width;
                destHeight = sourceHeight * Width / sourceWidth;
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight);//,
            // PixelFormat.Format32bppPArgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.Default;//HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(0, 0, (int)destWidth, (int)destHeight),
                new Rectangle(0, 0, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
        #endregion
    }
}