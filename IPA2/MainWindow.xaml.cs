using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace IPA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool closing = false;
        const int skeletonCount = 6;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];
        LinkedList<Double> values = new LinkedList<double>();
        int count = 0;
        bool firstRun = true;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);
        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor old = (KinectSensor)e.OldValue;

            StopKinect(old);

            KinectSensor sensor = (KinectSensor)e.NewValue;

            if (sensor == null)
            {
                return;
            }




            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
            sensor.SkeletonStream.Enable(parameters);

            sensor.SkeletonStream.Enable();

            sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            try
            {
                sensor.Start();
            }
            catch (System.IO.IOException)
            {
                kinectSensorChooser1.AppConflictOccurred();
            }
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (closing)
            {
                return;
            }

            //Get a skeleton
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
            {
                return;
            }

            double elx = first.Joints[JointType.ElbowLeft].Position.X;
            double hlx = first.Joints[JointType.HandLeft].Position.X;
            double wlx = first.Joints[JointType.WristLeft].Position.X;
            double ely = first.Joints[JointType.ElbowLeft].Position.Y;
            double hly = first.Joints[JointType.HandLeft].Position.Y;
            double wly = first.Joints[JointType.WristLeft].Position.Y;

            double erx = first.Joints[JointType.ElbowRight].Position.X;
            double hrx = first.Joints[JointType.HandRight].Position.X;
            double wrx = first.Joints[JointType.WristRight].Position.X;
            double ery = first.Joints[JointType.ElbowRight].Position.Y;
            double hry = first.Joints[JointType.HandRight].Position.Y;
            double wry = first.Joints[JointType.WristRight].Position.Y;
 
            values.AddFirst(hlx * 100);

            if(!firstRun)
                values.RemoveLast();

            double result = regression(values.ToArray());

            if (values.Count == 15)
                firstRun = false;


            if (elx + 0.1 > hlx && elx - 0.1 < hlx && ely + 0.1 < hly && erx + 0.1 > hrx && erx - 0.1 < hrx && ery + 0.1 < hry)
            {
                FirstContent.Content = "CALIBRATION";
                count = 15;
            }
            else if (!firstRun && result > 0.5)
            {

              FirstContent.Content = "LEFT HAND SWIPES";
              count = 15;
            }
            else if (count <= 0)
            {
                FirstContent.Content = "";
            }
            else
            {
                count = count - 1;
            }

        }


        Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return null;
                }


                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                //get the first tracked skeleton
                Skeleton first = (from s in allSkeletons
                                  where s.TrackingState == SkeletonTrackingState.Tracked
                                  select s).FirstOrDefault();

                return first;

            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    kinectSkeletonViewer1.Kinect = null;

                    //stop sensor 
                    sensor.Stop();

                    //stop audio if not null
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }
                    
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true;
            StopKinect(kinectSensorChooser1.Kinect);
        }

        private void kinectSkeletonViewer1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private double regression(double[] values)
        {

 
            double xAvg = 0;
            double yAvg = 0;
 
            for (int x = 0; x < values.Length; x++)
            {
                xAvg += x;
                yAvg += values[x];
            }
 
            xAvg = xAvg / values.Length;
            yAvg = yAvg / values.Length;
 
            double v1 = 0;
            double v2 = 0;
 
            for (int x = 0; x < values.Length; x++)
            {
                v1 += (x - xAvg) * (values[x] - yAvg);
                v2 += Math.Pow(x - xAvg, 2);
            }
 
            double a = v1 / v2;
            double b = yAvg - a * xAvg;

            return a;

        }
    }
}
