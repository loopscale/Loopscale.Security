using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;

namespace Loopscale.Shared.Helpers
{
    public class CaptchaHelper
    {
        private SpeechSynthesizer sp;
        public byte[] CaptchaImageGenerator(string captchaText)
        {
            int height = 80;
            int width = 190;
            Random random = new Random();

            int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
            int[] aTextColor = new int[] { 0, 0, 0 };
            int[] fontEmSizes = new int[] { 15, 20, 25, 30, 35 };

            string[] aFontNames = new string[] { "Comic Sans MS", "Arial", "Times New Roman", "Georgia", "Verdana", "Geneva" };
            FontStyle[] fontStyles = new FontStyle[] { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline };
            HatchStyle[] hatchStyles = new HatchStyle[]
            {
    HatchStyle.BackwardDiagonal, HatchStyle.Cross,
     HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
     HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
     HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
     HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid,
     HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
     HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
     HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
     HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
     HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
     HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
     HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
     HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
     HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
     HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
     HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
     HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
   };

            //Get Captcha in Session
            // string sCaptchaText = context.Session["Captcha"].ToString();

            //Creates an output Bitmap
            Bitmap outputBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics graphics = Graphics.FromImage(outputBitmap);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Create a Drawing area
            RectangleF rectangleF = new RectangleF(0, 0, width, height);
            Brush brush = default(Brush);

            //Draw background (Lighter colors RGB 100 to 255)
            brush = new HatchBrush(hatchStyles[random.Next(hatchStyles.Length - 1)], Color.FromArgb((random.Next(100, 255)),
             (random.Next(100, 255)), (random.Next(100, 255))), Color.White);
            graphics.FillRectangle(brush, rectangleF);

            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            int i = 0;
            for (i = 0; i <= captchaText.Length - 1; i++)
            {
                matrix.Reset();
                int iChars = captchaText.Length;

                int x = width / (iChars + 1) * i;
                int y = height / 2;

                //Rotate text Random
                matrix.RotateAt(random.Next(-40, 40), new PointF(x, y));
                graphics.Transform = matrix;


                //Draw the letters with Random Font Type, Size and Color
                graphics.DrawString(
                 //Text
                 captchaText.Substring(i, 1),
                 //Random Font Name and Style
                 new Font(aFontNames[random.Next(aFontNames.Length - 1)],
                  fontEmSizes[random.Next(fontEmSizes.Length - 1)],
                  fontStyles[random.Next(fontStyles.Length - 1)]),
                 //Random Color (Darker colors RGB 0 to 100)
                 new SolidBrush(Color.FromArgb(random.Next(0, 100),
                  random.Next(0, 100), random.Next(0, 100))),
                 x,
                 random.Next(10, 40)
                );
                graphics.ResetTransform();
            }

            MemoryStream memoryStream = new MemoryStream();
            outputBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] captchaImage = memoryStream.GetBuffer();


            outputBitmap.Dispose();
            memoryStream.Close();
            return captchaImage;
        }

        public byte[] CaptchaReader(string captcha)
        {
            sp = new SpeechSynthesizer();
            //setting volume   
            sp.Volume = 100;
            //passing text box text to SpeakAsync method 
            //var p = new PromptBuilder();
            //for (var i = 0; i <= captcha.Length - 1; i++)
            //{
            //    sp.Speak(captcha[i].ToString());
            //}

            using (SpeechSynthesizer s = new SpeechSynthesizer())
            {
                s.Volume = 100;
                using (MemoryStream ms = new MemoryStream())
                {
                    s.SetOutputToWaveStream(ms);
                    for (var i = 0; i <= captcha.Length - 1; i++)
                    {
                        s.Speak(captcha[i].ToString());
                    }
                    return ms.GetBuffer();
                }
            }


            //sp.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(SpeakCompleted);

            //return true;
        }

        private void SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            sp.Dispose();
        }
    }
}