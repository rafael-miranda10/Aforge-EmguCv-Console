using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace FaceDetection_Console
{
    class Program
    {
        //para mais exemplos procure na internet pelo nome do arquivo. existe um github do opencv com mais exemplos
        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        static void Main(string[] args)
        {
            //############################## reconhecimento facial  ##########################################
            //using (ImageViewer viewer = new ImageViewer()) //create an image viewer
            //using (VideoCapture capture = new VideoCapture()) //create a camera captue
            //{
            //    capture.ImageGrabbed += delegate (object sender, EventArgs e)
            //    {  //run this until application closed (close button click on image viewer)
            //        Mat m = new Mat();
            //        capture.Retrieve(m);
            //       // viewer.Image = m; //draw the image obtained from camera


            //        Image<Bgr, byte> grayImage = m.ToImage<Bgr, Byte>();
            //        Bitmap bitmap = grayImage.Bitmap;
            //        Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.05, 3);
            //        foreach (Rectangle rectangle in rectangles)
            //        {
            //            using (Graphics graphics = Graphics.FromImage(bitmap))
            //            {
            //                using (Pen pen = new Pen(Color.Red, 5))
            //                {
            //                    graphics.DrawRectangle(pen, rectangle);
            //                }
            //            }
            //        }
            //        Image<Bgr, byte> imgFaced = new Image<Bgr, byte>(bitmap);
            //        //imgFaced._EqualizeHist();
            //        // imgFaced._GammaCorrect(1d);
            //        viewer.Image = imgFaced.Mat;
            //    };
            //    capture.Start();
            //    viewer.ShowDialog(); //show the image viewer
            //}


            //##################################### Canny #####################################################

            ImageViewer viewer = new ImageViewer(); //create an image viewer
            ImageViewer viewer2 = new ImageViewer(); //create an image viewer
            VideoCapture capture = new VideoCapture(); //create a camera capture

            Application.Idle += new EventHandler(delegate (object sender, EventArgs e)
            {  //run this until application closed (close button click on image viewer)

                Image<Bgr, byte> imgOriginal = capture.QueryFrame().ToImage<Bgr, byte>(); //draw the image obtained from camera
                Image<Gray, byte> _imgGray = imgOriginal.Convert<Gray, byte>();

                //Filtro Canny
                 Image<Gray, byte> imgProcessedCanny = _imgGray.Canny(20, 50);
                //Image<Gray, byte> imgProcessed = _imgGray.ThresholdBinary(new Gray(100), new Gray(0));
                Image<Bgr, byte> imgProcessed = imgOriginal.SmoothGaussian(15);
                Image<Gray, byte> _imgGray2 = imgProcessed.Convert<Gray, byte>();
                Image<Gray, byte> imgProcessed2 = _imgGray2.Canny(20, 50);


                viewer.Image = imgOriginal.Mat ;
                viewer2.Image = imgProcessed2.Mat;

            });

            viewer.Show();
            viewer2.ShowDialog();

        CvInvoke.WaitKey();
        }



        //####################  exemplos com imagem     ##########################

        //var ImgInput = new Image<Bgra, byte>(@"C:\Users\Rafael Arthur\Desktop\predios.jpg");
        //CvInvoke.Imshow("Original Image", ImgInput);
        //Image<Gray, byte> _imgCanny = new Image<Gray, byte>(ImgInput.Width, ImgInput.Height, new Gray(0));
        //_imgCanny = ImgInput.Canny(20, 50);
        //CvInvoke.Imshow("Edge Canny Image", _imgCanny);

        //Image<Gray, byte> _imgGray = ImgInput.Convert<Gray, byte>();
        //Image<Gray, float> _imgSobel = new Image<Gray, float>(ImgInput.Width, ImgInput.Height, new Gray(0));

        //_imgSobel = _imgGray.Sobel(1, 0, 5);

        //CvInvoke.Imshow("Edge Sobel Image", _imgSobel);

        //Image<Gray, float> _imgLaplacian = new Image<Gray, float>(ImgInput.Width, ImgInput.Height, new Gray(0));

        //_imgLaplacian = _imgGray.Laplace(7);

        //CvInvoke.Imshow("Edge Laplacian Image", _imgLaplacian);

        //CvInvoke.WaitKey();

    }
}


